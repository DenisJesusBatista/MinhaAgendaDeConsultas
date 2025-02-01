﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MinhaAgendaDeConsultas.Infraestrutura.Migrations
{
    [DbContext(typeof(MinhaAgendaDeConsultasContext))]
    [Migration("20250201021940_AddCpfColumnToMedicoAndPacienteTest35")]
    partial class AddCpfColumnToMedicoAndPacienteTest35
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MinhaAgendaDeConsultas.Domain.Entidades.EntidadeBase", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.HasKey("Id");

                    b.ToTable("EntidadeBase");
                });

            modelBuilder.Entity("MinhaAgendaDeConsultas.Domain.Entidades.Usuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("Identificador")
                        .HasColumnType("uuid");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<int>("Tipo")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Usuario", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("MinhaAgendaDeConsultas.Domain.Entidades.Medico", b =>
                {
                    b.HasBaseType("MinhaAgendaDeConsultas.Domain.Entidades.Usuario");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Crm")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("UsuarioMedico", (string)null);
                });

            modelBuilder.Entity("MinhaAgendaDeConsultas.Domain.Entidades.Paciente", b =>
                {
                    b.HasBaseType("MinhaAgendaDeConsultas.Domain.Entidades.Usuario");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.ToTable("UsuarioPaciente", (string)null);
                });

            modelBuilder.Entity("MinhaAgendaDeConsultas.Domain.Entidades.Medico", b =>
                {
                    b.HasOne("MinhaAgendaDeConsultas.Domain.Entidades.Usuario", null)
                        .WithOne()
                        .HasForeignKey("MinhaAgendaDeConsultas.Domain.Entidades.Medico", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MinhaAgendaDeConsultas.Domain.Entidades.Paciente", b =>
                {
                    b.HasOne("MinhaAgendaDeConsultas.Domain.Entidades.Usuario", null)
                        .WithOne()
                        .HasForeignKey("MinhaAgendaDeConsultas.Domain.Entidades.Paciente", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
