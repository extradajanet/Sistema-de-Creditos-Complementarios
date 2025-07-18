import { useEffect, useState } from "react";
import {
    Users, Search, SlidersHorizontal, Check, ChevronDown
} from "lucide-react";
import Modal from "../../components/Modal";
import { Listbox } from "@headlessui/react";


export default function VerCursos() {
    const coordinadorId = localStorage.getItem("coordinadorId");
    //console.log(coordinadorId)

    const [cursos, setCursos] = useState([]);
    const [busqueda, setBusqueda] = useState("");
    const [mostrarFiltro, setMostrarFiltro] = useState(false);
    const [tipoSeleccionado, setTipoSeleccionado] = useState("");
    const [loading, setLoading] = useState(true);
    const [showList, setShowList] = useState(false)
    const [listaAlumnos, setListaAlumnos] = useState([])
    const [busquedaAlumnos, setBusquedaAlumnos] = useState("");

    const imagenes = import.meta.glob('../../images/*.{png,jpg,jpeg,gif}', { eager: true });

    const tipos = ["Deportiva", "Cultural", "Tutorias", "MOOC", "Todas las actividades"]

    const fetchCursosData = async () => {
        try {
            const url = coordinadorId ? `https://localhost:7238/api/Actividades/coordinador/${coordinadorId}` : null;
            if (!url) return;

            const response = await fetch(url, {
                headers: { Accept: "application/json" },
            });

            if (!response.ok) throw new Error("Error al obtener cursos");
            const data = await response.json();

            setCursos(data)
            setLoading(false);
            //console.log(data)

        } catch (error) {
            console.error("Se ha producido un error al obtener la lista de alumnos", error)
            setLoading(false);
        }
    }

    const fetchAlumnosPorActividad = async (id) => {
        try {
            const url = id ? `https://localhost:7238/api/AlumnoActividad/alumnos-inscritos/${id}` : null;

            const response = await fetch(url, {
                headers: { Accept: "application/json" },
            });

            if (!response.ok) throw new Error("Error al obtener alumnos");
            const data = await response.json();

            setListaAlumnos(data)
            console.log(data)

        } catch (err) {
            console.error("Se ha producido un problema al cargar la lista de alumnos")
        }
    }

    useEffect(() => {
        fetchCursosData();
    }, []);

    const actividadesFiltradas = cursos.filter((curso) => {
        const nombreCoincide = curso.nombre.toLowerCase().includes(busqueda.toLowerCase());

        const tipoActividad = curso.tipoActividad
        //console.log(tipoActividad)

        const TipoCoincide = (() => {
            switch (tipoSeleccionado) {
                case "Deportiva":
                    return tipoActividad == 1;
                case "Cultural":
                    return tipoActividad == 2;
                case "Tutorias":
                    return tipoActividad == 3;
                case "MOOC":
                    return tipoActividad == 4;
                default:
                    return true;
            }
        })();

        return nombreCoincide && TipoCoincide
    })

    const obtenerImagen = (nombre) => {
        const entrada = Object.entries(imagenes).find(([ruta]) =>
            ruta.includes(nombre)
        );
        return entrada ? entrada[1].default : predeterminado; // usa imagen predeterminada si no se encuentra
    };


    const alumnosFiltrados = listaAlumnos.filter((alumno => {
        const nombreCompletoCoincide = alumno.nombreCompleto.toLowerCase().includes(busquedaAlumnos.toLowerCase());

        return nombreCompletoCoincide
    }))

    return (
        <div className="flex flex-col gap-6 w-full">
            {/* Título */}
            <div className="flex justify-between items-center bg-gray-200 rounded-xl p-6">
                <h1 className="text-3xl font-bold  text-gray-900 custom-heading">
                    Ver Cursos
                </h1>
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
                                        <span>{tipoSeleccionado || "Todas las actividades"}</span>
                                        <ChevronDown className="w-5 h-5 text-gray-500" />
                                    </Listbox.Button>
                                    <Listbox.Options className="absolute mt-1 w-full rounded-lg bg-white border border-blue-950 shadow-lg z-10 max-h-48 overflow-auto">
                                        {tipos.map((tipo, index) => (
                                            <Listbox.Option
                                                key={index}
                                                value={tipo}
                                                className={({ active }) =>
                                                    `cursor-pointer select-none px-4 py-2 text-sm ${active ? "bg-blue-100 text-blue-900" : "text-gray-700"
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
                ) : cursos.length === 0 ? (
                    <p className="text-center mt-10 text-black-600">No hay cursos disponibles</p>
                ) : (
                    <div className="flex justify-center overflow-y-auto max-h-[520px] px-4">
                        <div className="grid grid-cols-1 sm:grid-cols-2 gap-x-6 gap-y-4 w-full max-w-6xl">
                            {actividadesFiltradas.map((curso) => (
                                <div
                                    key={curso.id}
                                    className="bg-blue-950 rounded-2xl shadow-md border-6 border-blue-950 h-28 flex items-center px-4 w-full"
                                >
                                    <img
                                        src={obtenerImagen(curso.imagenNombre)}
                                        alt={curso.nombre}
                                        className="rounded-md object-cover h-20 w-20 mr-4"
                                    />
                                    <div className="flex-1 flex flex-col justify-center items-left text-white">
                                        <h3 className="text-xl font-semibold mb-1">{curso.nombre}</h3>
                                        <p className="text-xs mb-1 text-[#9A9A9A]">
                                            <strong>Fecha de inicio: </strong>
                                            {new Date(curso.fechaInicio).toLocaleDateString()}
                                        </p>
                                        <p className="text-xs text-[#9A9A9A]">
                                            <strong>
                                                {curso.creditos} Crédito{curso.creditos > 1 ? "s" : ""}
                                            </strong>
                                        </p>
                                    </div>
                                    <button
                                        className="bg-blue-950 text-white rounded h-8 w-8 flex items-center justify-center cursor-pointer"
                                        onClick={() => {
                                            fetchAlumnosPorActividad(curso.id)
                                            setShowList(true)
                                        }}
                                    >
                                        <Users strokeWidth={2} color="white" className="w-6 h-6" />
                                    </button>
                                    <Modal show={showList} onClose={() => { setShowList(false); setBusquedaAlumnos("") }} className="w-[500px] bg-gray-200">
                                        <div className="text-center mb-4 flex items-center justify-center gap-2 text-blue-950">
                                            <h2 className="text-2xl font-bold">Alumnos Inscritos</h2>
                                            <Users />
                                        </div>
                                        <div className="bg-gray-200 rounded-xl p-4">
                                            <div className="relative w-full max-w-md mb-4">
                                                <input
                                                    type="text"
                                                    placeholder="Buscar alumnos"
                                                    className="w-full bg-white border border-blue-950 rounded-3xl px-4 py-2 text-base focus:outline-none focus:ring-2 focus:ring-blue-600"
                                                    value={busquedaAlumnos}
                                                    onChange={(e) => setBusquedaAlumnos(e.target.value)}
                                                />
                                                <div className="absolute right-2 top-1/2 transform -translate-y-1/2 bg-blue-950 p-1.5 rounded-full">
                                                    <Search className="h-4 w-4 text-white" />
                                                </div>
                                            </div>
                                            <div className="bg-white rounded-lg max-h-60 overflow-y-auto">
                                                {alumnosFiltrados.map((alumno) => (
                                                    <div key={alumno.alumnoId} className="flex flex-col px-4 py-2 border-b last:border-b-0">
                                                        <span className="text-sm font-semibold text-gray-800">{alumno.nombreCompleto}</span>
                                                        <span className="text-xs text-gray-500">
                                                            {alumno.carreraNombre} · Semestre {alumno.semestre}
                                                        </span>
                                                    </div>
                                                ))}
                                            </div>
                                        </div>
                                    </Modal>
                                </div>
                            ))}
                        </div>
                    </div>
                )}
            </div>
        </div>
    )
}