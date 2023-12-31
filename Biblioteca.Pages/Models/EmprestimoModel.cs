using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Pages.Models
{
    public class EmprestimoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmprestimoID { get; set; }

        [Required(ErrorMessage = "Data do empréstimo é obrigatória!")]
        public DateTime DataEmprestimo { get; set; }

        [Required(ErrorMessage = "Data de devolução prevista é obrigatória!")]
        public DateTime DataDevolucaoPrevista { get; set; }

        [ForeignKey("Bibliotecario")]
        public int BibliotecarioID { get; set; }
        public BibliotecarioModel? Bibliotecario { get; set; }

        [ForeignKey("Livro")]
        public int LivroID { get; set; } 
        public LivroModel? Livro { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteID { get; set; }
        public ClienteModel? Cliente { get; set; }

        public bool Ativo { get; set; }
    }
}