// src/components/Sidebar.jsx
import { Link, Outlet } from "react-router-dom";
import { Home, GraduationCap, User, LogOut } from "lucide-react";
import logo from "../images/logo2.png";

export default function Layout() {
  return (
    <div className="flex h-screen overflow-hidden">
      {/* Sidebar */}
      <aside
        className="bg-blue-900 text-white w-20 md:w-56 transition-all duration-300 h-[700px] flex flex-col items-center py-5 m-4 rounded-2xl"
      >
        {/* Logo */}
        <div className="bg-white p-2 rounded-full mb-20">
          <img
            src={logo}
            alt="Logo"
            className="w-25 h-25 rounded-full transition-all duration-300"
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
              to="/cursos"
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
            <SidebarLink
              to="/login"
              icon={<LogOut strokeWidth={0.5} className="w-6 h-6" />}
              label="Cerrar"
            />
          </div>
        </nav>
      </aside>

      {/* Main content */}
      <main className="flex-1 p-6  h-full overflow-auto">
        <Outlet />
      </main>
    </div>
  );
}

function SidebarLink({ to, icon, label, danger = false }) {
  return (
    <Link
      to={to}
      className={`w-full flex items-center gap-4 p-2 rounded transition-colors duration-200 hover:bg-[#001F54] `}
    >
      <div className="w-8 h-8 flex items-center justify-center">{icon}</div>
      <span className="hidden md:inline whitespace-nowrap">{label}</span>
    </Link>
  );
}
