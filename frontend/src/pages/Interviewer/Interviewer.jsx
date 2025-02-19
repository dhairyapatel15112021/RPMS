import React from 'react'
import { Job } from '../../component/Recruiter/Job'
import { Outlet } from 'react-router-dom'
import { Title } from '../../component/Title'
import { SidebarItem } from '../../component/Sidebar/SidebarItem'
import { Sidebar } from '../../component/Sidebar/Sidebar'

export const Interviewer = () => {
    return (
        <div className='flex gap-2 w-screen h-[90vh]'>
            <Sidebar type="Interviewer">
                <SidebarItem text="Positions" icon={<Job />} to="/interviewer/position" />
            </Sidebar>
            <div className='mt-3'>
                <Title text="Application Interview & Management" />
                <Outlet />
            </div>
        </div>
    )
}
