import React, { useState } from 'react'

export const ModelRole = ({onchange,onsubmit }) => {
    return (
        <dialog id="role_modal" className="modal">
            <div className="modal-box">
                <h3 className="font-bold text-lg">ADD NEW ROLES</h3>
                <input type="text" placeholder="role type" name='role_type' className="input input-neutral mt-3" onChange={onchange} />
                <div className="modal-action">
                    <form method="dialog">
                        <button className="btn" onClick={onsubmit}>Submit</button>
                    </form>
                </div>
            </div>
        </dialog>
    )
}
