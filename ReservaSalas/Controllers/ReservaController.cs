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

        [HttpGet]
        [Route(@"RecuperarReservaPorData/{data:regex({0-9}{4}\-{0-9}{2})}/{id:int}")]
        public IHttpActionResult ListarReservasPorData(string data)
        {
            try
            {
                ReservaDAO reservaDAO = new ReservaDAO();
                //List<Reserva> lstReservas = ListarReservas1().Where(x => x.data == data).ToList();
                return Ok(reservaDAO.ListarReservas());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Reserva/5
        [HttpGet]
        [Route("RecuperarReserva/{id:int}")]
        public ReservaDTO RecuperarReserva(int id)
        {
            ReservaDAO reservaDAO = new ReservaDAO();
            return reservaDAO.ListarReserva(id);
        }

        [HttpGet]
        [Route("ListarReservaPorSala/{id:int}")]
        [Authorize]
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
        [Route(@"RecuperarReservaPorData/{data:regex({0-9}{4}\-{0-9}{2})}/{id:int}")]
        public ReservaDTO RecuperarReservaPorData(int id)
        {
            ReservaDAO reservaDAO = new ReservaDAO();
            return reservaDAO.ListarReservas().Where(x => x.Id == id).FirstOrDefault();
        }

        // PUT: api/Reserva/5
        [HttpPut]
        public IHttpActionResult Put(int Id, ReservaDTO reserva)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                ReservaModel sl = new ReservaModel();

                if (Id == 0)
                    sl.Inserir(reserva);
                else
                    sl.Editar(reserva);

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
