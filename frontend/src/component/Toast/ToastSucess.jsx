import React from 'react'

export const ToastSucess = ({message}) => {
    return (
        <div className="toast toast-top toast-center">
            <div className="alert alert-success text-white">
                {message}
            </div>
        </div>
    )
}
