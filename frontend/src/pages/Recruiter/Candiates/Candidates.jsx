import React from 'react'
import { Percentage } from '../../../component/Percentage'
import { Job } from '../../../component/Recruiter/Job'
import { MiniSidebar } from '../../../component/MiniSidebar'
import { Outlet } from 'react-router-dom'

export const Candidates = () => {
  const SidebarItem = [{ icon: <Percentage />, text: "Manage", to: "/recruiter/candidates/manage" }, { icon: <Percentage />, text: "Application", to: "/recruiter/candidates/application" }];

  return (
    <div className='w-[88vw] h-fit flex gap-2'>
      <div className='shadow-md w-[15vw] p-2 mt-3 flex flex-col gap-2 rounded-md h-fit'>
        <div className='font-semibold bg-violet-100 p-2 rounded-md text-blue-500 flex gap-2 items-center'>
          <div><Job /></div>
          <div>Candidate & Application</div>
        </div>
        <MiniSidebar miniSideBarItem={SidebarItem} />
      </div>
      <Outlet />
    </div>
  )
}
