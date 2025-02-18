import { InitialState } from "./InitialState";

export const Reducer = (state = InitialState, action) => {
    switch (action.type) {
        case 'login':
            const { isLogin, ...data } = { ...action.payload };
            state.user = { ...state.user, ...data };
            state.isLogin = isLogin;
            console.log(state);
            return state;

        case 'logout':
            state.user = {
                id: 0,
                name: "",
                email: "",
                isCandidate: false,
                isAdmin: false,
                isInterviewer: false,
                isReviewer: false,
                isRecruiter: false,
                isHr: false,
                isViewer: false
            };
            state.isLogin = false;
            return {...state};
    }
}