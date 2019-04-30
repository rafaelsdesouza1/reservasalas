using System.ComponentModel.DataAnnotations;

namespace ReservaSalas.Domain.DTOs
{
    public class SalaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [StringLength(255, ErrorMessage = "Nome tem no mínimo 2 caracteres e no máximo 255.", MinimumLength = 2)]
        public string Nome { get; set; }

        //[Required(ErrorMessage = "O campo Local é obrigatório.")]
        public int LocalId { get; set; }
    }
}