import axios from 'axios';
import { useEffect } from 'react'
import { useDispatch } from 'react-redux'
import { ApiEndPoints } from '../data/ApiEndPoints';
import { useNavigate } from 'react-router-dom';
import { login } from '../store/Action';

export const UseAuthCheck = ({setIsChecked}) => {
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const validateUser = async () => {
        try {
            setIsChecked(false);
            const response = await axios.post(ApiEndPoints.validate, localStorage.getItem("token").split(" ")[1], { headers: { "Content-Type": "application/json" } });
            const roles = response.data.roles;
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
                dispatch(login(data));
                // if (response.data.is_candidate) {
                //     navigate("/");
                //     return;
                // }
                // navigate("/navigation");
            }
        }
        catch (err) {
            localStorage.removeItem("token");
            navigate("/login");
            console.log(err.message || err.response.data);
        }
        finally{
            setIsChecked(true);
        }
    }

    useEffect(() => {
        validateUser();
    }, [dispatch]);
}
