import React, { useState, useEffect } from 'react'
import Loader from '../../../component/Loader';
import { SearchInput } from '../../../component/Recruiter/SearchInput';
import { Table } from '../../../component/Table';
import { ClickSVG } from '../../../component/Recruiter/ClickSVG';
import axios from 'axios';
import { ApiEndPoints } from '../../../data/ApiEndPoints';
import { Modal } from '../../../component/Modal';
import { InterviewScheduler } from './InterviewScheduler';

export const ApplicationCandidateModal = ({ id, positionId }) => {
    const [applications, setApplications] = useState([]);
    const [isLoading, setLoading] = useState(true);
    const [applicationId, setApplicationId] = useState(0);
    // const [interscheduleData, setInterviewScheduleData] = useState({
    //     "fk_application_id": 0,
    //     "no_of_hr_round": 1,
    //     "no_of_tech_round": 2,
    //     "assesment_link": "",
    //     "interview_link": "",
    //     "interview_date": "",
    //     "interview_time": ""
    // });
    const columns = ["Name", "Email", "Date", "Status", ""];
    const applicationStatus = ["applied", "review", "interview", "completed", "hired", "on_hold"];
    const interviewScheduleModal = "interview_schedule_modal";
    const inputs = [{ type: "number", name: "no_of_hr_round", placeholder: "HR Round" },
    { type: "number", name: "no_of_tech_round", placeholder: "Tech Round" },
    { type: "text", name: "assesment_link", placeholder: "Assesment Link" },
    { type: "text", name: "interview_link", placeholder: "Meeting Link" },
    { type: "time", name: "interview_time", placeholder: "Time" },
    { type: "date", name: "interview_date", placeholder: "Date" }
    ]
    const getApplications = async () => {
        try {
            if (positionId === 0) {
                setLoading(false);
                return;
            }
            setApplications([]);
            setLoading(true);
            const response = await axios.get(`${ApiEndPoints.getAllApplicationByPosition}${positionId}?id=1`, { headers: { Authorization: localStorage.getItem("token") } });
            setApplications(response.data);
        }
        catch (err) {
            console.log(err.message || err.response.data);
        }
        finally {
            setLoading(false);
        }
    }

    const setApplicationHoldSettings = async (applicationId) => {
        try {
            const response = await axios.put(`${ApiEndPoints.holdApplication}${applicationId}`, null, { headers: { Authorization: localStorage.getItem("token") } });
            console.log(response.data);
            getApplications();
        }
        catch (err) {
            console.log(err.message || err.response.data);
        }
    }

    const setInterviewSettings = (applicationId) => {
        setApplicationId(applicationId);
        document.getElementById(interviewScheduleModal).showModal();
    }

    useEffect(() => {
        getApplications();
    }, [positionId]);

    return (
        <dialog id={id} className="modal w-full">
            <InterviewScheduler applicationId={applicationId} id={interviewScheduleModal} />
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
                                                                <details className="dropdown dropdown-left relative">
                                                                    <summary className="btn p-0 h-fit"><ClickSVG /></summary>
                                                                    <ul className="menu dropdown-content bg-base-100 rounded-box absolute z-1 w-fit p-2 shadow-sm">
                                                                        <li><div onClick={() => setApplicationHoldSettings(item.application.pk_application_id)}>Hold</div></li>
                                                                        {
                                                                            item.application.applicationStatus === 2 && ( item.interview != null && item.interview.length != 0 ?<li><div>Already Scheduled</div></li> : <li><div onClick={() => setInterviewSettings(item.application.pk_application_id)}>Schedule Interview</div></li> )
                                                                        }
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
