﻿using System;

namespace ReservaSalas.Domain.DTOs
{
    public class ReservaDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public DateTime DataIni { get; set; }

        public DateTime DataFim { get; set; }

        public int SalaId { get; set; }

        public virtual SalaDTO Sala { get; set; }
    }
}
