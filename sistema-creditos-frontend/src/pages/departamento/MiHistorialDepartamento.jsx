import React, { act, useEffect, useState } from "react";
import { 
  PencilLine, Users, Search, SlidersHorizontal, Pencil, Calendar, Trash2, Check, X, ChevronDown 
} from "lucide-react";
import predeterminado from "../../images/PredeterminadoCursos.png";
import Modal from "../../components/Modal";

import { Listbox } from "@headlessui/react";

const estados = {
  1: "Activo",
  2: "En Progreso",
  3: "Finalizado",
};

const imagenes = import.meta.glob('../../images/*.{png,jpg,jpeg,gif}', { eager: true });

const obtenerImagen = (nombre) => {
  const entrada = Object.entries(imagenes).find(([ruta]) =>
    ruta.includes(nombre)
  );
  return entrada ? entrada[1].default : predeterminado; // usa imagen predeterminada si no se encuentra
};

const obtenerEstadoActividad = (actividad) => {
  const hoy = new Date();
  hoy.setHours(0, 0, 0, 0); // Normaliza a medianoche

  const inicio = new Date(actividad.fechaInicio);
  inicio.setHours(0, 0, 0, 0);

  const fin = new Date(actividad.fechaFin);
  fin.setHours(0, 0, 0, 0);

  if (hoy < inicio) return "Activo";
  if (hoy >= inicio && hoy <= fin) return "En Progreso";
  return "Finalizado";
};




