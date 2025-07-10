import React, { useEffect, useState } from "react";
import { PencilLine,Users, Search, SlidersHorizontal } from "lucide-react";
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
  const [loading, setLoading] = useState(true);
  const [busqueda, setBusqueda] = useState("");
  const [mostrarFiltro, setMostrarFiltro] = useState(false);
  const [tipoSeleccionado, setTipoSeleccionado] = useState("");
    const [showEdit, setShowEdit] = useState(false);
    const [showList, setShowList] = useState(false);

  useEffect(() => {
    // Mientras el backend no está listo, usamos datos falsos
    setActividades(mockActividades);
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


return (
    <div className="flex flex-col gap-6 w-full">
      {/* Título */}
      <div className="flex justify-between items-center bg-gray-200 rounded-xl p-6">
        <h1 className="text-3xl font-bold text-gray-900 custom-heading">Crear Actividad</h1>
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

                    <Modal show={showEdit} onClose={() => setShowEdit(false)} title="Editar curso">
                      <form className="flex flex-col gap-3">
                        <input className="border p-2 rounded" type="text" placeholder="Nombre del curso" />
                        <input className="border p-2 rounded" type="text" placeholder="Fecha de inicio" />
                        <input className="border p-2 rounded" type="number" placeholder="Créditos" />
                        <button className="bg-blue-700 text-white py-2 px-4 rounded mt-2">Guardar cambios</button>
                      </form>
                    </Modal>

                    <Modal show={showList} onClose={() => setShowList(false)} title="Lista de alumnos">
                      <ul className="list-disc pl-5">
                        <li>Juan Pérez</li>
                        <li>Ana López</li>
                        <li>Marco Díaz</li>
                      </ul>
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

