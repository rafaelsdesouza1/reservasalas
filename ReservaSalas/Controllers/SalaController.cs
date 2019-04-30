﻿using ReservaSalas.Domain.DTOs;
using ReservaSalas.Models;
using ReservaSalas.Repository.DAO;
using System;
using System.Data;
using System.Linq;
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

        [HttpGet]
        [Route(@"RecuperarSalaPorData/{data:regex({0-9}{4}\-{0-9}{2})}/{id:int}")]
        public IHttpActionResult ListarSalasPorData(string data)
        {
            try
            {
                SalaDAO salaDAO = new SalaDAO();
                //List<Sala> lstSalas = ListarSalas1().Where(x => x.data == data).ToList();
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
        [Route(@"RecuperarSalaPorData/{data:regex({0-9}{4}\-{0-9}{2})}/{id:int}")]
        public SalaDTO RecuperarSalaPorData(int id)
        {
            SalaDAO salaDAO = new SalaDAO();
            return salaDAO.ListarSalas().Where(x => x.Id == id).FirstOrDefault();
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