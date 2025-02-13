import React, { useState } from 'react'
import Loader from './Loader';
import { Unauthorised } from './Unauthorised';
import { useSelector } from 'react-redux';

export const ProtectedRoute = ({ roles, children }) => {
    const [isLoading, setLoading] = useState(true);
    const data = useSelector(state => state);
    const [isAuthorised, setAuthorised] = useState(false);

    useState(() => {

        for (let i = 0; i < roles.length; i++) {
            if ((roles[i] === "admin" && data?.user?.isAdmin)
                || (roles[i] === "hr" && data?.user?.isHr)
                || (roles[i] === "interviewer" && data?.user?.isInterviewer)
                || (roles[i] === "reviewer" && data?.user?.isReviewer)
                || (roles[i] === "recruiter" && data?.user?.isRecruiter)
                || (roles[i] === "admin" && data?.user?.isCandidate)) {
                setLoading(false);
                setAuthorised(true);
                return;
            }
        }
        setLoading(false);
        setAuthorised(false);
    }, []);

    if (isLoading) {
        return <Loader />
    }

    if (!isLoading && !isAuthorised) {
        return <Unauthorised />
    }

    return (
        children
    )

}
