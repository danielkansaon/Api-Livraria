using Microsoft.AspNetCore.Mvc;
using Api_Livraria.Models;
using System.Collections.Generic;
using System.Linq;

namespace Api_Livraria.Controllers.v1
{
    /// <summary>
    /// Recurso para gerenciamento de livros
    /// </summary>
    [Route("api/publico/v1/livro")]
    public class LivroController : ControllerBase
    {
        private static List<livro_model> _listaLivro;

        /// <summary>
        /// ///         
        /// </summary>
        /// <returns></returns>
        public static List<livro_model> ListaLivro
        {
            get
            {
                if (_listaLivro == null)
                {
                    _listaLivro = new List<livro_model>()  { new livro_model(){isbn = "1", nome = "1"}};
                }
                return _listaLivro;
            }
        }

        /// <summary>
        /// Retorna todos os livros cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        public ActionResult Get()
        {
            return Ok(ListaLivro);
        }

        /// <summary>
        /// Retorna o livro que possui o respectivo ISBN
        /// </summary>
        /// <param name="isbn">Código do livro</param>
        /// <returns></returns>
        [ProducesResponseType(200), ProducesResponseType(500), ProducesResponseType(404)]
        [HttpGet, Route("{isbn}")]
        public ActionResult Get(string isbn)
        {
            return Ok(ListaLivro.Where(x => x.isbn.ToLower() == isbn.ToLower()));
        }

        /// <summary>
        /// Adiciona um novo livro na biblioteca
        /// </summary>
        /// <param name="livro">Livro que será incluído</param>
        /// <returns></returns>
        [ProducesResponseType(200), ProducesResponseType(500), ProducesResponseType(400),
        ProducesResponseType(404)]
        [HttpPost, Route("")]
        public ActionResult Post([FromBody] livro_model livro)
        {
            if (ListaLivro.Exists(x => x.isbn.ToUpper() == livro.isbn.ToUpper()))
            {
                return BadRequest("Já existe um livro com ISBN cadastrado.");
            }

            ListaLivro.Add(livro);
            return Ok("Cadastrado!");
        }

        /// <summary>
        /// Adiciona um comentário sobre um livro
        /// </summary>
        /// <param name="comentario">Comentário para ser adicionado</param>
        /// <param name="isbn">Código do livro</param>
        /// <returns></returns>
        [ProducesResponseType(200), ProducesResponseType(500), ProducesResponseType(400),
        ProducesResponseType(404)]
        [HttpPost, Route("{isbn}/comentario")]
        public ActionResult Post([FromBody] comentario_model comentario, string isbn)
        {
            livro_model livro = ListaLivro.Find(x => x.isbn.ToUpper() == isbn.ToUpper());

            if (livro == null)
            {
                return BadRequest("Código isbn inválido para o livro");
            }

            ListaLivro.Find(x => x.isbn.ToUpper() == isbn.ToUpper())
            .comentarios.Add(new comentario_model() { descricao = comentario.descricao });

            return Ok("Comentário adicionado!");
        }
    }
}