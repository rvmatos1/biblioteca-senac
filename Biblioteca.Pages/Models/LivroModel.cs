using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Pages.Models
{
    public class LivroModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LivroID { get; set; }
        
        [Required(ErrorMessage = "Título do Livro é obrigatório!")]
        public string? TituloLivro { get; set; }

        [Required(ErrorMessage = "Autor é obrigatório!")]
        public string? Autor { get; set; }

        [Required(ErrorMessage = "ISBN é obrigatório!")]
        public string? ISBN { get; set; }

        [Required(ErrorMessage = "Ano de Publicação é obrigatória!")]
        public int AnoPublicacao { get; set; }

        [Required(ErrorMessage = "Quantidade disponível é obrigatória!")]
        [Range(1, int.MaxValue, ErrorMessage = "O valor deve ser maior que 0.")]
        public int QuantidadeDisponivel { get; set; }

        public bool Ativo { get; set; }
    }
}