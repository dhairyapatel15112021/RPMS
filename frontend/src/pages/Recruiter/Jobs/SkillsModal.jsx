import React, { useState, useEffect } from 'react'
import Loader from '../../../component/Loader';
import Select from 'react-select';
import { ApiEndPoints } from '../../../data/ApiEndPoints';
import axios from 'axios';

export const SkillsModal = ({ id, positionId }) => {
    const [isLoading, setLoading] = useState(true);
    const [minimumRequiredSkill, setMinimumRequiredSkills] = useState([]);
    const [preferedSkill, setPreferedSkills] = useState([]);
    const [skills, setSkills] = useState([]);

    const getSkills = async () => {
        try {
            setSkills([]);
            const response = await axios.get(`${ApiEndPoints.getAllSkills}`, { headers: { Authorization: localStorage.getItem("token") } });
            const formatted_skill = response.data.map((skill) => ({
                value: skill.pk_skills_id,
                label: skill.skills_name
            }));
            setSkills(formatted_skill);
        }
        catch (err) {
            console.log(err.message || err.response.data);
        }
    }


    const getSkillsByPositionId = async () => {
        try {
            if (positionId === 0) {
                setLoading(false);
                return;
            }
            setLoading(true);
            setMinimumRequiredSkills([]);
            setPreferedSkills([]);

            const response = await axios.get(`${ApiEndPoints.getPositionSkillMapped}${positionId}`, { headers: { Authorization: localStorage.getItem("token") } });
            const localskills = response.data;
            let localMinimumRequiredSkill = [], localPreferedSkill = [];
            const formatted_skill = localskills.map((skill) => ({
                value: skill.pk_skills_id,
                label: skill.skills_name
            }));
console.log(localskills);
            for (let i = 0; i < localskills.length; i++) {
                if (localskills[i].is_min_req_skills) localMinimumRequiredSkill.push(formatted_skill[i]);
                else localPreferedSkill.push(formatted_skill[i]);
            }

            if (localMinimumRequiredSkill.length != 0)
                setMinimumRequiredSkills(localMinimumRequiredSkill);
            if (localPreferedSkill.length != 0)
                setPreferedSkills(localPreferedSkill);

        }
        catch (err) {
            console.log(err.message || err.response.data);
        }
        finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        getSkills();
    }, []);

    useEffect(() => {
        getSkillsByPositionId();
    }, [positionId]);


    const onMinimumRequiredChange = (selected) => {
        setMinimumRequiredSkills(selected);
    }

    const onPreferedRequiredChange = (selected) => {
        setPreferedSkills(selected);
    }

    const getFilterOptins = (selectedSkills) => {
        return skills.filter((skill) => !selectedSkills.some(selected => selected.value === skill.value));
    }

    const onsubmitFunction = async () => {
        try {
            const mSkill = minimumRequiredSkill.map((item) => item.value);
            const pSkill = preferedSkill.map((item) => item.value);
            const response = await axios.post(`${ApiEndPoints.addPositionSkill}`, { minimumSkill: mSkill, preferedSkill: pSkill, positionId: positionId }, { headers: { Authorization: localStorage.getItem("token") } });
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
                            <legend className="fieldset-legend text-xl">Add Skills</legend>
                            <fieldset className="fieldset"> <legend className="fieldset-legend text-sm">Required Skill</legend>
                                <Select
                                    placeholder="Required Skill"
                                    closeMenuOnSelect={false}
                                    value={minimumRequiredSkill}
                                    isMulti
                                    options={getFilterOptins(preferedSkill)}
                                    onChange={onMinimumRequiredChange}
                                />
                            </fieldset>
                            <fieldset className="fieldset"> <legend className="fieldset-legend text-sm">Prefered Skill</legend>
                                <Select
                                    placeholder="Prefered Skill"
                                    closeMenuOnSelect={false}
                                    value={preferedSkill}
                                    isMulti
                                    options={getFilterOptins(minimumRequiredSkill)}
                                    onChange={onPreferedRequiredChange}
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
