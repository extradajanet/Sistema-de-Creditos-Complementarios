import { useState, useEffect } from "react";
import { SlidersHorizontal, Search, ChevronDown, Check } from "lucide-react";
import { Listbox } from "@headlessui/react";

export default function VerAlumnos() {
    const coordinadorId = localStorage.getItem("coordinadorId");
    console.log(coordinadorId)

    const [mostrarFiltro, setMostrarFiltro] = useState(false);
    const [busqueda, setBusqueda] = useState("");
    const [alumnos, setAlumnos] = useState([]);
    const [filtroCarrera, setFiltroCarrera] = useState("");
    const [filtroCreditos, setFiltroCreditos] = useState("");
    const [carreras, setCarreras] = useState([]);

    const creditos = ["Todos", 0.0, 1.0, 2.0, 3.0, 4.0, 5.0, "Más de 5.0"];

    { /* Obtener las carreras*/ }
    useEffect(() => {
        const fetchCareers = async () => {
            try {
                const res = await fetch("https://localhost:7238/api/Carrera/carreras");

                const data = await res.json();
                setCarreras(data);
            } catch (err) {
                console.error("Error fetching careers:", err);
            }
        };

        fetchCareers();
    }, []);

    const fetchAlumnosData = async () => {
        try {
            const url = coordinadorId ? `https://localhost:7238/api/Alumno/filtrados/${coordinadorId}` : null;

            const response = await fetch(url, {
                headers: { Accept: "application/json" },
            });

            if (!response.ok) throw new Error("Error al obtener alumnos");

            const data = await response.json();
            console.log(data)
            setAlumnos(data)
        } catch (error) {
            console.error("Se ha producido un error al obtener la lista de alumnos", error)
        }
    }

    useEffect(() => {
        fetchAlumnosData();
    }, []);

    const alumnosFiltrados = alumnos.filter((alumno) => {
        const nombreCompletoCoincide = `${alumno.nombre} ${alumno.apellido}`.toLowerCase().includes(busqueda.toLowerCase());

        const carreraCoincide = filtroCarrera === "" || alumno.carreraNombre === filtroCarrera;

        const creditos = alumno.totalCreditos;

        const creditosCoincide = (() => {

            switch (filtroCreditos) {
                case 0.0:
                    return creditos == 0.0;
                case 1.0:
                    return creditos >= 1.0 && creditos < 2.0;
                case 2.0:
                    return creditos >= 2.0 && creditos < 3.0;
                case 3.0:
                    return creditos >= 3.0 && creditos < 4.0;
                case 4.0:
                    return creditos >= 4.0 && creditos < 5.0;
                case 5.0:
                    return creditos >= 5.0 && creditos < 6.0;
                case "Más de 5.0":
                    return creditos > 5.0;
                default:
                    return true; // "Todos"
            }
        })();

        return nombreCompletoCoincide && carreraCoincide && creditosCoincide

    });

    return (
        <div className="flex flex-col gap-6 w-full">
            {/* Título */}
            <div className="flex justify-between items-center bg-gray-200 rounded-xl p-6">
                <h1 className="text-3xl font-bold  text-gray-900 custom-heading">
                    Ver Alumnos
                </h1>
            </div>

            <div className="flex justify-between items-center mb-2">
                {/* Buscador */}
                <div className="relative w-full max-w-md">
                    <input
                        type="text"
                        placeholder="Buscar alumnos"
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
                        <div className="absolute right-0 mt-2 bg-white border border-blue-950 rounded-xl shadow-lg z-10 p-2 w-48">
                            <Listbox value={filtroCarrera} onChange={setFiltroCarrera}>
                                <div className="relative">
                                    <Listbox.Button className="w-full flex justify-between items-center bg-white py-2 px-3 text-sm text-gray-800 focus:outline-none">
                                        <span>{filtroCarrera || "Carrera"}</span>
                                        <ChevronDown className="w-5 h-5 text-gray-500" />
                                    </Listbox.Button>
                                    <Listbox.Options className="absolute mt-1 w-full rounded-lg bg-white border border-blue-950 shadow-lg z-10 max-h-48 overflow-auto">
                                        {carreras.map((opcion, index) => (
                                            <Listbox.Option
                                                key={index}
                                                value={opcion.nombre}
                                                className={({ active }) =>
                                                    `cursor-pointer select-none px-4 py-2 text-sm ${active ? "bg-blue-100 text-blue-900" : "text-gray-700"
                                                    }`
                                                }
                                            >
                                                {({ selected }) => (
                                                    <div className="flex justify-between items-center">
                                                        <span>{opcion.nombre}</span>
                                                        {selected && <Check className="w-4 h-4 text-blue-950" />}
                                                    </div>
                                                )}
                                            </Listbox.Option>
                                        ))}
                                    </Listbox.Options>
                                </div>
                            </Listbox>
                            <Listbox value={filtroCreditos} onChange={setFiltroCreditos}>
                                <div className="relative">
                                    <Listbox.Button className="w-full flex justify-between items-center bg-white py-2 px-3 text-sm text-gray-800 focus:outline-none">
                                        <span>{filtroCreditos || "Créditos"}</span>
                                        <ChevronDown className="w-5 h-5 text-gray-500" />
                                    </Listbox.Button>
                                    <Listbox.Options className="absolute mt-1 w-full rounded-lg bg-white border border-blue-950 shadow-lg z-10 max-h-48 overflow-auto">
                                        {creditos.map((opcion, index) => (
                                            <Listbox.Option
                                                key={index}
                                                value={opcion}
                                                className={({ active }) =>
                                                    `cursor-pointer select-none px-4 py-2 text-sm ${active ? "bg-blue-100 text-blue-900" : "text-gray-700"
                                                    }`
                                                }
                                            >
                                                {({ selected }) => (
                                                    <div className="flex justify-between items-center">
                                                        <span>{opcion}</span>
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


            {alumnosFiltrados.map((alumno) => (
                <div key={alumno.id} className="flex items-start space-x-4 bg-white p-4 rounded-xl border border-[#001F54] hover:scale-101 transition">
                    <div className="bg-[#001F54] text-white w-10 h-10 rounded-full flex items-center justify-center font-bold text-lg">
                        {alumno.nombre[0]}
                    </div>
                    <div>
                        <h3 className="text-[25px] font-semibold">{alumno.nombre} {alumno.apellido}</h3>
                        <p className="text-gray-500">Carrera: {alumno.carreraNombre}</p>
                        <p className="text-gray-500">Semestre: {alumno.semestre} | Créditos: {alumno.totalCreditos}</p>
                    </div>
                </div>
            ))}
        </div>


    )
}