import React from 'react'

export const InterviewScheduledModal = ({id,interview}) => {
    return (
        <dialog id={id} className="modal">
            <div className="modal-box pt-0">
                <div className="modal-action">
                    <fieldset className="fieldset w-full bg-base-200 border border-base-300 p-4 rounded-box">
                        <legend className="fieldset-legend text-xl">Scheduled Interview</legend>
                        <div className='flex gap-2 items-center'>
                            <div>Assesment Link</div>
                            <Link className="text-blue-700 bg-violet-100">Click</Link>
                        </div>
                        <div className='flex flex-col gap-1 mt-1 relative top-0'>
                            {
                                interview.interview_rounds.map((item, index) => {
                                    return <div key={index}> {item} </div>
                                })
                            }
                        </div>
                        <form method="dialog">
                            <button className="btn bg-violet-100 text-blue-400 mt-2" onClick={onSubmitFunction}>Submit</button>
                            <button className='btn text-white ml-3 bg-red-400 mt-2' onClick={cancleContentFunction}>Cancel</button>
                        </form>
                    </fieldset>

                </div>
            </div>
        </dialog>

    )
}
