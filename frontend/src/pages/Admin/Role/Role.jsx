import React from 'react'
import { Sidebar } from '../../../component/Sidebar/Sidebar'
import { SidebarItem } from '../../../component/Sidebar/SidebarItem'
import { Outlet } from 'react-router-dom'

export const Role = () => {
    return (
        <div className='flex gap-2'>
            <Sidebar>
                <SidebarItem text="Add Roles" to="/admin/roles/add" />
                <SidebarItem text="Assign Roles" to="/admin/roles/assign" />
            </Sidebar>
            <Outlet />
        </div>
    )
}
