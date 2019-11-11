using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Domains
{
    public partial class Usuario
    {
        public Usuario()
        {
            Presencas = new HashSet<Presencas>();
        }

        [Key]
        [Column("Usuario_id")]
        public int UsuarioId { get; set; }
        [Column("Tipo_usuario_id")]
        public int? TipoUsuarioId { get; set; }
        [Required]
        [StringLength(255)]
        public string Nome { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [StringLength(255)]
        public string Senha { get; set; }

        [ForeignKey(nameof(TipoUsuarioId))]
        [InverseProperty("Usuario")]
        public virtual TipoUsuario TipoUsuario { get; set; }
        [InverseProperty("Usuario")]
        public virtual ICollection<Presencas> Presencas { get; set; }
    }
}
