import React from 'react'
import { Sidebar } from '../../component/Sidebar/Sidebar'
import { SidebarItem } from '../../component/Sidebar/SidebarItem'
import { Outlet } from 'react-router-dom'
import { Title } from '../../component/Title'

export const Admin = () => {
  return (
    <div className='flex gap-2 my-2'>
      <Sidebar type="Admin">
        <SidebarItem text="Roles" to="/admin/roles" />
        <SidebarItem text="Employees" to="/admin/employees" />
      </Sidebar>
      <div className='mt-3'>
        <Title text="Role & Employee Management" />
        <Outlet />
      </div>
    </div>
  )
}
