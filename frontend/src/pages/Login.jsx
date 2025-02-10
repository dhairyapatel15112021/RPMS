import React, { useState } from 'react'
import axios from 'axios';

const Login = () => {

    const [loginData, setLoginData] = useState({ email: "", password: "", is_candidate: false });
    const [Error, setError] = useState("");

    const onChangeFunction = (event) => {
        setLoginData({ ...loginData, [event.target.name]: event.target.value });
    }

    const submitForm = async () => {
        if (loginData.email.trim() === "") {
            setError("Username Should Not Be Blank");
            return;
        }
        if (!loginData.password === "") {
            setError("Password Should Not be Blank");
        }

        try {
            const response = await axios.post("http://localhost:5083/api/auth/login", loginData);
            console.log(response.data);
        }
        catch (err) {
            console.log(err);
        }
    }

    return (
        <div className='flex justify-center items-center h-[90vh] w-[100vw]'>
            <div className="flex flex-col justify-center items-center border rounded-md p-4">
                <div className='tracking-wide text-3xl'>LOGIN</div>
                <div className='flex flex-col mt-5 gap-5'>
                    <input onChange={onChangeFunction} type="text" name="email" placeholder="Email" className='border rounded-sm p-2 focus:outline-none' />
                    <input onChange={onChangeFunction} type="password" name="password" placeholder='Password' className='border rounded-sm p-2 focus:outline-none' />
                    <div className='flex justify-center gap-10'>
                        {
                            Error && <div className='text-sm self-start'>{Error}</div>
                        }
                    </div>
                    <button onClick={submitForm} type="button" className='bg-blue-700 w-fit py-1 px-2 text-white rounded-sm hover:bg-white hover:text-blue-700 hover:cursor-pointer self-end'>SUBMIT</button>
                </div>
            </div>
        </div>
    )
}

export default Login;