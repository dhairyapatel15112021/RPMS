import React from 'react'
import { NavLink } from 'react-router-dom'
export const MiniSidebar = ({ miniSideBarItem }) => {
    return (
        <div className='flex flex-col gap-2 p-2'>
            {
                miniSideBarItem.map((item, index) => {
                    return (
                        <NavLink key={index} to={`${item.to}`} className={({ isActive }) => `p-2 ${isActive ? `bg-violet-50` : `bg-none`} text-blue-500 flex gap-1 items-center rounded-md`}>
                            <div>{item.icon}</div>
                            <div>{item.text}</div>
                        </NavLink>
                    )
                })
            }
        </div>
    )
}
