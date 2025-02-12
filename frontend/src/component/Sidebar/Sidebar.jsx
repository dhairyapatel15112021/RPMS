import { createContext } from "react"


export const SidebarContext = createContext();

export const Sidebar = ({ children }) => {

    return (
        <div className="w-[10vw] h-[90vh] shadow-sm flex flex-col py-10 px-2 rounded-md bg-[#E3E2F8]">
            <ul>{children}</ul>
        </div>
    )
}
