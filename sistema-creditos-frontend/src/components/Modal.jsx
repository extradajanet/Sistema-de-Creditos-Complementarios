import React from "react";

// Modal.jsx
export default function Modal({ show, onClose, title, children, className = "" }) {
  if (!show) return null;

  return (
    <div className="fixed inset-0 bg-opacity-50 flex justify-center items-center z-50 ">
      <div className={`bg-gray-200 border border-blue-950 rounded-xl relative shadow-lg ${className}`}>
        {/* Encabezado */}
        <div className="p-6 border-b border-gray-200 relative text-center">
          <h2 className="text-xl font-bold text-black ">{title}</h2>
          <button
            onClick={onClose}
            className="absolute top-6 right-6 text-gray-500 hover:text-red-600 text-xl font-bold"
          >
            ×
          </button>
        </div>

        {/* Contenido */}
        <div className="p-6">
          {children}
        </div>
      </div>
    </div>
  );
}
