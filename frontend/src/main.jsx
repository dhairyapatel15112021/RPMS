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

const router = createBrowserRouter(
  [
    {
      path: "/", element: <App />, children: [
        { path: "login", element: <Login /> },
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
