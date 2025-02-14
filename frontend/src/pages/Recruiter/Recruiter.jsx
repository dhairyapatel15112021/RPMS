import React from 'react'
import { Sidebar } from '../../component/Sidebar/Sidebar'
import { SidebarItem } from '../../component/Sidebar/SidebarItem'
import { Job } from '../../component/Recruiter/Job'
import { Candidate } from '../../component/Recruiter/Candidate'
import { Outlet } from 'react-router-dom'
import { Title } from '../../component/Title'

export const Recruiter = () => {
  return (
    <div className='flex gap-2 w-screen h-[90vh]'>
      <Sidebar type="Recruiter">
        <SidebarItem text="Jobs" icon={<Job />} to="/recruiter/job" />
        <SidebarItem text="Candidates" icon={<Candidate />} to="/recruiter/candidates" />
      </Sidebar>
      <div className='mt-3'>
        <Title text="Openings & candidate management" />
        <Outlet />
      </div>
    </div>
  )
}
