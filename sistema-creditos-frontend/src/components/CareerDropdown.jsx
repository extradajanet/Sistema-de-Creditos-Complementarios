import { Listbox } from "@headlessui/react";
import { useState } from "react";
import { ChevronDown } from 'lucide-react';

export default function CareerDropdown({ careers = [], onChange }) {
  const [selected, setSelected] = useState(null);

  return (
    <div className="w-[250px]">
      <Listbox value={selected} onChange={(value) => {
        setSelected(value);
        onChange(value);
      }}>
        <div className="relative mt-1">
          <Listbox.Button className="relative w-full cursor-pointer rounded-lg border border-[#001F54] bg-white py-2 pl-3 pr-10 text-left text-black shadow-md focus:outline-none">
            <span>{selected ? selected.nombre : "Selecciona tu carrera"}</span>
            <span className="absolute inset-y-0 right-0 flex items-center pr-2">
              <ChevronDown className="h-5 w-5 text-gray-400" />
            </span>
          </Listbox.Button>
          <Listbox.Options className="absolute mt-1 max-h-40 w-full overflow-y-auto rounded-md bg-white py-1 text-base shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none sm:text-sm">
            {careers.map((career) => (
              <Listbox.Option
                key={career.id}
                value={career}
                className={({ active }) =>
                  `relative cursor-pointer select-none py-2 pl-10 pr-4 ${
                    active ? "bg-blue-100 text-blue-900" : "text-gray-900"
                  }`
                }
              >
                {career.nombre}
              </Listbox.Option>
            ))}
          </Listbox.Options>
        </div>
      </Listbox>
    </div>
  );
}
