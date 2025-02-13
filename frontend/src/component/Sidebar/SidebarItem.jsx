import React, { useContext } from 'react'
import { NavLink } from 'react-router-dom'

export const SidebarItem = ({ text , to , icon }) => {
    return (
        <NavLink to={to} className={ ({isActive}) => `items-center py-2 rounded-md cursor-pointer transition-colors p-2 flex gap-2 ${isActive ? 'text-white bg-[#0F06FF]' : 'text-white bg-none'}`}>
            <div>{icon}</div>
            <div>{text}</div>  
        </NavLink>
    )
}
