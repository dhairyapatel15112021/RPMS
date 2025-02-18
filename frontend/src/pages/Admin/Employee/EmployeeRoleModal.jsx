import axios from 'axios';
import React, { useEffect, useState } from 'react'
import { ApiEndPoints } from '../../../data/ApiEndPoints';
import Loader from '../../../component/Loader';

export const EmployeeRoleModal = ({ onchange, onsubmit, id, employeeId, roles }) => {
    const [isLoading, setLoading] = useState(true);
    const [employeeRoles, setEmployeeRoles] = useState([]);
    const getRolesOfEmployess = async () => {
        try {
            if (employeeId === 0) {
                setLoading(false);
                return;
            }
            const response = await axios.get(`${ApiEndPoints.getRolesByEmployeeId}${employeeId}`, { headers: { Authorization: localStorage.getItem("token") } });
            setEmployeeRoles(response.data);
        }
        catch (err) {
            console.log(err.message || err.response.data);
        }
        finally {
            setLoading(false);
        }
    }
    useEffect(()=>{
        getRolesOfEmployess();
    },[employeeId]);

    return (
        <dialog id={id} className="modal">
            <div className="modal-box pt-0">
                <div className="modal-action">
                    {isLoading ? <Loader /> :
                        <fieldset className="fieldset w-full bg-base-200 border border-base-300 p-4 rounded-box">
                            <legend className="fieldset-legend text-xl">Assign Roles</legend>
                            {
                                employeeRoles.length === 0 ? <div className='mb-2'>Employee Does Not Have Roles</div> :
                                    <div className='flex flex-wrap gap-1 items-center'>
                                        {employeeRoles.map((role, index) => {
                                            return (<div key={index} className='p-1 bg-violet-100 text-blue-700 rounded-sm mb-2'>{role} </div>);
                                        })}
                                    </div>
                            }
                            <select defaultValue="Select Role" onChange={onchange} className="select mb-2">
                                <option disabled={true}>Select Role</option>
                                {
                                    roles.map((role) => {
                                        return (
                                            <option key={role.pk_role_id} value={role.pk_role_id}>{role.role_type}</option>
                                        )
                                    })}
                            </select>
                            <form method="dialog">
                                <button className="btn bg-violet-100 text-blue-400" onClick={onsubmit}>Submit</button>
                                <button className='btn text-white ml-3 bg-red-400'>Cancel</button>
                            </form>
                        </fieldset>
                    }
                </div>
            </div>
        </dialog>
    )
}
