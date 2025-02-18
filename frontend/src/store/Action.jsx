export const login = (payload) => {
    return {
        type : 'login',
        payload
    }
}

export const logout = () => {
    return {
        type : 'logout'
    }
}