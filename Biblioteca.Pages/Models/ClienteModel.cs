using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Pages.Models
{
    public class ClienteModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório!")] //verificar como por este campo como unico
        public string? Email { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório!")]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório!")]
        public string? Cpf { get; set; }
    }
}
