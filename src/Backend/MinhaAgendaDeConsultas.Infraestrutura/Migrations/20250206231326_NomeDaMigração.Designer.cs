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
    [Migration("20250206231326_NomeDaMigração")]
    partial class NomeDaMigração
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

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("character varying(21)");

                    b.HasKey("Id");

                    b.ToTable("EntidadeBase");

                    b.HasDiscriminator().HasValue("EntidadeBase");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("MinhaAgendaDeConsultas.Domain.Entidades.Medico", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Crm")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Especialidade")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Tipo")
                        .HasColumnType("integer");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UsuarioMedico", (string)null);
                });

            modelBuilder.Entity("MinhaAgendaDeConsultas.Domain.Entidades.Paciente", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Tipo")
                        .HasColumnType("integer");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UsuarioPaciente", (string)null);
                });

            modelBuilder.Entity("MinhaAgendaDeConsultas.Domain.Entidades.RefreshToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataExpiracao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("RefreshToken", (string)null);
                });

            modelBuilder.Entity("MinhaAgendaDeConsultas.Domain.Entidades.Usuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("Identificador")
                        .HasColumnType("uuid");

                    b.Property<string>("IdentificadorString")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<int>("Tipo")
                        .HasColumnType("integer");

                    b.Property<string>("Token")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.HasKey("Id");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("MinhaAgendaDeConsultas.Domain.Entidades.HorarioDisponivel", b =>
                {
                    b.HasBaseType("MinhaAgendaDeConsultas.Domain.Entidades.EntidadeBase");

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Disponivel")
                        .HasColumnType("boolean");

                    b.Property<long?>("MedicoId")
                        .HasColumnType("bigint");

                    b.HasIndex("MedicoId");

                    b.HasDiscriminator().HasValue("HorarioDisponivel");
                });

            modelBuilder.Entity("MinhaAgendaDeConsultas.Domain.Entidades.Medico", b =>
                {
                    b.HasOne("MinhaAgendaDeConsultas.Domain.Entidades.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MinhaAgendaDeConsultas.Domain.Entidades.Paciente", b =>
                {
                    b.HasOne("MinhaAgendaDeConsultas.Domain.Entidades.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MinhaAgendaDeConsultas.Domain.Entidades.RefreshToken", b =>
                {
                    b.HasOne("MinhaAgendaDeConsultas.Domain.Entidades.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MinhaAgendaDeConsultas.Domain.Entidades.HorarioDisponivel", b =>
                {
                    b.HasOne("MinhaAgendaDeConsultas.Domain.Entidades.Medico", null)
                        .WithMany("HorariosDisponiveis")
                        .HasForeignKey("MedicoId");
                });

            modelBuilder.Entity("MinhaAgendaDeConsultas.Domain.Entidades.Medico", b =>
                {
                    b.Navigation("HorariosDisponiveis");
                });
#pragma warning restore 612, 618
        }
    }
}
