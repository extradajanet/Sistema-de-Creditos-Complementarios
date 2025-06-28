import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Sidebar from "./components/Sidebar";

import Home from "./pages/Home";
import Cursos from "./pages/Cursos";
import Perfil from "./pages/Perfil";7
import CursosDisponibles from "./pages/CursosDisponibles";


function App() {
  return (
    <Router>
      <div className="flex">
        <Sidebar />
        <main className="flex-1 p-6 bg-gray-100 min-h-screen">
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/cursos" element={<Cursos />} />
            <Route path="/perfil" element={<Perfil />} />
            <Route path="/cursosdisponibles" element={<CursosDisponibles />} />

            {/* Puedes agregar más rutas aquí */}
          </Routes>
        </main>
      </div>
    </Router>
  );
}

export default App;
