import { X } from "lucide-react";
import React from "react";

// Modal.jsx
export default function Modal({ show, onClose, title, children, className = "" }) {
  if (!show) return null;

  return (
    <div className="fixed inset-0 bg-opacity-80 flex justify-center items-center z-50 ">
      <div className={`bg-[#001F54] border border-blue-950 rounded-xl relative shadow-lg ${className}`}>
        <div className="p-2">
          <button
            onClick={onClose}
            className="absolute top-2 right-2 text-white hover:text-red-600 text-xl font-bold"
          >
            <X className="w-5 h-5"/>
          </button>
        </div>
        {/* Encabezado */}
        <div className="p-2 relative text-center">
          <h2 className="custom-subheading2 font-semibold text-white ">{title}</h2>
        </div>

        {/* Contenido */}
        <div className=" h-full">
          {children}
        </div>
      </div>
    </div>
  );
}
