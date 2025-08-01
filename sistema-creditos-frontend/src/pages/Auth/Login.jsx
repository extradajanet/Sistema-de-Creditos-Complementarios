import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { EyeClosed, Eye } from "lucide-react";
import Modal from "../../components/Modal";
import "/src/App.css";

function parseJwt(token) {
  try {
    const base64Url = token.split(".")[1];
    const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split("")
        .map((c) => "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2))
        .join("")
    );
    return JSON.parse(jsonPayload);
  } catch {
    return null;
  }
}


function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [showModal, setShowModal] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const navigate = useNavigate();

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
      console.log("Login response:", data);
      // Guarda el token en localStorage
      localStorage.setItem("token", data.token);
      localStorage.setItem("alumnoId", data.alumnoId);
      localStorage.setItem("departamentoId", data.departamentoId);
      localStorage.setItem("coordinadorId", data.coordinadorId);

      // Decodifica el token manualmente
      const decoded = parseJwt(data.token);
      const roleClaim =
        "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
      const userRole = decoded ? decoded[roleClaim] : null;

      if (userRole) {
        localStorage.setItem("rol", userRole);
      }

      navigate("/home");
    } catch (err) {
      console.error(err);
      setError(err.message);
    }
  };

  return (
    <>
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
                className="flex flex-col items-center space-y-3"
                onSubmit={handleSubmit}
              >
                <div className="flex flex-col">
                  <label
                    htmlFor="username"
                    className="mb-1 text-left text-sm font-semibold text-[#001F54]"
                  >
                    Número de control
                  </label>
                  <input
                    type="text"
                    value={username}
                    placeholder="Número de control"
                    onChange={(e) => setUsername(e.target.value)}
                    required
                    className="w-[250px] px-4 py-2 border border-[#001F54] rounded-lg"
                  />
                </div>
                <div className="flex flex-col">
                  <label
                    htmlFor="password"
                    className="mb-1 text-sm font-semibold text-[#001F54]"
                  >
                    Contraseña
                  </label>
                  <div className="relative w-[250px]">
                    <input
                      type={ showPassword ? "text" : "password"}
                      value={password}
                      placeholder="Contraseña"
                      onChange={(e) => setPassword(e.target.value)}
                      className="w-full px-4 py-2 border border-[#001F54] rounded-lg pr-10"
                    />
                    <div
                      onClick={() => setShowPassword((prev) => !prev)}
                      className="absolute right-3 top-1/2 -translate-y-1/2 text-[#001F54] cursor-pointer"
                    >
                      {showPassword ? <EyeClosed /> : <Eye />}
                    </div>
                  </div>

                </div>
                <p className="custom-smtext font-semibold text-[#001F54] underline cursor-pointer">
                  <a onClick={() => {
                    setShowModal(true);
                  }}>¿Olvidaste tu contraseña?</a>
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
                {error && <p className="text-red-600 mt-2">{error}</p>}
              </form>
            </div>
          </div>
        </div>
      </div>
      <Modal
        show={showModal}
        onClose={() => setShowModal(false)}
        title="¿Olvidaste tu contraseña?"
        className="bg-[#001F54] text-white w-150 p-4"
        closeButtonClassName="text-white hover:text-gray-900"
      >
        <div className="mb-4 text-center whitespace-pre-line">
          <h1 className="text-2xl">Por favor, comunícate con el Departamento de Conectividad en el Centro de Cómputo para restablecer tu contraseña.</h1>
        </div>
      </Modal>
    </>
  );
}

export default Login;
