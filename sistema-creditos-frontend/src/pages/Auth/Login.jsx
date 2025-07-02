import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import "/src/App.css";

function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();
  const [error, setError] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");
    try {
      const response = await fetch("https://localhost:7238/api/Auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ Usuario: username, Password: password }),

      });

      if (!response.ok) {
        throw new Error("Error de autenticación");
      }

      const data = await response.json();
      // Guarda el token en localStorage
      localStorage.setItem("token", data.token);
      // Redirige, por ejemplo, a la página principal u otra protegida
      navigate("/");
    } catch (err) {
      console.error(err);
      setError(err.message);
    }
  };

  return (
    <div className="h-screen flex items-center justify-center bg-gradient-to-b from-[#FFFFFF] to-[#999999]">
      <div className="bg-white rounded-lg shadow-lg flex w-[800px] h-[450px] overflow-hidden">
        <div className="w-1/2 bg-gradient-to-b from-[#001F54] to-[#0045BA] flex items-center justify-center text-white">
          <img src="/src/images/logo2.png" alt="logo" />
        </div>

        {/* Login/Register Form*/}
        <div className="w-1/2 flex flex-col justify-center pt-2">
          <h3 className="custom-heading text-xl font-bold text-[#001F54] text-center mb-10">
            ¡Bienvenido!
          </h3>
          <div className="p-6 pt-2 ">
            <form
              className="flex flex-col items-center space-y-3 "
              onSubmit={handleSubmit}
            >
              <input
                type="text"
                value={username}
                placeholder="Número de control"
                onChange={(e) => setUsername(e.target.value)}
                required
                className="w-[250px] px-4 py-2 border border-[#001F54] rounded-lg"
              />
              <input
                type="password"
                value={password}
                placeholder="Contraseña"
                onChange={(e) => setPassword(e.target.value)}
                className="w-[250px] px-4 py-2 border border-[#001F54] rounded-lg"
              />
              <p className="custom-smtext font-semibold text-[#001F54] underline">
                <a href="/">¿Olvidaste tu contraseña?</a>
              </p>
              <div className="pt-2">
                <Link to={"/register"}>
                  <button className="custom-text font-bold w-[120px] bg-[#001F54] border border-2 border-[#001F54] text-white py-2 rounded-xl hover:bg-[#1282A2] transition mr-5">
                    Registrarse
                  </button>
                </Link>
                <button
                  className="custom-text font-bold w-[120px] bg-white border border-2 border-[#001F54] text-[#001F54] py-2 rounded-xl hover:bg-[#1282A2] transition"
                  type="submit"
                >
                  Login
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
}
export default Login;
