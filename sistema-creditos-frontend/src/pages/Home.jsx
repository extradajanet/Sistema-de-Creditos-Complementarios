import {LibraryBig, Trophy, BellRing } from "lucide-react";

export default function Home() {
  return (
    <div className="flex flex-col gap-18  w-full">
      {/* Bienvenida y gráfica */}
      <div className="flex justify-between items-center bg-gray-200 rounded-xl p-6">
        <h1 className="text-3xl font-bold text-gray-900">¡Bienvenido, Alumno!</h1>
        <div className="w-50 h-50 rounded-full bg-blue-500 text-white flex items-center justify-center flex-col text-center">
          <span className="text-sm">Créditos Obtenidos</span>
          <span className="text-3xl font-bold">3</span>
        </div>
      </div>

      {/* Tarjetas de navegación */}
      <div className="grid grid-cols-1 sm:grid-cols-3 gap-40">
        <div className="bg-blue-900 text-white rounded-xl flex flex-col items-center justify-center p-15 hover:scale-105 transition  w-80">
          <LibraryBig  strokeWidth={0.5} className="h-40 w-40 mb-2" />
          <span className="text-center font-bold text-lg">Cursos Disponibles</span>
        </div>

        <div className="bg-blue-900 text-white rounded-xl flex flex-col items-center justify-center p-6 hover:scale-105 transition w-80">
          <Trophy strokeWidth={0.5} className="h-40 w-40 mb-2"/>
          <span className="text-center font-bold text-lg">Actividades Extraescolares</span>
        </div>

        <div className="bg-blue-900 text-white rounded-xl flex flex-col items-center justify-center p-6 hover:scale-105 transition w-80">
          <BellRing strokeWidth={0.5} className="h-40 w-40 mb-2"/>
          <span className="text-center font-bold text-lg">Avisos</span>
        </div>
      </div>
    </div>
  );
}
