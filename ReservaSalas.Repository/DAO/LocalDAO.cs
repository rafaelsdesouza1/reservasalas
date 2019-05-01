using ReservaSalas.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ReservaSalas.Repository.DAO
{
    public class LocalDAO
    {
        private string strConn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private IDbConnection conn;

        public LocalDAO()
        {
            conn = new SqlConnection(strConn);
            conn.Open();
        }

        public List<LocalDTO> ListarLocais()
        {
            var lstLocals = new List<LocalDTO>();
            try
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = "select * from Locais";

                IDataReader res = command.ExecuteReader();

                while (res.Read())
                {
                    var Local = new LocalDTO
                    {
                        Id = Convert.ToInt32(res["Id"]),
                        Nome = Convert.ToString(res["Nome"])
                    };

                    lstLocals.Add(Local);
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

            return lstLocals;
        }

        public LocalDTO ListarLocal(int Id)
        {
            var Local = new LocalDTO();

            try
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = $"select * from Locais where Id = {Id}";

                IDataReader res = command.ExecuteReader();

                while (res.Read())
                {
                    Local.Id = Convert.ToInt32(res["Id"]);
                    Local.Nome = Convert.ToString(res["Nome"]);
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

            return Local;
        }

        public void InserirLocal(LocalDTO Local)
        {
            try
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = "insert into Locais (Nome) values (@Nome)";

                IDbDataParameter Nome = new SqlParameter("Nome", Local.Nome);
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

        public void EditarLocal(LocalDTO Local)
        {
            try
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = "update Locais set Nome = @Nome where Id = @Id";

                IDbDataParameter Id = new SqlParameter("Id", Local.Id);
                IDbDataParameter Nome = new SqlParameter("Nome", Local.Nome);
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

        public void ExcluirLocal(int id)
        {
            try
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = "delete from Locais where Id = @Id";

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
