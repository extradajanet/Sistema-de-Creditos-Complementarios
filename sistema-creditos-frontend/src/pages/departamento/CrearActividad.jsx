import React, { useState } from "react";
import { Users } from "lucide-react";
import predeterminado from "../../images/PredeterminadoCursos.png";
import Modal from "../../components/Modal";

export default function ActividadesList() {
  const [horaInicio, setHoraInicio] = useState("");
const [horaFin, setHoraFin] = useState("");
const [capacidad, setCapacidad] = useState(0);


  const [tipoSeleccionado, setTipoSeleccionado] = useState("");
  const [carreraId, setCarreraId] = useState(0);
  const [creditos, setCreditos] = useState(0);
  const [nombre, setNombre] = useState("");
  const [descripcion, setDescripcion] = useState("");
  const [fechaInicio, setFechaInicio] = useState("");
  const [fechaFin, setFechaFin] = useState("");
  const [successMessage, setSuccessMessage] = useState("");

  const carreras = [
    { id: 1, nombre: "Ingeniería en Sistemas Computacionales" },
    { id: 2, nombre: "Ingeniería en Tecnologías de la Información y Comunicaciones" },
    { id: 3, nombre: "Ingeniería en Administración" },
    { id: 4, nombre: "Licenciatura en Administración" },
    { id: 5, nombre: "Arquitectura" },
    { id: 6, nombre: "Licenciatura en Biología" },
    { id: 7, nombre: "Licenciatura en Turismo" },
    { id: 8, nombre: "Ingeniería Civil" },
    { id: 9, nombre: "Contador Público" },
    { id: 10, nombre: "Ingeniería Eléctrica" },
    { id: 11, nombre: "Ingeniería Electromecánica" },
    { id: 12, nombre: "Ingeniería en Gestión Empresarial" },
    { id: 13, nombre: "Ingeniería en Desarrollo de Aplicaciones" },
    { id: 14, nombre: "Todas las carreras" }
  ];

  function obtenerTipoActividad(nombre) {
  switch (nombre) {
    case "Deportiva": return 1;
    case "Cultural": return 2;
    case "Tutorias": return 3;
    case "MOOC": return 4;
    default: return 0;
  }
}


const handleSubmit = async () => {
  const actividad = {
    nombre,
    descripcion,
    fechaInicio: `${fechaInicio}T00:00:00Z`,
    fechaFin: `${fechaFin}T00:00:00Z`,
    creditos,
    capacidad,
    dias: 1,
    horaInicio: `${horaInicio}:00`,
    horaFin: `${horaFin}:00`,
    tipoActividad: obtenerTipoActividad(tipoSeleccionado),
    estadoActividad: 1,
    imagenNombre: "PredeterminadoCursos.png",
    departamentoId: 1,
    carreraIds: [carreraId],
  };

  try {
    const response = await fetch("https://localhost:7238/api/Actividades", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(actividad), // ✅ Esto es lo correcto
    });

    if (response.ok) {
      setSuccessMessage("Actividad creada exitosamente ✅");
      setTimeout(() => setSuccessMessage(""), 3000);
    } else {
      const error = await response.text();
      console.error("Error al crear actividad:", error);
      alert("Error al crear actividad ❌");
    }
  } catch (err) {
    console.error("Excepción al hacer POST:", err);
    alert("Error de red o servidor ❌");
  }
};





  return (
    <div className="flex flex-col gap-6 w-full">
      <div className="flex justify-between items-center bg-gray-200 rounded-xl p-6">
        <h1 className="text-3xl font-bold text-gray-900 custom-heading">Crear Actividad</h1>
      </div>

      <div className="flex justify-center">
        <div className="grid grid-cols-2 justify-items-center-safe bg-gray-200 rounded-xl p-6 border border-blue-950 h-140 w-295">
          <div className="mt-7 text-center text-blue-950 font-bold">
            <p>Selecciona una imagen</p>
            <div className="bg-gray-200 rounded-xl p-6 border border-blue-950 h-108 w-115"></div>
          </div>

          <div className="grid grid-rows-5 bg-gray-200 rounded-xl h-125 w-140">
            <div className="w-full text-blue-950 font-bold">
              <div className="mb-4">
                <p>Nombre de la actividad:</p>
                <div className="flex items-start border border-blue-950 rounded bg-gray-200 h-12">
                  <textarea
                    className="flex-1 text-sm resize-none outline-none bg-transparent text-gray-600"
                    rows={1}
                    value={nombre}
                    onChange={(e) => setNombre(e.target.value)}
                  />
                </div>
              </div>

              <div className="mb-4">
                <p>Descripción:</p>
                <div className="flex items-start border border-blue-950 rounded bg-gray-200 h-20">
                  <textarea
                    className="flex-1 text-sm resize-none outline-none bg-transparent text-gray-600"
                    rows={2}
                    value={descripcion}
                    onChange={(e) => setDescripcion(e.target.value)}
                  />
                </div>

                <label className="mt-2">Carrera(s):</label>
                <div className="relative w-fit mt-1">
                  <select
                    className="border rounded pl-2 text-gray-700 w-125 h-8"
                    value={carreraId}
                    onChange={(e) => setCarreraId(Number(e.target.value))}
                  >
<option value={0} disabled>Selecciona una carrera</option>
                    {carreras.map((carrera) => (
                      <option key={carrera.id} value={carrera.id}>
                        {carrera.nombre}
                      </option>
                    ))}
                  </select>
                </div>
              </div>

              <div className="grid grid-cols-3 mb-2">
                <div className="row">
                  <p>Día(s):</p>
                  <div className="flex flex-col gap-1.5 text-sm text-gray-700 mt-1">
                    {["Lunes", "Martes", "Miércoles", "Jueves", "Viernes"].map((dia) => (
                      <label key={dia} className="flex items-center gap-1">
                        <input type="checkbox" className="accent-blue-950" />
                        {dia}
                      </label>
                    ))}
                  </div>
                </div>

                <div className="row">
                  <div className="items-center gap-2 mb-1">
                    <label>Fecha de Inicio:</label>
                    <input
                      type="date"
                      className="border rounded px-2 py-1 mt-0.5"
                      value={fechaInicio}
                      onChange={(e) => setFechaInicio(e.target.value)}
                    />
                  </div>
                  <div className="items-center gap-2">
                    <label>Fecha de Fin:</label>
                    <input
                      type="date"
                      className="border rounded px-2 py-1 mt-0.5"
                      value={fechaFin}
                      onChange={(e) => setFechaFin(e.target.value)}
                    />
                  </div>

                  <div className="z-10 mt-2">
                    Tipo de Actividad
                    <div className="flex flex-row">
                      {["Deportiva", "Cultural", "Tutorias", "MOOC"].map((tipo) => (
                        <button
                          key={tipo}
                          type="button"
                          className={`px-4 m-0.5 rounded border border-blue-950 text-sm font-medium transition h-8 w-25 ${
                            tipoSeleccionado === tipo
                              ? "bg-blue-950 text-white"
                              : "bg-white text-blue-950 hover:bg-blue-100"
                          }`}
                          onClick={() => setTipoSeleccionado(tipo)}
                        >
                          {tipo}
                        </button>
                      ))}
                    </div>
                  </div>
                </div>

                <div className="row h-30">
                  <label>Capacidad total:</label>
                  <div className="relative w-fit mt-0.5 mb-1">
                    <input type="number" className="border rounded pl-2 pr-8 py-1 text-gray-700 w-28" 
                      value={capacidad}
  onChange={(e) => setCapacidad(Number(e.target.value))}
                    />
                    <Users className="absolute right-2 top-1/2 transform -translate-y-1/2 text-gray-500" size={18} />
                  </div>

                  <div className="relative w-fit mt-0.5">
                    <p>Créditos:</p>
                    <div className="flex gap-4 text-sm text-gray-700 mt-1">
                      {[1, 2, 3].map((num) => (
                        <label key={num} className="flex items-center gap-1">
                          <input
                            type="radio"
                            name="creditos"
                            value={num}
                            className="accent-blue-950"
                            checked={creditos === num}
                            onChange={() => setCreditos(num)}
                          />
                          {num}
                        </label>
                      ))}
                    </div>
                  </div>
                </div>

<div className="grid grid-cols-2 w-145 mt-1 z-10">
  <div className="relative w-fit">
    <p>Horario:</p>
    <div className="flex items-center gap-2">
      <input
        type="time"
        value={horaInicio}
        onChange={(e) => setHoraInicio(e.target.value)}
        className="appearance-none text-sm border border-blue-950 rounded px-1 py-1 bg-gray-100 text-gray-700 w-29"
      />
      <span>-</span>
      <input
        type="time"
        value={horaFin}
        onChange={(e) => setHoraFin(e.target.value)}
        className="appearance-none text-sm border border-blue-950 rounded px-1 py-1 bg-gray-100 text-gray-700 w-29"
      />
    </div>
  </div>

  <div className="flex flex-col items-end mt-5 mr-4">
    <button
      className="bg-blue-950 text-white px-6 py-2 rounded hover:bg-blue-800 transition w-50"
      onClick={handleSubmit}
    >
      Crear Actividad
    </button>
    {successMessage && (
      <p className="text-sm text-green-700 mt-2 font-medium">{successMessage}</p>
    )}
  </div>
</div>

              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
