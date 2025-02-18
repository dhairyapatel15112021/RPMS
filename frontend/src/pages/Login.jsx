import React, { useEffect, useState } from 'react'
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { Toast } from '../component/Toast/Toast';
import { ApiEndPoints } from '../data/ApiEndPoints';
import { useDispatch } from 'react-redux';
import { login } from '../store/Action';

const Login = () => {
    const navigate = useNavigate();
    const [loginData, setLoginData] = useState({ email: "", password: "", is_candidate: false });
    const [Error, setError] = useState("");
    const dispatch = useDispatch();

    useEffect(() => {
        const interval = setInterval(() => {
            if (!Error) {
                setError("");
            }
        }, 5000);
        return () => clearInterval(interval);
    }, []);

    const onChangeFunction = (event) => {
        if (event.target.type === "checkbox") {
            setLoginData({ ...loginData, [event.target.name]: event.target.checked });
            return;
        }
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
            localStorage.setItem("token", `Bearer ${response.data.token}`);
            const data = {
                id: response.data.id,
                isCandidate: false,
                isAdmin: false,
                isInterviewer: false,
                isReviewer: false,
                isRecruiter: false,
                isHr: false,
                isLogin: true
            };
            for (let i = 0; i < roles.length; i++) {
                switch (roles[i]) {
                    case "admin":
                        data.isAdmin = true;
                        data.isHr = true;
                        data.isReviewer = true;
                        data.isInterviewer = true;
                        data.isRecruiter = true;
                        break;

                    case "hr":
                        data.isHr = true;
                        break;

                    case "Interviewer":
                        data.isInterviewer = true;
                        break;

                    case "Reviewer":
                        data.isReviewer = true;
                        break;

                    case "recruiter":
                        data.isRecruiter = true;
                        break;

                    case "candidate":
                        data.isCandidate = true;
                        break;
                }
            }
            dispatch(login(data));
            if (loginData.is_candidate) {
                navigate("/");
                return;
            }
            navigate("/navigation");
        }
        catch (err) {
            setError(err.message || err.response.data);
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
                    <div className='flex justify-between items-center w-full'>
                        <div>
                            <label className="label text-xs font-medium">
                                <input type="checkbox" onChange={onChangeFunction} name='is_candidate' className="checkbox checkbox-sm" />
                                Candidate?
                            </label>
                        </div>
                        <button onClick={submitForm} type="button" className='bg-blue-700 w-fit py-1 px-2 text-white rounded-sm hover:bg-white hover:text-blue-700 hover:cursor-pointer'>SUBMIT</button>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Login;