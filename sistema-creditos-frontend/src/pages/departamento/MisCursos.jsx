import React, { useEffect, useState } from "react";
import { 
  PencilLine, Users, Search, SlidersHorizontal, Pencil, Trash2, Check, X, ChevronDown 
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
  return entrada ? entrada[1].default : predeterminado;
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

  // Cargar alumnos inscritos para una actividad
  const cargarAlumnosInscritos = (idActividad) => {
    fetch(`https://localhost:7238/api/AlumnoActividad/alumnos-inscritos/${idActividad}`, {
      headers: { Accept: "application/json" },
    })
      .then((res) => {
        if (!res.ok) throw new Error("Error al obtener alumnos");
        return res.json();
      })
      .then((data) => setAlumnos(data))
      .catch((error) => {
        console.error("Error al cargar alumnos inscritos:", error);
        alert("No se pudieron cargar los alumnos inscritos.");
      });
  };

  // Acreditar alumno
  const acreditarAlumno = (alumnoId, actividadId) => {
    fetch(`https://localhost:7238/api/AlumnoActividad/${alumnoId}/${actividadId}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        alumnoId,
        actividadId,
        estadoAlumnoActividad: 4,
        fechaInscripcion: new Date().toISOString(),
      }),
    })
      .then((res) => {
        if (!res.ok) throw new Error("Error al acreditar alumno");
        cargarAlumnosInscritos(actividadId);
      })
      .catch(console.error);
  };

  // No acreditar alumno
  const noAcreditarAlumno = (alumnoId, actividadId) => {
    fetch(`https://localhost:7238/api/AlumnoActividad/${alumnoId}/${actividadId}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        alumnoId,
        actividadId,
        estadoAlumnoActividad: 5,
        fechaInscripcion: new Date().toISOString(),
      }),
    })
      .then((res) => {
        if (!res.ok) throw new Error("Error al no acreditar alumno");
        cargarAlumnosInscritos(actividadId);
      })
      .catch(console.error);
  };

  // Eliminar alumno
  const eliminarAlumno = (alumnoId, actividadId) => {
    fetch(`https://localhost:7238/api/AlumnoActividad/${alumnoId}/${actividadId}`, {
      method: "DELETE",
    })
      .then((res) => {
        if (!res.ok) throw new Error("Error al eliminar alumno");
        alert("Alumno eliminado de la actividad");
        cargarAlumnosInscritos(actividadId);
      })
      .catch((err) => {
        console.error(err);
        alert("Error al eliminar alumno");
      });
  };

  // Acreditar todos los alumnos
  const acreditarTodos = () => {
    if (!actividadSeleccionada) return;
    const solicitudes = alumnos.map((alumno) =>
      fetch(`https://localhost:7238/api/AlumnoActividad/${alumno.alumnoId}/${actividadSeleccionada.id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          alumnoId: alumno.alumnoId,
          actividadId: actividadSeleccionada.id,
          estadoAlumnoActividad: 4,
          fechaInscripcion: new Date().toISOString(),
        }),
      })
    );

    Promise.all(solicitudes)
      .then((respuestas) => {
        const algunaFalló = respuestas.some((res) => !res.ok);
        if (algunaFalló) throw new Error("Al menos una solicitud falló.");
        alert("Todos los alumnos han sido acreditados");
        cargarAlumnosInscritos(actividadSeleccionada.id);
      })
      .catch((err) => {
        console.error("Error al acreditar todos:", err);
        alert("Ocurrió un error al acreditar a los alumnos");
      });
  };

  // Actualizar actividad
  const handleUpdateActividad = () => {
    if (!actividadSeleccionada) return;

    let carrerasFinales = [...carreraIdsEdit];
    if (carrerasFinales.includes(14)) {
      carrerasFinales = todasLasCarreras.filter(c => c.id !== 14).map(c => c.id);
    }

    const actividadActualizada = {
      nombre: actividadSeleccionada.nombre,
      descripcion: descripcionEdit,
      fechaInicio: fechaInicioEdit,
      fechaFin: fechaFinEdit,
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
      headers: { "Content-Type": "application/json" },
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
              ? { ...a, ...actividadActualizada, carreraNombres: nuevasCarreraNombres }
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

  // Cargar actividades al montar el componente
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

    return () => { isMounted = false; };
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
            <div className="absolute right-0 mt-2 bg-white border border-blue-950 rounded-xl shadow-lg z-10 p-2 w-48">
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
      <div className="flex flex-col gap-4">
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
                  className="bg-blue-950 rounded-2xl shadow-md border-6 border-blue-950 h-28 w-[500px] flex items-center px-4"
                >
                  <img
                    src={obtenerImagen(actividad.imagenNombre)}
                    alt={actividad.nombre}
                    className="rounded-md object-cover h-20 w-20 mr-4"
                  />
                  <div className="flex-1 flex flex-col justify-center items-left text-white">
                    <h3 className="text-xl font-semibold mb-1">{actividad.nombre}</h3>
                    <p className="text-xs mb-1 text-[#9A9A9A]">
                      <strong>Fecha de inicio: </strong>
                      {new Date(actividad.fechaInicio).toLocaleDateString()}
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
                      onClick={() => {
                        setActividadSeleccionada(actividad);
                        cargarAlumnosInscritos(actividad.id);
                        setShowList(true);
                      }}
                    >
                      <Users strokeWidth={2} color="white" className="w-6 h-6" />
                    </button>

                    {/* Modal Edición Actividad */}
                    <Modal
                      show={showEdit}
                      onClose={() => setShowEdit(false)}
                      title={actividadSeleccionada?.nombre || "Editar Actividad"}
                      className="w-[900px] bg-gray-200"
                    >
                      <div className="grid grid-cols-[270px_1fr] gap-4">
                        <div className="bg-white rounded-xl flex items-center justify-center p-2">
                          <img src={obtenerImagen(actividadSeleccionada?.imagenNombre)} alt={actividadSeleccionada?.nombre} className="w-full h-auto object-contain" />
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

                          <div className="flex flex-col gap-1 col-span-1">
                            <label className="font-semibold">Créditos:</label>
                            <div className="flex gap-2 items-center">
                              {[1, 2, 3].map((num) => (
                                <label
                                  key={num}
                                  className={`rounded-lg px-3 py-1 cursor-pointer transition duration-300 ${
                                    creditosEdit === num
                                      ? "bg-blue-950 text-white font-bold"
                                      : "bg-gray-200 text-gray-800"
                                  }`}
                                >
                                  <input
                                    type="radio"
                                    name="creditos"
                                    value={num}
                                    checked={creditosEdit === num}
                                    onChange={() => setCreditosEdit(num)}
                                    className="hidden"
                                  />
                                  {num}
                                </label>
                              ))}
                            </div>
                          </div>

                          <div className="flex flex-col gap-1 col-span-1">
                            <label className="font-semibold">Capacidad:</label>
                            <input
                              type="number"
                              value={capacidadEdit}
                              min={0}
                              onChange={(e) => setCapacidadEdit(Number(e.target.value))}
                              className="rounded-lg px-3 py-1 text-gray-700"
                            />
                          </div>

                          <div className="flex flex-col gap-1 col-span-2">
                            <label className="font-semibold">Carreras:</label>
                            <div className="border border-blue-950 rounded p-2 max-h-40 overflow-auto bg-white">
                              {todasLasCarreras.map((carrera) => (
                                <label key={carrera.id} className="flex items-center gap-2 mb-1 cursor-pointer">
                                  <input
                                    type="checkbox"
                                    checked={carreraIdsEdit.includes(carrera.id)}
                                    onChange={() => {
                                      if (carreraIdsEdit.includes(carrera.id)) {
                                        setCarreraIdsEdit(carreraIdsEdit.filter((id) => id !== carrera.id));
                                      } else {
                                        setCarreraIdsEdit([...carreraIdsEdit, carrera.id]);
                                      }
                                    }}
                                    className="cursor-pointer"
                                  />
                                  <span>{carrera.nombre}</span>
                                </label>
                              ))}
                            </div>
                          </div>

                          <div className="flex flex-col gap-1 col-span-1">
                            <label className="font-semibold">Fecha inicio:</label>
                            <input
                              type="date"
                              value={fechaInicioEdit}
                              onChange={(e) => setFechaInicioEdit(e.target.value)}
                              className="rounded-lg px-3 py-1 text-gray-700"
                            />
                          </div>

                          <div className="flex flex-col gap-1 col-span-1">
                            <label className="font-semibold">Fecha fin:</label>
                            <input
                              type="date"
                              value={fechaFinEdit}
                              onChange={(e) => setFechaFinEdit(e.target.value)}
                              className="rounded-lg px-3 py-1 text-gray-700"
                            />
                          </div>
                        </div>
                      </div>
                      <div className="mt-6 flex justify-end gap-4">
                        <button
                          className="bg-red-600 text-white rounded px-4 py-2 hover:bg-red-700 transition"
                          onClick={() => setShowEdit(false)}
                        >
                          Cancelar
                        </button>
                        <button
                          className="bg-blue-950 text-white rounded px-4 py-2 hover:bg-blue-800 transition"
                          onClick={handleUpdateActividad}
                        >
                          Guardar
                        </button>
                      </div>
                    </Modal>

                    {/* Modal Lista Alumnos */}
                    <Modal
                      show={showList}
                      onClose={() => setShowList(false)}
                      title={`Alumnos inscritos en ${actividadSeleccionada?.nombre || ""}`}
                      className="w-[900px] bg-gray-200"
                    >
                      <div className="mb-4 flex items-center gap-2">
                        <input
                          type="text"
                          placeholder="Buscar alumno"
                          className="border border-blue-950 rounded px-3 py-2 w-full"
                          value={busquedaAlumno}
                          onChange={(e) => setBusquedaAlumno(e.target.value)}
                        />
                        <button
                          className="bg-blue-950 text-white px-3 py-2 rounded"
                          onClick={acreditarTodos}
                          disabled={alumnos.length === 0}
                        >
                          Acreditar todos
                        </button>
                      </div>
                      <div className="max-h-80 overflow-y-auto bg-white rounded border border-blue-950">
                        {AlumnosBusqueda.length === 0 ? (
                          <p className="p-4 text-center text-gray-600">No hay alumnos inscritos</p>
                        ) : (
                          <table className="w-full border-collapse text-left text-sm">
                            <thead className="bg-blue-950 text-white">
                              <tr>
                                <th className="px-3 py-2">Nombre</th>
                                <th className="px-3 py-2">Correo</th>
                                <th className="px-3 py-2">Estado</th>
                                <th className="px-3 py-2 text-center">Acciones</th>
                              </tr>
                            </thead>
                            <tbody>
                              {AlumnosBusqueda.map((alumno) => (
                                <tr key={alumno.alumnoId} className="border-b border-gray-200">
                                  <td className="px-3 py-2">{alumno.nombreCompleto}</td>
                                  <td className="px-3 py-2">{alumno.email}</td>
                                  <td className="px-3 py-2">{estados[alumno.estadoAlumnoActividad] || "Pendiente"}</td>
                                  <td className="px-3 py-2 flex justify-center gap-2">
                                    <button
                                      onClick={() => acreditarAlumno(alumno.alumnoId, actividadSeleccionada.id)}
                                      title="Acreditar"
                                      className="text-green-600 hover:text-green-800"
                                    >
                                      <Check />
                                    </button>
                                    <button
                                      onClick={() => noAcreditarAlumno(alumno.alumnoId, actividadSeleccionada.id)}
                                      title="No acreditar"
                                      className="text-orange-600 hover:text-orange-800"
                                    >
                                      <X />
                                    </button>
                                    <button
                                      onClick={() => eliminarAlumno(alumno.alumnoId, actividadSeleccionada.id)}
                                      title="Eliminar"
                                      className="text-red-600 hover:text-red-800"
                                    >
                                      <Trash2 />
                                    </button>
                                  </td>
                                </tr>
                              ))}
                            </tbody>
                          </table>
                        )}
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
