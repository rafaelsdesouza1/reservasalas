using ReservaSalas.Domain.DTOs;
using ReservaSalas.Repository.DAO;
using System;

namespace ReservaSalas.Models
{
    public class SalaModel
    {
        public void Inserir(SalaDTO sala)
        {
            try
            {
                SalaDAO salaDAO = new SalaDAO();
                salaDAO.InserirSala(sala);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir sala: " + ex.Message);
            }
        }

        public void Editar(SalaDTO sala)
        {
            try
            {
                SalaDAO salaDAO = new SalaDAO();
                salaDAO.EditarSala(sala);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao editar sala: " + ex.Message);
            }
        }
    }
}