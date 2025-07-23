import React, { useEffect, useState } from "react";
import { CircleAlert, Search, SlidersHorizontal, ChevronDown, Check } from "lucide-react";
import predeterminado from "../../images/PredeterminadoCursos.png";
import { Listbox } from "@headlessui/react";

const estados = {
  1: "Inscrito",
  2: "En Curso",
  3: "Completado",
  4: "Acreditado",
  5: "No Acreditado",
};

const opcionesEstado = [
  { id: "", nombre: "Todos los estados" },
  { id: 1, nombre: "Inscrito" },
  { id: 2, nombre: "En Curso" },
  { id: 3, nombre: "Completado" },
  { id: 4, nombre: "Acreditado" },
  { id: 5, nombre: "No Acreditado" },
];

const imagenes = import.meta.glob('/src/images/*.{png,jpg,jpeg,gif}', { eager: true });


const obtenerImagen = (nombre) => {
  const entrada = Object.entries(imagenes).find(([ruta]) =>
    ruta.includes(nombre)
  
  );
  console.log("imagenNombre:", nombre);

  return entrada ? entrada[1].default : predeterminado; // usa imagen predeterminada si no se encuentra
};
export default function ActividadesList() {
  const [actividades, setActividades] = useState([]);
  const [loading, setLoading] = useState(true);
  const [busqueda, setBusqueda] = useState("");
  const [mostrarFiltro, setMostrarFiltro] = useState(false);
  const [estadoSeleccionado, setEstadoSeleccionado] = useState(opcionesEstado[0]);
  const userId = localStorage.getItem("alumnoId");

  useEffect(() => {
    let isMounted = true;
    fetch(`api/AlumnoActividad/cursos-alumno/${userId}`, {
      headers: { Accept: "application/json" },
    })
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
      (estadoSeleccionado.id === "" || actividad.estadoAlumnoActividad === estadoSeleccionado.id)
  );

  return (
    <div className="flex flex-col gap-6 w-full h-screen">
      {/* Título */}
      <div className="flex justify-between items-center bg-gray-200 rounded-xl p-6">
        <h1 className="text-3xl font-bold text-[#0A1128] custom-heading">Mi Historial</h1>
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

        {/* Botón filtro */}
        <div className="relative">
          <button
            className="ml-4 bg-white border-2 border-blue-950 rounded-2xl px-2 py-2 text-base font-semibold hover:bg-[#001F54] transition"
            onClick={() => setMostrarFiltro(!mostrarFiltro)}
          >
            <SlidersHorizontal strokeWidth={1} />
          </button>

          {/* Filtro desplegable */}
          {mostrarFiltro && (
            <div className="absolute right-0 mt-2 bg-white border border-blue-950 rounded-xl shadow-lg z-10 p-2 w-48">
              <Listbox value={estadoSeleccionado} onChange={setEstadoSeleccionado}>
                <div className="relative">
                  <Listbox.Button className="w-full flex justify-between items-center bg-white py-2 px-3 text-sm text-gray-800 focus:outline-none">
                    <span>{estadoSeleccionado.nombre}</span>
                    <ChevronDown className="w-5 h-5 text-gray-500" />
                  </Listbox.Button>
                  <Listbox.Options className="absolute mt-1 w-full rounded-lg bg-white border border-blue-950 shadow-lg z-10 max-h-48 overflow-auto">
                    {opcionesEstado.map((estado) => (
                      <Listbox.Option
                        key={estado.id}
                        value={estado}
                        className={({ active }) =>
                          `cursor-pointer select-none px-4 py-2 text-sm ${
                            active ? "bg-blue-100 text-blue-900" : "text-gray-700"
                          }`
                        }
                      >
                        {({ selected }) => (
                          <div className="flex justify-between items-center">
                            <span>{estado.nombre}</span>
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
    <div className="flex-1 overflow-y-auto pr-2 mb-8">
       <div className="grid grid-cols-1 sm:grid-cols-1 md:grid-cols-1 xl:grid-cols-2 gap-9">
        {loading ? (
          <p className="text-center col-span-full mt-10 text-black-600">
            Cargando actividades...
          </p>
        ) : actividadesFiltradas.length === 0 ? (
          <p className="text-center col-span-full mt-10 text-black-600">
            No te has inscrito a alguna actividad
          </p>
        ) : (
          actividadesFiltradas.map((actividad) => (
            <div
              key={actividad.id}
              className="grid grid-cols-[150px_auto] bg-[#001F54] w-[550px] h-[100px] rounded-2xl shadow-md border-3 border-black overflow-hidden"
            >
              <div className="flex items-center justify-center">
                <img src={obtenerImagen(actividad.imagenNombre)} alt={actividad.nombre} 
                    className="rounded-md object-cover h-20 w-20"
                  />
                
              </div>

              <div className="flex flex-col justify-center p-2">
                <h3 className="custom-subheading text-white text-base pb-2">
                  {actividad.nombre}
                </h3>
                <div className="grid grid-cols-2 gap-2">
                  <div className="flex flex-col">
                    <p className="text-xs text-[#9A9A9A]">
                      <strong>Fecha de Inicio:</strong>{" "}
                      {new Date(actividad.fechaInicio).toLocaleDateString()}
                    </p>
                    <p className="text-xs text-[#9A9A9A]">
                      <strong>Fecha de Fin:</strong>{" "}
                      {new Date(actividad.fechaFin).toLocaleDateString()}
                    </p>
                  </div>
                  <div>
                    <p className="text-xs text-[#9A9A9A] col-span-2">
                      <strong>Estado:</strong>{" "}
                      {estados[actividad.estadoAlumnoActividad]}
                    </p>
                    {actividad.estadoAlumnoActividad === 4 && (
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
      
    </div>
  );
}
