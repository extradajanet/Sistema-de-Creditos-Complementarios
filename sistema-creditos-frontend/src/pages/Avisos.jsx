import { useState, useEffect } from "react";
import Toast from "../components/Toast";

export default function Avisos() {
    const [rol] = useState(() => localStorage.getItem("rol"));

    const ids = {
        Departamento: localStorage.getItem("departamentoId"),
        Coordinador: localStorage.getItem("coordinadorId"),
    };

    const [avisos, setAvisos] = useState([])
    const [newAviso, setNewAviso] = useState({
        titulo: "",
        mensaje: "",
        departamentoId: null,
        coordinadorId: null
    });

    const [toast, setToast] = useState(null);

    const fetchAvisos = async () => {
        try {
            const url = `https://localhost:7238/api/Aviso`

            const response = await fetch(url, {
                headers: { Accept: "application/json" },
            });

            if (!response.ok) throw new Error("Error al obtener avisos");

            const data = await response.json();
            setAvisos(data);
        } catch (error) {
            console.error("Error al cargar datos de avisos:", error);
        }
    };

    useEffect(() => {
        fetchAvisos();
    }, []);

    const handleSave = async () => {
        if (newAviso.titulo.trim() === "") {
            setToast({ message: "El título no puede estar vacío", type: "warning" });
            return;
        } else if (newAviso.mensaje.trim() === "") {
            setToast({ message: "El mensaje no puede estar vacío", type: "warning" });
            return;
        }

        try {
            if (!rol || (!ids.Coordinador && !ids.Departamento)) {
                alert("No se pudo determinar el remitente.");
                return;
            }

            const avisoData = {
                titulo: newAviso.titulo,
                mensaje: newAviso.mensaje,
                departamentoId: ids.Departamento ? parseInt(ids.Departamento) : null,
                coordinadorId: ids.Coordinador ? parseInt(ids.Coordinador) : null
            };

            const response = await fetch("https://localhost:7238/api/Aviso", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(avisoData)
            });

            if (!response.ok) throw new Error("No se pudo crear el aviso");
            setToast({ message: "El aviso se ha publicado correctamente", type: "success" });
            setNewAviso({ titulo: "", mensaje: "" });
            fetchAvisos()
        } catch (error) {
            console.error("Error al crear aviso:", error);
            setToast({ message: "No se pudo crear el aviso", type: "error" });
        }

    }

    return (
        <div className="flex flex-col gap-6 w-full h-full">
            {/* Título */}
            <div className="flex justify-between items-center bg-gray-200 rounded-xl p-6">
                <h1 className="text-3xl font-bold text-gray-900 custom-heading">
                    Avisos
                </h1>
            </div>

            {toast && (
                <Toast
                    message={toast.message}
                    type={toast.type}
                    onClose={() => setToast(null)}
                />
            )}

            <div className="flex-1 overflow-y-auto pr-2">
                {(rol == 'Coordinador' || rol == 'Departamento') && (
                    <div>
                        <form className="bg-gray-200 rounded-xl p-4 space-y-4 w-full">
                            <div>
                                <label htmlFor="titulo" className="block text-sm font-semibold text-[#001F54] mb-1">Título</label>
                                <input
                                    id="titulo"
                                    value={newAviso.titulo}
                                    onChange={(e) =>
                                        setNewAviso({ ...newAviso, titulo: e.target.value })
                                    }
                                    type="text"
                                    className="w-full px-3 py-2 bg-white rounded-lg text-sm"
                                    placeholder="Escribe el título del aviso"
                                />
                            </div>

                            <div>
                                <label htmlFor="mensaje" className="block text-sm font-semibold text-[#001F54] mb-1">Mensaje</label>
                                <textarea
                                    id="mensaje"
                                    value={newAviso.mensaje}
                                    onChange={(e) =>
                                        setNewAviso({ ...newAviso, mensaje: e.target.value })
                                    }
                                    rows={4}
                                    className="w-full px-3 py-2 bg-white rounded-lg text-sm resize-none"
                                    placeholder="Escribe el contenido del aviso"
                                />
                            </div>
                            <div className="flex justify-end">
                                <button
                                    type="button"
                                    onClick={handleSave}
                                    className="bg-[#001F54] text-white text-sm font-semibold px-5 py-2 rounded-lg border border-[#001F54] hover:bg-white hover:text-[#001F54] transition-colors cursor-pointer"
                                >
                                    Publicar aviso
                                </button>
                            </div>
                        </form>

                        <hr className="my-4 border-gray-300" />
                    </div>
                )}

                {avisos.map((aviso) => (
                    <div key={aviso.id} className="border border-[#001F54] rounded-xl overflow-hidden mb-4">
                        <div className="bg-[#001F54] text-white px-4 py-2 text-sm font-semibold">
                            {aviso.coordinadorNombre
                                ? `${aviso.coordinadorNombre} ${aviso.coordinadorApellido}`
                                : aviso.departamentoNombre}
                        </div>
                        <div className="bg-white px-4 py-3">
                            <div className="flex justify-between items-center mb-1">
                                <h4 className="text-base font-bold text-[#001F54]">{aviso.titulo}</h4>
                                <span className="text-xs text-gray-500">[Fecha]</span>
                            </div>
                            <p className="text-sm text-gray-800">{aviso.mensaje}</p>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );

}