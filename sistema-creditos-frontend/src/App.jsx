import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { ToastContainer } from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css'
import Layout from "./components/Layout";
import Home from "./pages/Home";
import MiHistorial from "./pages/MiHistorial";
import Perfil from "./pages/Perfil";
import CursosDisponibles from "./pages/alumno/CursosDisponibles";
import MisCursos from "./pages/departamento/MisCursos";
import CrearActividad from "./pages/departamento/CrearActividad";
import ActividadesExtraescolares from "./pages/alumno/ActividadesExtraescolares";
import VerCursos from "./pages/coordinador/VerCursos";
import VerAlumnos from "./pages/coordinador/VerAlumnos";
import Avisos from "./pages/Avisos";
import MiHistorialDepartamento from "./pages/departamento/MiHistorialDepartamento";

import Login from "./pages/Auth/Login";
import Register from "./pages/Auth/Register";
import ProtectedRoute from "./components/ProtectedRoute";

function App() {
  return (

    <BrowserRouter>
    
    <ToastContainer 
            position="top-right"
            autoClose={3000}
            hideProgressBar
            pauseOnHover
          />

      <Routes>
        {/* Redirect root to login */}
        <Route path="/" element={<Navigate to="/login" replace />} />

        {/*Public Routes */}
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />

        {/*Protected Routes */}
        <Route
          path="/"
          element={
            <ProtectedRoute>
              <Layout />
            </ProtectedRoute>
          }
        >
          <Route path="/home" element={<Home />} />
          <Route path="/historial" element={<MiHistorial />} />
          <Route path="/perfil" element={<Perfil />} />
          <Route path="/cursosdisponibles" element={<CursosDisponibles />} />
          <Route path="/actividadesextraescolares" element={<ActividadesExtraescolares />} />
          <Route path="/miscursos" element={<MisCursos/>} />
          <Route path="/crearactividad" element={<CrearActividad/>} />
          <Route
            path="historialdepartamento"
            element={<MiHistorialDepartamento />}
          />{" "}
          <Route path="/vercursos" element={<VerCursos/>} />
          <Route path="/veralumnos" element={<VerAlumnos/>} />
          <Route path="/avisos" element={<Avisos/>} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