export default function ActividadesList() {
  const [carreraIdsEdit, setCarreraIdsEdit] = useState([]);
  const [actividades, setActividades] = useState([]);
  const [alumnos, setAlumnos] = useState([]);
  const [loading, setLoading] = useState(true);
  const [busqueda, setBusqueda] = useState("");
  const [busquedaAlumno, setBusquedaAlumno] = useState("");
  const [mostrarFiltro, setMostrarFiltro] = useState(false);
  const [tipoSeleccionado, setTipoSeleccionado] = useState("");
  const [showEdit, setShowEdit] = useState(false);
  const [showList, setShowList] = useState(false);
  const [actividadSeleccionada, setActividadSeleccionada] = useState(null); //se usa
  const [descripcionEdit, setDescripcionEdit] = useState("");
  const [creditosEdit, setCreditosEdit] = useState(0);
  const [capacidadEdit, setCapacidadEdit] = useState(0);
  const [fechaInicioEdit, setFechaInicioEdit] = useState("");
  const [fechaFinEdit, setFechaFinEdit] = useState("");
  //const actividadId = localStorage.getItem("actividadId");


  const todasLasCarreras = [
    { id: 1, nombre: "Ingeniería en Sistemas Computacionales" },
    { id: 2, nombre: "Ingeniería en Tecnologías de la Información" },
    { id: 3, nombre: "Ingeniería en Administración" },
    { id: 4, nombre: "Licenciatura en Administración" },
    { id: 5, nombre: "Arquitectura" },
    { id: 6, nombre: "Licenciatura en Biología" },
    { id: 7, nombre: "Licenciatura en Turismo" },
    { id: 8, nombre: "Ingeniería Civil" },
    { id: 9, nombre: "Contador Público" },
    { id: 10, nombre: "Ingeniería Eléctrica" },
    { id: 11, nombre: "Ingeniería Electromecánica" },
    { id: 12, nombre: "Ingeniería en Gestión Empresarial" },
    { id: 13, nombre: "Ingeniería en Desarrollo de Aplicaciones" },
    { id: 14, nombre: "Todas las carreras" },
  ];

  const diasSemana = {
  1: "Lunes",
  2: "Martes",
  3: "Miércoles",
  4: "Jueves",
  5: "Viernes",
  6: "Sábado",
  7: "Domingo",
};


  const normalizar = (texto) => texto.trim().toLowerCase();

  const obtenerCarreraIdsDesdeNombres = (nombres) => {
    const nombresNormalizados = nombres.map(normalizar);
    return todasLasCarreras
      .filter((carrera) => nombresNormalizados.includes(normalizar(carrera.nombre)))
      .map((carrera) => carrera.id);
  };

  const cargarAlumnosInscritos = (idActividad) => {
  fetch(`https://localhost:7238/api/AlumnoActividad/alumnos-inscritos/${idActividad}`, {
    headers: {
      Accept: "application/json",
    },
  })
    .then((res) => {
      if (!res.ok) throw new Error("Error al obtener alumnos");
      return res.json();
    })
    .then((data) => {
      setAlumnos(data);
    })
    .catch((error) => {
      console.error("Error al cargar alumnos inscritos:", error);
      alert("No se pudieron cargar los alumnos inscritos.");
    });
};

  useEffect(() => {
    let isMounted = true;
    fetch("/api/Actividades", { headers: { Accept: "application/json" } })
      .then((res) => {
        if (!res.ok) throw new Error("Error: " + res.status);
        return res.json();
      })
      .then((data) => {
        if (isMounted) {
          setActividades(data);
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

  const tiposActividad = {
    1: "Deportiva",
    2: "Cultural",
    3: "Tutorias",
    4: "MOOC",
  };

  const obtenerTipoIdPorNombre = (nombre) => {
    const entrada = Object.entries(tiposActividad).find(([, n]) => n === nombre);
    return entrada ? Number(entrada[0]) : null;
  };

  const tipos = ["", ...Object.values(tiposActividad)];

  const actividadesFiltradas = actividades.filter(
    (actividad) =>
      actividad.nombre.toLowerCase().includes(busqueda.toLowerCase()) &&
      (tipoSeleccionado === "" || actividad.tipoActividad === obtenerTipoIdPorNombre(tipoSeleccionado))
  );

const AlumnosBusqueda = alumnos.filter((alumno) =>
  alumno.nombreCompleto?.toLowerCase().includes(busquedaAlumno.toLowerCase())
);


  return (
    <div className="flex flex-col gap-6 w-full h-screen">
      <div className="flex justify-between items-center bg-gray-200 rounded-xl p-6">
        <h1 className="text-3xl font-bold text-gray-900 custom-heading">Historial de Actividades</h1>
      </div>

      <div className="flex justify-between items-center mb-2">
        <div className="relative w-full max-w-md">
          <input
            type="text"
            placeholder="Buscar cursos"
            className="w-full border border-blue-950 rounded-3xl px-4 py-2 text-base focus:outline-none focus:ring-2 focus:ring-blue-600"
            value={busqueda}
            onChange={(e) => setBusqueda(e.target.value)}
          />
          <div className="absolute right-2 top-1/2 transform -translate-y-1/2 bg-blue-950 p-1.5 rounded-full">
            <Search className="h-4 w-4 text-white" />
          </div>
        </div>

        <div className="relative">
          <button
            className="ml-4 bg-white border-2 border-blue-950 rounded-2xl px-2 py-2 text-base font-semibold hover:bg-blue-800 transition"
            onClick={() => setMostrarFiltro(!mostrarFiltro)}
          >
            <SlidersHorizontal strokeWidth={1} />
          </button>

          {mostrarFiltro && (
            <div className="absolute right-0 mt-2 bg-white border border-blue-950 rounded-xl shadow-lg z-10 p-2 w-48">
              {/* Aquí reemplazamos el select por Listbox */}
              <Listbox value={tipoSeleccionado} onChange={setTipoSeleccionado}>
                <div className="relative">
                    <Listbox.Button className="w-full flex justify-between items-center bg-white py-2 px-3 text-sm text-gray-800 focus:outline-none">
                    <span>{tipoSeleccionado || "Todos los tipos"}</span>
                    <ChevronDown className="w-5 h-5 text-gray-500" />
                  </Listbox.Button>
                  <Listbox.Options className="absolute mt-1 w-full rounded-lg bg-white border border-blue-950 shadow-lg z-10 max-h-48 overflow-auto">
                    {tipos.map((tipo, index) => (
                      <Listbox.Option
                        key={index}
                        value={tipo}
                        className={({ active }) =>
                          `cursor-pointer select-none px-4 py-2 text-sm ${
                            active ? "bg-blue-100 text-blue-900" : "text-gray-700"
                          }`
                        }
                      >
                        {({ selected }) => (
                          <div className="flex justify-between items-center">
                            <span>{tipo || "Todos los tipos"}</span>
                            {selected && <Check className="w-4 h-4 text-blue-950" />}
                          </div>
                        )}
                      </Listbox.Option>
                    ))}
                  </Listbox.Options>
                </div>
              </Listbox>
            </div>
          )}
        </div>
      </div>

     {/* Lista de actividades */}
      <div className="flex-1 overflow-y-auto pr-2 mb-8">
        {loading ? (
          <p className="text-center mt-10 text-black-600">Cargando actividades...</p>
        ) : actividadesFiltradas.length === 0 ? (
          <p className="text-center mt-10 text-black-600">No hay cursos disponibles</p>
        ) : (
          <div className="flex justify-center overflow-y-auto max-h-[520px]">
            <div className="grid grid-cols-1 sm:grid-cols-2 gap-x-25 gap-y-4.5">
              {actividadesFiltradas.map((actividad) => (
                <div
                  key={actividad.id}
                  className="bg-blue-950 rounded-2xl shadow-md border-[6px] border-blue-950 w-[500px] flex p-4"
                >
                  {/* Imagen */}
                  <div className="flex items-center justify-center">
                  <img
                    src={obtenerImagen(actividad.imagenNombre)}
                    alt={actividad.nombre}
                    className="rounded-md object-cover h-20 w-20 mr-4"
                  />
                  </div>


                  {/* Contenido principal */}
                  <div className="flex-1 flex flex-col justify-center text-white">
                    <h3 className="text-xl font-semibold mb-1 break-words">{actividad.nombre}</h3>
                    <p className="text-xs mb-1 text-[#9A9A9A]">
                      <strong>Fecha de inicio: </strong>
                      {new Date(actividad.fechaInicio).toLocaleDateString()}
                    </p>
                    <p className="text-xs mb-1 text-[#9A9A9A]">
                      <strong>
                        {actividad.creditos} Crédito{actividad.creditos > 1 ? "s" : ""}
                      </strong>
                    </p>
                    <p className="text-xs text-[#9A9A9A]">
                      <strong>Estado: </strong>{obtenerEstadoActividad(actividad)}
                    </p>
                  </div>
                  {/* Botones de acción */}
                  <div className="flex items-center gap-2 ml-4">
                    <button
                    className="bg-blue-950 text-white rounded h-8 w-8 flex items-center justify-center"
                    onClick={() => {
                        setActividadSeleccionada(actividad);
                        cargarAlumnosInscritos(actividad.id); // 👈 Cargar alumnos desde API
                        setShowList(true);
                    }}
                    >
                    <Users strokeWidth={2} color="white" className="w-6 h-6" />
                    </button>
                    
                    <Modal
                      show={showList}
                      onClose={() => setShowList(false)}
                      className="w-[500px] bg-gray-200"
                    >
                      <div className="text-center mb-4 flex items-center justify-center gap-2 text-blue-950">
                        <h2 className="text-2xl font-bold">Alumnos Inscritos</h2>
                        <Users />
                      </div>
                      <div className="bg-gray-200 rounded-xl p-4">
                        <div className="relative w-full max-w-md mb-1">
                          <input
                            type="text"
                            placeholder="Buscar alumnos"
                            className="w-full border border-blue-950 rounded-3xl px-4 py-2 text-base focus:outline-none focus:ring-2 focus:ring-blue-600"
                            value={busquedaAlumno}
                            onChange={(e) => setBusquedaAlumno(e.target.value)}
                          />
                          <div className="absolute right-2 top-1/2 transform -translate-y-1/2 bg-blue-950 p-1.5 rounded-full">
                            <Search className="h-4 w-4 text-white" />
                          </div>
                        </div>
                        <div className="bg-white rounded-lg max-h-60 overflow-y-auto">
                        {[...AlumnosBusqueda]
                        .sort((a, b) => a.nombreCompleto.localeCompare(b.nombreCompleto))
                        .map((alumno) => (
                        <div
                          key={alumno.alumnoId}
                          className="flex items-center justify-between px-4 py-2 border-b last:border-b-0"
                        >
                          <span className="text-sm font-semibold">{alumno.nombreCompleto}</span>
                          <div className="flex items-center gap-2">
                            {alumno.estadoAlumnoActividad === 4 ? (
                              <Check className="w-5 h-5 text-green-600" />
                            ) : (
                              <X className="w-5 h-5 text-red-600" />
                            )}
                          </div>
                        </div>
                      ))}
                    </div>
                      </div>
                        <div className="flex justify-center gap-4 mt-6">
                        </div>
                    </Modal>
                  </div>
                </div>
              ))}
            </div>
          </div>
        )}
      </div>
    </div>
  );
}