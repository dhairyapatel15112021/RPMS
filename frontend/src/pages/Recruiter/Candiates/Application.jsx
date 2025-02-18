import React, { useEffect, useState } from 'react'
import { SearchInput } from '../../../component/Recruiter/SearchInput'
import Loader from '../../../component/Loader';
import axios from 'axios';
import { ApiEndPoints } from '../../../data/ApiEndPoints';
import { Table } from '../../../component/Table';
import { ClickSVG } from '../../../component/Recruiter/ClickSVG';
import { Apply } from './Apply';
import { ApplicationCandidateModal } from './ApplicationCandidateModal';

export const Application = () => {
  const [isLoading, setLoading] = useState(true);
  const [openPositions, setOpenPosition] = useState([]);
  const columns = ["PositionId", "Title", "Description", "Experience", "Position Level", ""];
  const [positionId, setPositionId] = useState(0);
  const applyModalId = "apply_modal_id";
  const applicationModalId = "application_modal_id";

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

  const setApplicationApplySetting = (id) => {
    setPositionId(() => id);
    document.getElementById(applyModalId).showModal();
  }

  const setApplicationSettings = (id) => {
    setPositionId(() => id);
    document.getElementById(applicationModalId).showModal();
  }

  return (
    <div className='p-2 shadow-md mt-3 rounded-md w-full overflow-hidden'>
      <Apply positionId={positionId} id={applyModalId} setPositionId={setPositionId} />
      <ApplicationCandidateModal id={applicationModalId} positionId={positionId} />
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
                          <li><div onClick={() => setApplicationApplySetting(item.pk_position_id)}>Apply</div></li>
                          <li><div onClick={() => setApplicationSettings(item.pk_position_id)}>Applications</div></li>
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
