import axios from 'axios';
import React, { useState, useEffect } from 'react'
import { ApiEndPoints } from '../../data/ApiEndPoints';
import Loader from '../../component/Loader';
import { Table } from '../../component/Table';
import { SearchInput } from '../../component/Recruiter/SearchInput';
import { Link } from 'react-router-dom';
import { ClickSVG } from '../../component/Recruiter/ClickSVG';
import { Document } from '../../component/Document';

export const ApplicationInterviewModal = ({ empId, id, positionId, setPositionId }) => {
    const [applications, setApplications] = useState([]);
    const [isLoading, setLoading] = useState(true);
    const columns = ["Name", "Email", "Date", "Status", "CV", ""];
    const applicationStatus = ["applied", "review", "interview", "completed", "hired", "on_hold"];
    const getApplications = async () => {
        try {
            if (positionId === 0) {
                setLoading(false);
                return;
            }
            setLoading(true);
            const response = await axios.get(`${ApiEndPoints.getAllApplicationByPosition}${positionId}?id=3`, { headers: { Authorization: localStorage.getItem("token") } });
            console.log(response.data);
            setApplications(response.data);
        }
        catch (err) {
            console.log(err.message || err.response.data);
        }
        finally {
            setLoading(false);
        }
    }

    const setCompletedStageSettings = async (id) => {
        try {
            const response = await axios.patch(`${ApiEndPoints.changeApplication}${id}`, [{
                "path": "applicationStatus",
                "op": "replace",
                "value": 3
            }], { headers: { Authorization: localStorage.getItem("token") } });
            getApplications();
        }
        catch (err) {
            console.log(err.message || err.response.data);
        }
    }

    useEffect(() => {
        getApplications();
    }, [positionId]);

    return (
        <dialog id={id} className="modal w-full">
            {/* <Modal data={{ "comments": comments }} id={modalId} onchange={onChangeFunction} onsubmit={onsubmitFunction} title="Add Feedback" inputs={inputs} /> */}
            <div className="modal-box w-11/12 max-w-5xl pt-0">
                <div className="modal-action">
                    {isLoading ? <Loader /> :
                        <fieldset className="fieldset w-full bg-base-200 border border-base-300 p-4 rounded-box">
                            <legend className="fieldset-legend text-xl">Applications</legend>

                            <div className='p-2 shadow-md mt-3 rounded-md w-full overflow-hidden'>
                                <div className='w-full flex justify-between items-center'>
                                    <div><SearchInput /></div>
                                </div>
                                {
                                    applications?.length === 0 ? <div className='h-[65vh] flex justify-center items-center'> No Applications </div> :
                                        <Table columns={columns}>
                                            {
                                                applications?.map((item, index) => {
                                                    return (
                                                        <tr key={item.application.pk_application_id} className='text-center'>
                                                            <td>{item.candidate_name}</td>
                                                            <td>{item.candidate_email}</td>
                                                            <td>{item.application.application_date?.split("T")[0]}</td>
                                                            <td>{applicationStatus[item.application.applicationStatus]}</td>
                                                            <td>
                                                                {
                                                                    item.cv_path ?
                                                                        <Link to={`http://localhost:5083${item.cv_path}`} target='_blank' className='flex justify-between items-center gap-2 w-fit bg-violet-100 text-blue-500 p-2 rounded-md cursor-pointer justify-self-center'>
                                                                            <div><Document /></div>
                                                                            <div>View</div>
                                                                        </Link>
                                                                        :
                                                                        "Not Uploaded"
                                                                }
                                                            </td>
                                                            <td>
                                                                <details className="dropdown dropdown-left relative">
                                                                    <summary className="btn p-0 h-fit"><ClickSVG /></summary>
                                                                    <ul className="menu dropdown-content bg-base-100 rounded-box absolute z-1 w-fit p-2 shadow-sm">
                                                                        <li><div>Interview Round</div></li>
                                                                        <li><div onClick={() => setCompletedStageSettings(item.application.pk_application_id)}>Select</div></li>
                                                                    </ul>
                                                                </details>
                                                            </td>
                                                        </tr>
                                                    )
                                                })
                                            }
                                        </Table>
                                }
                                <div className='flex justify-end my-3'>
                                    <div className='p-2 bg-violet-100 text-blue-500 rounded-md'>pagination</div>
                                </div>
                            </div>

                            <form method="dialog">
                                <button className='btn text-white ml-3 bg-red-400'>Cancel</button>
                            </form>
                        </fieldset>
                    }
                </div>
            </div>
        </dialog>
    )
}
