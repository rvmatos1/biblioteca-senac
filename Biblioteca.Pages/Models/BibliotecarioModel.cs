using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Pages.Models
{
    public class BibliotecarioModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BibliotecarioID { get; set; }

        [Required(ErrorMessage ="Nome é obrigatório!")]
        public string? Nome { get; set; }

        [Required(ErrorMessage ="Email é obrigatório!")] //verificar como por este campo como unico
        public string? Email { get; set; }

        [Required(ErrorMessage ="Senha é obrigatória!")]
        public string? Senha { get; set; }

        [Required(ErrorMessage ="Telefone é obrigatório!")]
        public string? Telefone { get; set; }
    }
}