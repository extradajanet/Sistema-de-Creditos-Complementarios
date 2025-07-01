import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from "./components/Layout";
import Home from "./pages/Home";
import Cursos from "./pages/Cursos";
import Perfil from "./pages/Perfil";
import Login from "./pages/Auth/Login";
import Register from "./pages/Auth/Register";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/" element={<Layout />}>
          <Route index element={<Home />} />
          <Route path="/cursos" element={<Cursos />} />
          <Route path="/perfil" element={<Perfil />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
