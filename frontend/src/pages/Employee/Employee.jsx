import React, { useState, useEffect } from 'react'
import { Toast } from '../../component/Toast/Toast'
import { ToastSucess } from '../../component/Toast/ToastSucess'
import { ApiEndPoints } from '../../data/ApiEndPoints';
import { DeleteSVG } from '../../component/DeleteSVG.JSX';
import axios from 'axios';
import { ModalEmployee } from './ModalEmployee';

export const Employee = () => {
  const [employeeData, setEmployeeData] = useState({ "emp_name": "", "emp_email": "", "emp_contact_number": "", "emp_designation": "", "emp_password": "", "emp_joining_date": "" });
  const [employees, setEmployees] = useState([]);
  const [Error, setError] = useState("");
  const [Sucess, setSucess] = useState("");

  const onChangeFunction = (event) => {
    setEmployeeData({ ...employeeData, [event.target.name]: event.target.value });
  }

  const onSubmitFunction = async () => {
    try {

    }
    catch (err) {

    }
  }

  const getEmployees = async () => {
    try {
      const response = await axios.get(ApiEndPoints.getAllEmployees, {
        headers: { Authorization: localStorage.getItem("token") }
      });
      setEmployees(response.data);
      console.log(response.data);
    }
    catch (err) {
      setError(err.message || err.response.data);
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
    <div className='p-2'>
      {Error && <Toast message={Error} />}
      {Sucess && <ToastSucess message={Sucess} />}
      <ModalEmployee/>
      <div className='font-bold text-lg'>
        Below Are Companies Employees
      </div>
      <div className="overflow-x-auto w-[85vw] mt-3">
        <table className="table table-xs">
          <thead>
            <tr>
              <th></th>
              <th>Employee ID</th>
              <th>Name</th>
              <th>Designation</th>
              <th>Email</th>
              <th>Contact Number</th>
              <th>Joining Date</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {
              employees?.map((emp, index) => {
                return (
                  <tr>
                    <th>{index + 1}</th>
                    <td>{emp.pk_emp_id}</td>
                    <td>{emp.emp_name}</td>
                    <td>{emp.emp_designation}</td>
                    <td>{emp.emp_email}</td>
                    <td>{emp.emp_contact_number}</td>
                    <td>{emp.emp_joining_date?.split("T")[0]}</td>
                    <td><button className="btn btn-error text-white px-2" onClick={() => DeleteRoles(role.pk_role_id)}> <DeleteSVG/> </button></td>
                  </tr>
                )
              }
              )
            }
            <tr>
              <th colSpan={7}><button className="btn btn-soft mt-3" onClick={() => document.getElementById('emp_modal').showModal()}>+ Add New Employee</button></th>
            </tr>
          </tbody>

          {/* <tfoot>
            <tr>
              <th></th>
              <th>Employee ID</th>
              <th>Name</th>
              <th>Designation</th>
              <th>Email</th>
              <th>Contact Number</th>
              <th>Joining Date</th>
              <th></th>
            </tr>
          </tfoot> */}
        </table>
      </div>
    </div>

  )
}
