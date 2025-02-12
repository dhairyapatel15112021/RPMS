import React, { useEffect, useState } from 'react'
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { Toast } from '../component/Toast/Toast';
import { ApiEndPoints } from '../data/ApiEndPoints';

const Login = () => {
    const navigate = useNavigate();
    const [loginData, setLoginData] = useState({ email: "", password: "", is_candidate: false });
    const [Error, setError] = useState("");

    useEffect(() => {
        const interval = setInterval(() => {
            if (!Error) {
                setError("");
            }
        }, 5000);
        return () => clearInterval(interval);
    }, []);

    const onChangeFunction = (event) => {
        setLoginData({ ...loginData, [event.target.name]: event.target.value });
    }

    const submitForm = async () => {
        if (loginData.email.trim() === "") {
            setError("Username Should Not Be Blank");
            return;
        }
        if (loginData.password.trim() === "") {
            setError("Password Should Not be Blank");
            return;
        }
        try {
            const response = await axios.post(ApiEndPoints.login, loginData);
            const roles = response.data.roles;
            roles.sort();
            localStorage.setItem("token", `Bearer ${response.data.token}`);
            localStorage.setItem("roles", JSON.stringify(roles));
            navigate("/admin/roles/add");
        }
        catch (err) {
            setError(err.response.data || err.message);
            console.log(err);
        }
    }

    return (
        <div className='flex justify-center items-center h-[90vh] w-[100vw]'>
            <div className="flex flex-col justify-center items-center border rounded-md p-4">
                {Error && <Toast message={Error} />}
                <div className='tracking-wide text-3xl'>LOGIN</div>
                <div className='flex flex-col mt-5 gap-5'>
                    <input onChange={onChangeFunction} type="text" name="email" placeholder="Email" className='border rounded-sm p-2 focus:outline-none' />
                    <input onChange={onChangeFunction} type="password" name="password" placeholder='Password' className='border rounded-sm p-2 focus:outline-none' />
                    <button onClick={submitForm} type="button" className='bg-blue-700 w-fit py-1 px-2 text-white rounded-sm hover:bg-white hover:text-blue-700 hover:cursor-pointer self-end'>SUBMIT</button>
                </div>
            </div>
        </div>
    )
}

export default Login;