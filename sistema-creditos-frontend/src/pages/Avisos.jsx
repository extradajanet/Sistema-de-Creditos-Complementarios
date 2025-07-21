export default function Avisos() {

    return (
        <div className="flex flex-col gap-6 w-full h-full">
            {/* Título */}
            <div className="flex justify-between items-center bg-gray-200 rounded-xl p-6">
                <h1 className="text-3xl font-bold text-gray-900 custom-heading">
                    Avisos
                </h1>
            </div>
            <div className="flex-1 overflow-y-auto pr-2 mb-8">
                <form className="bg-gray-200 rounded-xl p-4 space-y-4 w-full">
                    <div>
                        <label htmlFor="titulo" className="block text-sm font-semibold text-[#001F54] mb-1">Título</label>
                        <input
                            id="titulo"
                            type="text"
                            className="w-full px-3 py-2 bg-white rounded-lg text-sm"
                            placeholder="Escribe el título del aviso"
                        />
                    </div>

                    <div>
                        <label htmlFor="mensaje" className="block text-sm font-semibold text-[#001F54] mb-1">Mensaje</label>
                        <textarea
                            id="mensaje"
                            rows={4}
                            className="w-full px-3 py-2 bg-white rounded-lg text-sm resize-none"
                            placeholder="Escribe el contenido del aviso"
                        />
                    </div>
                    <div className="flex justify-end">
                        <button
                            type="submit"
                            className="bg-[#001F54] text-white text-sm font-semibold px-5 py-2 rounded-lg border border-[#001F54] hover:bg-white hover:text-[#001F54] transition-colors cursor-pointer"
                        >
                            Publicar aviso
                        </button>
                    </div>
                </form>

                <hr className="my-4 border-gray-300" />

                {[1, 2, 3, 4, 5].map((i) => (
                    <div key={i} className="border border-[#001F54] rounded-xl overflow-hidden mb-4">
                        <div className="bg-[#001F54] text-white px-4 py-2 text-sm font-semibold">
                            [Departamento o Coordinador]
                        </div>
                        <div className="bg-white px-4 py-3">
                            <div className="flex justify-between items-center mb-1">
                                <h4 className="text-base font-bold text-[#001F54]">[Título del aviso]</h4>
                                <span className="text-xs text-gray-500">[Fecha]</span>
                            </div>
                            <p className="text-sm text-gray-800">[Mensaje del aviso]</p>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );

}