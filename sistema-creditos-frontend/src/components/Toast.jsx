import { useEffect } from "react";
import { X, CheckCircle, Info, AlertTriangle, AlertCircle } from "lucide-react";

const icons = {
  success: <CheckCircle className="text-green-600 w-5 h-5" />,
  error: <AlertCircle className="text-red-600 w-5 h-5" />,
  info: <Info className="text-blue-600 w-5 h-5" />,
  warning: <AlertTriangle className="text-yellow-600 w-5 h-5" />,
};

const bgColors = {
  success: "bg-green-100 border-green-500",
  error: "bg-red-100 border-red-500",
  info: "bg-blue-100 border-blue-500",
  warning: "bg-yellow-100 border-yellow-500",
};

export default function Toast({ message, type = "success", onClose }) {
  useEffect(() => {
    const timer = setTimeout(onClose, 3000);
    return () => clearTimeout(timer);
  }, [onClose]);

  return (
    <div
      className={`fixed top-10 right-10 z-50 px-4 py-3 rounded-lg flex items-start gap-3 min-w-[300px] border-l-4 ${bgColors[type]} animate-slide-in`}
    >
      <div className="mt-0.5">{icons[type]}</div>
      <div className="flex-1 text-sm text-[#001F54] font-medium">{message}</div>
      <button onClick={onClose} className="ml-2 text-gray-500 hover:text-gray-700">
        <X className="w-4 h-4" />
      </button>
    </div>
  );
}
