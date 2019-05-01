using ReservaSalas.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ReservaSalas.Repository.DAO
{
    public class ReservaDAO
    {
        private string strConn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private IDbConnection conn;

        public ReservaDAO()
        {
            conn = new SqlConnection(strConn);
            conn.Open();
        }

        public List<ReservaDTO> ListarReservas()
        {
            var lstReservas = new List<ReservaDTO>();
            try
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = "select * from reservas";

                IDataReader res = command.ExecuteReader();

                while (res.Read())
                {
                    SalaDTO sl = new SalaDTO();
                    SalaDAO sDAO = new SalaDAO();
                    int SalaId = 0;

                    if (res["SalaId"] != DBNull.Value)
                        SalaId = Convert.ToInt32(res["SalaId"]);

                    sl = sDAO.ListarSala(SalaId);
                    var reserva = new ReservaDTO
                    {
                        Id = Convert.ToInt32(res["Id"]),
                        Nome = Convert.ToString(res["Nome"]),
                        SalaId = 0
                    };

                    if (sl != null)
                    {
                        reserva = new ReservaDTO
                        {
                            Id = Convert.ToInt32(res["Id"]),
                            Nome = Convert.ToString(res["Nome"]),
                            SalaId = SalaId,
                            Sala = new SalaDTO
                            {
                                Id = SalaId,
                                Nome = sl.Nome,
                                LocalId = sl.LocalId
                            }
                        };
                    }

                    lstReservas.Add(reserva);
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

            return lstReservas;
        }

        public ReservaDTO ListarReserva(int Id)
        {
            var reserva = new ReservaDTO();

            try
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = $"select * from reservas where Id = {Id}";

                IDataReader res = command.ExecuteReader();

                while (res.Read())
                {
                    reserva.Id = Convert.ToInt32(res["Id"]);
                    reserva.Nome = Convert.ToString(res["Nome"]);
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

            return reserva;
        }

        public ReservaDTO ListarReservaPorSala(int SalaId)
        {
            var reserva = new ReservaDTO();

            try
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = $"select * from reservas where SalaId = {SalaId}";

                IDataReader res = command.ExecuteReader();

                while (res.Read())
                {
                    reserva.Id = Convert.ToInt32(res["Id"]);
                    reserva.Nome = Convert.ToString(res["Nome"]);
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

            return reserva;
        }

        public void InserirReserva(ReservaDTO reserva)
        {
            try
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = "insert into reservas (Nome) values (@Nome)";

                IDbDataParameter Nome = new SqlParameter("Nome", reserva.Nome);
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

        public void EditarReserva(ReservaDTO reserva)
        {
            try
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = "update Reservas set Nome = @Nome where Id = @Id";

                IDbDataParameter Id = new SqlParameter("Id", reserva.Id);
                IDbDataParameter Nome = new SqlParameter("Nome", reserva.Nome);
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

        public void ExcluirReserva(int id)
        {
            try
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = "delete from Reservas where Id = @Id";

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
