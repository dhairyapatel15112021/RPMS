import React,{useState,useEffect} from 'react'
import Loader from '../../../component/Loader'
import { Table } from '../../../component/Table'
import { Plus } from '../../../component/Recruiter/Plus'
import { SearchInput } from '../../../component/Recruiter/SearchInput'
import { Modal } from '../../../component/Modal'
import axios from 'axios'
import { ApiEndPoints } from '../../../data/ApiEndPoints'

// validation while positin creatin
// error messages whith toast
// fetching skills from backend
// remove
// upate
// api endpoint in backend for get skills
export const Skills = () => {
  const [skillsData, setSkillsData] = useState({
    "skills_name": "",
    "skills_description": "",
    "is_min_req_skills": false
  });

  const [skills, setSkills] = useState([]);
  const [isLoading, setLoading] = useState(true);

  const columns = ["SkillId", "Name","Description" ,"Minimum Skill" , ""];
  const inputs = [{ type: "text", name: "skills_name", placeholder: "Skill Name" },
    { type: "text", name: "skills_description", placeholder: "Skill Description" },
    { type: "checkbox", name: "is_min_req_skills", placeholder: "Is It minimum required skill?" }
  ]
  const modalId = "skills_modal";

  const getAllSkills= async () => {
    try {
      setLoading(true);
      const response = await axios.get(ApiEndPoints.getAllPositions, { headers: { Authorization: localStorage.getItem("token") } });
      setSkills(response.data);
      // setPositionData({"skills_name": "","skills_description": "","is_min_req_skills": false}));
    }
    catch (err) {
      console.log(err.message || err.response.data);
    }
    finally {
      setLoading(false);
    }
  }

  const onChangeFunction = (event) => {
    if(event.target.name === "is_min_req_skills"){
      setSkillsData({ ...skillsData, [event.target.name]: event.target.checked })
    }
    setSkillsData({ ...skillsData, [event.target.name]: event.target.value });
  }

  const onSubmitFunction = async () => {
    try {
      const response = await axios.post(ApiEndPoints.addSkills, skillsData, { headers: { Authorization: localStorage.getItem("token") } });
      console.log(response.data);
      // getAllSkills();
    }
    catch (err) {
      console.log(err.message || err.response.data);
    }
  }

    useEffect(() => {
      // getAllSkills();
    }, []);


  return (
    <div className='p-2 shadow-md mt-3 rounded-md w-full overflow-hidden'>
      <Modal onchange={onChangeFunction} onsubmit={onSubmitFunction} id={modalId} inputs={inputs} title={"Create New Skill"} />
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
                          <li><div>Update</div></li>
                          <li><div>Remove</div></li>
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
