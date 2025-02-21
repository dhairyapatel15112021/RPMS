import React, { useState, useEffect } from 'react'
import { useSelector } from 'react-redux';
import Loader from '../../../component/Loader';
import Select from 'react-select';
import { ApiEndPoints } from '../../../data/ApiEndPoints';
import axios from 'axios';

export const PositionCreateModal = ({ id, getAllPostions }) => {
    const [isLoading, setLoading] = useState(true);
    const data = useSelector(state => state);
    const [positionData, setPositionData] = useState({ "position_title": "", "position_description": "", "position_min_experience": "", "position_level": "", "position_location": "", "position_creation_date": new Date(), "salary_range": "", "qualification": "", "fk_emp_id": data.user.id });
    const [minimumRequiredSkill, setMinimumRequiredSkills] = useState([]);
    const [preferedSkill, setPreferedSkills] = useState([]);
    const [skills, setSkills] = useState([]);

    const onChangeFunction = (event) => {
        setPositionData({ ...positionData, [event.target.name]: event.target.value });
    }

    const inputs = [{ type: "text", name: "position_title", placeholder: "Title" },
    { type: "text", name: "position_description", placeholder: "Description" },
    { type: "number", name: "position_min_experience", placeholder: "Number Years Of Experience" },
    { type: "text", name: "position_level", placeholder: "Level" },
    { type: "text", name: "position_location", placeholder: "Locations" },
    { type: "text", name: "salary_range", placeholder: "Salary Range" },
    { type: "text", name: "qualification", placeholder: "Qualification" }
    ];

    const getSkills = async () => {
        try {
            setLoading(true);
            setSkills([]);
            const response = await axios.get(`${ApiEndPoints.getAllSkills}`, { headers: { Authorization: localStorage.getItem("token") } });
            const formatted_skill = response.data.map((skill) => ({
                value: skill.pk_skills_id,
                label: skill.skills_name
            }));
            setSkills(formatted_skill);
            setMinimumRequiredSkills([]);
            setPreferedSkills([]);
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
            const data = { "position": positionData, "skills": { "minimumSkill": mSkill, "preferedSkill": pSkill } };
            const response = await axios.post(ApiEndPoints.createPosition, data, { headers: { Authorization: localStorage.getItem("token") } });
            getAllPostions();
            setPreferedSkills([]);
            setMinimumRequiredSkills([]);
            setPositionData(() => ({ ...positionData, "position_title": "", "position_description": "", "position_min_experience": "", "position_level": "", "position_location": "", "position_creation_date": new Date(), "salary_range": "", "qualification": "" }));
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
                            <legend className="fieldset-legend text-xl">Create Position</legend>
                            {
                                inputs.map((input, index) => {
                                    return (
                                            <input key={index} value={data?.[input.name]} type={input.type} className="input mb-2" placeholder={data?.[input.name] || input.placeholder} name={input.name} onChange={onChangeFunction} />              
                                    )
                                })
                            }
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
