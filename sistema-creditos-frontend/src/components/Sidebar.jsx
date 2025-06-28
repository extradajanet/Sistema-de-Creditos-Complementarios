// src/components/Sidebar.jsx
import { Link } from "react-router-dom";
import { Home, GraduationCap, User, LogOut } from "lucide-react";
import logo from '../images/logo-mapache.jpeg';

export default function Sidebar() {
  return (
    <aside className="bg-blue-950 text-white w-35 hover:w-56 transition-all duration-300 h-screen flex flex-col items-center py-15 group m-4 rounded-2xl">
      {/* Logo redondo y que se expande */}
      
        <div className="bg-white p-3 rounded-full transition-all duration-300 group-hover:p-2 gap-4 mb-10">
            <img
            src={logo}
            alt="Logo"
            className="w-20 h-20 rounded-full transition-all duration-300 group-hover:w-20 group-hover:h-20"
            />
        </div>


        <nav className="flex flex-col w-full px-2 gap-4 flex-grow">
          {/* Íconos arriba */}
        <div className="flex flex-col gap-4 items-center">
            <Link to="/" className="flex items-center gap-4 p-2 hover:bg-blue-700 rounded">
            <Home strokeWidth={0.5} className="w-8 h-8" />
            <span className="hidden group-hover:inline">Inicio</span>
            </Link>
            <Link to="/cursos" className="flex items-center gap-4 p-2 hover:bg-blue-700 rounded">
            <GraduationCap strokeWidth={0.5} className="w-8 h-8" />
            <span className="hidden group-hover:inline">Mi historial</span>
            </Link>
            <Link to="/perfil" className="flex items-center gap-4 p-2 hover:bg-blue-700 rounded">
            <User strokeWidth={0.5} className="w-8 h-8" />
            <span className="hidden group-hover:inline">Editar Perfil</span>
            </Link>
        </div>

        {/* Ícono abajo */}
        <div className="flex flex-col items-center mt-auto w-full ">
            <Link to="/logout" className="flex items-center gap-4 p-2 hover:bg-red-700 rounded w-full justify-center">
            <LogOut strokeWidth={0.5} className="w-8 h-8" />
            <span className="hidden group-hover:inline">Cerrar</span>
            </Link>
        </div>
        </nav>

    </aside>
  );
}
