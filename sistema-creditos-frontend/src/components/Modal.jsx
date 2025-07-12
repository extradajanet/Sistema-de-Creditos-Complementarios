import { X } from "lucide-react";
import React from "react";

// Modal.jsx
export default function Modal({ show, onClose, title, children, className = "",closeButtonClassName = "" }) {
  if (!show) return null;

  return (
    <div className="fixed inset-0 bg-opacity-80 flex justify-center items-center z-50 ">
      <div className={` border border-blue-950 rounded-xl relative shadow-lg ${className}`}>
        <div className="p-2">
          <button
            onClick={onClose}
            className={`absolute top-2 cursor-pointer right-2 font-bold ${closeButtonClassName}`}
          >
            <X className="w-8 h-8 hover:text-red-600" onClick={onClose}/>
          </button>
        </div>
        {/* Encabezado */}
        <div className="p-2 relative text-center">
          <h2 className="custom-subheading2 font-semibold  ">{title}</h2>
        </div>

        {/* Contenido */}
        <div className=" h-full">
          {children}
        </div>
      </div>
    </div>
  );
}
