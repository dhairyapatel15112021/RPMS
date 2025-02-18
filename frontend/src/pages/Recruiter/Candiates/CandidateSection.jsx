import React, { useEffect, useState } from 'react'
import { SearchInput } from '../../../component/Recruiter/SearchInput'
import { Plus } from '../../../component/Recruiter/Plus'
import { Document } from '../../../component/Document'
import { Table } from '../../../component/Table'
import Loader from '../../../component/Loader'
import { Modal } from '../../../component/Modal'
import axios from 'axios'
import { ApiEndPoints } from '../../../data/ApiEndPoints'
import { ClickSVG } from '../../../component/Recruiter/ClickSVG'
import { Link } from 'react-router-dom'

// validation while manual canidation creation.
// toast for proper error and sucess message.
export const CandidateSection = () => {
    const [candidateData, setCandidateData] = useState({ "candidate_name": "", "candidate_contact_number": "", "candidate_email": "", "candidate_password": "", "candidate_linkdien": "" });
    const [isLoading, setLoading] = useState(true);
    const [candidates, setCandidates] = useState([]);
    const modalId = "candidate_modal";
    const columns = ["CandidateId", "Name", "Email", "Contact", "Applications", "Skills", "Experience", "Education", "CV", ""];

    const onChangeFunction = (event) => {
        setCandidateData({ ...candidateData, [event.target.name]: event.target.value });
    }

    const getAllCandidates = async () => {
        try {
            setLoading(true);
            const response = await axios.get(ApiEndPoints.getAllCandidates, { headers: { Authorization: localStorage.getItem("token") } });
            setCandidates(response.data);
        }
        catch (err) {
            console.log(err.message || err.response.data);
        }
        finally {
            setLoading(false);
        }
    }

    const onFileChange = async (event) => {
        const file = event.target.files[0];
        if (file === null) {
            console.log("file is not selected");
            return;
        }

        const formData = new FormData();
        formData.append('file', file)

        try {
            const response = await axios.post(ApiEndPoints.addAllCandidate, formData, {
                headers: {
                    Authorization: localStorage.getItem("token"),
                    "Content-Type": "multipart/form-data"
                }
            });
            getAllCandidates();
        }
        catch (err) {
            console.log(err.response.data || err.message);
        }
    }

    const inputs = [{ type: "text", name: "candidate_name", placeholder: "Name" },
    { type: "text", name: "candidate_contact_number", placeholder: "Contact" },
    { type: "email", name: "candidate_email", placeholder: "Email" },
    { type: "password", name: "candidate_password", placeholder: "Password" },
    { type: "text", name: "candidate_linkdien", placeholder: "Linkdien" }];

    const onCvChange = async (event, id) => {
        try {
            const file = event.target.files[0];
            if (file === null) {
                console.log("Please Select Valid Files");
                return;
            }
            console.log(id);
            const data = new FormData();
            data.append("cv", file);

            const response = await axios.post(`${ApiEndPoints.uploadCV}${id}`, data, {
                headers: {
                    Authorization: localStorage.getItem("token"),
                    "Content-Type": "multipart/form-data"
                }
            });
            getAllCandidates();
        }
        catch (err) {
            console.log();
        }
    }
    const onSubmitFunction = async () => {
        try {
            const response = await axios.post(ApiEndPoints.addCandidate, candidateData, { headers: { Authorization: localStorage.getItem("token") } });
            getAllCandidates();
        }
        catch (err) {
            console.log(err.message || err.response.data);
        }
    }

    useEffect(() => {
        getAllCandidates();
    }, []);

    return (
        <div className='p-2 shadow-md mt-3 rounded-md w-full overflow-hidden'>
            <Modal id={modalId} inputs={inputs} title="New Candidates" onchange={onChangeFunction} onsubmit={onSubmitFunction} />
            <div className='w-full flex justify-between items-center'>
                <div><SearchInput /></div>
                <div className='flex justify-between items-center gap-3'>
                    <div>
                        <label for='dropzone-file' className='flex justify-between items-center gap-2 bg-violet-100 text-blue-500 p-2 rounded-md cursor-pointer'>
                            <div><Document /></div>
                            <div>Excel</div>
                        </label>
                        <input onChange={onFileChange} id='dropzone-file' type="file" accept='.xlsx' className="file-input hidden" />
                    </div>
                    <div className='flex justify-between items-center gap-2 bg-violet-100 text-blue-500 p-2 rounded-md cursor-pointer' onClick={() => document.getElementById(modalId).showModal()}>
                        <div><Plus /></div>
                        <div>New Candidate</div>
                    </div>
                </div>
            </div>
            {isLoading ? <div className='h-[65vh] flex justify-center items-center'> <Loader /> </div>
                :
                candidates.length === 0 ? <div className='h-[65vh] flex justify-center items-center'> No Candidates Available </div> :
                    <Table columns={columns}>
                        {
                            candidates.map((item, index) => {
                                return (
                                    <tr key={item.pk_candidate_id} className='text-center'>
                                        <td>{item.pk_candidate_id}</td>
                                        <td>{item.candidate_name}</td>
                                        <td>{item.candidate_email}</td>
                                        <td>{item.candidate_contact_number}</td>
                                        <td><button className='btn'>Application</button></td>
                                        <td><button className='btn'>Skills</button></td>
                                        <td><button className='btn'>Experience</button></td>
                                        <td><button className='btn'>Education</button></td>
                                        <td>{
                                            item.cv_path ?
                                                <Link to={`http://localhost:5083${item.cv_path}`} target='_blank' className='flex justify-between items-center gap-2 w-fit bg-violet-100 text-blue-500 p-2 rounded-md cursor-pointer justify-self-center'>
                                                    <div><Document /></div>
                                                    <div>View</div>
                                                </Link>
                                                :
                                                <input onChange={(event) => onCvChange(event, item.pk_candidate_id)} id='upload-cv' type="file" className="file-input file-input-ghost file-input-xs w-fit" />
                                        }
                                        </td>
                                        <td>
                                            <details className="dropdown dropdown-left relative">
                                                <summary className="btn p-0 h-fit"><ClickSVG /></summary>
                                                <ul className="menu dropdown-content bg-base-100 rounded-box absolute z-1 w-fit p-2 shadow-sm">
                                                    <li>dummy</li>
                                                </ul>
                                            </details>
                                        </td>
                                    </tr>
                                )
                            })
                        }
                    </Table>
            }
            <div className='flex justify-end my-3'>
                <div className='p-2 bg-violet-100 text-blue-500 rounded-md'>pagination</div>
            </div>
        </div>
    )
}
