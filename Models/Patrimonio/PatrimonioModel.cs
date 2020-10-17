using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudPatrimonioEmpresarialJWT.Models.Patrimonio
{
    [Table(nameof(PatrimonioModel))]
    public class PatrimonioModel
    {
        [Column("Nome")]
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome do Patrimonio é obrigatório", AllowEmptyStrings = false)]
        public string Nome { get; set; }

        [Key]
        [Column("Id")]
        [Display(Name = "Código")]
        [Required(ErrorMessage = "O numero da marca é obrigatório", AllowEmptyStrings = false)]
        public int MarcaId { get; set; }

        [Column("Descricao")]
        [Display(Name = "Descrição do Patrimonio")]
        public string Descricao { get; set; }

        [Column("NumeroTombo")]
        [Display(Name = "Numero do tombo")]
        public int NumeroTombo { get; set; }
    }
}