using System;
using System.Collections.Generic;
using CriptoTrabalhoFinalInfraestrutura.infraestrutura;
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

            modelBuilder.Entity("CriptoTrabalhoFinalInfraestrutura.Entities.LogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<List<string>>("Criptos")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .HasColumnName("criptos")
                        .HasConversion(
                            value => LogEntryConversions.SerializeCriptos(value),
                            value => LogEntryConversions.DeserializeCriptos(value));

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

            modelBuilder.Entity("CriptoTrabalhoFinalInfraestrutura.Entities.LogEntry", b =>
                {
                    b.Property<List<string>>("Criptos").Metadata.SetValueComparer(LogEntryConversions.CriptosComparer);
                });
#pragma warning restore 612, 618
        }
    }
}
