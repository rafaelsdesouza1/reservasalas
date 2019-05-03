using ReservaSalas.Domain.DTOs;
using ReservaSalas.Models;
using ReservaSalas.Repository.DAO;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ReservaReservas.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Reserva")]
    public class ReservaController : ApiController
    {
        [HttpGet]
        [Route("ListarReservas")]
        [Authorize]
        public IHttpActionResult ListarReservas()
        {
            try
            {
                ReservaDAO reservaDAO = new ReservaDAO();
                return Ok(reservaDAO.ListarReservas());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Reserva/5
        [HttpGet]
        [Route("ListarReserva/{id:int}")]
        public ReservaDTO ListarReserva(int id)
        {
            ReservaDAO reservaDAO = new ReservaDAO();
            return reservaDAO.ListarReserva(id);
        }

        [HttpGet]
        [Route("ListarReservaPorSala/{id:int}")]
        public IHttpActionResult ListarReservaPorSala(int id)
        {
            try
            {
                ReservaDAO reservaDAO = new ReservaDAO();
                return Ok(reservaDAO.ListarReservaPorSala(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpGet]
        [Route("ValidarDisponibilidadeSala/{id}/{DtIni}/{HrIni}/{DtFim}/{HrFim}")]
        [Authorize]
        public IHttpActionResult ValidarDisponibilidadeSala(int id, string DtIni, string HrIni, string DtFim, string HrFim)
        {
            try
            {
                var HrIniRpl = HrIni.Replace("-", ":");
                var HrFimRpl = HrFim.Replace("-", ":");
                DateTime DtHrIni = Convert.ToDateTime(DtIni + " " + HrIniRpl);
                DateTime DtHrFim = Convert.ToDateTime(DtFim + " " + HrFimRpl);
                ReservaDAO reservaDAO = new ReservaDAO();
                return Ok(reservaDAO.ValidarDisponibilidadeSala(id, DtHrIni, DtHrFim));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Reserva/5
        [HttpPut]
        public IHttpActionResult Put(int Id, ReservaDTO reserva)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                ReservaDAO reservaDAO = new ReservaDAO();
                ReservaDTO reservaDTO = reservaDAO.ValidarDisponibilidadeSala(reserva.SalaId, reserva.DtHrIni, reserva.DtHrFim);

                if (reservaDTO.Nome == null)
                {
                    ReservaModel sl = new ReservaModel();

                    if (Id == 0)
                        sl.Inserir(reserva);
                    else
                        sl.Editar(reserva);
                }
                
                return Ok("Executado com sucesso!");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Reserva/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                ReservaDAO reservaDAO = new ReservaDAO();
                reservaDAO.ExcluirReserva(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
