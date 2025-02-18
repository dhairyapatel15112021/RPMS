import axios from 'axios';
import React, { useEffect, useState } from 'react'
import { ApiEndPoints } from '../../../data/ApiEndPoints';
import Loader from '../../../component/Loader';
import Select from 'react-select';

export const Apply = ({ positionId, id, setPositionId }) => {
    const [isLoading, setLoading] = useState(true);
    const [candidates, setCandidates] = useState([]);
    const [appliedCandidates, setAppliedCandidates] = useState([]);

    const getAllCandidates = async () => {
        try {
            if (positionId === 0) {
                setLoading(false);
                return;
            }
            setCandidates([]);
            setAppliedCandidates([]);
            const response = await axios.get(`${ApiEndPoints.getAllCandidatesByNotApplied}${positionId}`, { headers: { Authorization: localStorage.getItem("token") } });
            const formatted_candidates = response.data.map((candidate) => ({
                value: candidate.pk_candidate_id,
                label: candidate.candidate_name,
                email: candidate.candidate_email
            }));
            console.log(response.data);
            setCandidates(formatted_candidates);
        }
        catch (err) {
            console.log(err.message || err.response.data);
        }
        finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        getAllCandidates();
    }, [positionId]);

    const onChangeFunction = (selected) => {
        setAppliedCandidates(selected);
    }

    const onsubmitFunction = async () => {
        try {
            const ids = appliedCandidates.map((applications)=>applications.value);
            const response = await axios.post(`${ApiEndPoints.applyApplication}${positionId}`,ids,{headers :
                {Authorization : localStorage.getItem("token"),
                    "Content-Type" : "application/json"}
            }) ;
            console.log(response.data);
        }
        catch (err) {
            console.log(err.message || err.response.data);
        }
    }

    return (
        <dialog id={id} className="modal">
            <div className="modal-box pt-0">
                <div className="modal-action">
                    {isLoading ? <Loader /> :
                        <fieldset className="fieldset w-full bg-base-200 border border-base-300 p-4 rounded-box">
                            <legend className="fieldset-legend text-xl">Apply</legend>
                            <fieldset className="fieldset"> <legend className="fieldset-legend text-sm">Select Candidates</legend>
                                <Select
                                    placeholder="Select Candidates"
                                    closeMenuOnSelect={false}
                                    // value={minimumRequiredSkill}
                                    isMulti
                                    options={candidates}
                                    onChange={onChangeFunction}
                                />
                            </fieldset>
                            <form method="dialog">
                                <button className="btn bg-violet-100 text-blue-400" onClick={onsubmitFunction}>Submit</button>
                                <button className='btn text-white ml-3 bg-red-400'>Cancel</button>
                            </form>
                        </fieldset>
                    }
                </div>
            </div>
        </dialog>
    )
}
