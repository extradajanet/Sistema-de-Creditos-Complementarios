import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "/src/App.css";

function Register() {
  const [name, setName] = useState("");
  const [surnname, setSurname] = useState("");
  const [numcontrol, setNumcontrol] = useState("");
  const [email, setEmail] = useState("");
  const [career, setCareer] = useState("");
  const [password, setPassword] = useState("");
  const [confirmpassword, setConfirmpassword] = useState("");
  const [error, setError] = useState("");
  const [careers, setCareers] = useState([]); 
  const navigate = useNavigate();

  useEffect(() => {
    const fetchCareers = async () => {
      try {
        const res = await fetch("http://localhost:5159/api/Careers"); 
        const data = await res.json();
        setCareers(data);
      } catch (err) {
        console.error("Error fetching careers:", err);
      }
    };

    fetchCareers();
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");

    if (password !== confirmpassword) {
      setError("Las contraseñas no coinciden");
      return;
    }

    try {
      const response = await fetch("http://localhost:5159/api/Account/register", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          name,
          surnname,
          numcontrol,
          email,
          career,
          password,
        }),
      });

      if (!response.ok) {
        throw new Error("Error al registrarse");
      }

      await response.json();
      navigate("/login");
    } catch (err) {
      console.error(err);
      setError(err.message);
    }
  };

  return (
    <div className="h-screen flex items-center justify-center bg-gradient-to-b from-[#FFFFFF] to-[#999999]">
      <div className="bg-white rounded-lg shadow-lg flex w-[900px] h-[550px] overflow-hidden">
        <div className="w-1/2 bg-gradient-to-b from-[#001F54] to-[#0045BA] flex items-center justify-center text-white">
          <img src="/src/images/logo2.png" alt="logo" />
        </div>
        {/* Register Form */}
        <div className="w-1/2 flex flex-col justify-center pt-2">
          <h3 className="custom-heading text-xl font-bold text-[#001F54] text-center mb-8">
            ¡Regístrate!
          </h3>
          <div className="p-2 pt-2">
            <form
              className="flex flex-col items-center space-y-3"
              onSubmit={handleSubmit}
            >
              <input
                type="text"
                value={name}
                onChange={(e) => setName(e.target.value)}
                placeholder="Nombre"
                required
                className="w-[250px] px-4 py-2 border border-[#001F54] rounded-md"
              />
              <input
                type="text"
                value={surnname}
                onChange={(e) => setSurname(e.target.value)}
                placeholder="Apellidos"
                required
                className="w-[250px] px-4 py-2 border border-[#001F54] rounded-lg"
              />
              <input
                type="text"
                value={numcontrol}
                onChange={(e) => setNumcontrol(e.target.value)}
                placeholder="Número de Control"
                required
                className="w-[250px] px-4 py-2 border border-[#001F54] rounded-lg"
              />
              <input
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                placeholder="Correo electrónico"
                required
                className="w-[250px] px-4 py-2 border border-[#001F54] rounded-lg"
              />

              <select
                value={career}
                onChange={(e) => setCareer(e.target.value)}
                
      
                className="w-[250px] px-4 py-2 border border-[#001F54] rounded-lg bg-white text-gray-500"
              >
                <option value="" disabled hidden>Selecciona tu carrera</option>
                {careers.map((c) => (
                  <option key={c.id} value={c.name}>
                    {c.name}
                  </option>
                ))}
              </select>

              <input
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="Contraseña"
                required
                className="w-[250px] px-4 py-2 border border-[#001F54] rounded-lg"
              />
              <input
                type="password"
                value={confirmpassword}
                onChange={(e) => setConfirmpassword(e.target.value)}
                placeholder="Confirmar Contraseña"
                required
                className="w-[250px] px-4 py-2 border border-[#001F54] rounded-lg"
              />

              {error && (
                <p className="text-red-500 text-sm text-center">{error}</p>
              )}

              <div className="pt-2">
                <button
                  type="submit"
                  className="custom-text font-bold w-[120px] bg-[#001F54] border border-2 border-[#001F54] text-white py-2 rounded-xl hover:bg-[#1282A2] transition"
                >
                  Crear Cuenta
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Register;
