const BASE_URL = "http://localhost:5083/api"
export const ApiEndPoints = {
    login : `${BASE_URL}/auth/login`,
    getRoles : `${BASE_URL}/admin/role/get/all`,
    addRoles : `${BASE_URL}/admin/role/add`,
    deleteRoles : `${BASE_URL}/admin/role/remove/`,
    addEmployees : `${BASE_URL}/employee/add`,
    getAllEmployees : `${BASE_URL}/employee/get/all`,
    getAllPositions : `${BASE_URL}/position/get/all`,
    createPosition : `${BASE_URL}/position/create`,
    addSkills : `${BASE_URL}/skills/add`
}