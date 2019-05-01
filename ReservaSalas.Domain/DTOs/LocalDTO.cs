using System.Collections.Generic;

namespace ReservaSalas.Domain.DTOs
{
    public class LocalDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public virtual ICollection<SalaDTO> Salas { get; set; }
    }
}
