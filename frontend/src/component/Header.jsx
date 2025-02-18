import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { Link } from "react-router-dom";
import { User } from './User';
import { logout } from '../store/Action';

const Header = () => {
    const data = useSelector(state => state);

    return (
        <div className='w-[100vw] h-[10vh] flex justify-between items-center p-5 shadow-sm'>
            <div className='font-bold tracking-wide md:text-2xl text-lg'>RPMS</div>

            <div className='flex gap-2'>
                {data != undefined && data.isLogin ? <div className='p-1 bg-blue-700 text-white rounded-md cursor-pointer'><User /></div> :
                    <Link to="/login" className='bg-blue-700 text-white px-2 py-1 tracking-wider rounded-md text-sm md:text-base'>LOGIN</Link>}
            </div>
        </div>
    )
}

export default Header;