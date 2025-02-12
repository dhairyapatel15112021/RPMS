import React from 'react'

export const ModalEmployee = ({ onchange, onsubmit }) => {
    return (
        <dialog id="emp_modal" className="modal">
            <div className="modal-box pt-0">
                <div className="modal-action">
                    <fieldset className="fieldset w-full bg-base-200 border border-base-300 p-4 rounded-box">
                        <legend className="fieldset-legend text-xl">Add Employee</legend>
                        <input type="text" className="input" placeholder="My awesome page" />
                        <input type="text" className="input" placeholder="My awesome page" />
                        
                        <form method="dialog">
                            <button className="btn btn-primary text-white" onClick={onsubmit}>Submit</button>
                        </form>
                    </fieldset>

                </div>
            </div>
        </dialog>
    )
}
