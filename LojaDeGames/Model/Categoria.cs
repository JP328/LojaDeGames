using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaDeGames.Model
{
    public class Categoria
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = "varchar"), StringLength(255)]
        public string tipo { get; set; } = string.Empty;

        [InverseProperty("Categoria")]
        public virtual ICollection<Produto>? Produto { get; set; }


    }
}
