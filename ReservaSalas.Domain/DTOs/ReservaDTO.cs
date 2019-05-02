using System;

namespace ReservaSalas.Domain.DTOs
{
    public class ReservaDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public DateTime DtHrIni { get; set; }

        public DateTime DtHrFim { get; set; }

        public string Responsavel { get; set; }

        public bool Cafe { get; set; }

        public int QtdePessoas { get; set; }

        public int SalaId { get; set; }

        public virtual SalaDTO Sala { get; set; }
    }
}
