const BASE_URL = "http://localhost:5083/api";

export const ApiEndPoints = {
    login : `${BASE_URL}/auth/login`,
    getRoles : `${BASE_URL}/admin/role/get/all`,
    addRoles : `${BASE_URL}/admin/role/add`,
    deleteRoles : `${BASE_URL}/admin/role/remove/`,
    addEmployees : `${BASE_URL}/employee/add`,
    getAllEmployees : `${BASE_URL}/employee/get/all`,
    getAllPositions : `${BASE_URL}/position/get/all`,
    createPosition : `${BASE_URL}/position/create`,
    addSkills : `${BASE_URL}/skills/add`,
    getAllSkills : `${BASE_URL}/skills/get/all`,
    updateSkills : `${BASE_URL}/skills/update/`,
    updatePosition : `${BASE_URL}/position/update/`,
    holdPosition : `${BASE_URL}/position/hold/`,
    reopenPosition : `${BASE_URL}/position/open/`,
    addCandidate : `${BASE_URL}/candidate/add`,
    addAllCandidate : `${BASE_URL}/candidate/add/all`,
    getAllCandidates : `${BASE_URL}/candidate/get/all`
}