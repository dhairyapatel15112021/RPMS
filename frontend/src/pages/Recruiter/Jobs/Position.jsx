import React, { useEffect, useState } from 'react'
import { SearchInput } from '../../../component/Recruiter/SearchInput'
import { Plus } from '../../../component/Recruiter/Plus'
import axios from 'axios';
import { ApiEndPoints } from '../../../data/ApiEndPoints';
import { Table } from '../../../component/Table';
import Loader from '../../../component/Loader';
import { Modal } from '../../../component/Modal';
import { useSelector } from 'react-redux';
import { ClickSVG } from '../../../component/Recruiter/ClickSVG';

// validation while positin creatin
// error messages whith toast
// hold
// close
// update
// reopen 

export const Position = () => {
  const data = useSelector(state => state);
  const [positionData, setPositionData] = useState({ "position_title": "", "position_description": "", "position_min_experience": "", "position_level": "", "position_location": "", "position_creation_date": new Date(), "salary_range": "", "qualification": "", "fk_emp_id": data.user.id });

  const [position, setPositions] = useState([]);
  const [isLoading, setLoading] = useState(true);

  const columns = ["PositionId", "Title", "Description", , "Level", "Location", "Qualification", "CreatedAt", "CreatedBy", "Salary Range", "Status", "Experience Rquired", "Selected Candiate", "Comments", ""];
  const positionStatus = ["open", "hold", "close"];
  const inputs = [{ type: "text", name: "position_title", placeholder: "Title" },
  { type: "text", name: "position_description", placeholder: "Description" },
  { type: "text", name: "position_min_experience", placeholder: "Number Years Of Experience" },
  { type: "text", name: "position_level", placeholder: "Level" },
  { type: "text", name: "position_location", placeholder: "Locations" },
  { type: "text", name: "salary_range", placeholder: "Salary Range" },
  { type: "text", name: "qualification", placeholder: "Qualification" }
  ];
  const modalId = "position_modal";

  const getAllPositions = async () => {
    try {
      setLoading(true);
      const response = await axios.get(ApiEndPoints.getAllPositions, { headers: { Authorization: localStorage.getItem("token") } });
      setPositions(response.data);
      // setPositionData({"position_title": "","position_description": "","position_min_experience": "","position_level": "","position_location": "","position_creation_date": new Date(),"salary_range": "","qualification": "","fk_emp_id" :data.user.id });
    }
    catch (err) {
      console.log(err.message || err.response.data);
    }
    finally {
      setLoading(false);
    }
  }

  const onChangeFunction = (event) => {
    setPositionData({ ...positionData, [event.target.name]: event.target.value });
  }

  const onSubmitFunction = async () => {
    try {
      const response = await axios.post(ApiEndPoints.createPosition, positionData, { headers: { Authorization: localStorage.getItem("token") } });
      getAllPositions();
    }
    catch (err) {
      console.log(err.message || err.response.data);
    }
  }

  useEffect(() => {
    getAllPositions();
  }, []);

  return (
    <div className='p-2 shadow-md mt-3 rounded-md w-full overflow-hidden'>
      <Modal onchange={onChangeFunction} onsubmit={onSubmitFunction} id={modalId} inputs={inputs} title={"Create New Position"} />
      <div className='w-full flex justify-between items-center'>
        <div><SearchInput /></div>
        <div className='flex justify-between items-center gap-2 bg-violet-100 text-blue-500 p-2 rounded-md cursor-pointer' onClick={() => document.getElementById(modalId).showModal()}>
          <div><Plus /></div>
          <div>New Position</div>
        </div>
      </div>
      {isLoading ? <div className='h-[65vh] flex justify-center items-center'> <Loader /> </div>
        :
        position.length === 0 ? <div className='h-[65vh] flex justify-center items-center'> No Positions Available </div> :
          <Table columns={columns}>
            {
              position.map((item, index) => {
                return (
                  <tr key={index} className='text-center'>
                    <td>{item.pk_position_id}</td>
                    <td>{item.position_title}</td>
                    <td>{item.position_description}</td>
                    <td>{item.position_level}</td>
                    <td>{item.position_location}</td>
                    <td>{item.qualification}</td>
                    <td>{item.position_creation_date?.split("T")[0]}</td>
                    <td>{item.emp_name}</td>
                    <td>{item.salary_range}</td>
                    <td>{positionStatus[item.is_open]}</td>
                    <td>{item.position_min_experience}</td>
                    <td>{item.candidate_name || "Not Selected Yet"}</td>
                    <td>{item.comments || "No Comments"}</td>
                    <td>
                      <details className="dropdown dropdown-left relative">
                        <summary className="btn p-0 h-fit"><ClickSVG/></summary>
                        <ul className="menu dropdown-content bg-base-100 rounded-box absolute z-1 w-fit p-2 shadow-sm">
                          <li><div>Hold</div></li>
                          <li><div>Close</div></li>
                          <li><div>Update</div></li>
                          <li><div>Reopen</div></li>
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
