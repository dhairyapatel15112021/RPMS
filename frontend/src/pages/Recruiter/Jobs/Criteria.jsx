import React, { useEffect, useState } from 'react'
import { SearchInput } from '../../../component/Recruiter/SearchInput'
import Loader from '../../../component/Loader';
import axios from 'axios';
import { ApiEndPoints } from '../../../data/ApiEndPoints';
import { Table } from '../../../component/Table';
import { ClickSVG } from '../../../component/Recruiter/ClickSVG';
import { SkillsModal } from './SkillsModal';
import { ReviewerModal } from './ReviewerModal';
import { InterviewerModal } from './InterviewerModal';

export const Criteria = () => {
  const [isLoading, setLoading] = useState(true);
  const [openPositions, setOpenPosition] = useState([]);
  const columns = ["PositionId", "Title", "Description", "Experience", "Position Level", ""];
  const [positionId, setPositionId] = useState(0);
  const modalId = "modal_id";
  const reviewerModalId = "reviewer_id";
  const interviewModalId = "interviewer_id";

  const getOpenPositions = async () => {
    try {
      const response = await axios.get(`${ApiEndPoints.getOpenPosition}`, { headers: { Authorization: localStorage.getItem("token") } });
      setOpenPosition(response.data);
    }
    catch (err) {
      console.log(err.message || err.response.data);
    }
    finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    getOpenPositions();
  }, []);

  const setSkillsSettings = (id) => {
    setPositionId(() => id);
    document.getElementById(modalId).showModal();
  }

  const setReviewerSettings = (id) => {
    setPositionId(() => id);
    document.getElementById(reviewerModalId).showModal();
  }

  const setInterviewSettings = (id) => {
    setPositionId(() => id);
    document.getElementById(interviewModalId).showModal();
  }

  return (
    <div className='p-2 shadow-md mt-3 rounded-md w-full overflow-hidden'>
      <SkillsModal positionId={positionId} id={modalId} />
      <ReviewerModal positionId={positionId} id={reviewerModalId} />
      <InterviewerModal positionId={positionId} id={interviewModalId} />
      <div className='w-full flex justify-between items-center'>
        <div><SearchInput /></div>
      </div>
      {isLoading ? <div className='h-[65vh] flex justify-center items-center'> <Loader /> </div>
        :
        openPositions?.length === 0 ? <div className='h-[65vh] flex justify-center items-center'> No Open Positions </div> :
          <Table columns={columns}>
            {
              openPositions?.map((item, index) => {
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
                          <li><div onClick={() => setSkillsSettings(item.pk_position_id)}>Skills</div></li>
                          <li><div onClick={() => setReviewerSettings(item.pk_position_id)}>Reviewer</div></li>
                          <li><div onClick={() => setInterviewSettings(item.pk_position_id)}>Interviewer</div></li>
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
