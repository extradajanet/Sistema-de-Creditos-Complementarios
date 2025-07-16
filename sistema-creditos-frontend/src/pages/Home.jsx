import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import {
  LibraryBig,
  FilePlus2,
  FolderOpen,
  Trophy,
  BellRing,
} from "lucide-react";
import Graph from "../components/Graph";

export default function Home() {
  const [loading, setLoading] = useState(true)
  const [userRole, setUserRole] = useState(null); // por defecto

  const [infoAlumno, setinfoAlumno] = useState([]);
  const [infoDepartamento, setInfoDepartamento] = useState([]);
  const [infoCoordinador, setInfoCoordinador] = useState([]);

  const ids = {
    Alumno: localStorage.getItem("alumnoId"),
    Departamento: localStorage.getItem("departamentoId"),
    Coordinador: localStorage.getItem("coordinadorId"),
  };

  useEffect(() => {

    const rol = localStorage.getItem("rol");
    if (rol) setUserRole(rol);
  }, []);

  useEffect(() => {
    if (!userRole) return;

    let isMounted = true;

    const id = ids[userRole];
    let url = `https://localhost:7238/api/${userRole}/${id}`;

    fetch(url, { headers: { Accept: "application/json" } })
      .then((res) => {
        if (!res.ok) throw new Error("Error: " + res.status);
        return res.json();
      })
      .then((data) => {
        if (!isMounted) return;

        if (userRole === 'Alumno') {
          setinfoAlumno(data);
          localStorage.setItem("alumnoInfo", JSON.stringify(data));
        } else if (userRole === 'Departamento') {
          setInfoDepartamento(data)
          localStorage.setItem("departamentoInfo", JSON.stringify(data));
        } else if (userRole === 'Coordinador') {
          setInfoCoordinador(data)
          localStorage.setItem("coordinadorInfo", JSON.stringify(data));
        }

        setLoading(false);
        console.log("Datos recibidos", data)

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

  }, [userRole]);

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
        link: "/actividadesextraescolares",
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
        link: "/revisaractividades",
      },
      {
        text: "Gestionar Cursos",
        icon: <LibraryBig strokeWidth={0.5} className="h-40 w-40 mb-2" />,
        link: "/gestionarcursos",
      },
    ],
    Departamento: [
      {
        text: "Crear Curso",
        icon: <FilePlus2 strokeWidth={0.5} className="h-40 w-40 mb-2" />,
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
        link: "/avisos",
      },
    ],
  };

  if (loading) {
    return (
      <div className="fixed inset-0 flex items-center justify-center">
        <div className="w-12 h-12 border-4 border-[#001F54] border-t-transparent rounded-full animate-spin" />
      </div>
    );
  }

  const tarjetas = cardsByRole[userRole] || cardsByRole["Alumno"];

  return (
    <div className="flex flex-col gap-6 w-full">
      {/* Bienvenida */}
      <div className="flex justify-between items-center bg-gray-200 rounded-xl p-6">
        {userRole === 'Alumno' && (
          <>
            <h1 className="custom-welcome  ml-4 font-bold text-[#0A1128]">¡Bienvenido {infoAlumno.nombre + " " + infoAlumno.apellido || ""}!</h1>
            {/* Graph Representation */}
            <div className="w-50 h-50 rounded-full  flex items-center justify-center flex-col text-center">
              <Graph obtained={infoAlumno.totalCreditos} total={5} />
            </div>
          </>
        )}

        {userRole === 'Departamento' && (
          <>
            <h1 className="custom-welcome  ml-4 font-bold text-[#0A1128]">¡Bienvenido, departamento de {infoDepartamento.nombre || ""}!</h1>
          </>
        )}

        {userRole === 'Coordinador' && (
          <>
            <h1 className="custom-welcome  ml-4 font-bold text-[#0A1128]">¡Bienvenido, {infoCoordinador.nombre || ""}!</h1>
          </>
        )}
      </div>

      {/* Tarjetas dinámicas por rol */}
      <div className="p-8 grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 w-full">
        {tarjetas.map((card, index) => (
          <div
            key={index}
            className="bg-[#001F54] text-white rounded-xl flex flex-col items-center justify-center p-6 hover:scale-105 transition w-80"
          >
            <Link
              to={card.link}
              className="flex flex-col items-center justify-center"
            >
              {card.icon}
              <span className="custom-subheading text-center font-bold text-lg">
                {card.text}
              </span>
            </Link>
          </div>
        ))}
      </div>
    </div>
  );
}
