using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudPatrimonioEmpresarialJWT.Models.Marca
{
    [Table(nameof(MarcaModel))]
    public class MarcaModel
    {
        [Column("Nome")]
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome da marca é obrigatório", AllowEmptyStrings = false)]
        public string Nome { get; set; }

        [Key]
        [Column("Id")]
        [Display(Name = "Código")]
        [Required(ErrorMessage = "O id da marca é obrigatório", AllowEmptyStrings = false)]
        public int MarcaId { get; set; }
    }
}