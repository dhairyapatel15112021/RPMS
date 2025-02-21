import React, { useEffect, useState } from 'react';
import { RoundAccordian } from './RoundAccordian';
import axios from "axios";
import { ApiEndPoints } from '../../../data/ApiEndPoints';

export const InterviewScheduler = ({ id, applicationId }) => {
    const [interscheduleData, setInterviewScheduleData] = useState({ "fk_application_id": applicationId, "no_of_hr_round": 0, "no_of_tech_round": 0, "assesment_link": "", });
    const [roundCount, setRoundCount] = useState(1);
    const [individualRoundDetails, setIndividualRoundDetails] = useState({ "remarks": "", "round_number": 0, "interview_type": "", "interview_link": "", "isDone": false, "index": roundCount, "interview_date": "", "interview_time": "" });
    const [roundRegister, setRoundRegister] = useState([]);
    const [content, setContent] = useState([]);

    const onChangeFunction = (event) => {
        setInterviewScheduleData({ ...interscheduleData, [event.target.name]: event.target.value });
    }

    const setRoundOnChangeDetails = (event) => {
        setIndividualRoundDetails((prev) => ({ ...prev, [event.target.name]: event.target.value }));
    }

    const addRound = () => {
        if (roundCount != 1)
            setRoundRegister([...roundRegister, individualRoundDetails]);
        setIndividualRoundDetails({ "remarks": "", "round_number": 0, "interview_type": "", "interview_link": "", "isDone": false, "index": roundCount, "interview_date": "", "interview_time": "" });
        setContent([...content, <RoundAccordian index={roundCount} onChangeFunction={setRoundOnChangeDetails} />]);
        setRoundCount(() => roundCount + 1);
    }

    const onSubmitFunction = async () => {
        try{
            let no_of_tech_round  = 0;
            let no_of_hr_round = 0;
            let localRoundRegister = [...roundRegister,individualRoundDetails];
            let localInterviewDetails = {...interscheduleData};
            localRoundRegister.forEach(r => {
                
                if(r.interview_type === "0"){
                    no_of_hr_round ++;
                    r.round_number = no_of_hr_round;
                }
                else{
                    no_of_tech_round ++;
                    r.round_number = no_of_tech_round;
                }
            });
            localInterviewDetails.no_of_hr_round = no_of_hr_round;
            localInterviewDetails.no_of_tech_round = no_of_tech_round;
            const response = await axios.post(ApiEndPoints.scheduleInterview,{"interview" : localInterviewDetails,"interview_rounds" : localRoundRegister},{headers : {Authorization : localStorage.getItem("token")}});
            console.log(response.data);
        }
        catch(err){
            console.log(err.message || err.response.data);
        }
        finally{
            cancleContentFunction();
        }
    }

    const cancleContentFunction = () => {
        setContent([]);
        setRoundRegister([]);
        setRoundCount(1);
    }

    useEffect(()=>{
        setInterviewScheduleData({...interscheduleData,"fk_application_id" : applicationId});
    },[applicationId]);

    return (
        <dialog id={id} className="modal">
            <div className="modal-box pt-0">
                <div className="modal-action">
                    <fieldset className="fieldset w-full bg-base-200 border border-base-300 p-4 rounded-box">
                        <legend className="fieldset-legend text-xl">Schedule Interview</legend>
                        <input type="text" className="input my-1" placeholder="Assesment Link" name="assesment_link" onChange={onChangeFunction} />
                        <div className='btn w-fit bg-violet-100 text-blue-700' onClick={addRound}>Add Round</div>
                        <div className='flex flex-col gap-1 mt-1 relative top-0'>
                            {
                                content.map((item, index) => {
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
