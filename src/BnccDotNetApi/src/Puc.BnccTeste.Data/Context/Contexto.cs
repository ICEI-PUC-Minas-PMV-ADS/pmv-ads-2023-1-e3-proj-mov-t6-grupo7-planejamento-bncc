using Microsoft.EntityFrameworkCore;
using Puc.BnccTeste.Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puc.BnccTeste.Infra.Data.Context
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) 
            : base(options) 
        {
        }       

        public virtual DbSet<BnccArtesEf> BnccArtesEfs { get; set; }

        public virtual DbSet<BnccCienciasEf> BnccCienciasEfs { get; set; }

        public virtual DbSet<BnccEducacaoFisicaEf> BnccEducacaoFisicaEfs { get; set; }

        public virtual DbSet<BnccEnsinoReligiosoEf> BnccEnsinoReligiosoEfs { get; set; }

        public virtual DbSet<BnccGeografiaEf> BnccGeografiaEfs { get; set; }

        public virtual DbSet<BnccHistoriaEf> BnccHistoriaEfs { get; set; }

        public virtual DbSet<BnccLinguaInglesaEf> BnccLinguaInglesaEfs { get; set; }

        public virtual DbSet<BnccLinguaPortuguesaEf> BnccLinguaPortuguesaEfs { get; set; }

        public virtual DbSet<BnccMatematicaEf> BnccMatematicaEfs { get; set; }

        public virtual DbSet<CompetenciasAreaEm> CompetenciasAreaEms { get; set; }

        public virtual DbSet<CompetenciasEspecificasPorAreaEf> CompetenciasEspecificasPorAreaEfs { get; set; }

        public virtual DbSet<CompetenciasGeraisEm> CompetenciasGeraisEms { get; set; }

        public virtual DbSet<CorpoGestosEdInf> CorpoGestosEdInfs { get; set; }

        public virtual DbSet<DfEduInf> DfEduInfs { get; set; }

        public virtual DbSet<DfHabilidadesEm> DfHabilidadesEms { get; set; }

        public virtual DbSet<EscutaFalaEdInf> EscutaFalaEdInfs { get; set; }

        public virtual DbSet<EspacoTempoEdInf> EspacoTempoEdInfs { get; set; }

        public virtual DbSet<EuOutroNosEdInf> EuOutroNosEdInfs { get; set; }

        public virtual DbSet<Usuario> Usuarios { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(ObterStringConexao());
                base.OnConfiguring(builder);
            }
        }

        public string ObterStringConexao()
        {
            return "Server=.\\SQLExpress;Database=bncc_database;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Usuario_Id);
                entity.Property(e => e.Usuario_Id)
                    .ValueGeneratedOnAdd();                    
            });

            modelBuilder.Entity<BnccArtesEf>(entity =>
            {
                entity.HasKey(e => e.Column1).HasName("PK__bncc_art__DB06C12DAFD67B65");

                entity.ToTable("bncc_artes_ef");

                entity.Property(e => e.Column1)
                    .ValueGeneratedNever()
                    .HasColumnName("column1");
                entity.Property(e => e.AnoFaixa)
                    .HasMaxLength(50)
                    .HasColumnName("ano_faixa");
                entity.Property(e => e.CodHab)
                    .HasMaxLength(50)
                    .HasColumnName("cod_hab");
                entity.Property(e => e.Componente)
                    .HasMaxLength(50)
                    .HasColumnName("componente");
                entity.Property(e => e.DescricaoCod)
                    .HasMaxLength(400)
                    .HasColumnName("descricao_cod");
                entity.Property(e => e.Habilidades)
                    .HasMaxLength(450)
                    .HasColumnName("habilidades");
                entity.Property(e => e.NonoEf).HasColumnName("nono_ef");
                entity.Property(e => e.ObjetosConhecimento)
                    .HasMaxLength(50)
                    .HasColumnName("objetos_conhecimento");
                entity.Property(e => e.OitavoEf).HasColumnName("oitavo_ef");
                entity.Property(e => e.PrimeiroEf).HasColumnName("primeiro_ef");
                entity.Property(e => e.QuartoEf).HasColumnName("quarto_ef");
                entity.Property(e => e.QuintoEf).HasColumnName("quinto_ef");
                entity.Property(e => e.SegundoEf).HasColumnName("segundo_ef");
                entity.Property(e => e.SetimoEf).HasColumnName("setimo_ef");
                entity.Property(e => e.SextoEf).HasColumnName("sexto_ef");
                entity.Property(e => e.TerceiroEf).HasColumnName("terceiro_ef");
                entity.Property(e => e.UnidadesTematicas)
                    .HasMaxLength(50)
                    .HasColumnName("unidades_tematicas");
            });

            modelBuilder.Entity<BnccCienciasEf>(entity =>
            {
                entity.HasKey(e => e.Column1).HasName("PK__bncc_cie__DB06C12DD30C486A");

                entity.ToTable("bncc_ciencias_ef");

                entity.Property(e => e.Column1)
                    .ValueGeneratedNever()
                    .HasColumnName("column1");
                entity.Property(e => e.AnoFaixa)
                    .HasMaxLength(50)
                    .HasColumnName("ano_faixa");
                entity.Property(e => e.CodHab)
                    .HasMaxLength(50)
                    .HasColumnName("cod_hab");
                entity.Property(e => e.Componente)
                    .HasMaxLength(50)
                    .HasColumnName("componente");
                entity.Property(e => e.DescricaoCod)
                    .HasMaxLength(350)
                    .HasColumnName("descricao_cod");
                entity.Property(e => e.Habilidades)
                    .HasMaxLength(350)
                    .HasColumnName("habilidades");
                entity.Property(e => e.NonoEf).HasColumnName("nono_ef");
                entity.Property(e => e.ObjetosConhecimento)
                    .HasMaxLength(200)
                    .HasColumnName("objetos_conhecimento");
                entity.Property(e => e.OitavoEf).HasColumnName("oitavo_ef");
                entity.Property(e => e.PrimeiroEf).HasColumnName("primeiro_ef");
                entity.Property(e => e.QuartoEf).HasColumnName("quarto_ef");
                entity.Property(e => e.QuintoEf).HasColumnName("quinto_ef");
                entity.Property(e => e.SegundoEf).HasColumnName("segundo_ef");
                entity.Property(e => e.SetimoEf).HasColumnName("setimo_ef");
                entity.Property(e => e.SextoEf).HasColumnName("sexto_ef");
                entity.Property(e => e.TerceiroEf).HasColumnName("terceiro_ef");
                entity.Property(e => e.UnidadesTematicas)
                    .HasMaxLength(50)
                    .HasColumnName("unidades_tematicas");
            });

            modelBuilder.Entity<BnccEducacaoFisicaEf>(entity =>
            {
                entity.HasKey(e => e.Column1).HasName("PK__bncc_edu__DB06C12D3AFB5F75");

                entity.ToTable("bncc_educacao_fisica_ef");

                entity.Property(e => e.Column1)
                    .ValueGeneratedNever()
                    .HasColumnName("column1");
                entity.Property(e => e.AnoFaixa)
                    .HasMaxLength(50)
                    .HasColumnName("ano_faixa");
                entity.Property(e => e.CodHab)
                    .HasMaxLength(50)
                    .HasColumnName("cod_hab");
                entity.Property(e => e.Componente)
                    .HasMaxLength(50)
                    .HasColumnName("componente");
                entity.Property(e => e.DescricaoCod)
                    .HasMaxLength(350)
                    .HasColumnName("descricao_cod");
                entity.Property(e => e.Habilidades)
                    .HasMaxLength(350)
                    .HasColumnName("habilidades");
                entity.Property(e => e.NonoEf).HasColumnName("nono_ef");
                entity.Property(e => e.ObjetosConhecimento)
                    .HasMaxLength(150)
                    .HasColumnName("objetos_conhecimento");
                entity.Property(e => e.OitavoEf).HasColumnName("oitavo_ef");
                entity.Property(e => e.PrimeiroEf).HasColumnName("primeiro_ef");
                entity.Property(e => e.QuartoEf).HasColumnName("quarto_ef");
                entity.Property(e => e.QuintoEf).HasColumnName("quinto_ef");
                entity.Property(e => e.SegundoEf).HasColumnName("segundo_ef");
                entity.Property(e => e.SetimoEf).HasColumnName("setimo_ef");
                entity.Property(e => e.SextoEf).HasColumnName("sexto_ef");
                entity.Property(e => e.TerceiroEf).HasColumnName("terceiro_ef");
                entity.Property(e => e.UnidadesTematicas)
                    .HasMaxLength(50)
                    .HasColumnName("unidades_tematicas");
            });

            modelBuilder.Entity<BnccEnsinoReligiosoEf>(entity =>
            {
                entity.HasKey(e => e.Column1).HasName("PK__bncc_ens__DB06C12DED312622");

                entity.ToTable("bncc_ensino_religioso_ef");

                entity.Property(e => e.Column1)
                    .ValueGeneratedNever()
                    .HasColumnName("column1");
                entity.Property(e => e.AnoFaixa)
                    .HasMaxLength(50)
                    .HasColumnName("ano_faixa");
                entity.Property(e => e.CodHab)
                    .HasMaxLength(50)
                    .HasColumnName("cod_hab");
                entity.Property(e => e.Componente)
                    .HasMaxLength(50)
                    .HasColumnName("componente");
                entity.Property(e => e.DescricaoCod)
                    .HasMaxLength(250)
                    .HasColumnName("descricao_cod");
                entity.Property(e => e.Habilidades)
                    .HasMaxLength(250)
                    .HasColumnName("habilidades");
                entity.Property(e => e.NonoEf).HasColumnName("nono_ef");
                entity.Property(e => e.ObjetosConhecimento)
                    .HasMaxLength(100)
                    .HasColumnName("objetos_conhecimento");
                entity.Property(e => e.OitavoEf).HasColumnName("oitavo_ef");
                entity.Property(e => e.PrimeiroEf).HasColumnName("primeiro_ef");
                entity.Property(e => e.QuartoEf).HasColumnName("quarto_ef");
                entity.Property(e => e.QuintoEf).HasColumnName("quinto_ef");
                entity.Property(e => e.SegundoEf).HasColumnName("segundo_ef");
                entity.Property(e => e.SetimoEf).HasColumnName("setimo_ef");
                entity.Property(e => e.SextoEf).HasColumnName("sexto_ef");
                entity.Property(e => e.TerceiroEf).HasColumnName("terceiro_ef");
                entity.Property(e => e.UnidadesTematicas)
                    .HasMaxLength(50)
                    .HasColumnName("unidades_tematicas");
            });

            modelBuilder.Entity<BnccGeografiaEf>(entity =>
            {
                entity.HasKey(e => e.Column1).HasName("PK__bncc_geo__DB06C12DA8811A55");

                entity.ToTable("bncc_geografia_ef");

                entity.Property(e => e.Column1)
                    .ValueGeneratedNever()
                    .HasColumnName("column1");
                entity.Property(e => e.AnoFaixa)
                    .HasMaxLength(50)
                    .HasColumnName("ano_faixa");
                entity.Property(e => e.CodHab)
                    .HasMaxLength(50)
                    .HasColumnName("cod_hab");
                entity.Property(e => e.Componente)
                    .HasMaxLength(50)
                    .HasColumnName("componente");
                entity.Property(e => e.DescricaoCod)
                    .HasMaxLength(400)
                    .HasColumnName("descricao_cod");
                entity.Property(e => e.Habilidades)
                    .HasMaxLength(450)
                    .HasColumnName("habilidades");
                entity.Property(e => e.NonoEf).HasColumnName("nono_ef");
                entity.Property(e => e.ObjetosConhecimento)
                    .HasMaxLength(150)
                    .HasColumnName("objetos_conhecimento");
                entity.Property(e => e.OitavoEf).HasColumnName("oitavo_ef");
                entity.Property(e => e.PrimeiroEf).HasColumnName("primeiro_ef");
                entity.Property(e => e.QuartoEf).HasColumnName("quarto_ef");
                entity.Property(e => e.QuintoEf).HasColumnName("quinto_ef");
                entity.Property(e => e.SegundoEf).HasColumnName("segundo_ef");
                entity.Property(e => e.SetimoEf).HasColumnName("setimo_ef");
                entity.Property(e => e.SextoEf).HasColumnName("sexto_ef");
                entity.Property(e => e.TerceiroEf).HasColumnName("terceiro_ef");
                entity.Property(e => e.UnidadesTematicas)
                    .HasMaxLength(50)
                    .HasColumnName("unidades_tematicas");
            });

            modelBuilder.Entity<BnccHistoriaEf>(entity =>
            {
                entity.HasKey(e => e.Column1).HasName("PK__bncc_his__DB06C12D3AE38912");

                entity.ToTable("bncc_historia_ef");

                entity.Property(e => e.Column1)
                    .ValueGeneratedNever()
                    .HasColumnName("column1");
                entity.Property(e => e.AnoFaixa)
                    .HasMaxLength(50)
                    .HasColumnName("ano_faixa");
                entity.Property(e => e.CodHab)
                    .HasMaxLength(150)
                    .HasColumnName("cod_hab");
                entity.Property(e => e.Componente)
                    .HasMaxLength(50)
                    .HasColumnName("componente");
                entity.Property(e => e.DescricaoCod)
                    .HasMaxLength(300)
                    .HasColumnName("descricao_cod");
                entity.Property(e => e.Habilidades)
                    .HasMaxLength(300)
                    .HasColumnName("habilidades");
                entity.Property(e => e.NonoEf).HasColumnName("nono_ef");
                entity.Property(e => e.ObjetosConhecimento)
                    .HasMaxLength(450)
                    .HasColumnName("objetos_conhecimento");
                entity.Property(e => e.OitavoEf).HasColumnName("oitavo_ef");
                entity.Property(e => e.PrimeiroEf).HasColumnName("primeiro_ef");
                entity.Property(e => e.QuartoEf).HasColumnName("quarto_ef");
                entity.Property(e => e.QuintoEf).HasColumnName("quinto_ef");
                entity.Property(e => e.SegundoEf).HasColumnName("segundo_ef");
                entity.Property(e => e.SetimoEf).HasColumnName("setimo_ef");
                entity.Property(e => e.SextoEf).HasColumnName("sexto_ef");
                entity.Property(e => e.TerceiroEf).HasColumnName("terceiro_ef");
                entity.Property(e => e.UnidadesTematicas)
                    .HasMaxLength(100)
                    .HasColumnName("unidades_tematicas");
            });

            modelBuilder.Entity<BnccLinguaInglesaEf>(entity =>
            {
                entity.HasKey(e => e.Column1).HasName("PK__bncc_lin__DB06C12DD0B91690");

                entity.ToTable("bncc_lingua_inglesa_ef");

                entity.Property(e => e.Column1)
                    .ValueGeneratedNever()
                    .HasColumnName("column1");
                entity.Property(e => e.AnoFaixa)
                    .HasMaxLength(50)
                    .HasColumnName("ano_faixa");
                entity.Property(e => e.CodHab)
                    .HasMaxLength(100)
                    .HasColumnName("cod_hab");
                entity.Property(e => e.Componente)
                    .HasMaxLength(50)
                    .HasColumnName("componente");
                entity.Property(e => e.DescricaoCod)
                    .HasMaxLength(350)
                    .HasColumnName("descricao_cod");
                entity.Property(e => e.Eixo)
                    .HasMaxLength(50)
                    .HasColumnName("eixo");
                entity.Property(e => e.Habilidades)
                    .HasMaxLength(400)
                    .HasColumnName("habilidades");
                entity.Property(e => e.NonoEf).HasColumnName("nono_ef");
                entity.Property(e => e.ObjetosConhecimento)
                    .HasMaxLength(100)
                    .HasColumnName("objetos_conhecimento");
                entity.Property(e => e.OitavoEf).HasColumnName("oitavo_ef");
                entity.Property(e => e.PrimeiroEf).HasColumnName("primeiro_ef");
                entity.Property(e => e.QuartoEf).HasColumnName("quarto_ef");
                entity.Property(e => e.QuintoEf).HasColumnName("quinto_ef");
                entity.Property(e => e.SegundoEf).HasColumnName("segundo_ef");
                entity.Property(e => e.SetimoEf).HasColumnName("setimo_ef");
                entity.Property(e => e.SextoEf).HasColumnName("sexto_ef");
                entity.Property(e => e.TerceiroEf).HasColumnName("terceiro_ef");
                entity.Property(e => e.UnidadesTematicas)
                    .HasMaxLength(100)
                    .HasColumnName("unidades_tematicas");
            });

            modelBuilder.Entity<BnccLinguaPortuguesaEf>(entity =>
            {
                entity.HasKey(e => e.Column1).HasName("PK__bncc_lin__DB06C12DA920FC3C");

                entity.ToTable("bncc_lingua_portuguesa_ef");

                entity.Property(e => e.Column1)
                    .ValueGeneratedNever()
                    .HasColumnName("column1");
                entity.Property(e => e.AnoFaixa)
                    .HasMaxLength(50)
                    .HasColumnName("ano_faixa");
                entity.Property(e => e.CampoAtuacao)
                    .HasMaxLength(50)
                    .HasColumnName("campo_atuacao");
                entity.Property(e => e.CodHab)
                    .HasMaxLength(1050)
                    .HasColumnName("cod_hab");
                entity.Property(e => e.Componente)
                    .HasMaxLength(50)
                    .HasColumnName("componente");
                entity.Property(e => e.DescricaoCod)
                    .HasMaxLength(4000)
                    .HasColumnName("descricao_cod");
                entity.Property(e => e.Habilidades)
                    .HasMaxLength(4000)
                    .HasColumnName("habilidades");
                entity.Property(e => e.NonoEf).HasColumnName("nono_ef");
                entity.Property(e => e.ObjetosConhecimento)
                    .HasMaxLength(250)
                    .HasColumnName("objetos_conhecimento");
                entity.Property(e => e.OitavoEf).HasColumnName("oitavo_ef");
                entity.Property(e => e.PraticasLinguagem)
                    .HasMaxLength(150)
                    .HasColumnName("praticas_linguagem");
                entity.Property(e => e.PrimeiroEf).HasColumnName("primeiro_ef");
                entity.Property(e => e.QuartoEf).HasColumnName("quarto_ef");
                entity.Property(e => e.QuintoEf).HasColumnName("quinto_ef");
                entity.Property(e => e.SegundoEf).HasColumnName("segundo_ef");
                entity.Property(e => e.SetimoEf).HasColumnName("setimo_ef");
                entity.Property(e => e.SextoEf).HasColumnName("sexto_ef");
                entity.Property(e => e.TerceiroEf).HasColumnName("terceiro_ef");
            });

            modelBuilder.Entity<BnccMatematicaEf>(entity =>
            {
                entity.HasKey(e => e.Column1).HasName("PK__bncc_mat__DB06C12DDAF8D1A1");

                entity.ToTable("bncc_matematica_ef");

                entity.Property(e => e.Column1)
                    .ValueGeneratedNever()
                    .HasColumnName("column1");
                entity.Property(e => e.AnoFaixa)
                    .HasMaxLength(50)
                    .HasColumnName("ano_faixa");
                entity.Property(e => e.CodHab)
                    .HasMaxLength(300)
                    .HasColumnName("cod_hab");
                entity.Property(e => e.Componente)
                    .HasMaxLength(50)
                    .HasColumnName("componente");
                entity.Property(e => e.DescricaoCod)
                    .HasMaxLength(400)
                    .HasColumnName("descricao_cod");
                entity.Property(e => e.Habilidades)
                    .HasMaxLength(400)
                    .HasColumnName("habilidades");
                entity.Property(e => e.NonoEf).HasColumnName("nono_ef");
                entity.Property(e => e.ObjetosConhecimento)
                    .HasMaxLength(300)
                    .HasColumnName("objetos_conhecimento");
                entity.Property(e => e.OitavoEf).HasColumnName("oitavo_ef");
                entity.Property(e => e.PrimeiroEf).HasColumnName("primeiro_ef");
                entity.Property(e => e.QuartoEf).HasColumnName("quarto_ef");
                entity.Property(e => e.QuintoEf).HasColumnName("quinto_ef");
                entity.Property(e => e.SegundoEf).HasColumnName("segundo_ef");
                entity.Property(e => e.SetimoEf).HasColumnName("setimo_ef");
                entity.Property(e => e.SextoEf).HasColumnName("sexto_ef");
                entity.Property(e => e.TerceiroEf).HasColumnName("terceiro_ef");
                entity.Property(e => e.UnidadesTematicas)
                    .HasMaxLength(50)
                    .HasColumnName("unidades_tematicas");
            });

            modelBuilder.Entity<CompetenciasAreaEm>(entity =>
            {
                entity.HasKey(e => e.Column1);

                entity.ToTable("competencias_area_em");

                entity.Property(e => e.Column1).HasColumnName("column1");
                entity.Property(e => e.Area)
                    .HasMaxLength(50)
                    .HasColumnName("area");
                entity.Property(e => e.Competencias)
                    .HasMaxLength(500)
                    .HasColumnName("competencias");
            });

            modelBuilder.Entity<CompetenciasEspecificasPorAreaEf>(entity =>
            {
                entity.HasKey(e => e.Column1).HasName("PK__competen__DB06C12D3DD2E737");

                entity.ToTable("competencias_especificas_por_area_ef");

                entity.Property(e => e.Column1)
                    .ValueGeneratedNever()
                    .HasColumnName("column1");
                entity.Property(e => e.Competencias)
                    .HasMaxLength(400)
                    .HasColumnName("competencias");
                entity.Property(e => e.Conteudo)
                    .HasMaxLength(50)
                    .HasColumnName("conteudo");
            });

            modelBuilder.Entity<CompetenciasGeraisEm>(entity =>
            {
                entity.HasKey(e => e.Column1);

                entity.ToTable("competencias_gerais_em");

                entity.Property(e => e.Column1).HasColumnName("column1");
                entity.Property(e => e.Area)
                    .HasMaxLength(50)
                    .HasColumnName("area");
                entity.Property(e => e.Competencias)
                    .HasMaxLength(450)
                    .HasColumnName("competencias");
            });

            modelBuilder.Entity<CorpoGestosEdInf>(entity =>
            {
                entity.HasKey(e => e.Column1).HasName("PK__corpo_ge__DB06C12D9B48A6A6");

                entity.ToTable("corpo_gestos_ed_inf");

                entity.Property(e => e.Column1)
                    .ValueGeneratedNever()
                    .HasColumnName("column1");
                entity.Property(e => e.CampoExp)
                    .HasMaxLength(50)
                    .HasColumnName("campo_exp");
                entity.Property(e => e.CodApr)
                    .HasMaxLength(50)
                    .HasColumnName("cod_apr");
                entity.Property(e => e.DescricaoCod)
                    .HasMaxLength(4000)
                    .HasColumnName("descricao_cod");
                entity.Property(e => e.FaixaEtaria)
                    .HasMaxLength(100)
                    .HasColumnName("faixa_etaria");
                entity.Property(e => e.IdadeAnosFinal).HasColumnName("idade_anos_final");
                entity.Property(e => e.IdadeAnosInicial).HasColumnName("idade_anos_inicial");
                entity.Property(e => e.IdadeMesesFinal).HasColumnName("idade_meses_final");
                entity.Property(e => e.IdadeMesesInicial).HasColumnName("idade_meses_inicial");
                entity.Property(e => e.Obj)
                    .HasMaxLength(4000)
                    .HasColumnName("obj");
            });

            modelBuilder.Entity<DfEduInf>(entity =>
            {
                entity.HasKey(e => e.Column1).HasName("PK__df_edu_i__DB06C12DE8F6ED31");

                entity.ToTable("df_edu_inf");

                entity.Property(e => e.Column1)
                    .ValueGeneratedNever()
                    .HasColumnName("column1");
                entity.Property(e => e.CampoExp)
                    .HasMaxLength(100)
                    .HasColumnName("campo_exp");
                entity.Property(e => e.CodApr)
                    .HasMaxLength(50)
                    .HasColumnName("cod_apr");
                entity.Property(e => e.DescricaoCod)
                    .HasMaxLength(4000)
                    .HasColumnName("descricao_cod");
                entity.Property(e => e.FaixaEtaria)
                    .HasMaxLength(100)
                    .HasColumnName("faixa_etaria");
                entity.Property(e => e.IdadeAnosFinal).HasColumnName("idade_anos_final");
                entity.Property(e => e.IdadeAnosInicial).HasColumnName("idade_anos_inicial");
                entity.Property(e => e.IdadeMesesFinal).HasColumnName("idade_meses_final");
                entity.Property(e => e.IdadeMesesInicial).HasColumnName("idade_meses_inicial");
            });

            modelBuilder.Entity<DfHabilidadesEm>(entity =>
            {
                entity.HasKey(e => e.Column1);

                entity.ToTable("df_habilidades_em");

                entity.Property(e => e.Column1).HasColumnName("column1");
                entity.Property(e => e.AnoFaixa)
                    .HasMaxLength(50)
                    .HasColumnName("ano_faixa");
                entity.Property(e => e.Area)
                    .HasMaxLength(50)
                    .HasColumnName("area");
                entity.Property(e => e.CamposAtuacao)
                    .HasMaxLength(50)
                    .HasColumnName("campos_atuacao");
                entity.Property(e => e.CodHab)
                    .HasMaxLength(50)
                    .HasColumnName("cod_hab");
                entity.Property(e => e.CompetenciasEsp)
                    .HasMaxLength(50)
                    .HasColumnName("competencias_esp");
                entity.Property(e => e.Habilidades)
                    .HasMaxLength(800)
                    .HasColumnName("habilidades");
                entity.Property(e => e.PrimeiroAno)
                    .HasMaxLength(50)
                    .HasColumnName("primeiro_ano");
                entity.Property(e => e.SegundoAno)
                    .HasMaxLength(50)
                    .HasColumnName("segundo_ano");
                entity.Property(e => e.TerceiroAno)
                    .HasMaxLength(50)
                    .HasColumnName("terceiro_ano");
            });

            modelBuilder.Entity<EscutaFalaEdInf>(entity =>
            {
                entity.HasKey(e => e.Column1).HasName("PK__escuta_f__DB06C12D347A94F5");

                entity.ToTable("escuta_fala_ed_inf");

                entity.Property(e => e.Column1)
                    .ValueGeneratedNever()
                    .HasColumnName("column1");
                entity.Property(e => e.CampoExp)
                    .HasMaxLength(50)
                    .HasColumnName("campo_exp");
                entity.Property(e => e.CodApr)
                    .HasMaxLength(50)
                    .HasColumnName("cod_apr");
                entity.Property(e => e.DescricaoCod)
                    .HasMaxLength(4000)
                    .HasColumnName("descricao_cod");
                entity.Property(e => e.FaixaEtaria)
                    .HasMaxLength(100)
                    .HasColumnName("faixa_etaria");
                entity.Property(e => e.IdadeAnosFinal).HasColumnName("idade_anos_final");
                entity.Property(e => e.IdadeAnosInicial).HasColumnName("idade_anos_inicial");
                entity.Property(e => e.IdadeMesesFinal).HasColumnName("idade_meses_final");
                entity.Property(e => e.IdadeMesesInicial).HasColumnName("idade_meses_inicial");
                entity.Property(e => e.Obj)
                    .HasMaxLength(4000)
                    .HasColumnName("obj");
            });

            modelBuilder.Entity<EspacoTempoEdInf>(entity =>
            {
                entity.HasKey(e => e.Column1).HasName("PK__espaco_t__DB06C12D5F4872F9");

                entity.ToTable("espaco_tempo_ed_inf");

                entity.Property(e => e.Column1)
                    .ValueGeneratedNever()
                    .HasColumnName("column1");
                entity.Property(e => e.CampoExp)
                    .HasMaxLength(100)
                    .HasColumnName("campo_exp");
                entity.Property(e => e.CodApr)
                    .HasMaxLength(50)
                    .HasColumnName("cod_apr");
                entity.Property(e => e.DescricaoCod)
                    .HasMaxLength(4000)
                    .HasColumnName("descricao_cod");
                entity.Property(e => e.FaixaEtaria)
                    .HasMaxLength(100)
                    .HasColumnName("faixa_etaria");
                entity.Property(e => e.IdadeAnosFinal).HasColumnName("idade_anos_final");
                entity.Property(e => e.IdadeAnosInicial).HasColumnName("idade_anos_inicial");
                entity.Property(e => e.IdadeMesesFinal).HasColumnName("idade_meses_final");
                entity.Property(e => e.IdadeMesesInicial).HasColumnName("idade_meses_inicial");
                entity.Property(e => e.Obj)
                    .HasMaxLength(4000)
                    .HasColumnName("obj");
            });

            modelBuilder.Entity<EuOutroNosEdInf>(entity =>
            {
                entity.HasKey(e => e.Column1).HasName("PK__eu_outro__DB06C12DC2845C0E");

                entity.ToTable("eu_outro_nos_ed_inf");

                entity.Property(e => e.Column1)
                    .ValueGeneratedNever()
                    .HasColumnName("column1");
                entity.Property(e => e.CampoExp)
                    .HasMaxLength(50)
                    .HasColumnName("campo_exp");
                entity.Property(e => e.CodApr)
                    .HasMaxLength(50)
                    .HasColumnName("cod_apr");
                entity.Property(e => e.DescricaoCod)
                    .HasMaxLength(4000)
                    .HasColumnName("descricao_cod");
                entity.Property(e => e.FaixaEtaria)
                    .HasMaxLength(100)
                    .HasColumnName("faixa_etaria");
                entity.Property(e => e.IdadeAnosFinal).HasColumnName("idade_anos_final");
                entity.Property(e => e.IdadeAnosInicial).HasColumnName("idade_anos_inicial");
                entity.Property(e => e.IdadeMesesFinal).HasColumnName("idade_meses_final");
                entity.Property(e => e.IdadeMesesInicial).HasColumnName("idade_meses_inicial");
                entity.Property(e => e.Obj)
                    .HasMaxLength(4000)
                    .HasColumnName("obj");
            });

        }

 
    }
}
