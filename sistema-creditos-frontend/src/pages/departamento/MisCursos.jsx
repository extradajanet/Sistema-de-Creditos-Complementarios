import React, { useEffect, useState } from "react";
import { PencilLine,Users, Search, SlidersHorizontal,Pencil, Calendar, Trash2, Check,X } from "lucide-react";
import predeterminado from "../../images/PredeterminadoCursos.png";
import Modal from "../../components/Modal";
const estados = {
  1: "Activo",
  2: "En Progreso",
  3: "Finalizado",
};

// Datos falsos para simular actividades mientras el backend no está listo
const mockActividades = [
  {
    id: 1,
    nombre: "Curso de React",
    fechaInicio: "07/03/2025",
    creditos: 2,
    imagenNombre: "react.png",
  },
  {
    id: 2,
    nombre: "Introducción a Ciberseguridad",
    fechaInicio: "07/03/2025",
    creditos: 1,
    imagenNombre: "ciber.png",
  },
  {
    id: 3,
    nombre: "Base de Datos con PostgreSQL",
    fechaInicio: "07/03/2025",
    creditos: 3,
    imagenNombre: "postgres.png",
  },
];

export default function ActividadesList() {
  const [actividades, setActividades] = useState([]);
  const [alumnos, setAlumnos] = useState([]);
  const [loading, setLoading] = useState(true);
  const [busqueda, setBusqueda] = useState("");
  const [busquedaAlumno, setBusquedaAlumno] = useState("");
  const [mostrarFiltro, setMostrarFiltro] = useState(false);
  const [tipoSeleccionado, setTipoSeleccionado] = useState("");
  const [showEdit, setShowEdit] = useState(false);
  const [showList, setShowList] = useState(false);

  useEffect(() => {
    // Mientras el backend no está listo, usamos datos falsos
  setActividades(mockActividades);

    // Datos simulados de alumnos
      const alumnosMock = [
        { id: 1, nombre: "Camila Rodríguez López" },
        { id: 2, nombre: "Diego Fernández Morales" },
        { id: 3, nombre: "Valentina Herrera Gómez" },
        { id: 4, nombre: "Santiago Martínez Delgado" },
        { id: 5, nombre: "Pamela Rodríguez López" },
        { id: 6, nombre: "Ricardo Fernández Morales" },
        { id: 7, nombre: "Valeria Herrera Gómez" },
      ];
      setAlumnos(alumnosMock);

      setLoading(false);



    // Cuando quieras usar el backend, reemplaza este código por el fetch original:
    /*
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
    */
  }, []);

  const actividadesFiltradas = actividades.filter(
    (actividad) =>
      actividad.nombre.toLowerCase().includes(busqueda.toLowerCase()) &&
      (tipoSeleccionado === "" || actividad.tipoActividad === tipoSeleccionado)
  );
    const AlumnosBusqueda = alumnos.filter(
    (actividad) =>
      actividad.nombre.toLowerCase().includes(busquedaAlumno.toLowerCase())
  );


return (
    <div className="flex flex-col gap-6 w-full">
      {/* Título */}
      <div className="flex justify-between items-center bg-gray-200 rounded-xl p-6">
        <h1 className="text-3xl font-bold text-gray-900 custom-heading">Mis Cursos</h1>
      </div>

      {/* Buscador y filtro */}
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
            <div className="absolute right-0 mt-2 bg-white border border-blue-950 rounded-xl px-3 py-2 shadow-lg z-10">
              <select
                className="text-sm border-none focus:outline-none"
                value={tipoSeleccionado}
                onChange={(e) => setTipoSeleccionado(e.target.value)}
              >
                <option value="">Todos los tipos</option>
                <option value="Taller">Taller</option>
                <option value="Conferencia">Conferencia</option>
                <option value="Seminario">Seminario</option>
              </select>
            </div>
          )}
        </div>
      </div>

      {/* Lista de actividades */}
      <div className="flex flex-col gap-4">
        {loading ? (
          <p className="text-center mt-10 text-black-600">Cargando actividades...</p>
        ) : actividadesFiltradas.length === 0 ? (
          <p className="text-center mt-10 text-black-600">No hay cursos disponibles</p>
        ) : (
          <div className="flex justify-center">
            <div className="grid grid-cols-1 sm:grid-cols-2 gap-x-25 gap-y-6">
              {actividadesFiltradas.map((actividad) => (
                <div
                  key={actividad.id}
                  className="bg-blue-950 rounded-2xl shadow-md border-6  border-blue-950 h-28 w-[500px] flex items-center px-4"
                >
                  <img
                    src={predeterminado}
                    alt={actividad.nombre}
                    className="rounded-md object-cover h-20 w-20 mr-4"
                  />
                  <div className="flex-1 flex flex-col justify-center items-left  text-white">
                    <h3 className="text-xl font-semibold mb-1">{actividad.nombre}</h3>
                    <p className="text-xs mb-1 text-[#9A9A9A]">
                      <strong>Fecha de inicio: {actividad.fechaInicio}</strong>
                    </p>
                    <p className="text-xs text-[#9A9A9A]">
                      <strong>
                        {actividad.creditos} Crédito{actividad.creditos > 1 ? "s" : ""}
                      </strong>
                    </p>
                  </div>
                  <div className="flex gap-2 mt-15">
                    <button
                      className="bg-blue-950 text-white rounded h-8 w-8 flex items-center justify-center"
                      onClick={() => setShowEdit(true)}
                    >
                      <PencilLine strokeWidth={2} color="white" className="w-6 h-6" />
                    </button>
                    <button
                      className="bg-blue-950 text-white rounded h-8 w-8 flex items-center justify-center"
                      onClick={() => setShowList(true)}
                    >
                      <Users strokeWidth={2} color="white" className="w-6 h-6" />
                    </button>


<Modal show={showEdit} onClose={() => setShowEdit(false)} title="Tutoria de Calculo Diferencial" className="w-[900px]">
  {/* Cuerpo principal: imagen + formulario */}
  <div className="grid grid-cols-[270px_1fr] gap-4">
    
    {/* Imagen */}
    <div className="bg-white rounded-xl flex items-center justify-center p-2">
      <img src={predeterminado} alt="Imagen" className="w-full h-auto object-contain" />
    </div>

    {/* Información editable */}
    <div className="grid grid-cols-2 gap-4 text-sm text-gray-800">
      
      {/* Descripción editable - ocupa dos columnas */}
      <div className="col-span-2 flex items-start gap-2 border border-blue-950 rounded px-2 py-1 bg-gray-200">
        <textarea
          className="flex-1 text-sm p-2 resize-none outline-none bg-transparent text-gray-600"
          rows={2}
          defaultValue="Sesiones para reforzar lo que sigue de la descripción del curso en gris para señalar que se puede editar"
        />
        <Pencil size={16} className="mt-2 text-gray-500 cursor-pointer" />
      </div>

      {/* Días y Horario */}
      <div className="col-span-1">
        <p className="font-semibold">Día(s) y Horario:</p>
        <p>Lunes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;13:00 - 15:00</p>
        <p>Jueves &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;13:00 - 15:00</p>
      </div>

      <div className="col-span-1">
        {/* Capacidad */}
        <div className="grid grid-cols-[90px_auto_auto_auto] items-center gap-2 m-2">
          <label className="font-semibold">Capacidad:</label>
          <input
            type="number"
            defaultValue={20}
            className="w-16 border rounded px-1 py-0.5 text-center"
          />
          <Users size={18} className="text-gray-500" />
          <Pencil size={14} className="text-gray-500 cursor-pointer" />
        </div>

        {/* Créditos */}
        <div className="grid grid-cols-[90px_auto_auto] items-center gap-2 m-2">
          <label className="font-semibold">Créditos:</label>
          <input
            type="number"
            defaultValue={2}
            className="w-12 border rounded px-1 py-0.5 text-center"
          />
          <Pencil size={14} className="text-gray-500 cursor-pointer" />
        </div>
      </div>




      {/* Carreras */}
      <div className="col-span-2">
        <div className="flex items-center gap-1">
          <p className="font-semibold">Carrera(s):</p>
          <Pencil size={14} className="text-gray-500 cursor-pointer" />
        </div>
        <ul className="list-disc ml-4 text-gray-700">
          <li>Ingeniería en Sistemas Computacionales</li>
          <li>Ingeniería en Tecnologías de la Información y Comunicaciones</li>
        </ul>
      </div>

      {/* Fecha de Inicio */}
      <div className="flex items-center gap-2">
        <label className="font-semibold">Fecha de Inicio:</label>
        <input type="date" defaultValue="2025-07-06" className="border rounded px-2 py-1" />
        <Calendar size={16} className="text-gray-500" />
      </div>

      {/* Fecha de Fin */}
      <div className="flex items-center gap-2">
        <label className="font-semibold">Fecha de Fin:</label>
        <input type="date" defaultValue="2025-08-06" className="border rounded px-2 py-1" />
        <Calendar size={16} className="text-gray-500" />
      </div>
    </div>
  </div>

  {/* Botones */}
  <div className="flex justify-end gap-4 mt-6">
    <button onClick={() => setShowEdit(false)} className="bg-white border border-blue-950 text-black px-4 py-2 rounded hover:bg-gray-200">
      Descartar Cambios
    </button>
    <button className="bg-white text-black border border-blue-950 px-4 py-2 rounded hover:bg-gray-200">
      Guardar Cambios
    </button>
  </div>
</Modal>



<Modal
  show={showList}
  onClose={() => setShowList(false)}
  title=""
  className="w-[500px]"
>
  {/* Título centrado con ícono */}
  <div className="text-center mb-4 flex items-center justify-center gap-2 text-blue-950">
    <h2 className="text-2xl font-bold">Alumnos Inscritos</h2>
    <Users />
  </div>

  {/* Contenedor gris claro */}
  <div className="bg-gray-200 rounded-xl p-4">
    {/* Buscador */}
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

    {/* Lista de alumnos con scroll */}
<div className="bg-white rounded-lg max-h-60 overflow-y-auto">
  {AlumnosBusqueda.map((alumno) => (
    <div
      key={alumno.id}
      className="flex items-center justify-between px-4 py-2 border-b last:border-b-0"
    >
      <span className="text-sm font-semibold">{alumno.nombre}</span>
      <div className="flex items-center gap-2">
        <Check className="bg-gray-200 text-blue-950 rounded p-1 w-6 h-6" />
        <X className="bg-gray-200 text-blue-950 rounded p-1 w-6 h-6" />
        <Trash2 className="bg-gray-200 text-blue-950 rounded p-1 w-6 h-6" />
      </div>
    </div>
  ))}
</div>

  </div>

  {/* Botones inferior */}
  <div className="flex justify-center gap-4 mt-6">
    <button className="border border-black px-4 py-2 rounded hover:bg-gray-100">
      Seleccionar todos
    </button>
    <button className="border border-black px-4 py-2 rounded hover:bg-gray-100">
      Deseleccionar todos
    </button>
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

