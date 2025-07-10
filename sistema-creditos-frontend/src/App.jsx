import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from "./components/Layout";
import Home from "./pages/Home";
import MiHistorial from "./pages/MiHistorial"
import Perfil from "./pages/Perfil";
import CursosDisponibles from "./pages/CursosDisponibles";

import Login from "./pages/Auth/Login";
import Register from "./pages/Auth/Register";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/" element={<Layout />}>
          <Route index element={<Home />} />
          <Route path="/historial" element={<MiHistorial />} />
          <Route path="/perfil" element={<Perfil />} />
            <Route path="/cursosdisponibles" element={<CursosDisponibles />} />

        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
