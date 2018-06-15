using Microsoft.AspNetCore.Mvc;
using Api_Livraria.Models;
using System.Collections.Generic;
using System.Linq;

namespace Api_Livraria.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/publico/v1/livro")]
    public class LivroController : ControllerBase
    {
        private static List<livro> _listaLivro;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

/// <summary>
/// 
/// </summary>
/// <returns></returns>
        [HttpGet, Route("")]
        public ActionResult Get()
        {
            return Ok(ListaLivro);
        }
/// <summary>
/// 
/// </summary>
/// <param name="isbn"></param>
/// <returns></returns>
        [HttpGet, Route("{isbn}")]
        public ActionResult Get(string isbn)
        {
            return Ok(ListaLivro.Where(x => x.isbn.ToLower() == isbn.ToLower()));
        }
/// <summary>
/// 
/// </summary>
/// <param name="parametro"></param>
/// <returns></returns>
        [HttpPost, Route("")]
        public ActionResult Post([FromBody] livro parametro)
        {
            if(ListaLivro.Exists(x => x.isbn.ToUpper() == parametro.isbn.ToUpper())){
                return BadRequest("Já existe um livro com isbn cadastrado.");
            }
            
            ListaLivro.Add(parametro);
            return Ok("Cadastrado!");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="descricao"></param>
        /// <returns></returns>
        [HttpPost, Route("comentario")]
        public ActionResult Post([FromBody] string isbn, [FromBody] string descricao)
        {
          livro livro = ListaLivro.Find(x => x.isbn.ToUpper() == isbn.ToUpper());

          if(livro == null){
              return BadRequest("Código isbn inválido para o livro");
          }

          ListaLivro.Find(x => x.isbn.ToUpper() == isbn.ToUpper())
          .comentarios.Add(new comentario(){ descricao = descricao});

          return Ok("Comentário adicionado");
        }
    }
}