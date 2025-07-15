import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import {LibraryBig,FilePlus2,FolderOpen, Trophy, BellRing } from "lucide-react";
import Graph from "../components/Graph";

export default function Home() {
  const [userRole, setUserRole] = useState("Alumno"); // por defecto
  const [infoAlumno, setinfoAlumno] = useState([]);
  const userId = localStorage.getItem("alumnoId");

  useEffect(() => {
    
    const rol = localStorage.getItem("rol");
    if (rol) setUserRole(rol);
  }, []);

  useEffect(() => {
  
      let isMounted = true;
      fetch(`https://localhost:7238/api/Alumno/${userId}`, { headers: { Accept: "application/json" } })
        .then((res) => {
          if (!res.ok) throw new Error("Error: " + res.status);
          return res.json();
        })
        .then((data) => {
          if (isMounted) {
            setinfoAlumno(data);
            console.log("Datos recibidos:", data);
            localStorage.setItem("alumnoInfo", JSON.stringify(data)); 
            setLoading(false);
          }
        })
        .catch((err) => {
          if (isMounted) {
            console.error("Fetch error:", err);
            setLoading(false);
          }
        });
  
      return () => {
        isMounted = false;
      };
      
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
    <div className="flex flex-col gap-6  w-full">
      {/* Bienvenida */}
      <div className="flex justify-between items-center bg-gray-200 rounded-xl p-6">
        <h1 className="custom-welcome  ml-4 font-bold text-[#0A1128]">¡Bienvenido {infoAlumno.nombre +" " + infoAlumno.apellido || ""}!</h1>
        {/* Graph Representation */}
        <div className="w-50 h-50 rounded-full  flex items-center justify-center flex-col text-center">
          <Graph obtained={infoAlumno.totalCreditos} total={5} />
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
      </div>
    </div>
  );
}
