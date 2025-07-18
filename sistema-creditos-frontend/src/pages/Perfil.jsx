import { useState, useEffect } from "react";
import FrameImg from "../images/Frame.png";
import { Pencil } from "lucide-react";
import Toast from "../components/Toast";

export default function EditProfile() {
  const [rol] = useState(() => localStorage.getItem("rol"));

  const ids = {
    Alumno: localStorage.getItem("alumnoId"),
    Departamento: localStorage.getItem("departamentoId"),
    Coordinador: localStorage.getItem("coordinadorId"),
  };

  const storedData = {
    Alumno: JSON.parse(localStorage.getItem("alumnoInfo")),
    Departamento: JSON.parse(localStorage.getItem("departamentoInfo")),
    Coordinador: JSON.parse(localStorage.getItem("coordinadorInfo")),
  };

  { /* Se guarda la información correspondiente (que se mostrará en los campos dependiendo del usuario) */ }
  const [info, setInfo] = useState(storedData[rol]);

  const [editedInfo, setEditedInfo] = useState({})
  const [editMode, setEditMode] = useState(false)

  const [careers, setCareers] = useState([]);

  const [toast, setToast] = useState(null);

  { /* Función para cargar los datos del usuario */ }
  const fetchData = async () => {
    try {
      const id = ids[rol];
      const url = id ? `https://localhost:7238/api/${rol}/${id}` : null;
      if (!url) return;

      const response = await fetch(url, {
        headers: { Accept: "application/json" },
      });

      if (!response.ok) throw new Error("Error al obtener usuario");

      const data = await response.json();
      setInfo(data);
    } catch (error) {
      console.error("Error al cargar datos del usuario:", error);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  { /* Obtener las carreras para el dropdown en modo edición */ }
  useEffect(() => {
    const fetchCareers = async () => {
      try {
        const res = await fetch("https://localhost:7238/api/Carrera/carreras");

        const data = await res.json();
        setCareers(data);
      } catch (err) {
        console.error("Error fetching careers:", err);
      }
    };

    fetchCareers();
  }, []);

  { /* Manejar botón de edición, al dar clic se habilita el modo de edición */ }
  const handleEditClick = () => {
    setEditedInfo({ ...info })
    setEditMode(true)
  }

  { /* Manejar botón de cancelación, al dar clic se deshabilita el modo de edición */ }
  const handleCancel = () => {
    setEditMode(false)
  }

  {/* Al guardar se hace una petición PUT para actualizar los datos cambiados */ }
  const handleSave = async () => {

    // verificar si hubo modificaciones
    const noChanges = Object.keys(editedInfo).every(key => editedInfo[key] === info[key])

    if (noChanges) {
      setToast({ message: "No has ingresado ningún cambio", type: "warning" });
      return;
    }

    try {
      const url = `https://localhost:7238/api/${rol}/${info.id}`;

      const response = await fetch(url, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(editedInfo),
      });

      console.log(editedInfo)

      if (!response.ok) throw new Error('Error al guardar cambios');

      setToast({ message: "Perfil actualizado", type: "success" });
      // al actualizarse, se cargan de nuevo los datos.
      await fetchData();
      setEditMode(false);

    } catch (error) {
      console.error(error);
      setToast({ message: "Error al actualizar datos", type: "error" });
    }
  }

  return (
    <div className="flex flex-col gap-6 w-full">
      {/* Título */}
      <div className="flex justify-between items-center bg-gray-200 rounded-xl p-6">
        <h1 className="text-3xl font-bold  text-gray-900 custom-heading">
          Editar Perfil
        </h1>
      </div>

      <div className="flex items-center justify-between w-full px-6">
        <div className="flex items-center gap-4">
          <img
            src={FrameImg}
            className="w-45 h-45 rounded-full object-cover"
          />

          {(rol === "Alumno" || rol === "Coordinador") && (
            <>
              {editMode ? (
                <>
                  <input
                    value={editedInfo.nombre || ""}
                    onChange={(e) => setEditedInfo({ ...editedInfo, nombre: e.target.value })}
                    className="border p-2"
                    placeholder="Nombre"
                  />

                  <input
                    value={editedInfo.apellido || ""}
                    onChange={(e) => setEditedInfo({ ...editedInfo, apellido: e.target.value })}
                    className="border p-2"
                    placeholder="Apellido"
                  />
                </>
              ) : (
                <span className="font-bold text-[40px] text-[#0A1128]">
                  {info.nombre + " " + info.apellido}
                </span>
              )}
            </>
          )}

          {rol === "Departamento" && (
            <>
              {editMode ? (
                <>
                  <input
                    value={editedInfo.nombre || ""}
                    onChange={(e) => setEditedInfo({ ...editedInfo, nombre: e.target.value })}
                    className="border p-2"
                    placeholder="Nombre del departamento"
                  />
                </>
              ) : (
                <span className="font-bold text-[40px] text-[#0A1128]">
                  {info.nombre}
                </span>
              )}
            </>
          )}


        </div>
        <Pencil
          strokeWidth={0.5}
          className="w-10 h-10 cursor-pointer"
          onClick={handleEditClick}
        />
      </div>


      <hr className="my-4 border-gray-300" />

      { /* Datos */}

      <div className="grid grid-cols-2 gap-y-4 gap-x-8 w-full px-6">
        {rol === "Alumno" && (
          <>
            <h3 className="font-bold text-[30px] text-[#001F54]">Número de control</h3>
            {editMode ? (
              <input
                value={editedInfo.numeroControl || ""}
                onChange={(e) => setEditedInfo({ ...editedInfo, numeroControl: e.target.value })}
                className="border p-2"
                placeholder="Número de control"
              />
            ) : (
              <h3 className="font-bold text-[30px] text-[#3F3F3F]">{info.numeroControl}</h3>
            )}

            <h3 className="font-bold text-[30px] text-[#001F54]">Carrera</h3>
            {editMode ? (
              <>
                <select
                  value={editedInfo.carreraId || ""}
                  onChange={(e) =>
                    setEditedInfo({ ...editedInfo, carreraId: parseInt(e.target.value) })
                  }
                  className="border p-2 text-lg"
                >
                  <option value="" disabled>Selecciona una carrera</option>
                  {careers
                    .filter(carrera => carrera.id !== 14)
                    .map((carrera) => (
                      <option key={carrera.id} value={carrera.id}>
                        {carrera.nombre}
                      </option>
                    ))}
                </select>
              </>
            ) : (
              <h3 className="font-bold text-[30px] text-[#3F3F3F]">{info.carreraNombre}</h3>
            )}
          </>
        )}

        { /* Datos editables por cualquier tipo de usuario */}

        <h3 className="font-bold text-[30px] text-[#001F54]">Correo electrónico</h3>
        {editMode ? (
          <input
            value={editedInfo.correoElectronico || ""}
            onChange={(e) => setEditedInfo({ ...editedInfo, correoElectronico: e.target.value })}
            className="border p-2"
            placeholder="Correo electrónico"
          />
        ) : (
          <h3 className="font-bold text-[30px] text-[#3F3F3F]">{info.correoElectronico}</h3>
        )}

        <h3 className="font-bold text-[30px] text-[#001F54]">Contraseña</h3>
        {editMode ? (
          <>
            <div className="flex justify-between w-full">
              <input
                value={editedInfo.currentPassword || ""}
                onChange={(e) =>
                  setEditedInfo({ ...editedInfo, currentPassword: e.target.value })
                }
                className="w-[250px] px-4 py-2 border border-[#001F54] rounded-lg"
                type="password"
                placeholder="Contraseña actual"

              />
              <input
                value={editedInfo.newPassword || ""}
                onChange={(e) =>
                  setEditedInfo({ ...editedInfo, newPassword: e.target.value })
                }
                className="w-[250px] px-4 py-2 border border-[#001F54] rounded-lg"
                type="password"
                placeholder="Nueva contraseña"
              />

            </div>
          </>
        ) : (
          <h3 className="font-bold text-[30px] text-[#3F3F3F]">*****</h3>
        )}
      </div>

      {toast && (
        <Toast
          message={toast.message}
          type={toast.type}
          onClose={() => setToast(null)}
        />
      )}

      {editMode ? (
        <div className="flex gap-4 mt-4">
          <button onClick={handleSave} className="bg-[#001F54] text-white px-6 py-4 rounded-lg text-lg font-bold cursor-pointer">
            Guardar Cambios
          </button>

          <button onClick={handleCancel} className="bg-gray-400 text-white px-6 py-4 rounded-lg text-lg font-bold cursor-pointer">
            Cancelar
          </button>
        </div>
      ) : (
        <div />
      )}

    </div>
  )
}