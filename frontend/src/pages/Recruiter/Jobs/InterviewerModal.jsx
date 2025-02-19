import axios from 'axios';
import React, { useState, useEffect } from 'react'
import { ApiEndPoints } from '../../../data/ApiEndPoints';
import Select from 'react-select';
import Loader from '../../../component/Loader';

export const InterviewerModal = ({ positionId, id }) => {
    const [isLoading, setLoading] = useState(true);
    const [empInterviewer, setEmpInterviewer] = useState([]);
    const [positionInterviewer, setPositionInterviewer] = useState([]);
    const getEmployeeInterviwer = async () => {
        try {
            setEmpInterviewer([]);
            const response = await axios.get(ApiEndPoints.getEmployeeInterviewer, { headers: { Authorization: localStorage.getItem("token") } });
            const formatted_Interviewer = response.data.map((interviewer) => ({
                value: interviewer.pk_emp_id,
                label: interviewer.emp_name
            }));
            setEmpInterviewer(formatted_Interviewer);
        }
        catch (err) {
            console.log(err.message || err.response.data);
        }
    }

    const getInterviewerByPositionId = async () => {
        try {
            if (positionId === 0) {
                setLoading(false);
                return;
            }
            setLoading(true);
            setPositionInterviewer([]);
            const response = await axios.get(`${ApiEndPoints.getPositionInterviewer}${positionId}`, { headers: { Authorization: localStorage.getItem("token") } });
            const formatted_Interviewer = response.data.map((interviewer) => ({
                value: interviewer.pk_emp_id,
                label: interviewer.emp_name
            }));
            setPositionInterviewer(formatted_Interviewer);
        }
        catch (err) {
            console.log(err.message || err.response.data);
        }
        finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        getEmployeeInterviwer();
    }, []);

    useEffect(() => {
        getInterviewerByPositionId();
    }, [positionId]);

    const onChangeFunction = (selected) => {
        setPositionInterviewer(selected);
    }

    const onsubmitFunction = async () => {
        try {
            const ids = positionInterviewer.map((interviewer) => interviewer.value);
            const response = await axios.post(`${ApiEndPoints.addPositionInterviewer}${positionId}`, ids, { headers: { Authorization: localStorage.getItem("token") } });
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
                            <legend className="fieldset-legend text-xl">Interviewer</legend>
                            <fieldset className="fieldset"> <legend className="fieldset-legend text-sm">Add Interviewer</legend>
                                <Select
                                    placeholder="Add Reviewer"
                                    closeMenuOnSelect={false}
                                    value={positionInterviewer}
                                    isMulti
                                    options={empInterviewer}
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
