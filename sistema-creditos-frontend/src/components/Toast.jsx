import { useEffect } from "react";
import { X } from "lucide-react";

export default function Toast({ message, type = "success", onClose }) {
    useEffect(() => {
        const timer = setTimeout(onClose, 3000); // Cierra después de 3s
        return () => clearTimeout(timer);
    }, [onClose]);

    const bgColor = {
        success: "bg-green-300",
        error: "bg-red-300",
        info: "bg-blue-300",
        warning: "bg-yellow-300",
    }[type];

    return (
        <div className={`fixed top-10 right-10 z-50 px-4 py-3 rounded-lg font-semibold ${bgColor} flex items-center justify-between min-w-[300px]`}>
            <span>{message}</span>
            <button onClick={onClose} className="ml-4 hover:text-gray-800 text-lg font-bold cursor-pointer">
                <X />
            </button>
        </div>
    );
}