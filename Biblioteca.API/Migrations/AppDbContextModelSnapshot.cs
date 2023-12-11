﻿// <auto-generated />
using System;
using Biblioteca.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Biblioteca.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Biblioteca.API.Models.BibliotecarioModel", b =>
                {
                    b.Property<int>("BibliotecarioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BibliotecarioID"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BibliotecarioID");

                    b.ToTable("Bibliotecario", (string)null);
                });

            modelBuilder.Entity("Biblioteca.API.Models.ClienteModel", b =>
                {
                    b.Property<int>("ClienteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClienteID"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClienteID");

                    b.ToTable("Cliente", (string)null);
                });

            modelBuilder.Entity("Biblioteca.API.Models.EmprestimoModel", b =>
                {
                    b.Property<int>("EmprestimoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmprestimoID"));

                    b.Property<int>("BibliotecarioID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataDevolucaoPrevista")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataEmprestimo")
                        .HasColumnType("datetime2");

                    b.Property<int>("LivroID")
                        .HasColumnType("int");

                    b.Property<string>("NomeUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmprestimoID");

                    b.HasIndex("BibliotecarioID");

                    b.HasIndex("LivroID");

                    b.ToTable("Emprestimo", (string)null);
                });

            modelBuilder.Entity("Biblioteca.API.Models.LivroModel", b =>
                {
                    b.Property<int>("LivroID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LivroID"));

                    b.Property<int>("AnoPublicacao")
                        .HasColumnType("int");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Autor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuantidadeDisponivel")
                        .HasColumnType("int");

                    b.Property<string>("TituloLivro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LivroID");

                    b.ToTable("Livro", (string)null);
                });

            modelBuilder.Entity("Biblioteca.API.Models.EmprestimoModel", b =>
                {
                    b.HasOne("Biblioteca.API.Models.BibliotecarioModel", "Bibliotecario")
                        .WithMany()
                        .HasForeignKey("BibliotecarioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Biblioteca.API.Models.LivroModel", "Livro")
                        .WithMany()
                        .HasForeignKey("LivroID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bibliotecario");

                    b.Navigation("Livro");
                });
#pragma warning restore 612, 618
        }
    }
}
