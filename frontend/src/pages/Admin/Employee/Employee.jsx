import React, { useState, useEffect } from 'react'
import { Toast } from '../../../component/Toast/Toast';
import { ToastSucess } from '../../../component/Toast/ToastSucess'
import { ApiEndPoints } from '../../../data/ApiEndPoints';
import { DeleteSVG } from '../../../component/DeleteSVG.JSX';
import axios from 'axios';
import { SearchInput } from '../../../component/Recruiter/SearchInput';
import { Plus } from '../../../component/Recruiter/Plus';
import { Table } from '../../../component/Table';
import { ClickSVG } from '../../../component/Recruiter/ClickSVG';
import Loader from '../../../component/Loader';
import { Modal } from '../../../component/Modal';
import { EmployeeRoleModal } from './EmployeeRoleModal';

// validation while creating new employee
// toast for error and sucess
// remove employee baaki
// search
// pagination

export const Employee = () => {
  const [employeeData, setEmployeeData] = useState({ "emp_name": "", "emp_email": "", "emp_contact_number": "", "emp_designation": "", "emp_password": "", "emp_joining_date": "" });
  const [employees, setEmployees] = useState([]);
  const [Error, setError] = useState("");
  const [Sucess, setSucess] = useState("");
  const [isLoading, setLoading] = useState(true);
  const [employeeId,setEmployeeId] = useState(0);
  const [roles,setRoles] = useState([]);
  const [roleId,setRoleId] = useState(0);

  const columns = ["", "Employee ID", "Name", "Designation", "Email", "Contact Number", "Joining Date", "Remove", ""];
  const inputs = [{ type: "text", placeholder: "Name", name: "emp_name" },
  { type: "email", placeholder: "Email", name: "emp_email" },
  { type: "text", placeholder: "Contact", name: "emp_contact_number" },
  { type: "text", placeholder: "Designation", name: "emp_designation" },
  { type: "password", placeholder: "Password", name: "emp_password" },
  { type: "date", placeholder: "Joining Date", name: "emp_joining_date" }];
  const modalId = "employee_modal";
  const employeeRoleModal = "employee_role_modal";

  const onChangeFunction = (event) => {
    setEmployeeData({ ...employeeData, [event.target.name]: event.target.value });
  }

  const onSubmitFunction = async () => {
    try {
      const response = await axios.post(`${ApiEndPoints.addEmployees}`,employeeData,{headers : {Authorization : localStorage.getItem("token")}});
      setEmployeeData({ "emp_name": "", "emp_email": "", "emp_contact_number": "", "emp_designation": "", "emp_password": "", "emp_joining_date": "" });
      getEmployees();
    }
    catch (err) {
      console.log(err.message || err.response.data);
    }
  }

  const getEmployees = async () => {
    try {
      setLoading(true);
      const response = await axios.get(ApiEndPoints.getAllEmployees, {
        headers: { Authorization: localStorage.getItem("token") }
      });
      setEmployees(response.data);
    }
    catch (err) {
      setError(err.message || err.response.data);
    }
    finally {
      setLoading(false);
    }
  }

  const MapRolesToEmployee = async (emp) => {
    try{
      const response = await axios.get(`${ApiEndPoints.getRoles}`,{headers : {Authorization : localStorage.getItem('token')}});
      setRoles(() => response.data);
      setEmployeeId(() => emp.pk_emp_id);
      document.getElementById(employeeRoleModal).showModal();
    }
    catch(err){
      console.log(err.message || err.response.data);
    }
  }

  const onEmployeeRoleModalChange = (event) => {
    setRoleId(event.target.value);
  }

  const onEmployeeRoleModalSubmit = async () => {
    try{
      const response = await axios.post(`${ApiEndPoints.addRolesToEmployee}${employeeId}`,roleId,{headers : {Authorization : localStorage.getItem("token"),"Content-Type" : "application/json"}});
      console.log(response.data);
    }
    catch(err){
      console.log(err.message || err.response.data);
    }
    finally{
      setRoleId(0);
      setEmployeeId(0);
    }
  }

  useEffect(() => {
    getEmployees();
    const interval = setInterval(() => {
      if (!Error) {
        setError("");
      }
      if (!Sucess) {
        setSucess("");
      }
    }, 5000);
    return () => clearInterval(interval);
  }, []);

  return (
    <div className='p-2 shadow-md mt-3 rounded-md w-full overflow-hidden'>
      {Error && <Toast message={Error} />}
      {Sucess && <ToastSucess message={Sucess} />}
      <Modal data={employeeData} title="New Employee" inputs={inputs} id={modalId} onchange={onChangeFunction} onsubmit={onSubmitFunction} />
      <EmployeeRoleModal onchange={onEmployeeRoleModalChange} onsubmit={onEmployeeRoleModalSubmit} id={employeeRoleModal} employeeId={employeeId} roles={roles}/>
      <div className='w-full flex justify-between items-center'>
        <div><SearchInput /></div>
        <div className='flex justify-between items-center gap-2 bg-violet-100 text-blue-500 p-2 rounded-md cursor-pointer' onClick={() => document.getElementById(modalId).showModal()}>
          <div><Plus /></div>
          <div>New Employee</div>
        </div>
      </div>
      {isLoading ? <div className='h-[65vh] flex justify-center items-center'> <Loader /> </div>
        :
        employees.length === 0 ? <div className='h-[65vh] flex justify-center items-center'> No Employees Available </div> :
          <Table columns={columns}>
            {
              employees?.map((emp, index) => {
                return (
                  <tr key={emp.pk_emp_id} className='text-center'>
                    <th>{index + 1}</th>
                    <td>{emp.pk_emp_id}</td>
                    <td>{emp.emp_name}</td>
                    <td>{emp.emp_designation}</td>
                    <td>{emp.emp_email}</td>
                    <td>{emp.emp_contact_number}</td>
                    <td>{emp.emp_joining_date?.split("T")[0]}</td>
                    <td><button className="btn btn-error text-white px-2" onClick={() => DeleteRoles(role.pk_role_id)}> <DeleteSVG /> </button></td>
                    <td>
                      <details className="dropdown dropdown-left relative">
                        <summary className="btn p-0 h-fit"><ClickSVG /></summary>
                        <ul className="menu dropdown-content bg-base-100 rounded-box absolute z-1 w-fit p-2 shadow-sm">
                          <li><div onClick={()=>MapRolesToEmployee(emp)}>Assign Roles</div></li>
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
