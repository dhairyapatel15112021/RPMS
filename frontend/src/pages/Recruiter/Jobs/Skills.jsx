import React, { useState, useEffect } from 'react'
import Loader from '../../../component/Loader'
import { Table } from '../../../component/Table'
import { Plus } from '../../../component/Recruiter/Plus'
import { SearchInput } from '../../../component/Recruiter/SearchInput'
import { Modal } from '../../../component/Modal'
import axios from 'axios'
import { ApiEndPoints } from '../../../data/ApiEndPoints'
import { ClickSVG } from '../../../component/Recruiter/ClickSVG'

// validation while positin creatin
// error messages whith toast
// search
// pagination
// update and remove buttons?

export const Skills = () => {
  const [skillsData, setSkillsData] = useState({
    "skills_name": "",
    "skills_description": "",
    "is_min_req_skills": false
  });

  const [skills, setSkills] = useState([]);
  const [isLoading, setLoading] = useState(true);
  const [isUpdate, setIsUpdate] = useState(false);
  const [skillId, setSkillId] = useState(0);

  const columns = ["SkillId", "Name", "Description", "Minimum Skill", "Update" , "Remove"];
  const inputs = [{ type: "text", name: "skills_name", placeholder: "Skill Name" },
  { type: "text", name: "skills_description", placeholder: "Skill Description" },
  { type: "checkbox", name: "is_min_req_skills", placeholder: "Is It minimum required skill?" }
  ]
  const modalId = "skills_modal";

  const getAllSkills = async () => {
    try {
      setLoading(true);
      const response = await axios.get(ApiEndPoints.getAllSkills, { headers: { Authorization: localStorage.getItem("token") } });
      setSkills(response.data);
    }
    catch (err) {
      console.log(err.message || err.response.data);
    }
    finally {
      setLoading(false);
    }
  }

  const onCheckboxChange = (event) => {
    setSkillsData({ ...skillsData, [event.target.name]: event.target.checked });
  }

  const onChangeFunction = (event) => {
    setSkillsData({ ...skillsData, [event.target.name]: event.target.value });
  }

  const onSubmitFunction = async () => {
    try {
      const response = isUpdate ? await axios.put(`${ApiEndPoints.updateSkills}${skillId}`, { ...skillsData, 'pk_skills_id': skillId }, { headers: { Authorization: localStorage.getItem("token") } }) : await axios.post(ApiEndPoints.addSkills, skillsData, { headers: { Authorization: localStorage.getItem("token") } });
      setSkillsData({ "skills_name": "", "skills_description": "", "is_min_req_skills": false });
      getAllSkills();
    }
    catch (err) {
      console.log(err.message || err.response.data);
    }
    finally {
      if (isUpdate){setIsUpdate(false)};
    }
  }

  useEffect(() => {
    getAllSkills();
  }, []);

  const setUpdateSettings = (skill) => {
    setSkillsData(() => skill);
    setIsUpdate(() => true);
    setSkillId(() => skill.pk_skills_id);
    document.getElementById(modalId).showModal();
  }


  return (
    <div className='p-2 shadow-md mt-3 rounded-md w-full overflow-hidden'>
      <Modal onchange={onChangeFunction} data={skillsData} oncheckboxchange={onCheckboxChange} onsubmit={onSubmitFunction} id={modalId} inputs={inputs} title={isUpdate ? "Update Skill" : "Create New Skill"} />
      <div className='w-full flex justify-between items-center'>
        <div><SearchInput /></div>
        <div className='flex justify-between items-center gap-2 bg-violet-100 text-blue-500 p-2 rounded-md cursor-pointer' onClick={() => document.getElementById(modalId).showModal()}>
          <div><Plus /></div>
          <div>New Skills</div>
        </div>
      </div>
      {isLoading ? <div className='h-[65vh] flex justify-center items-center'> <Loader /> </div>
        :
        skills.length === 0 ? <div className='h-[65vh] flex justify-center items-center'> No Skills Available </div> :
          <Table columns={columns}>
            {
              skills.map((item, index) => {
                return (
                  <tr key={index} className='text-center'>
                    <td>{item.pk_skills_id}</td>
                    <td>{item.skills_name}</td>
                    <td>{item.skills_description}</td>
                    <td>{item.is_min_req_skills ? "True" : "False"}</td>
                    <td><button className='btn' onClick={() => setUpdateSettings(item)} >Update</button></td>
                    <td><button className='btn'>Remove</button></td>
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
