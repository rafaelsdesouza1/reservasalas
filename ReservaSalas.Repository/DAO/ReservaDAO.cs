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
                        DtHrIni = Convert.ToDateTime(res["DtHrIni"]),
                        DtHrFim = Convert.ToDateTime(res["DtHrFim"]),
                        Responsavel = Convert.ToString(res["Responsavel"]),
                        Cafe = Convert.ToBoolean(res["Cafe"]),
                        QtdePessoas = Convert.ToInt32(res["QtdePessoas"]),
                        SalaId = 0
                    };

                    if (sl != null)
                    {
                        reserva = new ReservaDTO
                        {
                            Id = Convert.ToInt32(res["Id"]),
                            Nome = Convert.ToString(res["Nome"]),
                            DtHrIni = Convert.ToDateTime(res["DtHrIni"]),
                            DtHrFim = Convert.ToDateTime(res["DtHrFim"]),
                            Responsavel = Convert.ToString(res["Responsavel"]),
                            Cafe = Convert.ToBoolean(res["Cafe"]),
                            QtdePessoas = Convert.ToInt32(res["QtdePessoas"]),
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
                    reserva.DtHrIni = Convert.ToDateTime(res["DtHrIni"]);
                    reserva.DtHrFim = Convert.ToDateTime(res["DtHrFim"]);
                    reserva.Responsavel = Convert.ToString(res["Responsavel"]);
                    reserva.Cafe = Convert.ToBoolean(res["Cafe"]);
                    reserva.QtdePessoas = Convert.ToInt32(res["QtdePessoas"]);
                    reserva.SalaId = Convert.ToInt32(res["SalaId"]);
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
                    reserva.DtHrIni = Convert.ToDateTime(res["DtHrIni"]);
                    reserva.DtHrFim = Convert.ToDateTime(res["DtHrFim"]);
                    reserva.Responsavel = Convert.ToString(res["Responsavel"]);
                    reserva.Cafe = Convert.ToBoolean(res["Cafe"]);
                    reserva.QtdePessoas = Convert.ToInt32(res["QtdePessoas"]);
                    reserva.SalaId = Convert.ToInt32(res["SalaId"]);
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

        public ReservaDTO ValidarDisponibilidadeSala(int SalaId, DateTime DtHrIni, DateTime DtHrFim)
        {
            var reserva = new ReservaDTO();

            try
            {
                IDbCommand command = conn.CreateCommand();
                command.CommandText = $"select * from reservas where SalaId = {SalaId} and ((DtHrIni between '{DtHrIni}' and '{DtHrFim}') or (DtHrFim between '{DtHrIni}' and '{DtHrFim}'))";

                IDataReader res = command.ExecuteReader();

                while (res.Read())
                {
                    reserva.Id = Convert.ToInt32(res["Id"]);
                    reserva.Nome = Convert.ToString(res["Nome"]);
                    reserva.DtHrIni = Convert.ToDateTime(res["DtHrIni"]);
                    reserva.DtHrFim = Convert.ToDateTime(res["DtHrFim"]);
                    reserva.Responsavel = Convert.ToString(res["Responsavel"]);
                    reserva.Cafe = Convert.ToBoolean(res["Cafe"]);
                    reserva.QtdePessoas = Convert.ToInt32(res["QtdePessoas"]);
                    reserva.SalaId = Convert.ToInt32(res["SalaId"]);
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
                command.CommandText = "insert into reservas (Nome, DtHrIni, DtHrFim, Responsavel, Cafe, QtdePessoas, SalaId) values (@Nome, @DtHrIni, @DtHrFim, @Responsavel, @Cafe, @QtdePessoas, @SalaId)";

                IDbDataParameter Nome = new SqlParameter("Nome", reserva.Nome);
                IDbDataParameter DtHrIni = new SqlParameter("DtHrIni", reserva.DtHrIni);
                IDbDataParameter DtHrFim = new SqlParameter("DtHrFim", reserva.DtHrFim);
                IDbDataParameter Responsavel = new SqlParameter("Responsavel", reserva.Responsavel);
                IDbDataParameter Cafe = new SqlParameter("Cafe", reserva.Cafe);
                IDbDataParameter QtdePessoas = new SqlParameter("QtdePessoas", reserva.QtdePessoas);
                IDbDataParameter SalaId = new SqlParameter("SalaId", reserva.SalaId);
                command.Parameters.Add(Nome);
                command.Parameters.Add(DtHrIni);
                command.Parameters.Add(DtHrFim);
                command.Parameters.Add(Responsavel);
                command.Parameters.Add(Cafe);
                command.Parameters.Add(QtdePessoas);
                command.Parameters.Add(SalaId);

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
                command.CommandText = "update Reservas set Nome = @Nome, DtHrIni = @DtHrIni, DtHrFim = @DtHrFim, Responsavel = @Responsavel, Cafe = @Cafe, QtdePessoas = @QtdePessoas, SalaId = @SalaId where Id = @Id";

                IDbDataParameter Id = new SqlParameter("Id", reserva.Id);
                IDbDataParameter Nome = new SqlParameter("Nome", reserva.Nome);
                IDbDataParameter DtHrIni = new SqlParameter("DtHrIni", reserva.DtHrIni);
                IDbDataParameter DtHrFim = new SqlParameter("DtHrFim", reserva.DtHrFim);
                IDbDataParameter Responsavel = new SqlParameter("Responsavel", reserva.Responsavel);
                IDbDataParameter Cafe = new SqlParameter("Cafe", reserva.Cafe);
                IDbDataParameter QtdePessoas = new SqlParameter("QtdePessoas", reserva.QtdePessoas);
                IDbDataParameter SalaId = new SqlParameter("SalaId", reserva.SalaId);
                command.Parameters.Add(Id);
                command.Parameters.Add(Nome);
                command.Parameters.Add(DtHrIni);
                command.Parameters.Add(DtHrFim);
                command.Parameters.Add(Responsavel);
                command.Parameters.Add(Cafe);
                command.Parameters.Add(QtdePessoas);
                command.Parameters.Add(SalaId);

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
