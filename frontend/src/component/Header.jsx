import React from 'react'
import { Link } from "react-router-dom";

const Header = () => {
  
    const onOnclickFunction = () => {
        console.log("clicked");
    }

    return (
        <div className='w-[100vw] h-[10vh] flex justify-between items-center p-5 shadow-sm'>
            <div className='font-bold tracking-wide md:text-2xl text-lg'>RPMS</div>

            <div className='flex gap-2'>
                <Link to="/signup" className='bg-blue-700 text-white px-2 py-1 tracking-wider rounded-md text-sm md:text-base'>SIGNUP</Link>
                {/* {
                    data?.user?.username === "" ? <Link to="/login" className='bg-blue-700 text-white px-2 py-1 tracking-wider rounded-md text-sm md:text-base'>LOGIN</Link> : <div className='bg-blue-700 text-white px-2 py-1 tracking-wider rounded-md cursor-pointer text-sm md:text-base' onClick={onOnclickFunction}>LOGOUT</div>
                } */}
            </div>
        </div>
    )
}

export default Header;