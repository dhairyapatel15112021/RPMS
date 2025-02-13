import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import Login from './pages/Login.jsx'
import { ProtectedRoute } from './component/ProtectedRoute.jsx'
import { Admin } from './pages/Admin/Admin.jsx'
import { Role } from './pages/Role/Role.jsx'
import { Employee } from './pages/Employee/Employee.jsx'
import { AddRole } from './pages/Role/AddRole.jsx'
import { AssignRole } from './pages/Role/AssignRole.jsx'
import { Navigation } from './pages/Navigation.jsx'
import { Recruiter } from './pages/Recruiter/Recruiter.jsx'
import { Jobs } from './pages/Recruiter/Jobs/Jobs.jsx'
import { Candidates } from './pages/Recruiter/Candiates/Candidates.jsx'
import { Position } from './pages/Recruiter/Jobs/Position.jsx'
import { Skills } from './pages/Recruiter/Jobs/Skills.jsx'
import { Criteria } from './pages/Recruiter/Jobs/Criteria.jsx'

const router = createBrowserRouter(
  [
    {
      path: "/", element: <App />, children: [
        { path: "login", element: <Login /> },
        { path: "navigation", element: <Navigation /> },
        {
          path: "admin", element: <ProtectedRoute roles={["admin"]}><Admin /></ProtectedRoute>, children: [
            {
              path: "roles", element: <ProtectedRoute roles={["admin"]}><Role /></ProtectedRoute>, children: [
                { path: "add", element: <ProtectedRoute roles={["admin"]}><AddRole /></ProtectedRoute> },
                { path: "assign", element: <ProtectedRoute roles={["admin"]}><AssignRole /></ProtectedRoute> }
              ]
            },
            { path: "employees", element: <ProtectedRoute roles={["admin"]}><Employee /></ProtectedRoute> }
          ]
        },
        {
          path: "recruiter", element: <ProtectedRoute roles={["recruiter"]}><Recruiter /></ProtectedRoute>, children: [
            {
              path: "job", element: <ProtectedRoute roles={["recruiter"]}><Jobs /></ProtectedRoute>, children: [
                { path: "position", element: <ProtectedRoute roles={["recruiter"]}><Position /></ProtectedRoute> },
                { path: "skills", element: <ProtectedRoute roles={["recruiter"]}> <Skills/> </ProtectedRoute> },
                { path: "criteria", element: <ProtectedRoute roles={["recruiter"]}><Criteria /></ProtectedRoute> }
              ]
            },
            { path: "candidates", element: <ProtectedRoute roles={["recruiter"]}><Candidates /></ProtectedRoute> }
          ]
        }
      ]
    },
  ]
)

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>,
)
