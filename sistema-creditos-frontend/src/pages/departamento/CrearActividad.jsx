import React, { useState, useEffect } from "react";
import { Users } from "lucide-react";
import Modal from "../../components/Modal";
import { Listbox } from "@headlessui/react";
import { Check, ChevronDown } from "lucide-react";
import { useNavigate } from "react-router-dom";
import imagen1 from "../../images/imagen1.png";
import imagen2 from "../../images/imagen2.png";
import imagen3 from "../../images/imagen3.png";
import imagen4 from "../../images/imagen4.png";
import imagen5 from "../../images/logo-mapache.jpeg";

export default function ActividadesList() {
  const navigate = useNavigate();
  const [horaInicio, setHoraInicio] = useState("");
  const [horaFin, setHoraFin] = useState("");
  const [capacidad, setCapacidad] = useState(0);
  const [tipoSeleccionado, setTipoSeleccionado] = useState("");
  const [subTipoExtraescolar, setSubTipoExtraescolar] = useState(""); // Cultural o Deportiva dentro de Extraescolar
  const [carreraIds, setCarreraIds] = useState([]);
  const [creditos, setCreditos] = useState(0);
  const [nombre, setNombre] = useState("");
  const [descripcion, setDescripcion] = useState("");
  const [fechaInicio, setFechaInicio] = useState("");
  const [fechaFin, setFechaFin] = useState("");
  const [successMessage, setSuccessMessage] = useState("");
  const [showModal, setShowModal] = useState(false);
  const [imagenSeleccionada, setImagenSeleccionada] = useState("imagen1.png");
  const [diaSeleccionado, setDiaSeleccionado] = useState("");
  const [generoSeleccionado, setGeneroSeleccionado] = useState(null);

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

  const imagenesPredeterminadas = [
    { nombre: "imagen1.png", src: imagen1 },
    { nombre: "imagen2.png", src: imagen2 },
    { nombre: "imagen3.png", src: imagen3 },
    { nombre: "imagen4.png", src: imagen4 },
    { nombre: "imagen5.png", src: imagen5 },
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

  function obtenerNumeroDia(dia) {
    switch (dia) {
      case "Lunes": return 1;
      case "Martes": return 2;
      case "Miércoles": return 3;
      case "Jueves": return 4;
      case "Viernes": return 5;
      case "Sábado": return 6;
      case "Domingo": return 7;
      default: return 0;
    }
  }

  const limpiarFormulario = () => {
    setNombre("");
    setDescripcion("");
    setFechaInicio("");
    setFechaFin("");
    setCreditos(0);
    setCapacidad(0);
    setHoraInicio("");
    setHoraFin("");
    setDiaSeleccionado("");
    setTipoSeleccionado("");
    setSubTipoExtraescolar("");
    setCarreraIds([]);
    setGeneroSeleccionado(null);
    setSuccessMessage("");
  };

  useEffect(() => {
    if (tipoSeleccionado === "Extraescolar") {
      setCarreraIds([14]); // Selecciona todas las carreras
      if (!subTipoExtraescolar) setSubTipoExtraescolar("Cultural");
    } else {
      if (carreraIds.includes(14)) setCarreraIds([]);
      setSubTipoExtraescolar("");
      setGeneroSeleccionado(null);
    }
  }, [tipoSeleccionado]);

  const handleSubmit = async () => {
    if (!fechaInicio || !fechaFin) {
      alert("Por favor selecciona las fechas de inicio y fin.");
      return;
    }

    const inicio = new Date(fechaInicio);
    const fin = new Date(fechaFin);

    if (fin < inicio) {
      alert("La fecha de fin no puede ser anterior a la fecha de inicio.");
      return;
    }

    if (tipoSeleccionado === "Extraescolar" && !generoSeleccionado) {
      alert("Por favor selecciona el género.");
      return;
    }

    let carrerasFinales = [...carreraIds];
    // if (carrerasFinales.includes(14)) { esta parte incluye todas las carreras
    //   carrerasFinales = carreras.filter((c) => c.id !== 14).map((c) => c.id);
    // }

    let tipoAct = tipoSeleccionado;
    if (tipoSeleccionado === "Extraescolar") {
      tipoAct = subTipoExtraescolar;
    }

const actividad = {
  nombre,
  descripcion,
  fechaInicio: `${fechaInicio}T00:00:00Z`,
  fechaFin: `${fechaFin}T00:00:00Z`,
  creditos,
  capacidad,
  dias: obtenerNumeroDia(diaSeleccionado),
  horaInicio: `${horaInicio}:00`,
  horaFin: `${horaFin}:00`,
  tipoActividad: obtenerTipoActividad(tipoAct),
  estadoActividad: 1,
  imagenNombre: imagenSeleccionada,
  departamentoId: 1,
  carreraIds: carrerasFinales,
};

// Agregar el género si el tipoActividad es 1 (Deportiva) o 2 (Cultural)
if ([1, 2].includes(actividad.tipoActividad)) {
  actividad.genero = generoSeleccionado;
}


    try {
      const response = await fetch("https://localhost:7238/api/Actividades", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(actividad),
      });

      if (response.ok) {
        setShowModal(true);
        limpiarFormulario();
      } else {
        const error = await response.text();
        console.error("Error al crear actividad:", error);
        alert("Error al crear actividad");
      }
    } catch (err) {
      console.error("Excepción al hacer POST:", err);
      alert("Error de red o servidor");
    }
  };

  return (
    <>
      <div className="flex flex-col gap-6 w-full">
        <div className="flex justify-between items-center bg-gray-200 rounded-xl p-6">
          <h1 className="text-3xl font-bold text-gray-900 custom-heading">Crear Actividad</h1>
        </div>

        <div className="flex justify-center">
          <div className="grid grid-cols-2 justify-items-center-safe bg-gray-200 rounded-xl p-6 border border-blue-950 h-145 w-295">
            <div className="mt-7 text-center text-blue-950 font-bold">
              <p>Selecciona una imagen</p>
              <div className="bg-gray-200 rounded-xl p-6 border border-blue-950 h-108 w-115 flex flex-col items-center">
                <img
                  src={imagenesPredeterminadas.find((img) => img.nombre === imagenSeleccionada)?.src}
                  alt="Imagen seleccionada"
                  className="w-auto h-auto object-cover mb-6 border-4 border-blue-700 rounded-lg shadow-md"
                />
                <div className="grid grid-cols-5 gap-3 px-2">
                  {imagenesPredeterminadas.map((img) => (
                    <img
                      key={img.nombre}
                      src={img.src}
                      alt={img.nombre}
                      title={img.nombre}
                      className={`w-auto h-auto object-cover rounded-md cursor-pointer border-2 transition ${
                        imagenSeleccionada === img.nombre
                          ? "border-blue-700 shadow-lg scale-105"
                          : "border-transparent hover:border-blue-400 hover:shadow-sm"
                      }`}
                      onClick={() => setImagenSeleccionada(img.nombre)}
                    />
                  ))}
                </div>
              </div>
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

                  {/* Carrera(s) */}
                  <div>
                    <label>Carrera(s):</label>
                    <div className="relative z-10">
                      <Listbox
                        value={carreraIds}
                        onChange={setCarreraIds}
                        multiple
                        disabled={tipoSeleccionado === "Extraescolar"}
                      >
                        <div className="relative">
                          <Listbox.Button className="w-full flex items-start justify-between border border-blue-950 bg-white py-2 px-3 text-sm text-gray-800 rounded h-auto min-h-[3rem]">
                            <div
                              className="max-h-[3rem] overflow-y-auto pr-4 whitespace-pre-line flex-1 text-left"
                              style={{ lineHeight: "1.5rem" }}
                            >
                              {carreraIds.length === 0
                                ? "Selecciona carrera(s)"
                                : carreraIds
                                    .map((id) => `- ${carreras.find((c) => c.id === id)?.nombre}`)
                                    .filter(Boolean)
                                    .join("\n")}
                            </div>
                            <ChevronDown className="w-5 h-5 text-gray-500 ml-2 flex-shrink-0 mt-1" />
                          </Listbox.Button>

                          {tipoSeleccionado !== "Extraescolar" && (
                            <Listbox.Options className="absolute mt-1 w-full rounded-lg bg-white border border-blue-950 shadow-lg max-h-48 overflow-auto z-20">
                              {carreras.map((carrera) => (
                                <Listbox.Option
                                  key={carrera.id}
                                  value={carrera.id}
                                  className={({ active }) =>
                                    `cursor-pointer select-none px-4 py-2 text-sm ${
                                      active ? "bg-blue-100 text-blue-900" : "text-gray-700"
                                    }`
                                  }
                                >
                                  {({ selected }) => (
                                    <div className="flex justify-between items-center">
                                      <span>{carrera.nombre}</span>
                                      {selected && <Check className="w-4 h-4 text-blue-600" />}
                                    </div>
                                  )}
                                </Listbox.Option>
                              ))}
                            </Listbox.Options>
                          )}
                        </div>
                      </Listbox>
                    </div>
                  </div>

                  {/* Día, fechas y tipo actividad */}
                  <div className="grid grid-cols-3">
                    <div className="row">
                      <p>Día:</p>
                      <div className="flex flex-col gap-1.5 text-sm text-gray-700 mt-1">
                        {["Lunes", "Martes", "Miércoles", "Jueves", "Viernes"].map((dia) => (
                          <label key={dia} className="flex items-center gap-1">
                            <input
                              type="radio"
                              name="dia"
                              value={dia}
                              checked={diaSeleccionado === dia}
                              onChange={() => setDiaSeleccionado(dia)}
                              className="accent-blue-950"
                            />
                            {dia}
                          </label>
                        ))}
                      </div>

                      <label>Horario:</label>
                      <div className="row gap-2 mt-1">
                        
                        <input
                          type="time"
                          className="border border-blue-950 rounded px-2 py-1 text-sm flex-1 "
                          value={horaInicio}
                          onChange={(e) => setHoraInicio(e.target.value)}
                        />
                        <div className="ms-14">a</div>
                
                        <input
                        
                          type="time"
                          className="border border-blue-950 rounded px-2 py-1 text-sm flex-1"
                          value={horaFin}
                          onChange={(e) => setHoraFin(e.target.value)}
                        />

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

                     <div className="z-10 mt-2 relative">
                        <label>Tipo de Actividad</label>
                        <div className="flex flex-row gap-2">
                          {["Extraescolar", "Tutorias", "MOOC"].map((tipo) => (
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

                        {/* Subtipo */}
                        {tipoSeleccionado === "Extraescolar" && (
                          <>
                            <div className="flex flex-row gap-2 mt-2">
                              {["Cultural", "Deportiva"].map((subtipo) => (
                                <button
                                  key={subtipo}
                                  type="button"
                                  className={`px-4 m-0.5 rounded border border-blue-950 text-sm font-medium transition h-8 w-28 ${
                                    subTipoExtraescolar === subtipo
                                      ? "bg-blue-950 text-white"
                                      : "bg-white text-blue-950 hover:bg-blue-100"
                                  }`}
                                  onClick={() => setSubTipoExtraescolar(subtipo)}
                                >
                                  {subtipo}
                                </button>
                              ))}
                            </div>

                            {/* Campo Género con posición absoluta */}
                            <div className="absolute top-[6.5rem] left-0">
                              <label>Género:</label>
                              <div className="flex gap-4 text-gray-700 mt-1">
                                {[{ id: 1, nombre: "Masculino" }, { id: 2, nombre: "Femenino" }].map((g) => (
                                  <label key={g.id} className="flex items-center gap-1">
                                    <input
                                      type="radio"
                                      name="genero"
                                      value={g.id}
                                      className="accent-blue-950"
                                      checked={generoSeleccionado === g.id}
                                      onChange={() => setGeneroSeleccionado(g.id)}
                                    />
                                    {g.nombre}
                                  </label>
                                ))}
                              </div>
                            </div>
                          </>
                        )}
                      </div>

                    </div>

                    <div className="row">
                      <label>Créditos:</label>
                      <div className="flex items-center gap-2 mt-1 text-gray-700 mb-3">
                        {[1, 2, 3, 4].map((num) => (
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

                      <label>Capacidad:</label>
                      <input
                        type="number"
                        className="border border-blue-950 rounded px-2 py-1 mt-0.5 text-sm w-35"
                        value={capacidad}
                        onChange={(e) => setCapacidad(Number(e.target.value))}
                        min={0}
                      />
                  <button
                    onClick={handleSubmit}
                    className="bg-blue-950 text-white px-6 py-2 rounded ms-6 mt-25 hover:bg-blue-800 transition"
                    type="button"
                  >
                    Crear actividad
                  </button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

        

        <Modal
        show={showModal}
        onClose={() => setShowModal(false)}
        title="¡La actividad ha sido creada con éxito!"
        className="bg-blue-950 text-white w-150 p-4"
        closeButtonClassName="text-white hover:text-gray-900"
      >
        <div className="mb-4 text-center whitespace-pre-line">
          <h1 className="text-2xl">¿Deseas crear otra actividad?</h1>
        </div>
        <div className="flex justify-center gap-6">
          <button
            onClick={() => {
              limpiarFormulario();
              setShowModal(false);
            }}
                    className="bottom-4 cursor-pointer right-4 p-2 bg-[#D9D9D9] w-[50px] rounded-md text-center custom-mdtext font-bold text-[#0A1128]"
          >
            Sí
          </button>
          <button
            onClick={() => {
              setShowModal(false);
              navigate("/");
            }}
            className="bottom-4 cursor-pointer right-4 p-2 bg-[#D9D9D9] w-[50px] rounded-md text-center custom-mdtext font-bold text-[#0A1128]"
          >
            No
          </button>

        </div>
      </Modal>
      </div>
    </>
  );
}
