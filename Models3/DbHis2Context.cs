using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HIS_API.Models3;

public partial class DbHis2Context : DbContext
{
  public DbHis2Context()
  {
  }

  public DbHis2Context(DbContextOptions<DbHis2Context> options)
      : base(options)
  {
  }

  public virtual DbSet<ExamenView> ExamenView { get; set; }

  public virtual DbSet<ExamenView2> ExamenView2 { get; set; }

  public virtual DbSet<HisConfiguracion> HisConfiguracions { get; set; }

  public virtual DbSet<HisConfiguracionExaman> HisConfiguracionExamen { get; set; }

  public virtual DbSet<HisContraste> HisContrastes { get; set; }

  public virtual DbSet<HisContrasteExaman> HisContrasteExamen { get; set; }

  public virtual DbSet<HisDiagnostico> HisDiagnosticos { get; set; }

  public virtual DbSet<HisExamDerivado> HisExamDerivados { get; set; }

  public virtual DbSet<HisExaman> HisExamen { get; set; }

  public virtual DbSet<HisExamenLateralidad> HisExamenLateralidads { get; set; }

  public virtual DbSet<HisExamenSolicitud> HisExamenSolicituds { get; set; }

  public virtual DbSet<HisFundamento> HisFundamentos { get; set; }

  public virtual DbSet<HisLateralidad> HisLateralidads { get; set; }

  public virtual DbSet<HisRegion> HisRegions { get; set; }

  public virtual DbSet<HisSolicitud> HisSolicituds { get; set; }

  public virtual DbSet<HisTipo> HisTipos { get; set; }

