using ReservaSalas.Domain.DTOs;
using ReservaSalas.Models;
using ReservaSalas.Repository.DAO;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ReservaSalas.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Sala")]
    public class SalaController : ApiController
    {
        [HttpGet]
        [Route("ListarSalas")]
        [Authorize]
        public IHttpActionResult ListarSalas()
        {
            try
            {
                SalaDAO salaDAO = new SalaDAO();
                return Ok(salaDAO.ListarSalas());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("ListarSalas/{ComReserva=true}")]
        public IHttpActionResult ListarSalasComReserva(bool ComReserva)
        {
            try
            {
                SalaDAO salaDAO = new SalaDAO();
                return Ok(salaDAO.ListarSalas());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Sala/5
        [HttpGet]
        [Route("RecuperarSala/{id:int}")]
        public SalaDTO RecuperarSala(int id)
        {
            SalaDAO salaDAO = new SalaDAO();
            return salaDAO.ListarSala(id);
        }

        [HttpGet]
        [Route("ListarSalaPorLocal/{id:int}")]
        public IHttpActionResult ListarSalaPorLocal(int id)
        {
            try
            {
                SalaDAO salaDAO = new SalaDAO();
                return Ok(salaDAO.ListarSalaPorLocal(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }

        [HttpGet]
        [Route("ListarSalasPorLocal/{id:int}")]
        public IHttpActionResult ListarSalasPorLocal(int id)
        {
            try
            {
                SalaDAO salaDAO = new SalaDAO();
                return Ok(salaDAO.ListarSalasPorLocal(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // PUT: api/Sala/5
        [HttpPut]
        public IHttpActionResult Put(int Id, SalaDTO sala)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                SalaModel sl = new SalaModel();

                if (Id == 0)
                    sl.Inserir(sala);
                else
                    sl.Editar(sala);

                return Ok("Executado com sucesso!");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Sala/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                SalaDAO salaDAO = new SalaDAO();
                salaDAO.ExcluirSala(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
