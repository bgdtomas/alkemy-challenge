using alkemyChallengeCSharp.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace alkemyChallengeCSharp.Database
{
    public class AcademiaDbContext : DbContext 
    {           
        public AcademiaDbContext(DbContextOptions<AcademiaDbContext> options) : base(options)
        {

        }

        #region DbSets

        public DbSet<Admin> Administradores { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Profesor> Profesores{ get; set; }

        #endregion
    }
}
