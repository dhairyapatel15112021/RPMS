import { createContext } from "react"
import { Logout } from "./Logout";


export const SidebarContext = createContext();

export const Sidebar = ({ children, type }) => {

    return (
        <div className="w-[10vw] h-[88vh] shadow-sm flex flex-col justify-between py-10 px-2 rounded-md bg-black text-white m-2">
            <div className="flex flex-col gap-2">
                <div className="font-medium">Hello {type}</div>
                <ul className="flex flex-col gap-2 text-sm font-normal">{children}</ul>
            </div>
            <div className="bg-blue-700 text-white py-2 px-3 rounded-sm flex gap-2">
                <div><Logout/></div>
                <div>LOGOUT</div>
            </div>
        </div>
    )
}
