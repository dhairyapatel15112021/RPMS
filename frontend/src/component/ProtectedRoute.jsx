import React, { useState } from 'react'
import Loader from './Loader';
import { Unauthorised } from './Unauthorised';

export const ProtectedRoute = ({ roles, children }) => {
    const [isLoading, setLoading] = useState(true);
    const [isAuthorised, setAuthorised] = useState(false);

    useState(() => {
        const allowed_roles = JSON.parse(localStorage.getItem("roles"));

        for (let i = 0; i < roles.length && i < allowed_roles.length; i++) {
            if (allowed_roles[i] == roles[i]) {
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
