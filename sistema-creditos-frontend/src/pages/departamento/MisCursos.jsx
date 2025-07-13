import React, { useEffect, useState } from "react";
import { PencilLine, Users, Search, SlidersHorizontal, Pencil, Calendar, Trash2, Check, X } from "lucide-react";
import predeterminado from "../../images/PredeterminadoCursos.png";
import Modal from "../../components/Modal";

const estados = {
  1: "Activo",
  2: "En Progreso",
  3: "Finalizado",
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
  const [actividadSeleccionada, setActividadSeleccionada] = useState(null);
  const [descripcionEdit, setDescripcionEdit] = useState("");
  const [creditosEdit, setCreditosEdit] = useState(0);
  const [capacidadEdit, setCapacidadEdit] = useState(0);
  const [fechaInicioEdit, setFechaInicioEdit] = useState("");
  const [fechaFinEdit, setFechaFinEdit] = useState("");

  const todasLasCarreras = [
  { id: 1, nombre: "Ingeniería en Sistemas Computacionales" },
  { id: 2, nombre: "Ingeniería en Tecnologías de la Información y Comunicaciones" },
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

const normalizar = (texto) => texto.trim().toLowerCase();

const obtenerCarreraIdsDesdeNombres = (nombres) => {
  const nombresNormalizados = nombres.map(normalizar);
  return todasLasCarreras
    .filter((carrera) => nombresNormalizados.includes(normalizar(carrera.nombre)))
    .map((carrera) => carrera.id);
};



  

const handleUpdateActividad = () => {
  if (!actividadSeleccionada) return;

  let carrerasFinales = [...carreraIdsEdit];
  if (carrerasFinales.includes(14)) {
    carrerasFinales = todasLasCarreras.filter(c => c.id !== 14).map(c => c.id);
  }

  const actividadActualizada = {
    nombre: actividadSeleccionada.nombre,
    descripcion: descripcionEdit,
    fechaInicio: `${fechaInicioEdit}T00:00:00Z`,
    fechaFin: `${fechaFinEdit}T00:00:00Z`,
    creditos: creditosEdit,
    capacidad: capacidadEdit,
    dias: actividadSeleccionada.dias || 1,
    horaInicio: actividadSeleccionada.horaInicio || "00:00:00",
    horaFin: actividadSeleccionada.horaFin || "00:00:00",
    tipoActividad: actividadSeleccionada.tipoActividad || 1,
    estadoActividad: actividadSeleccionada.estadoActividad || 1,
    imagenNombre: actividadSeleccionada.imagenNombre || "PredeterminadoCursos.png",
    departamentoId: actividadSeleccionada.departamentoId || 1,
    carreraIds: carrerasFinales,
  };

  fetch(`https://localhost:7238/api/Actividades/${actividadSeleccionada.id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(actividadActualizada),
  })
    .then((res) => {
      if (!res.ok) throw new Error("Error al actualizar");
      return res.json();
    })
.then(() => {
  const nuevasCarreraNombres = todasLasCarreras
    .filter(c => actividadActualizada.carreraIds.includes(c.id))
    .map(c => c.nombre);

  setActividades((prev) =>
    prev.map((a) =>
      a.id === actividadSeleccionada.id
        ? {
            ...a,
            ...actividadActualizada,
            carreraNombres: nuevasCarreraNombres, // ✅ actualiza también los nombres
          }
        : a
    )
  );

  setShowEdit(false);
})
.catch((err) => {
  console.error("Error actualizando:", err);
  alert("Error al guardar los cambios");
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

  const actividadesFiltradas = actividades.filter(
    (actividad) =>
      actividad.nombre.toLowerCase().includes(busqueda.toLowerCase()) &&
      (tipoSeleccionado === "" || actividad.tipoActividad === tipoSeleccionado)
  );

  const AlumnosBusqueda = alumnos.filter((actividad) =>
    actividad.nombre.toLowerCase().includes(busquedaAlumno.toLowerCase())
  );

  return (
    <div className="flex flex-col gap-6 w-full">
      <div className="flex justify-between items-center bg-gray-200 rounded-xl p-6">
        <h1 className="text-3xl font-bold text-gray-900 custom-heading">Mis Cursos</h1>
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
                  className="bg-blue-950 rounded-2xl shadow-md border-6 border-blue-950 h-28 w-[500px] flex items-center px-4"
                >
                  <img
                    src={predeterminado}
                    alt={actividad.nombre}
                    className="rounded-md object-cover h-20 w-20 mr-4"
                  />
                  <div className="flex-1 flex flex-col justify-center items-left text-white">
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
onClick={() => {
    console.log("Actividad seleccionada:", actividad); // <-- Asegúrate de que actividad.carreraIds exista y tenga valores

  setActividadSeleccionada(actividad);
  setDescripcionEdit(actividad.descripcion || "");
  setCreditosEdit(actividad.creditos);
  setCapacidadEdit(actividad.capacidad);
  setFechaInicioEdit(actividad.fechaInicio?.slice(0, 10));
  setFechaFinEdit(actividad.fechaFin?.slice(0, 10) || "");
const nombres = Array.isArray(actividad.carreraNombres) ? actividad.carreraNombres : [];
setCarreraIdsEdit(obtenerCarreraIdsDesdeNombres(nombres));
  setShowEdit(true);
}}

                    >
                      <PencilLine strokeWidth={2} color="white" className="w-6 h-6" />
                    </button>

                    <button
                      className="bg-blue-950 text-white rounded h-8 w-8 flex items-center justify-center"
                      onClick={() => setShowList(true)}
                    >
                      <Users strokeWidth={2} color="white" className="w-6 h-6" />
                    </button>

                    <Modal
                      show={showEdit}
                      onClose={() => setShowEdit(false)}
                      title={actividadSeleccionada?.nombre || "Editar Actividad"}
                      className="w-[900px]"
                    >
                      <div className="grid grid-cols-[270px_1fr] gap-4">
                        <div className="bg-white rounded-xl flex items-center justify-center p-2">
                          <img src={predeterminado} alt="Imagen" className="w-full h-auto object-contain" />
                        </div>
                        <div className="grid grid-cols-2 gap-4 text-sm text-gray-800">
                          <div className="col-span-2 flex items-start gap-2 border border-blue-950 rounded px-2 py-1 bg-gray-200">
                            <textarea
                              className="flex-1 text-sm p-2 resize-none outline-none bg-transparent text-gray-600"
                              rows={2}
                              value={descripcionEdit}
                              onChange={(e) => setDescripcionEdit(e.target.value)}
                            />
                            <Pencil size={16} className="mt-2 text-gray-500 cursor-pointer" />
                          </div>
                          <div className="col-span-1">
                            <p className="font-semibold">Día(s) y Horario:</p>
                            <p>Lunes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;13:00 - 15:00</p>
                            <p>Jueves &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;13:00 - 15:00</p>
                          </div>
                          <div className="col-span-1">
                            <div className="grid grid-cols-[90px_auto_auto_auto] items-center gap-2 m-2">
                              <label className="font-semibold">Capacidad:</label>
                              <input
                                type="number"
                                value={capacidadEdit}
                                onChange={(e) => setCapacidadEdit(Number(e.target.value))}
                                className="w-16 border rounded px-1 py-0.5 text-center"
                              />
                              <Users size={18} className="text-gray-500" />
                              <Pencil size={14} className="text-gray-500 cursor-pointer" />
                            </div>
                            <div className="grid grid-cols-[90px_auto_auto] items-center gap-2 m-2">
                              <label className="font-semibold">Créditos:</label>
                              <input
                                type="number"
                                value={creditosEdit}
                                onChange={(e) => setCreditosEdit(Number(e.target.value))}
                                className="w-12 border rounded px-1 py-0.5 text-center"
                              />
                              <Pencil size={14} className="text-gray-500 cursor-pointer" />
                            </div>
                          </div>
<div className="col-span-2">
  <label className="font-semibold">Carrera(s):</label>
  <div className="relative mt-1 w-full">

    <p className="text-sm font-medium mb-1">
      Carreras seleccionadas: {
        carreraIdsEdit.length > 0
          ? carreraIdsEdit
              .map(id => todasLasCarreras.find(c => c.id === id)?.nombre)
              .filter(Boolean)
              .join(", ")
          : "Ninguna"
      }
    </p>

<select
  multiple
  className="w-full border rounded px-3 py-2 bg-white text-sm text-gray-700"
  value={carreraIdsEdit.map(String)} // <-- AQUÍ
  onChange={(e) => {
    const seleccionadas = Array.from(e.target.selectedOptions).map((opt) => Number(opt.value));
    setCarreraIdsEdit(seleccionadas);
  }}
>
  {todasLasCarreras.map((carrera) => (
    <option key={carrera.id} value={String(carrera.id)}>
      {carrera.nombre}
    </option>
  ))}
</select>


  </div>
</div>


                          <div className="flex items-center gap-2">
                            <label className="font-semibold">Fecha de Inicio:</label>
                            <input
                              type="date"
                              value={fechaInicioEdit}
                              onChange={(e) => setFechaInicioEdit(e.target.value)}
                              className="border rounded px-2 py-1"
                            />
                          </div>
                          <div className="flex items-center gap-2">
                            <label className="font-semibold">Fecha de Fin:</label>
                            <input
                              type="date"
                              value={fechaFinEdit}
                              onChange={(e) => setFechaFinEdit(e.target.value)}
                              className="border rounded px-2 py-1"
                            />
                          </div>
                        </div>
                      </div>
                      <div className="flex justify-end gap-4 mt-6">
                        <button
                          onClick={() => setShowEdit(false)}
                          className="bg-white border border-blue-950 text-black px-4 py-2 rounded hover:bg-gray-200"
                        >
                          Descartar Cambios
                        </button>
                        <button
                          onClick={handleUpdateActividad}
                          className="bg-white text-black border border-blue-950 px-4 py-2 rounded hover:bg-gray-200"
                        >
                          Guardar Cambios
                        </button>
                      </div>
                    </Modal>

                    <Modal
                      show={showList}
                      onClose={() => setShowList(false)}
                      title="Alumnos Inscritos"
                      className="w-[500px]"
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