  public virtual DbSet<HisValor> HisValors { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
      => optionsBuilder.UseSqlServer("SERVER=10.5.214.129;DATABASE=DB_HIS2;USER ID=rene;PASSWORD=1779;TRUSTED_CONNECTION=false;TRUSTSERVERCERTIFICATE=true;Encrypt=True;");

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.HasDefaultSchema("db_datareader");

    modelBuilder.Entity<ExamenView>(entity =>
    {
      entity
              .HasNoKey()
              .ToView("ExamenView", "dbo");

      entity.Property(e => e.ExamenCodigoFonasa)
              .HasMaxLength(20)
              .IsUnicode(false)
              .HasColumnName("Examen_codigoFonasa");
      entity.Property(e => e.ExamenId).HasColumnName("Examen_id");
      entity.Property(e => e.ExamenNombre)
              .HasMaxLength(255)
              .IsUnicode(false)
              .HasColumnName("Examen_nombre");
      entity.Property(e => e.RegionId).HasColumnName("Region_id");
      entity.Property(e => e.RegionNombre)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("Region_nombre");
      entity.Property(e => e.TipoId).HasColumnName("Tipo_id");
      entity.Property(e => e.TipoNombre)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("Tipo_nombre");
    });

    modelBuilder.Entity<ExamenView2>(entity =>
    {
      entity
              .HasNoKey()
              .ToView("ExamenView2", "dbo");

      entity.Property(e => e.ExamenCodigoFonasa)
              .HasMaxLength(20)
              .IsUnicode(false)
              .HasColumnName("Examen_codigoFonasa");
      entity.Property(e => e.ExamenId).HasColumnName("Examen_id");
      entity.Property(e => e.ExamenNombre)
              .HasMaxLength(255)
              .IsUnicode(false)
              .HasColumnName("Examen_nombre");
      entity.Property(e => e.RegionId).HasColumnName("Region_id");
      entity.Property(e => e.RegionNombre)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("Region_nombre");
      entity.Property(e => e.TipoId).HasColumnName("Tipo_id");
      entity.Property(e => e.TipoNombre)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("Tipo_nombre");
    });

    modelBuilder.Entity<HisConfiguracion>(entity =>
    {
      entity.HasKey(e => e.ConfiguracionId);

      entity.ToTable("HIS_Configuracion", "dbo");

      entity.Property(e => e.ConfiguracionId).HasColumnName("Configuracion_id");
      entity.Property(e => e.ConfiguracionEsVigente).HasColumnName("Configuracion_esVigente");
      entity.Property(e => e.ConfiguracionNombre)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("Configuracion_nombre");
    });

    modelBuilder.Entity<HisConfiguracionExaman>(entity =>
    {
      entity.HasKey(e => e.ConfigExamId).HasName("PK_HIS_ConfiguracionXExamen");

      entity.ToTable("HIS_Configuracion_Examen", "dbo");

      entity.Property(e => e.ConfigExamId).HasColumnName("ConfigExam_id");
      entity.Property(e => e.ConfigExamConfiguracionId).HasColumnName("ConfigExam_Configuracion_id");
      entity.Property(e => e.ConfigExamEsArray).HasColumnName("ConfigExam_esArray");
      entity.Property(e => e.ConfigExamEsRequerido).HasColumnName("ConfigExam_esRequerido");
      entity.Property(e => e.ConfigExamExamenId).HasColumnName("ConfigExam_Examen_id");
      entity.Property(e => e.ConfigExamValorPorDefecto).HasColumnName("ConfigExam_valorPorDefecto");

      entity.HasOne(d => d.ConfigExamConfiguracion).WithMany(p => p.HisConfiguracionExamen)
              .HasForeignKey(d => d.ConfigExamConfiguracionId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_HIS_Configuracion_Examen_HIS_Configuracion");

      entity.HasOne(d => d.ConfigExamExamen).WithMany(p => p.HisConfiguracionExamen)
              .HasForeignKey(d => d.ConfigExamExamenId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_HIS_Configuracion_Examen_HIS_Examen");
    });

    modelBuilder.Entity<HisContraste>(entity =>
    {
      entity.HasKey(e => e.ContrasteId);

      entity.ToTable("HIS_Contraste", "dbo");

      entity.Property(e => e.ContrasteId).HasColumnName("Contraste_id");
      entity.Property(e => e.ContrasteValor)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("Contraste_valor");
    });

    modelBuilder.Entity<HisContrasteExaman>(entity =>
    {
      entity.HasKey(e => e.ContrasExamExamSolId).HasName("PK_HIS_ContrasteXExamen");

      entity.ToTable("HIS_Contraste_Examen", "dbo");

      entity.Property(e => e.ContrasExamExamSolId)
              .ValueGeneratedNever()
              .HasColumnName("ContrasExam_ExamSol_id");
      entity.Property(e => e.ContrasExamContrasteId).HasColumnName("ContrasExam_Contraste_Id");

      entity.HasOne(d => d.ContrasExamContraste).WithMany(p => p.HisContrasteExamen)
              .HasForeignKey(d => d.ContrasExamContrasteId)
              .HasConstraintName("FK_HIS_Contraste_Examen_HIS_Contraste");

      entity.HasOne(d => d.ContrasExamExamSol).WithOne(p => p.HisContrasteExaman)
              .HasForeignKey<HisContrasteExaman>(d => d.ContrasExamExamSolId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_HIS_Contraste_Examen_HIS_Examen_Solicitud");
    });

    modelBuilder.Entity<HisDiagnostico>(entity =>
    {
      entity.HasKey(e => e.DiagnosticoId).HasName("PK_HIS_SolicitudExamenXDiagnostico_1");

      entity.ToTable("HIS_Diagnostico", "dbo");

      entity.Property(e => e.DiagnosticoId).HasColumnName("Diagnostico_id");
      entity.Property(e => e.DiagnosticoDescripcion)
              .HasMaxLength(255)
              .IsUnicode(false)
              .HasColumnName("Diagnostico_descripcion");
      entity.Property(e => e.DiagnosticoSolicitudId).HasColumnName("Diagnostico_Solicitud_id");

      entity.HasOne(d => d.DiagnosticoSolicitud).WithMany(p => p.HisDiagnosticos)
              .HasForeignKey(d => d.DiagnosticoSolicitudId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_HIS_Diagnostico_HIS_Solicitud");
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
      entity.HasKey(e => e.ExamenId);

      entity.ToTable("HIS_Examen", "dbo");

      entity.Property(e => e.ExamenId).HasColumnName("Examen_id");
      entity.Property(e => e.ExamenCodigoFonasa)
              .HasMaxLength(20)
              .IsUnicode(false)
              .HasColumnName("Examen_codigoFonasa");
      entity.Property(e => e.ExamenEsVisible).HasColumnName("Examen_esVisible");
      entity.Property(e => e.ExamenNombre)
              .HasMaxLength(255)
              .IsUnicode(false)
              .HasColumnName("Examen_nombre");
      entity.Property(e => e.ExamenRegionId).HasColumnName("Examen_Region_id");
      entity.Property(e => e.ExamenTipoId).HasColumnName("Examen_Tipo_id");

      entity.HasOne(d => d.ExamenRegion).WithMany(p => p.HisExamen)
              .HasForeignKey(d => d.ExamenRegionId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_HIS_Examen_HIS_Region");

      entity.HasOne(d => d.ExamenTipo).WithMany(p => p.HisExamen)
              .HasForeignKey(d => d.ExamenTipoId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_HIS_Examen_HIS_Tipo");
    });

    modelBuilder.Entity<HisExamenLateralidad>(entity =>
    {
      entity.HasKey(e => e.ExamLatExamSolId).HasName("PK_HIS_ExamenXLateralidad");

      entity.ToTable("HIS_Examen_Lateralidad", "dbo");

      entity.Property(e => e.ExamLatExamSolId)
              .ValueGeneratedNever()
              .HasColumnName("ExamLat_ExamSol_id");
      entity.Property(e => e.ExamLatLateralidadId).HasColumnName("ExamLat_Lateralidad_id");

      entity.HasOne(d => d.ExamLatExamSol).WithOne(p => p.HisExamenLateralidad)
              .HasForeignKey<HisExamenLateralidad>(d => d.ExamLatExamSolId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_HIS_Examen_Lateralidad_HIS_Examen_Solicitud");

      entity.HasOne(d => d.ExamLatLateralidad).WithMany(p => p.HisExamenLateralidads)
              .HasForeignKey(d => d.ExamLatLateralidadId)
              .HasConstraintName("FK_HIS_Examen_Lateralidad_HIS_Lateralidad");
    });

    modelBuilder.Entity<HisExamenSolicitud>(entity =>
    {
      entity.HasKey(e => e.ExamSolId).HasName("PK_HIS_ExamenXSolicitudExamen");

      entity.ToTable("HIS_Examen_Solicitud", "dbo");

      entity.Property(e => e.ExamSolId).HasColumnName("ExamSol_id");
      entity.Property(e => e.ExamSolExamenId).HasColumnName("ExamSol_Examen_id");
      entity.Property(e => e.ExamSolSolicitudId).HasColumnName("ExamSol_Solicitud_id");

      entity.HasOne(d => d.ExamSolExamen).WithMany(p => p.HisExamenSolicituds)
              .HasForeignKey(d => d.ExamSolExamenId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_HIS_Examen_Solicitud_HIS_Examen");

      entity.HasOne(d => d.ExamSolSolicitud).WithMany(p => p.HisExamenSolicituds)
              .HasForeignKey(d => d.ExamSolSolicitudId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_HIS_Examen_Solicitud_HIS_Solicitud");
    });

    modelBuilder.Entity<HisFundamento>(entity =>
    {
      entity.HasKey(e => e.FundamentoId);

      entity.ToTable("HIS_Fundamento", "dbo");

      entity.Property(e => e.FundamentoId).HasColumnName("Fundamento_id");
      entity.Property(e => e.FundamentoDescripcion)
              .HasMaxLength(255)
              .IsUnicode(false)
              .HasColumnName("Fundamento_descripcion");
      entity.Property(e => e.FundamentoSolicitudId).HasColumnName("Fundamento_Solicitud_id");

      entity.HasOne(d => d.FundamentoSolicitud).WithMany(p => p.HisFundamentos)
              .HasForeignKey(d => d.FundamentoSolicitudId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_HIS_Fundamento_HIS_Solicitud");
    });

    modelBuilder.Entity<HisLateralidad>(entity =>
    {
      entity.HasKey(e => e.LateralidadId);

      entity.ToTable("HIS_Lateralidad", "dbo");

      entity.Property(e => e.LateralidadId).HasColumnName("Lateralidad_id");
      entity.Property(e => e.LateralidadValor)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("Lateralidad_valor");
    });

    modelBuilder.Entity<HisRegion>(entity =>
    {
      entity.HasKey(e => e.RegionId);

      entity.ToTable("HIS_Region", "dbo");

      entity.Property(e => e.RegionId).HasColumnName("Region_id");
      entity.Property(e => e.RegionNombre)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("Region_nombre");
      entity.Property(e => e.RegionRutaIcono)
              .HasMaxLength(255)
              .IsUnicode(false)
              .HasColumnName("Region_rutaIcono");
    });

    modelBuilder.Entity<HisSolicitud>(entity =>
    {
      entity.HasKey(e => e.SolicitudId).HasName("PK_HIS_SolicitudExamen");

      entity.ToTable("HIS_Solicitud", "dbo");

      entity.Property(e => e.SolicitudId).HasColumnName("Solicitud_id");
      entity.Property(e => e.SolicitudCodigoPaciente).HasColumnName("Solicitud_codigoPaciente");
      entity.Property(e => e.SolicitudCuentaCorriente).HasColumnName("Solicitud_cuentaCorriente");
      entity.Property(e => e.SolicitudFecha)
              .HasColumnType("datetime")
              .HasColumnName("Solicitud_fecha");
    });

    modelBuilder.Entity<HisTipo>(entity =>
    {
      entity.HasKey(e => e.TipoId).HasName("PK_HIS_Unidad");

      entity.ToTable("HIS_Tipo", "dbo");

      entity.Property(e => e.TipoId).HasColumnName("Tipo_id");
      entity.Property(e => e.TipoNombre)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("Tipo_nombre");
    });

    modelBuilder.Entity<HisValor>(entity =>
    {
      entity.HasKey(e => e.ValorId);

      entity.ToTable("HIS_Valor", "dbo");

      entity.Property(e => e.ValorId).HasColumnName("Valor_id");
      entity.Property(e => e.ValorConfiguracionId).HasColumnName("Valor_Configuracion_id");
      entity.Property(e => e.ValorNombre)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("Valor_nombre");

      entity.HasOne(d => d.ValorConfiguracion).WithMany(p => p.HisValors)
              .HasForeignKey(d => d.ValorConfiguracionId)
              .HasConstraintName("FK_HIS_Valor_HIS_Configuracion");
    });

    OnModelCreatingPartial(modelBuilder);
  }

  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
