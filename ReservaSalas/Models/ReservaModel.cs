using ReservaSalas.Domain.DTOs;
using ReservaSalas.Repository.DAO;
using System;

namespace ReservaSalas.Models
{
    public class ReservaModel
    {
        public void Inserir(ReservaDTO reserva)
        {
            try
            {
                ReservaDAO reservaDAO = new ReservaDAO();
                reservaDAO.InserirReserva(reserva);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir reserva: " + ex.Message);
            }
        }

        public void Editar(ReservaDTO reserva)
        {
            try
            {
                ReservaDAO reservaDAO = new ReservaDAO();
                reservaDAO.EditarReserva(reserva);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao editar reserva: " + ex.Message);
            }
        }
    }
}