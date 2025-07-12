import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Layout from "./components/Layout";
import Home from "./pages/Home";
import MiHistorial from "./pages/MiHistorial"
import Perfil from "./pages/Perfil";
import CursosDisponibles from "./pages/alumno/CursosDisponibles";
import MisCursos from "./pages/departamento/MisCursos";
import CrearActividad from "./pages/departamento/CrearActividad";

import Login from "./pages/Auth/Login";
import Register from "./pages/Auth/Register";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        {/* Redirect root path to login */}
        <Route path="/" element={<Navigate to="/login" />} />

        {/* Auth routes */}
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />

        {/* Main layout with nested routes */}
        <Route path="/app" element={<Layout />}>
          <Route index element={<Home />} />
          <Route path="historial" element={<MiHistorial />} />
          <Route path="perfil" element={<Perfil />} />
          <Route path="cursosdisponibles" element={<CursosDisponibles />} />
          <Route path="miscursos" element={<MisCursos />} />
          <Route path="crearactividad" element={<CrearActividad />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
