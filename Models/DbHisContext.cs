﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HIS_API.Models;

public partial class DbHisContext : DbContext
{
    public DbHisContext()
    {
    }

    public DbHisContext(DbContextOptions<DbHisContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ExamenView> ExamenView { get; set; }

    public virtual DbSet<HisConfiguracion> HisConfiguracions { get; set; }

    public virtual DbSet<HisConfiguracionXexaman> HisConfiguracionXexamen { get; set; }

    public virtual DbSet<HisContraste> HisContrastes { get; set; }

    public virtual DbSet<HisContrasteXexaman> HisContrasteXexamen { get; set; }

    public virtual DbSet<HisDiagnostico> HisDiagnosticos { get; set; }

    public virtual DbSet<HisExamDerivado> HisExamDerivados { get; set; }

    public virtual DbSet<HisExaman> HisExamen { get; set; }

    public virtual DbSet<HisExamenXlateralidad> HisExamenXlateralidads { get; set; }

    public virtual DbSet<HisExamenXsolicitudExaman> HisExamenXsolicitudExamen { get; set; }

    public virtual DbSet<HisFundamento> HisFundamentos { get; set; }

    public virtual DbSet<HisLateralidad> HisLateralidads { get; set; }

    public virtual DbSet<HisRegion> HisRegions { get; set; }

    public virtual DbSet<HisSolicitudExaman> HisSolicitudExamen { get; set; }

    public virtual DbSet<HisSolicitudExamenXddiagnostico> HisSolicitudExamenXddiagnosticos { get; set; }

    public virtual DbSet<HisTipoExaman> HisTipoExamen { get; set; }

    public virtual DbSet<HisValor> HisValors { get; set; }

    public virtual DbSet<RegionXtipoExaman> RegionXtipoExamen { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=10.5.214.129;initial catalog=db_his;user id=rene;password=1779;Trusted_Connection=False;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("db_datareader");
        modelBuilder.Entity<ExamenView>().HasKey(E => E.ExamenId);
        modelBuilder.Entity<HisConfiguracion>(entity =>
        {
            entity.ToTable("HIS_Configuracion", "dbo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EsVigente).HasColumnName("esVigente");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<HisConfiguracionXexaman>(entity =>
        {
            entity.ToTable("HIS_ConfiguracionXExamen", "dbo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EsArray).HasColumnName("esArray");
            entity.Property(e => e.EsRequerido).HasColumnName("esRequerido");
            entity.Property(e => e.HisConfiguracionId).HasColumnName("Configuracion_id");
            entity.Property(e => e.HisExamenId).HasColumnName("Examen_id");
            entity.Property(e => e.ValorPorDefecto).HasColumnName("valorPorDefecto");

            entity.HasOne(d => d.HisConfiguracion).WithMany(p => p.HisConfiguracionXexamen)
                .HasForeignKey(d => d.HisConfiguracionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HIS_ConfiguracionXExamen_HIS_Configuracion");
        });

        modelBuilder.Entity<HisContraste>(entity =>
        {
            entity.ToTable("HIS_Contraste", "dbo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Valor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("valor");
        });

        modelBuilder.Entity<HisContrasteXexaman>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("HIS_ContrasteXExamen", "dbo");

            entity.Property(e => e.ContrasteId).HasColumnName("Contraste_Id");
            entity.Property(e => e.ExaIde).HasColumnName("Exa_Ide");

            entity.HasOne(d => d.Contraste).WithMany()
                .HasForeignKey(d => d.ContrasteId)
                .HasConstraintName("FK_HIS_ContrasteXExamen_HIS_Contraste");

            entity.HasOne(d => d.ExaIdeNavigation).WithMany()
                .HasForeignKey(d => d.ExaIde)
                .HasConstraintName("FK_HIS_ContrasteXExamen_HIS_Examen");
        });

        modelBuilder.Entity<HisDiagnostico>(entity =>
        {
            entity.HasKey(e => e.DiagnosticoId);

            entity.ToTable("HIS_Diagnostico", "dbo");

            entity.Property(e => e.DiagnosticoId)
                .ValueGeneratedNever()
                .HasColumnName("Diagnostico_id");
            entity.Property(e => e.DiagnosticoDescripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Diagnostico_descripcion");
        });

        modelBuilder.Entity<HisExamDerivado>(entity =>
        {
            entity.HasKey(e => e.CodSol).HasName("PK__HIS_Ex_D__980C6339D6FCB0DC");

            entity.ToTable("HIS_ExamDerivados", "dbo");

            entity.Property(e => e.CodSol)
                .ValueGeneratedNever()
                .HasColumnName("codSol");
            entity.Property(e => e.Solicitudes)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("solicitudes");
        });

        modelBuilder.Entity<HisExaman>(entity =>
        {
            entity.ToTable("HIS_Examen", "dbo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CodigoFonasa)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("codigoFonasa");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.RegionId).HasColumnName("Region_id");
            entity.Property(e => e.TipoExamenId).HasColumnName("TipoExamen_id");

            entity.HasOne(d => d.Region).WithMany(p => p.HisExamen)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HIS_Examen_HIS_Region");

            entity.HasOne(d => d.TipoExamen).WithMany(p => p.HisExamen)
                .HasForeignKey(d => d.TipoExamenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HIS_Examen_HIS_TipoExamen");
        });

        modelBuilder.Entity<HisExamenXlateralidad>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("HIS_ExamenXLateralidad", "dbo");

            entity.Property(e => e.ExaIde).HasColumnName("Exa_Ide");
            entity.Property(e => e.LatLateralidadId).HasColumnName("Lat_lateralidad_id");

            entity.HasOne(d => d.ExaIdeNavigation).WithMany()
                .HasForeignKey(d => d.ExaIde)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HIS_ExamenXLateralidad_HIS_Examen");

            entity.HasOne(d => d.LatLateralidad).WithMany()
                .HasForeignKey(d => d.LatLateralidadId)
                .HasConstraintName("FK_HIS_ExamenXLateralidad_HIS_Lateralidad");
        });

        modelBuilder.Entity<HisExamenXsolicitudExaman>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("HIS_ExamenXSolicitudExamen", "dbo");

            entity.Property(e => e.ExaId).HasColumnName("exa_Id");
            entity.Property(e => e.SolicitudExamenId).HasColumnName("SolicitudExamen_Id");

            entity.HasOne(d => d.Exa).WithMany()
                .HasForeignKey(d => d.ExaId)
                .HasConstraintName("FK_HIS_ExamenXSolicitudExamen_HIS_Examen");

            entity.HasOne(d => d.SolicitudExamen).WithMany()
                .HasForeignKey(d => d.SolicitudExamenId)
                .HasConstraintName("FK_HIS_ExamenXSolicitudExamen_HIS_SolicitudExamen");
        });

        modelBuilder.Entity<HisFundamento>(entity =>
        {
            entity.HasKey(e => e.FundamentoId);

            entity.ToTable("HIS_Fundamento", "dbo");

            entity.Property(e => e.FundamentoId)
                .ValueGeneratedNever()
                .HasColumnName("Fundamento_id");
            entity.Property(e => e.FundamentoDescripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Fundamento_descripcion");
            entity.Property(e => e.SolicitudExamenId).HasColumnName("SolicitudExamen_id");

            entity.HasOne(d => d.SolicitudExamen).WithMany(p => p.HisFundamentos)
                .HasForeignKey(d => d.SolicitudExamenId)
                .HasConstraintName("FK_HIS_Fundamento_HIS_SolicitudExamen");
        });

        modelBuilder.Entity<HisLateralidad>(entity =>
        {
            entity.ToTable("HIS_Lateralidad", "dbo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Valor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("valor");
        });

        modelBuilder.Entity<HisRegion>(entity =>
        {
            entity.ToTable("HIS_Region", "dbo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.RutaIcono)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("rutaIcono");
        });

        modelBuilder.Entity<HisSolicitudExaman>(entity =>
        {
            entity.HasKey(e => e.SolExamId);

            entity.ToTable("HIS_SolicitudExamen", "dbo");

            entity.Property(e => e.SolExamId)
                .ValueGeneratedNever()
                .HasColumnName("SolExam_id");
            entity.Property(e => e.Cp).HasColumnName("cp");
            entity.Property(e => e.Ctacte).HasColumnName("ctacte");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.SolExamDiagnosticoId).HasColumnName("SolExam_Diagnostico_id");
        });

        modelBuilder.Entity<HisSolicitudExamenXddiagnostico>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("HIS_SolicitudExamenXDdiagnostico", "dbo");

            entity.Property(e => e.DiagnosticoId).HasColumnName("Diagnostico_Id");
            entity.Property(e => e.SolExamId).HasColumnName("solExam_Id");

            entity.HasOne(d => d.Diagnostico).WithMany()
                .HasForeignKey(d => d.DiagnosticoId)
                .HasConstraintName("FK_HIS_SolicitudExamenXDdiagnostico_HIS_Diagnostico");

            entity.HasOne(d => d.SolExam).WithMany()
                .HasForeignKey(d => d.SolExamId)
                .HasConstraintName("FK_HIS_SolicitudExamenXDdiagnostico_HIS_SolicitudExamen");
        });

        modelBuilder.Entity<HisTipoExaman>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HIS_Unidad");

            entity.ToTable("HIS_TipoExamen", "dbo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<HisValor>(entity =>
        {
            entity.ToTable("HIS_Valor", "dbo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConfiguracionId).HasColumnName("Configuracion_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.Configuracion).WithMany(p => p.HisValors)
                .HasForeignKey(d => d.ConfiguracionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HIS_Valor_HIS_Configuracion");
        });

        modelBuilder.Entity<RegionXtipoExaman>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RegionXTipoExamen", "dbo");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}