using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaDeGames.Model
{
    public class Produto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = "varchar"), StringLength(255)]
        public string nome { get; set; } = string.Empty;

        [Column(TypeName = "varchar"), StringLength(2000)]
        public string descricao { get; set;} = string.Empty;

        [Column(TypeName = "varchar"), StringLength(255)]
        public string console { get; set;} = string.Empty;


        [Column(TypeName = "date")]
        public DateOnly DataLancamento { get; set; }

        //[Range(0.1, 10000, ErrorMessage = "O preço não pode ser negativo, nem passar de dez mil.")]
        //[DisplayFormat(DataFormatString = "{0:N2}")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal preco { get; set;} = decimal.Zero;

        [Column(TypeName = "varchar"), StringLength(2000)]
        public string foto { get; set;} = string.Empty;


        public virtual Categoria? Categoria { get; set;}

    }
}
