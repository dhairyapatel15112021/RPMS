import axios from 'axios';
import React, { useEffect, useState } from 'react'
import { ApiEndPoints } from '../../../data/ApiEndPoints';
import { DeleteSVG } from '../../../component/DeleteSVG';
import { Toast } from '../../../component/Toast/Toast';
import { ToastSucess } from '../../../component/Toast/ToastSucess';
import { Modal } from '../../../component/Modal';

export const AddRole = () => {
    const [roles, setRoles] = useState([]);
    const [roleData, setRoleData] = useState({ role_type: "" });
    const [Error, setError] = useState("");
    const [Sucess, setSucess] = useState("");
    const modalID = "role_modal";
    const inputs = [{type : "text" , placeholder : "Role Type" , name : "role_type"}];

    const onChangeFunction = (event) => {
        setRoleData({ ...roleData, [event.target.name]: event.target.value });
    }

    const onSubmitFunction = async () => {
        if (roleData.role_type.trim() === "") {
            setError("Role Can not be empty");
            return;
        }
        try {
            const response = await axios.post(ApiEndPoints.addRoles, roleData, { headers: { Authorization: localStorage.getItem("token") } });
            setSucess(response.data);
            setRoleData({ role_type: "" });
            getRoles();
        }
        catch (err) {
            setError(err.message || err.response.message);
            console.log(err);
        }
    }

    const DeleteRoles = async (id) => {
        try {
            const response = await axios.delete(`${ApiEndPoints.deleteRoles}${id}`,{ headers: { Authorization: localStorage.getItem("token") } });
            setSucess(response.data);
            getRoles();
        }
        catch (err) {
            setError(err.message || err.response.data);
            console.log(err);
        }
    }

    const getRoles = async () => {
        try {
            const response = await axios.get(ApiEndPoints.getRoles, {
                headers: { Authorization: localStorage.getItem("token") }
            });
            setRoles(response.data);
        }
        catch (err) {
            setError(err.message || err.response.data);
        }
    }

    useEffect(() => {
        getRoles();
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
        <div className='p-2 mt-3 w-fit'>
            {Error && <Toast message={Error} />}
            {Sucess && <ToastSucess message={Sucess} />}
            <Modal title="New Role" onchange={onChangeFunction} onsubmit={onSubmitFunction} id={modalID} inputs={inputs}  />
            <div className='font-bold text-lg'>
                Below Are Listed Roles
            </div>
            <div className="overflow-x-auto rounded-box border border-base-content/5 bg-base-100 mt-3">
                <table className="table">
                    <thead>
                        <tr>
                            <th></th>
                            <th>Role</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            roles?.map((role, index) => {
                                return (
                                    <tr key={role.pk_role_id}>
                                        <th>{index + 1}</th>
                                        <td>{role.role_type}</td>
                                        <td><button className="btn btn-error text-white" onClick={() => DeleteRoles(role.pk_role_id)}><DeleteSVG /></button></td>
                                    </tr>
                                )
                            })
                        }
                        <tr>
                            <td colSpan={3} className='font-bold cursor-pointer' onClick={() => document.getElementById(modalID).showModal()}>+ add new roles</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    )
}
