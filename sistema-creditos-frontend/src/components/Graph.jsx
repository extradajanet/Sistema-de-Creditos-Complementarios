

import React from "react";
import {
  PieChart,
  Pie,
  Cell,
  ResponsiveContainer
} from "recharts";

const COLORS = ["#001F54", "#1282A2"]; // Main and second segment color

const Graph = ({ obtained, total }) => {
  const data = [
    { name: "Obtenidos", value: obtained },
    { name: "Faltantes", value: total - obtained }
  ];

  return (
    <div className="relative w-[400px] h-[200px]">
      <ResponsiveContainer width="100%" height="100%">
        <PieChart>
          <Pie
            data={data}
            innerRadius={50}
            outerRadius={90}
            paddingAngle={2}
            dataKey="value"
            stroke="none"
          >
            {data.map((entry, index) => (
              <Cell key={`cell-${index}`} fill={COLORS[index]} />
            ))}
          </Pie>
        </PieChart>
      </ResponsiveContainer>

      {/* Center Text */}
      <div className="custom-text absolute top-[50%] left-[50%] translate-x-[-50%] translate-y-[-50%] text-center font-bold text-black">
        <div>Creditos</div>
        <div>Obtenidos</div>
        <div className="text-2xl">{obtained}</div>
      </div>
    </div>
  );
};


export default Graph;
