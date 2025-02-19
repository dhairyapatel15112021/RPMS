import React, { useEffect, useState } from 'react'
import Loader from '../../../component/Loader';
import axios from 'axios';
import { ApiEndPoints } from '../../../data/ApiEndPoints';

// search bar implemetation for id or name search
// toast implementation for status or error

export const ClosePosition = ({ positionId, onchange, onsubmit, id, title }) => {
    const [isLoading, setLoading] = useState(true);
    const [options, setOptions] = useState([]);

    const getAllAppliedCandidates = async () => {
        try {
            if (positionId == 0) {
                return;
            }
            setLoading(true);
            const response = await axios.get(`${ApiEndPoints.getAllApplicationByPosition}${positionId}?id=1`, { headers: { Authorization: localStorage.getItem("token") } });
            setOptions(response.data);
        }
        catch (err) {
            console.log(err.message || err.response.data);
        }
        finally {
            setLoading(false);
        }
    }
    useEffect(() => {
        getAllAppliedCandidates();
    }, [positionId]);

    return (
        <dialog id={id} className="modal">
            <div className="modal-box pt-0">
                <div className="modal-action">
                    <fieldset className="fieldset w-full bg-base-200 border border-base-300 p-4 rounded-box">
                        <legend className="fieldset-legend text-xl">{title}</legend>
                        <input type="text" className="input mb-2" placeholder="Comments" name="comments" onChange={onchange} />
                        <select defaultValue="Select Candidate" name='fk_candidate_id' onChange={onchange} className="select">
                            <option disabled={true}>Select Candidate</option>
                            {isLoading ? <span className="loading loading-spinner loading-sm"></span> : options.length === 0 ? <option>No Candidate Apply For this position</option> :
                                options.map((opt) => {
                                    return (
                                        <option value={opt.application.fk_candidate_id}>{opt.candidate_name}</option>
                                    )
                                })}
                        </select>
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
