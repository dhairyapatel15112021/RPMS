import React, { useEffect, useState } from 'react'
import { SearchInput } from '../../component/Recruiter/SearchInput'
import Loader from '../../component/Loader'
import { Table } from '../../component/Table'
import { ApiEndPoints } from '../../data/ApiEndPoints'
import { useSelector } from 'react-redux'
import axios from 'axios'
import { ClickSVG } from '../../component/Recruiter/ClickSVG'
import { ApplicationInterviewModal } from './ApplicationInterviewModal'

export const InterviewPositions = () => {
    const [isLoading, setLoading] = useState(true);
    const data = useSelector(state => state);
    const [openAssignedPositions, setOpenAssignedPositions] = useState([]);
    const columns = ["PositionId", "Title", "Description", "Experience", "Position Level", "Status", ""];
    const [positionId, setPositionId] = useState(0);
    const applicationInterviewModal = "application_interview_modal";

    const getOpenAssignedPositions = async () => {
        try {
            const response = await axios.get(`${ApiEndPoints.getAllInterviewPosition}${data.user.id}`, { headers: { Authorization: localStorage.getItem("token") } });
            setOpenAssignedPositions(response.data);
        }
        catch (err) {
            console.log(err.message || err.response.data);
        }
        finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        getOpenAssignedPositions();
    }, []);

    const setApplicationSettings = (id) => {
        setPositionId(() => id);
        document.getElementById(applicationInterviewModal).showModal();
    }

    return (
        <div className='p-2 shadow-md mt-3 rounded-md w-full overflow-hidden'>
            <ApplicationInterviewModal empId={data.user.id} id={applicationInterviewModal} positionId={positionId} setPositionId={setPositionId}/>
            <div className='w-full flex justify-between items-center'>
                <div><SearchInput /></div>
            </div>
            {isLoading ? <div className='h-[65vh] flex justify-center items-center'> <Loader /> </div>
                :
                openAssignedPositions?.length === 0 ? <div className='h-[65vh] flex justify-center items-center'> No Assigned Positions </div> :
                    <Table columns={columns}>
                        {
                            openAssignedPositions?.map((item, index) => {
                                return (
                                    <tr key={item.pk_position_id} className='text-center'>
                                        <td>{item.pk_position_id}</td>
                                        <td>{item.position_title}</td>
                                        <td>{item.position_description}</td>
                                        <td>{item.position_min_experience}</td>
                                        <td>{item.position_level}</td>
                                        <td>
                                            <details className="dropdown dropdown-left relative">
                                                <summary className="btn p-0 h-fit"><ClickSVG /></summary>
                                                <ul className="menu dropdown-content bg-base-100 rounded-box absolute z-1 w-fit p-2 shadow-sm">
                                                    <li><div onClick={() => setApplicationSettings(item.pk_position_id)}>applications</div></li>
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
    )
}
