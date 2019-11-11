using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Domains
{
    public partial class Presencas
    {
        [Key]
        [Column("Preseca_id")]
        public int PresecaId { get; set; }
        [Column("Usuario_id")]
        public int? UsuarioId { get; set; }
        [Column("Evento_id")]
        public int? EventoId { get; set; }
        [Required]
        [Column("Presenca_status")]
        [StringLength(255)]
        public string PresencaStatus { get; set; }

        [ForeignKey(nameof(EventoId))]
        [InverseProperty("Presencas")]
        public virtual Evento Evento { get; set; }
        [ForeignKey(nameof(UsuarioId))]
        [InverseProperty("Presencas")]
        public virtual Usuario Usuario { get; set; }
    }
}
