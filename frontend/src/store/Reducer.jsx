import { InitialState } from "./InitialState";

export const Reducer = (state = InitialState, action) => {
    switch (action.type) {
        case 'login':
            state.user = { ...state.user, ...action.payload };
            return state;
    }
}