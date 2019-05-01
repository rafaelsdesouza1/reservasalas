using ReservaSalas.Domain.DTOs;
using ReservaSalas.Repository.DAO;
using System;

namespace ReservaSalas.Models
{
    public class LocalModel
    {
        public void Inserir(LocalDTO local)
        {
            try
            {
                LocalDAO localDAO = new LocalDAO();
                localDAO.InserirLocal(local);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir local: " + ex.Message);
            }
        }

        public void Editar(LocalDTO local)
        {
            try
            {
                LocalDAO localDAO = new LocalDAO();
                localDAO.EditarLocal(local);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao editar local: " + ex.Message);
            }
        }
    }
}