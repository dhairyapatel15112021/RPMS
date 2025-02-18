import axios from 'axios';
import React, { useState, useEffect } from 'react'
import { ApiEndPoints } from '../../../data/ApiEndPoints';
import Select from 'react-select';
import Loader from '../../../component/Loader';

export const ReviewerModal = ({ positionId, id }) => {
    const [isLoading, setLoading] = useState(true);
    const [empReviewer, setEmpReviewer] = useState([]);
    const [positionReviewer, setPositionReviewer] = useState([]);
    const getEmployeeReviewer = async () => {
        try {
            setEmpReviewer([]);
            const response = await axios.get(ApiEndPoints.getEmployeeReviewer, { headers: { Authorization: localStorage.getItem("token") } });
            const formatted_reviewer = response.data.map((reviewer) => ({
                value: reviewer.pk_emp_id,
                label: reviewer.emp_name
            }));
            setEmpReviewer(formatted_reviewer);
        }
        catch (err) {
            console.log(err.message || err.response.data);
        }
    }

    const getReviewerByPositionId = async () => {
        try {
            if (positionId === 0) {
                setLoading(false);
                return;
            }
            setLoading(true);
            setPositionReviewer([]);
            const response = await axios.get(`${ApiEndPoints.getPositionReviewer}${positionId}`, { headers: { Authorization: localStorage.getItem("token") } });
            const formatted_reviewer = response.data.map((reviewer) => ({
                value: reviewer.pk_emp_id,
                label: reviewer.emp_name
            }));
            setPositionReviewer(formatted_reviewer);
        }
        catch (err) {
            console.log(err.message || err.response.data);
        }
        finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        getEmployeeReviewer();
    }, []);

    useEffect(() => {
        getReviewerByPositionId();
    }, [positionId]);

    const onChangeFunction = (selected) => {
        setPositionReviewer(selected);
    }

    const onsubmitFunction = async () => {
        try {
            const ids = positionReviewer.map((reviewer)=> reviewer.value);
            const response = await axios.post(`${ApiEndPoints.addPositionReviewer}${positionId}`,ids,{headers : {Authorization : localStorage.getItem("token")}});
            console.log("done");
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
                            <legend className="fieldset-legend text-xl">Reviewer</legend>
                            <fieldset className="fieldset"> <legend className="fieldset-legend text-sm">Add Reviewer</legend>
                                <Select
                                    placeholder="Add Reviewer"
                                    closeMenuOnSelect={false}
                                    value={positionReviewer}
                                    isMulti
                                    options={empReviewer}
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
