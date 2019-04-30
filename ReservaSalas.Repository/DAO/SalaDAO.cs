using ReservaSalas.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ReservaSalas.Repository.DAO
{
    public class SalaDAO
    {
        private string strConn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private IDbConnection conn;

        public SalaDAO()
        {
            conn = new SqlConnection(strConn);
            conn.Open();
        }

        public List<SalaDTO> ListarSalas()
        {
            var lstSalas = new List<SalaDTO>();
            try
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = "select * from salas";

                IDataReader res = command.ExecuteReader();

                while (res.Read())
                {
                    var sala = new SalaDTO
                    {
                        Id = Convert.ToInt32(res["Id"]),
                        Nome = Convert.ToString(res["Nome"])
                    };

                    lstSalas.Add(sala);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lstSalas;
        }

        public SalaDTO ListarSala(int Id)
        {
            var sala = new SalaDTO();

            try
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = $"select * from salas where Id = {Id}";

                IDataReader res = command.ExecuteReader();

                while (res.Read())
                {
                    sala.Id = Convert.ToInt32(res["Id"]);
                    sala.Nome = Convert.ToString(res["Nome"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return sala;
        }

        public void InserirSala(SalaDTO sala)
        {
            try
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = "insert into Salas (Nome) values (@Nome)";

                IDbDataParameter Nome = new SqlParameter("Nome", sala.Nome);
                command.Parameters.Add(Nome);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void EditarSala(SalaDTO sala)
        {
            try
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = "update Salas set Nome = @Nome where Id = @Id";

                IDbDataParameter Id = new SqlParameter("Id", sala.Id);
                IDbDataParameter Nome = new SqlParameter("Nome", sala.Nome);
                command.Parameters.Add(Id);
                command.Parameters.Add(Nome);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void ExcluirSala(int id)
        {
            try
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = "delete from Salas where Id = @Id";

                IDbDataParameter Id = new SqlParameter("Id", id);
                command.Parameters.Add(Id);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}