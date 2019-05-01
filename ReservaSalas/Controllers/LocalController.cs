using ReservaSalas.Domain.DTOs;
using ReservaSalas.Models;
using ReservaSalas.Repository.DAO;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ReservaSalas.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Local")]
    public class LocalController : ApiController
    {
        [HttpGet]
        [Route("ListarLocais")]
        [Authorize]
        public IHttpActionResult ListarLocais()
        {
            try
            {
                LocalDAO localDAO = new LocalDAO();
                return Ok(localDAO.ListarLocais());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        
        // GET: api/Local/5
        [HttpGet]
        [Route("RecuperarLocal/{id:int}")]
        public LocalDTO RecuperarLocal(int id)
        {
            LocalDAO localDAO = new LocalDAO();
            return localDAO.ListarLocal(id);
        }

        // PUT: api/Local/5
        [HttpPut]
        public IHttpActionResult Put(int Id, LocalDTO local)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                LocalModel lc = new LocalModel();

                if (Id == 0)
                    lc.Inserir(local);
                else
                    lc.Editar(local);

                return Ok("Executado com sucesso!");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Local/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                LocalDAO localDAO = new LocalDAO();
                localDAO.ExcluirLocal(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
