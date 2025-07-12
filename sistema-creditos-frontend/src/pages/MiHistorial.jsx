import React, { useEffect, useState } from "react";
import { CircleAlert, Search, SlidersHorizontal } from "lucide-react";
import predeterminado from "../images/PredeterminadoCursos.png";

const estados = {
  1: "Inscrito",
  2: "En Curso",
  3: "Completado",
  4: "Acreditado",
  5: "No Acreditado"
};


export default function ActividadesList() {
  const [actividades, setActividades] = useState([]);
  const [loading, setLoading] = useState(true);
  const [busqueda, setBusqueda] = useState("");
  const [mostrarFiltro, setMostrarFiltro] = useState(false);
  const [tipoSeleccionado, setTipoSeleccionado] = useState("");
  const userId = localStorage.getItem("alumnoId");

  useEffect(() => {
    let isMounted = true;
    fetch(`api/AlumnoActividad/cursos-alumno/${userId}`, { headers: { Accept: "application/json" } })
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

  return (
    <div className="flex flex-col gap-6 w-full">
      {/* Título */}
      <div className="flex justify-between items-center bg-gray-200 rounded-xl p-6">
        <h1 className="text-3xl font-bold  text-[#0A1128] custom-heading">
          Mi Historial
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
          >
            <SlidersHorizontal strokeWidth={1} />
          </button>

          {/* Filtro desplegable */}
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
      <div className="grid grid-cols-1 sm:grid-cols-2 gap-9">
        {loading ? (
          <p className="text-center col-span-full mt-10 text-black-600">
            Cargando actividades...
          </p>
        ) : actividadesFiltradas.length === 0 ? (
          <p className="text-center col-span-full mt-10 text-black-600">
            No te haz inscrito a alguna actividad
          </p>
        ) : (
          actividadesFiltradas.map((actividad) => (
            <div
              key={actividad.id}
              className="grid grid-cols-[150px_auto] bg-[#001F54] w-[550px] h-[100px] rounded-2xl shadow-md border-3 border-black overflow-hidden"
            >
              {/* Image column */}
              <div className="flex items-center justify-center">
                {actividad.imagenNombre ? (
                  <img
                    src={predeterminado}
                    alt={actividad.nombre}
                    className="rounded-md object-cover h-20 w-20"
                  />
                ) : null}
              </div>

              {/* Text column */}
              <div className="flex flex-col justify-center p-2">
                <h3 className="custom-subheading text-white text-base pb-2">
                  {actividad.nombre}
                </h3>
                <div className="grid grid-cols-2 gap-2">
                  <div className="flex flex-col">
                    <p className="text-xs text-[#9A9A9A]">
                      <strong>Fecha de Inicio:</strong>
                      {new Date(actividad.fechaInicio).toLocaleDateString()}
                    </p>
                    <p className="text-xs text-[#9A9A9A]">
                      <strong>Fecha de Fin:</strong>
                      {new Date(actividad.fechaFin).toLocaleDateString()}
                    </p>
                  </div>
                  <div>
                    <p className="text-xs text-[#9A9A9A] col-span-2">
                      <strong>Estado:</strong>{" "}
                      {estados[actividad.estadoAlumnoActividad]}
                    </p>
                    {actividad.estadoActividad === 3 && (
                      <p className="text-xs text-[#9A9A9A] col-span-2">
                        <strong>Créditos Obtenidos:</strong>{" "}
                        {actividad.creditos} 
                      </p>
                    )}
                  </div>
                </div>
              </div>
            </div>
          ))
        )}
      </div>
    </div>
  );
}
