import React from 'react'
import { useSelector } from 'react-redux'
import { Link } from 'react-router-dom';
import { Chevron } from '../component/Chevron';

export const Navigation = () => {
    const data = useSelector(state => state);
    return (
        <div className='h-[90vh] w-[100vw] flex justify-center items-center'>
            <div className='shadow-md rounded-sm p-5 w-[30vw]'>
                <div className='text-center font-bold'>Welcome back!</div>
                <div className='flex flex-col mt-3 gap-2'>
                    {data?.user?.isAdmin && <Link to="/admin/roles/add" className='flex justify-between items-center border border-blue-200 rounded-md shadow-2xs p-2'>
                        <div>Admin</div>
                        <div><Chevron /></div>
                    </Link>
                    }
                    {data?.user?.isRecruiter && <Link to="/recruiter/job/position" className='flex justify-between items-center border border-blue-200 rounded-md shadow-2xs p-2'>
                        <div>Recruiter</div>
                        <div><Chevron /></div>
                    </Link>
                    }
                    {data?.user?.isInterviewer && <Link to="/" className='flex justify-between items-center border border-blue-200 rounded-md shadow-2xs p-2'>
                        <div>Interviewer</div>
                        <div><Chevron /></div>
                    </Link>}
                    {data?.user?.isReviewer && <Link to="/" className='flex justify-between items-center border border-blue-200 rounded-md shadow-2xs p-2'>
                        <div>Reviewer</div>
                        <div><Chevron /></div>
                    </Link>}
                    {data?.user?.isHr && <Link to="/" className='flex justify-between items-center border border-blue-200 rounded-md shadow-2xs p-2'>
                        <div>HR</div>
                        <div><Chevron /></div>
                    </Link>}
                </div>
            </div>
        </div>
    )
}
