import React, { useEffect, useState } from "react";
import { CircleAlert, Search, SlidersHorizontal, ChevronDown, Check } from "lucide-react";
import predeterminado from "../../images/PredeterminadoCursos.png";
import Modal from "../../components/Modal";
import { Listbox } from "@headlessui/react";

const tipoActividad = {
  1: "Deportivo",
  2: "Cultural",
  3: "Tutorias",
  4: "Mooc",
};

const tipos = ["", "Deportivo", "Cultural", "Tutorias", "Mooc"];

const dias = {
  1: "Lunes",
  2: "Martes",
  3: "Miércoles",
  4: "Jueves",
  5: "Viernes",
};

export default function ActividadesList() {
  const [actividades, setActividades] = useState([]);
  const [loading, setLoading] = useState(true);
  const [busqueda, setBusqueda] = useState("");
  const [mostrarFiltro, setMostrarFiltro] = useState(false);
  const [tipoSeleccionado, setTipoSeleccionado] = useState("");
  const [showModal, setShowModal] = useState(false);
  const [showConfirmModal, setShowConfirmModal] = useState(false);
  const [isEnrolled, setIsEnrolled] = useState(false);
  const [selectedActividad, setSelectedActividad] = useState(null);
  const [selectedTotal, settotalAlumnos] = useState(0);
  const [error, setError] = useState("");
  const userId = localStorage.getItem("alumnoId");

  // Manejar inscripción a actividad
  const handleSubmit = async () => {
    setError("");
    try {
      const response = await fetch("https://localhost:7238/api/AlumnoActividad", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          alumnoId: userId,
          actividadId: selectedActividad.id,
          estadoAlumnoActividad: 1,
        }),
      });

      if (!response.ok) throw new Error("Error al registrarse");

      // Actualizar inscritos inmediatamente tras inscripción
      const resInscritos = await fetch(
        `/api/AlumnoActividad/alumnos-inscritos/${selectedActividad.id}`,
        { headers: { Accept: "application/json" } }
      );
      if (!resInscritos.ok) throw new Error("Error al actualizar inscritos");
      const inscritos = await resInscritos.json();

      settotalAlumnos(inscritos.length);
      setIsEnrolled(inscritos.some((a) => a.alumnoId.toString() === userId));
    } catch (err) {
      console.error(err);
      setError(err.message);
    }
  };

  // Cargar actividades al montar componente
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

  // Cargar inscritos y estado de inscripción cada vez que cambia la actividad seleccionada
  useEffect(() => {
    if (!selectedActividad) return;

    let isMounted = true;

    fetch(`/api/AlumnoActividad/alumnos-inscritos/${selectedActividad.id}`, {
      headers: { Accept: "application/json" },
    })
      .then((res) => {
        if (!res.ok) throw new Error("Error: " + res.status);
        return res.json();
      })
      .then((data) => {
        if (isMounted) {
          settotalAlumnos(data.length);
          const alreadyEnrolled = data.some(
            (alumno) => alumno.alumnoId.toString() === userId
          );
          setIsEnrolled(alreadyEnrolled);
        }
      })
      .catch((err) => {
        if (isMounted) console.error("Fetch error:", err);
      });

    return () => {
      isMounted = false;
    };
  }, [selectedActividad, userId]);

  // Filtrar actividades según búsqueda y tipo seleccionado
  const actividadesFiltradas = actividades.filter(
    (actividad) =>
      actividad.nombre.toLowerCase().includes(busqueda.toLowerCase()) &&
      (tipoSeleccionado === "" ||
        tipoActividad[actividad.tipoActividad] === tipoSeleccionado)
  );

  return (
    <div className="flex flex-col gap-6 w-full">
      {/* Título */}
      <div className="flex justify-between items-center bg-gray-200 rounded-xl p-6">
        <h1 className="text-3xl font-bold text-gray-900 custom-heading">
          Cursos Disponibles
        </h1>
      </div>

      {/* Buscador y filtro */}
      <div className="flex justify-between items-center mb-2">
        {/* Buscador */}
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

        {/* Botón filtro */}
        <div className="relative">
          <button
            className="ml-4 bg-white border-2 border-blue-950 rounded-2xl px-2 py-2 text-base font-semibold hover:bg-blue-800 transition"
            onClick={() => setMostrarFiltro(!mostrarFiltro)}
            aria-label="Toggle filtro de tipo de actividad"
          >
            <SlidersHorizontal strokeWidth={1} />
          </button>

          {/* Filtro desplegable */}
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
      <div className="max-h-[500px] overflow-y-auto pr-2">
        <div className="grid grid-cols-1 sm:grid-cols-4 gap-9">
          {loading ? (
            <p className="text-center col-span-full mt-10 text-black-600">
              Cargando actividades...
            </p>
          ) : actividadesFiltradas.length === 0 ? (
            <p className="text-center col-span-full mt-10 text-black-600">
              No hay cursos disponibles
            </p>
          ) : (
            actividadesFiltradas.map((actividad) => (
              <div
                key={actividad.id}
                onClick={() => {
                  setSelectedActividad(actividad);
                  setShowModal(true);
                }}
                className="bg-white rounded-lg shadow-md cursor-pointer p-6 flex flex-col border-3 border-blue-950 hover:bg-[#D9D9D9]"
                aria-label={`Ver detalles del curso ${actividad.nombre}`}
              >
                {actividad.imagenNombre ? (
                  <img
                    src={predeterminado}
                    alt={actividad.nombre}
                    className="rounded-md mb-4 object-cover h-24 w-24 mx-auto"
                  />
                ) : null}

                <h3 className="text-xl text-blue-950 font-semibold mb-2 text-center">
                  {actividad.nombre}
                </h3>

                <p className="text-xs text-gray-700 mb-1 text-center">
                  <strong>
                    {tipoActividad[actividad.tipoActividad]} · {actividad.creditos} Crédito
                    {actividad.creditos > 1 ? "s" : ""}
                  </strong>
                </p>

                <div className="flex items-center gap-1 text-gray-700 text-xs mt-2 font-semibold justify-center">
                  <CircleAlert strokeWidth={0.8} className="h-4 w-4" />
                  <p>Más información</p>
                </div>
              </div>
            ))
          )}

          {/* Modal de detalle de curso */}
          {selectedActividad && (
            <Modal
              show={showModal}
              onClose={() => setShowModal(false)}
              title={selectedActividad.nombre}
              className="w-[700px] h-[350px] max-w-full border-4 bg-[#001F54] text-white"
              closeButtonClassName="text-white"
            >
              <div className="text-center mb-4 text-[#BFBFBF] font-semibold">
                {selectedActividad.descripcion}
              </div>

              {/* Imagen curso y detalles */}
              <div className="flex h-full">
                <div className="w-1/3">
                  {selectedActividad.imagenNombre ? (
                    <img
                      src={predeterminado}
                      alt={selectedActividad.nombre}
                      className="rounded-md object-cover h-[200px] w-[200px] mx-auto"
                    />
                  ) : null}
                </div>

                {/* Información de curso */}
                <div className="grid grid-cols-2 w-2/3 h-full customtext2 text-white relative">
                  <div className="w-full h-full mr-4">
                    <p className="mb-4">
                      Horario:
                      <br /> {dias[selectedActividad.dias]} <br />
                      {selectedActividad.horaInicio.slice(0, 5)} -{" "}
                      {selectedActividad.horaFin.slice(0, 5)}
                    </p>
                    <p>
                      Carrera(s):
                      <br />
                      {selectedActividad.carreraNombres.map((carrera, index) => (
                        <span key={index}>
                          {carrera.trim()}
                          <br />
                        </span>
                      ))}
                    </p>
                  </div>
                  <div>
                    <p>
                      Capacidad: {selectedTotal} / {selectedActividad.capacidad}
                    </p>
                    <p>Créditos: {selectedActividad.creditos}</p>
                    <p className="mt-10">
                      Fecha de Inicio:{" "}
                      {new Date(selectedActividad.fechaInicio).toLocaleDateString()}
                    </p>
                    <p>
                      Fecha de Fin:{" "}
                      {new Date(selectedActividad.fechaFin).toLocaleDateString()}
                    </p>
                  </div>

                  {/* Botón inscribirme */}
                  <div
                    onClick={() => setShowConfirmModal(true)}
                    className="absolute bottom-4 cursor-pointer right-4 p-2 bg-[#D9D9D9] w-[155px] rounded-md text-center custom-mdtext font-bold text-[#0A1128]"
                    role="button"
                    tabIndex={0}
                    onKeyDown={(e) => {
                      if (e.key === "Enter" || e.key === " ") setShowConfirmModal(true);
                    }}
                    aria-label="Inscribirme en la actividad"
                  >
                    Inscribirme
                  </div>

                  {/* Error en inscripción */}
                  {error && (
                    <p className="text-red-500 text-sm mt-2 absolute bottom-2 left-4">
                      {error}
                    </p>
                  )}
                </div>
              </div>
            </Modal>
          )}

          {/* Modal confirmación inscripción */}
          {showConfirmModal && (
            <Modal
              show={showConfirmModal}
              onClose={() => {
                setShowConfirmModal(false);
                setShowModal(false);
              }}
              title="Confirmación"
              className="w-[500px] h-[220px] max-w-full border-4 bg-[#D9D9D9] text-[#001F54] custom-text font-semibold"
              closeButtonClassName="text-[#001F54]"
            >
              <div className="text-center text-[#0A1128] font-medium mt-4">
                ¿Estás seguro de querer inscribirte a la actividad de <br /> "
                {selectedActividad.nombre}"?
              </div>

              <div className="flex justify-center gap-6 mt-8">
                <button
                  className={`px-4 py-2 rounded-md font-bold border-2 cursor-pointer ${
                    isEnrolled
                      ? "bg-[#001F54] text-white cursor-not-allowed"
                      : "border-[#001F54] text-[#001F54]"
                  }`}
                  disabled={isEnrolled}
                  onClick={async () => {
                    await handleSubmit();
                    setShowConfirmModal(false);
                    // Opcional: puedes dejar abierto el modal principal
                    setShowModal(false);
                  }}
                  aria-label={isEnrolled ? "Ya inscrito" : "Confirmar inscripción"}
                >
                  {isEnrolled ? "Ya inscrito" : "Sí, inscribirme"}
                </button>

                <button
                  className="border-2 border-[#001F54] text-[#001F54] cursor-pointer px-4 py-2 rounded-md font-bold"
                  onClick={() => {
                    setShowConfirmModal(false);
                    setShowModal(false);
                  }}
                  aria-label="Cancelar inscripción"
                >
                  Cancelar
                </button>
              </div>
            </Modal>
          )}
        </div>
      </div>
    </div>
  );
}
