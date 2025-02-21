import React from 'react'

export const RoundAccordian = ({ index, onChangeFunction }) => {
    return (
        <div key={index} className="collapse collapse-arrow bg-base-100 border border-base-300">
            <input type="radio" name="my-accordion-2" defaultChecked />
            <div className="collapse-title font-semibold">Round - {index} </div>
            <div className='collapse-content m-0'>
                <fieldset className="fieldset">
                    <legend className="fieldset-legend">Time</legend>
                    <input onChange={onChangeFunction} type="time" name="interview_time" className="input" />
                </fieldset>
                <fieldset className="fieldset">
                    <legend className="fieldset-legend">Date</legend>
                    <input onChange={onChangeFunction} type="date" name="interview_date" className="input" />
                </fieldset>
                <fieldset className="fieldset">
                    <legend className="fieldset-legend">Interview Type</legend>
                    <select onChange={onChangeFunction} defaultValue="Interview Type" name="interview_type" className="select">
                        <option disabled={true}>Interview Type</option>
                        <option value="0">HR</option>
                        <option value="1">Technical</option>
                    </select>
                </fieldset>
                <fieldset className="fieldset">
                    <legend className="fieldset-legend">Interview Link</legend>
                    <input onChange={onChangeFunction} type="text" name="interview_link" className="input" placeholder='Interview Link' />
                </fieldset>
            </div>
        </div>

    )
}
