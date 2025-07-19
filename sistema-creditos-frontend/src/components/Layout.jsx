// src/components/Sidebar.jsx
import { Link, Outlet } from "react-router-dom";
import { Home, GraduationCap, User, LogOut } from "lucide-react";
import logo from "../images/logo2.png";
import { useEffect, useState } from "react";

export default function Layout() {
  const [historialPath, setHistorialPath] = useState("/historial");

  useEffect(() => {
    const rol = localStorage.getItem("rol");
    if (rol === "Departamento") {
      setHistorialPath("/historialdepartamento");
    } else if (rol === "Coordinador") {
      setHistorialPath("/historial-coordinador");
    } else {
      setHistorialPath("/historial"); // Alumno o default
    }
  }, []);

  return (
    <div className="flex h-screen h-full overflow-hidden">
      {/* Sidebar */}
      <aside
        className="bg-[#001F54] text-white w-20 md:w-56 transition-all duration-300  flex flex-col items-center py-5 m-4 rounded-2xl"
      >
        {/* Logo */}
        <div className="bg-white p-4 rounded-full mb-20">
          <img
            src={logo}
            alt="Logo"
            className="w-25 h-25 rounded-full transition-all duration-400"
          />
        </div>

        {/* Navigation */}
        <nav className="flex flex-col w-full px-2 gap-4 flex-grow">
          <div className="flex flex-col gap-4 items-center">
            <SidebarLink
              to="/"
              icon={<Home strokeWidth={0.5} className="w-6 h-6" />}
              label="Inicio"
            />
            <SidebarLink
              to={historialPath}
              icon={<GraduationCap strokeWidth={0.5} className="w-6 h-6" />}
              label="Mi historial"
            />
            <SidebarLink
              to="/perfil"
              icon={<User strokeWidth={0.5} className="w-6 h-6" />}
              label="Editar Perfil"
            />
          </div>

          <div className="flex flex-col items-center mt-auto w-full">
            <button
              onClick={() => {
                localStorage.clear();
                window.location.href = "/login";
              }}
              className="flex items-center w-full px-4 py-2 text-left cursor-pointer">
              <LogOut strokeWidth={0.5} className="w-6 h-6 mr-2" />
              <span>Cerrar</span>
            </button>
          </div>
        </nav>
      </aside>

      {/* Main content */}
      <main className="flex-1 p-4 h-full ">
        <Outlet />
      </main>
    </div>
  );
}

function SidebarLink({ to, icon, label, danger = false }) {
  return (
    <Link
      to={to}
      className={`w-full flex items-center gap-4 p-2 rounded transition-colors duration-200 hover:bg-[#1282A2] `}
    >
      <div className="w-8 h-8 flex items-center justify-center">{icon}</div>
      <span className="hidden md:inline whitespace-nowrap">{label}</span>
    </Link>
  );
}
