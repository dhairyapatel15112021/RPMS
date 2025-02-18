import React from 'react'
import { Sidebar } from '../../component/Sidebar/Sidebar'
import { SidebarItem } from '../../component/Sidebar/SidebarItem'
import { Title } from '../../component/Title'
import { Outlet } from 'react-router-dom'
import { Job } from '../../component/Recruiter/Job'
import { useSelector } from 'react-redux'

export const Reviewe = () => {

    return (
        <div className='flex gap-2 w-screen h-[90vh]'>
            <Sidebar type="Reviewer">
                <SidebarItem text="Positions" icon={<Job />} to="/reviewer/position" />
            </Sidebar>
            <div className='mt-3'>
                <Title text="Application Review & Management" />
                <Outlet />
            </div>
        </div>
    )
}
