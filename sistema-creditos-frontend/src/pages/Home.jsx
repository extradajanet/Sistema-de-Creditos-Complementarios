import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import {LibraryBig, Trophy, BellRing } from "lucide-react";
import Graph from "../components/Graph";

export default function Home() {
  const [userRole, setUserRole] = useState("Alumno"); // por defecto

  useEffect(() => {
    const rol = localStorage.getItem("rol");
    if (rol) setUserRole(rol);
  }, []);

  const cardsByRole = {
    Alumno: [
      {
        text: "Cursos Disponibles",
        icon: <LibraryBig strokeWidth={0.5} className="h-40 w-40 mb-2" />,
        link: "/cursosdisponibles",
      },
      {
        text: "Actividades Extraescolares",
        icon: <Trophy strokeWidth={0.5} className="h-40 w-40 mb-2" />,
        link: "/actividades",
      },
      {
        text: "Avisos",
        icon: <BellRing strokeWidth={0.5} className="h-40 w-40 mb-2" />,
        link: "/avisos",
      },
    ],
    Coordinador: [
      {
        text: "Revisar Actividades",
        icon: <Trophy strokeWidth={0.5} className="h-40 w-40 mb-2" />,
        link: "",
      },
      {
        text: "Gestionar Cursos",
        icon: <LibraryBig strokeWidth={0.5} className="h-40 w-40 mb-2" />,
        link: "",
      },
    ],
    Departamento: [
      {
        text: "Crear Curso",
        icon: <FilePlus2 strokeWidth={0.5} className="h-40 w-40 mb-2"/>,
        link: "/crearactividad",
      },
      {
        text: "Mis Cursos",
        icon: <FolderOpen strokeWidth={0.5} className="h-40 w-40 mb-2" />,
        link: "/miscursos",
      },
      {
        text: "Avisos",
        icon: <BellRing strokeWidth={0.5} className="h-40 w-40 mb-2" />,
        link: "",
      },
    ],
  };

  const tarjetas = cardsByRole[userRole] || cardsByRole["Alumno"];

  return (
<<<<<<< HEAD
    <div className="flex flex-col gap-6  w-full">
      {/* Bienvenida */}
      <div className="flex justify-between items-center bg-gray-200 rounded-xl p-6">
        <h1 className="custom-welcome  ml-4 font-bold text-[#0A1128]">¡Bienvenido, Alumno!</h1>
        {/* Graph Representation */}
        <div className="w-50 h-50 rounded-full  flex items-center justify-center flex-col text-center">
          <Graph obtained={3} total={5} />
        </div>
      </div>

      {/* Tarjetas de navegación */}
      <div className="p-8 grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 w-full ">
        <div className="bg-[#001F54] text-white rounded-xl flex flex-col items-center justify-center p-15 hover:scale-105 transition  w-80">
          <Link to="/cursosdisponibles" className="flex flex-col items-center justify-center">
          <LibraryBig  strokeWidth={0.5} className="h-40 w-40 mb-2" />
          <span className="custom-subheading text-center font-bold text-lg">Cursos Disponibles</span>
          </Link>
        </div>

        <div className="bg-[#001F54] text-white rounded-xl flex flex-col items-center justify-center p-6 hover:scale-105 transition w-80">
          <Trophy strokeWidth={0.5} className="h-40 w-40 mb-2"/>
          <span className="custom-subheading text-center font-bold text-lg">Actividades Extraescolares</span>
        </div>

        <div className="bg-[#001F54] text-white rounded-xl flex flex-col items-center justify-center p-6 hover:scale-105 transition w-80">
          <BellRing strokeWidth={0.5} className="h-40 w-40 mb-2"/>
          <span className="custom-subheading text-center font-bold text-lg">Avisos</span>
        </div>
=======
    <div className="flex flex-col gap-18 w-full">
      {/* Bienvenida y gráfica */}
      <div className="flex justify-between items-center bg-gray-200 rounded-xl p-6">
        <h1 className="custom-heading text-3xl font-bold text-gray-900">
          ¡Bienvenido, {userRole}!
        </h1>
        <div className="w-50 h-50 rounded-full bg-blue-500 text-white flex items-center justify-center flex-col text-center">
          <span className="text-sm">Créditos Obtenidos</span>
          <span className="text-3xl font-bold">3</span>
        </div>
      </div>

      {/* Tarjetas de navegación dinámicas */}
      <div className="grid grid-cols-1 sm:grid-cols-3 gap-40">
        {tarjetas.map((card, index) => (
          <div
            key={index}
            className="bg-blue-950 text-white rounded-xl flex flex-col items-center justify-center p-6 hover:scale-105 transition w-80"
          >
            <Link to={card.link} className="flex flex-col items-center justify-center">
              {card.icon}
              <span className="custom-subheading text-center font-bold text-lg">
                {card.text}
              </span>
            </Link>
          </div>
        ))}
>>>>>>> 5d9d5bdc0a9dc4dcca375b423a6abce31b598e07
      </div>
    </div>
  );
}
