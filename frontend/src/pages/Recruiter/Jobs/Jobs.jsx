import React, { useEffect } from 'react'
import { NavLink, Outlet, useNavigate } from 'react-router-dom'
import { Job } from '../../../component/Recruiter/Job';
import { Percentage } from '../../../component/Percentage';
import { MiniSidebar } from '../../../component/MiniSidebar';

export const Jobs = () => {
    const SidebarItem = [{ icon: <Percentage />, text: "Position", to: "/recruiter/job/position" }, { icon: <Percentage />, text: "Skills", to: "/recruiter/job/skills" }, { icon: <Percentage />, text: "Manage", to: "/recruiter/job/criteria" }];

    return (
        <div className='w-[88vw] h-fit flex gap-2'>
            <div className='shadow-md w-[15vw] p-2 mt-3 flex flex-col gap-2 rounded-md h-fit'>
                <div className='font-semibold bg-violet-100 p-2 rounded-md text-blue-500 flex gap-2 items-center'>
                    <div><Job /></div>
                    <div>Openings</div>
                </div>
                <MiniSidebar miniSideBarItem={SidebarItem} />
            </div>
            <Outlet />
        </div>
    )
}
