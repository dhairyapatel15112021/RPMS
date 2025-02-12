import React, { useContext } from 'react'
import { NavLink } from 'react-router-dom'

export const SidebarItem = ({ text , to }) => {
    return (
        <NavLink to={to} className={ ({isActive}) => `flex items-center gap-1 py-2 font-medium rounded-md cursor-pointer transition-colors group p-2 ${isActive ? 'text-white bg-[#0F06FF]' : 'text-black bg-none'}`}>
            {text}
        </NavLink>
    )
}
