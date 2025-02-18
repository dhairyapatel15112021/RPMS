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
import { ClosePosition } from './ClosePosition';

// validation while positin creation,updation,hold,close,reopen
// error messages whith toast
// positin status wise button color
// search
// pagination

export const Position = () => {
  const data = useSelector(state => state);
  const [positionData, setPositionData] = useState({ "position_title": "", "position_description": "", "position_min_experience": "", "position_level": "", "position_location": "", "position_creation_date": new Date(), "salary_range": "", "qualification": "", "fk_emp_id": data.user.id });
  const [patchPositionData, setPatchPositionData] = useState({ comments: "", fk_candidate_id: 0 });
  const [position, setPositions] = useState([]);
  const [isLoading, setLoading] = useState(true);
  const [isUpdate, setIsUpdate] = useState(false);
  const [isClose, setIsClose] = useState(false);
  const [positionId, setPositionId] = useState(0);

  const columns = ["PositionId", "Title", "Description", , "Level", "Location", "Qualification", "CreatedAt", "CreatedBy", "Salary Range", "Status", "Experience Rquired", "Selected Candiate", "Comments", ""];
  const positionStatus = ["open", "hold", "close"];
  const inputs = [{ type: "text", name: "position_title", placeholder: "Title" },
  { type: "text", name: "position_description", placeholder: "Description" },
  { type: "number", name: "position_min_experience", placeholder: "Number Years Of Experience" },
  { type: "text", name: "position_level", placeholder: "Level" },
  { type: "text", name: "position_location", placeholder: "Locations" },
  { type: "text", name: "salary_range", placeholder: "Salary Range" },
  { type: "text", name: "qualification", placeholder: "Qualification" }
  ];
  const holdPositionInputs = [{ type: "text", name: "comments", placeholder: "Comments" }];

  const modalId = "position_modal";
  const modalHoldId = "position_hold_id";
  const modalCloseId = "position_close_id";

  const patchOnChange = (event) => {
    console.log("hi");
    setPatchPositionData({ ...patchPositionData, [event.target.name]: event.target.value });
  }

  const onChangeFunction = (event) => {
    setPositionData({ ...positionData, [event.target.name]: event.target.value });
  }

  const getAllPositions = async () => {
    try {
      setLoading(true);
      const response = await axios.get(ApiEndPoints.getAllPositions, { headers: { Authorization: localStorage.getItem("token") } });
      setPositions(response.data);
    }
    catch (err) {
      console.log(err.message || err.response.data);
    }
    finally {
      setLoading(false);
    }
  }

  const reopenPosition = async (position) => {
    try {
      const response = await axios.put(`${ApiEndPoints.reopenPosition}${position.pk_position_id}`, null, { headers: { Authorization: localStorage.getItem("token") } });
      console.log(response.data);
      getAllPositions();
    }
    catch (err) {
      console.log(err || err.message || err.response.data);
    }
  }

  const onPatchSubmitFunction = async () => {
    try {
      console.log(patchPositionData);
      let patchData = [{ "path": "comments", "op": "replace", "value": patchPositionData.comments }];
      if (isClose) {
        patchData.push({ "path": "fk_candidate_id", "op": "replace", "value": patchPositionData.fk_candidate_id });
      }
      const response = isClose ? await axios.patch(`${ApiEndPoints.ClosePosition}${positionId}`, patchData, { headers: { Authorization: localStorage.getItem("token") } }) : await axios.patch(`${ApiEndPoints.holdPosition}${positionId}`, patchData, { headers: { Authorization: localStorage.getItem("token") } });
      console.log(response.data);
      getAllPositions();
    }
    catch (err) {
      console.log(err || err.message || err.response.data);
    }
    finally {
      if(isClose)
        setIsClose(false);
    }
  }

  const onSubmitFunction = async () => {
    try {
      let { candidate_name, emp_name, ...updatePositiondata } = positionData;
      const response = isUpdate ? await axios.put(`${ApiEndPoints.updatePosition}${updatePositiondata.pk_position_id}`, { ...updatePositiondata, "fk_emp_id": data.user.id }, { headers: { Authorization: localStorage.getItem("token") } }) : await axios.post(ApiEndPoints.createPosition, positionData, { headers: { Authorization: localStorage.getItem("token") } });
      setPositionData({ "position_title": "", "position_description": "", "position_min_experience": "", "position_level": "", "position_location": "", "position_creation_date": new Date(), "salary_range": "", "qualification": "", "fk_emp_id": data.user.id });
      getAllPositions();
    }
    catch (err) {
      console.log(err || err.message || err.response.data);
    }
    finally {
      if (isUpdate) { setIsUpdate(false) };
    }
  }

  useEffect(() => {
    getAllPositions();
  }, []);

  const setUpdateSettings = (position) => {
    setPositionData(() => position);
    setIsUpdate(() => true);
    document.getElementById(modalId).showModal();
  }

  const setPatchSettings = (position) => {
    document.getElementById(modalHoldId).showModal();
    setPositionId(() => position.pk_position_id);
  }

  const setClosePositionSettings = (position) => {
    document.getElementById(modalCloseId).showModal();
    setPositionId(() => position.pk_position_id);
    setIsClose(true);
  }

  return (
    <div className='p-2 shadow-md mt-3 rounded-md w-full overflow-hidden'>
      <Modal onchange={onChangeFunction} onsubmit={onSubmitFunction} data={positionData} id={modalId} inputs={inputs} title={isUpdate ? "Update Position" : "Create New Position"} />
      <Modal onsubmit={onPatchSubmitFunction} onchange={patchOnChange} id={modalHoldId} inputs={holdPositionInputs} title="Hold Position" />
      <ClosePosition positionId={positionId} onsubmit={onPatchSubmitFunction} onchange={patchOnChange} id={modalCloseId} title="Close Position" />
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
              position?.map((item, index) => {
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
                        <summary className="btn p-0 h-fit"><ClickSVG /></summary>
                        <ul className="menu dropdown-content bg-base-100 rounded-box absolute z-1 w-fit p-2 shadow-sm">
                          <li><div onClick={() => setPatchSettings(item)}>Hold</div></li>
                          <li><div onClick={() => setClosePositionSettings(item)}>Close</div></li>
                          <li><div onClick={() => setUpdateSettings(item)}>Update</div></li>
                          <li><div onClick={() => reopenPosition(item)}>Reopen</div></li>
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
