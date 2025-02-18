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
                                    input.name === "no_of_hr_round" || input.name === "no_of_tech_round" ?
                                        <div>
                                            <input key={index} type={input.type} className="input mb-2" placeholder={data?.[input.name] || input.placeholder} name={input.name} onChange={onchange} />
                                            <div className='flex items-center gap-2'>
                                                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" className="stroke-info h-2 w-2 shrink-">
                                                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"></path>
                                                </svg>
                                                <span>Default {input.name} is {data?.[input.name]}</span>
                                            </div>
                                        </div>
                                        :
                                        <input key={index} type={input.type} className="input mb-2" placeholder={data?.[input.name] || input.placeholder} name={input.name} onChange={onchange} />
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
