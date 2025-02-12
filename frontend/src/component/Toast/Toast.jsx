import React from 'react'

export const Toast = ({ message }) => {
    return (
        <div className="toast toast-top toast-center">
            <div className="alert alert-error text-white">
                {message}
            </div>
        </div>
    )
}
