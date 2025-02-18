import { createContext } from "react"
import { Logout } from "./Logout";
import { useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import { login, logout } from "../../store/Action";
import { InitialState } from "../../store/InitialState";


export const SidebarContext = createContext();

export const Sidebar = ({ children, type }) => {
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const LogoutFunction = () => {
        localStorage.removeItem("token");
        dispatch(logout());
        navigate("/login");
    }

    return (
        <div className="w-[10vw] h-[88vh] shadow-sm flex flex-col justify-between py-10 px-2 rounded-md bg-black text-white m-2">
            <div className="flex flex-col gap-2">
                <div className="font-medium">Hello {type}</div>
                <ul className="flex flex-col gap-2 text-sm font-normal">{children}</ul>
            </div>
            <div className="bg-blue-700 text-white py-2 px-3 rounded-sm flex gap-2" onClick={LogoutFunction}>
                <div><Logout /></div>
                <div className="cursor-pointer">Logout</div>
            </div>
        </div>
    )
}
