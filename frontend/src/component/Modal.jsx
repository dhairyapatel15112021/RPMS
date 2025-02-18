import React from 'react'

export const Modal = ({ onchange, data, onsubmit, id, title, inputs }) => {
    return (
        <dialog id={id} className="modal">
            <div className="modal-box pt-0">
                <div className="modal-action">
                    <fieldset className="fieldset w-full bg-base-200 border border-base-300 p-4 rounded-box">
                        <legend className="fieldset-legend text-xl">{title}</legend>
                        {
                            inputs.map((input, index) => {
                                return (
                                    <input type={input.type} className="input mb-2" placeholder={data?.[input.name] || input.placeholder} name={input.name} onChange={onchange} />
                                )
                            })
                        }
                        <form method="dialog">
                            <button className="btn bg-violet-100 text-blue-400" onClick={onsubmit}>Submit</button>
                            <button className='btn text-white ml-3 bg-red-400'>Cancel</button>
                        </form>
                    </fieldset>
                </div>
            </div>
        </dialog>
    )
}
