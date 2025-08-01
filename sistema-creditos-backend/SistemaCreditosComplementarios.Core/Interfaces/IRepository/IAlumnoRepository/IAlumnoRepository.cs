﻿using SistemaCreditosComplementarios.Core.Models;
using SistemaCreditosComplementarios.Core.Models.Alumnos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditosComplementarios.Core.Interfaces.IRepository.IAlumnoRepository
{
    public interface IAlumnoRepository
    {
        Task<IEnumerable<Alumno>> GetAllAsync();
        Task<IEnumerable<Alumno>> GetByCarreraIdsAsync(IEnumerable<int> carreraIds);

        Task<Alumno> GetByIdAsync(int id);
        Task<Alumno> GetByUserIdAsync(string userId);
        Task AddAsync(Alumno alumno);
        Task<Alumno> UpdateAsync(Alumno alumno);
        Task DeleteAsync(int id);
    }
}
