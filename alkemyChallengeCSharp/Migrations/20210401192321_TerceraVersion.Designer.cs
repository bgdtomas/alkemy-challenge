﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using alkemyChallengeCSharp.Database;

namespace alkemyChallengeCSharp.Migrations
{
    [DbContext(typeof(AcademiaDbContext))]
    [Migration("20210401192321_TerceraVersion")]
    partial class TerceraVersion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("alkemyChallengeCSharp.Models.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Legajo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Password")
                        .HasColumnType("BLOB");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Administradores");
                });

            modelBuilder.Entity("alkemyChallengeCSharp.Models.Alumno", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Legajo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Password")
                        .HasColumnType("BLOB");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Alumnos");
                });

            modelBuilder.Entity("alkemyChallengeCSharp.Models.Materia", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AlumnoId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Horario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxAlumnos")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProfesorId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AlumnoId");

                    b.HasIndex("ProfesorId");

                    b.ToTable("Materias");
                });

            modelBuilder.Entity("alkemyChallengeCSharp.Models.Profesor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Profesores");
                });

            modelBuilder.Entity("alkemyChallengeCSharp.Models.RegistroAlumnosInscriptos", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AlumnoId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MateriaId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AlumnoId");

                    b.HasIndex("MateriaId");

                    b.ToTable("RegistroAlumnosInscriptos");
                });

            modelBuilder.Entity("alkemyChallengeCSharp.Models.Materia", b =>
                {
                    b.HasOne("alkemyChallengeCSharp.Models.Alumno", null)
                        .WithMany("Materias")
                        .HasForeignKey("AlumnoId");

                    b.HasOne("alkemyChallengeCSharp.Models.Profesor", "Profesor")
                        .WithMany()
                        .HasForeignKey("ProfesorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profesor");
                });

            modelBuilder.Entity("alkemyChallengeCSharp.Models.RegistroAlumnosInscriptos", b =>
                {
                    b.HasOne("alkemyChallengeCSharp.Models.Alumno", "Alumno")
                        .WithMany()
                        .HasForeignKey("AlumnoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("alkemyChallengeCSharp.Models.Materia", "Materia")
                        .WithMany()
                        .HasForeignKey("MateriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Alumno");

                    b.Navigation("Materia");
                });

            modelBuilder.Entity("alkemyChallengeCSharp.Models.Alumno", b =>
                {
                    b.Navigation("Materias");
                });
#pragma warning restore 612, 618
        }
    }
}
