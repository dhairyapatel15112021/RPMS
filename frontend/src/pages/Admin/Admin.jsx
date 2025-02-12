import React from 'react'
import { Sidebar } from '../../component/Sidebar/Sidebar'
import { SidebarItem } from '../../component/Sidebar/SidebarItem'
import { Outlet } from 'react-router-dom'

export const Admin = () => {
  return (
    <div className='flex gap-2 my-2'>
      <Sidebar>
        <SidebarItem text="Roles" to="/admin/roles" />
        <SidebarItem text="Employees" to="/admin/employees" />
      </Sidebar>
      <Outlet />
    </div>
  )
}
