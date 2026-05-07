using System;
using CriptoTrabalhoFinalInfraestrutura.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CriptoTrabalhoFinalInfraestrutura.Migrations
{
    [DbContext(typeof(AppDbContext))]
    public partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "10.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CriptoTrabalhoFinalInfraestrutura.Models.LogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Criptos")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .HasColumnName("criptos");

                    b.Property<DateTime>("Horario")
                        .HasColumnType("datetime2")
                        .HasColumnName("horario");

                    b.Property<string>("Mensagem")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .HasColumnName("mensagem");

                    b.HasKey("Id");

                    b.ToTable("logs");
                });
#pragma warning restore 612, 618
        }
    }
}
