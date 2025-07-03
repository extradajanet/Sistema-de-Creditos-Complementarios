import React, { useEffect, useState } from "react";
import { CircleAlert, Search, SlidersHorizontal } from "lucide-react";
import predeterminado from "../images/PredeterminadoCursos.png";

const estados = {
  1: "Activo",
  2: "En Progreso",
  3: "Finalizado",
};

export default function ActividadesList() {
  const [actividades, setActividades] = useState([]);
  const [loading, setLoading] = useState(true);
  const [busqueda, setBusqueda] = useState("");
  const [mostrarFiltro, setMostrarFiltro] = useState(false);
  const [tipoSeleccionado, setTipoSeleccionado] = useState("");

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


  return (
    <div className="flex flex-col gap-6 w-full">
      {/* Título */}
      <div className="flex justify-between items-center bg-gray-200 rounded-xl p-6">
        <h1 className="text-3xl font-bold  text-gray-900 custom-heading">Cursos Disponibles</h1>
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
      <div className="grid grid-cols-1 sm:grid-cols-4 gap-9">
        {loading ? (
          <p className="text-center col-span-full mt-10 text-black-600">Cargando actividades...</p>
        ) : actividadesFiltradas.length === 0 ? (
          <p className="text-center col-span-full mt-10 text-black-600">No hay cursos disponibles</p>
        ) : (
          actividadesFiltradas.map((actividad) => (
            <div
              key={actividad.id}
              className="bg-white rounded-lg shadow-md p-6 flex flex-col border-3 border-blue-950"
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
                  {actividad.tipoActividad} · {actividad.creditos} Crédito
                  {actividad.creditos > 1 ? "s" : ""}
                </strong>
              </p>

              <div className="flex items-center gap-1 text-gray-700 text-xs mt-2 cursor-pointer font-semibold justify-center">
                <CircleAlert strokeWidth={0.8} className="h-4 w-4" />
                <p>Más información</p>
              </div>
            </div>
          ))
        )}
      </div>
    </div>
  );
}
