using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Domains
{
    public partial class Evento
    {
        public Evento()
        {
            Presencas = new HashSet<Presencas>();
        }

        [Key]
        [Column("Evento_id")]
        public int EventoId { get; set; }
        [Column("Categoria_id")]
        public int? CategoriaId { get; set; }
        [Column("Localizacao_id")]
        public int? LocalizacaoId { get; set; }
        [Required]
        [Column("Titulo_evento")]
        [StringLength(255)]
        public string TituloEvento { get; set; }
        [Column("Data_evento", TypeName = "datetime")]
        public DateTime DataEvento { get; set; }
        [Required]
        [Column("Acesso_livre")]
        public bool? AcessoLivre { get; set; }

        [ForeignKey(nameof(CategoriaId))]
        [InverseProperty("Evento")]
        public virtual Categoria Categoria { get; set; }
        [ForeignKey(nameof(LocalizacaoId))]
        [InverseProperty("Evento")]
        public virtual Localizacao Localizacao { get; set; }
        [InverseProperty("Evento")]
        public virtual ICollection<Presencas> Presencas { get; set; }
    }
}
