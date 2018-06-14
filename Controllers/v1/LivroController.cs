using Microsoft.AspNetCore.Mvc;
using Api_Livraria.Models;
using System.Collections.Generic;
using System.Linq;

namespace Api_Livraria.Controllers.v1
{
    [Route("api/publico/v1/livro")]
    public class LivroController : ControllerBase
    {
        private static List<livro> _listaLivro;
        public static List<livro> ListaLivro
        {
            get
            {
                if (_listaLivro == null)
                {
                    _listaLivro = new List<livro>();
                }
                return _listaLivro;
            }
        }


        [HttpGet, Route("")]
        public ActionResult Get()
        {
            return Ok(ListaLivro);
        }

        [HttpGet, Route("{isbn}")]
        public ActionResult Get(string isbn)
        {
            return Ok(ListaLivro.Where(x => x.isbn.ToLower() == isbn.ToLower()));
        }

        [HttpPost, Route("")]
        public ActionResult Post([FromBody] livro parametro)
        {
            if(ListaLivro.Exists(x => x.isbn.ToUpper() == parametro.isbn.ToUpper())){
                return BadRequest("Já existe um livro com isbn cadastrado.");
            }
            
            ListaLivro.Add(parametro);
            return Ok("Cadastrado!");
        }
    }
}