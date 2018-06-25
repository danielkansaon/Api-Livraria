using System.Collections.Generic;
using Api_Livraria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api_Livraria.Controllers.v1
{
    /// <summary>
    /// Recurso para gerenciamento dos carrinho
    /// </summary>
    [Route("api/publico/v1/carrinho")]
    public class CarrinhoController : ControllerBase
    {
        private const string API_AUTENTICACAO = " http://localhost:5000/publico/v1/credencial/";
        private static List<carrinho_model> _listaCarrinho;
        private static List<carrinho_model> ListaCarrinho
        {
            get
            {
                if (_listaCarrinho == null)
                {
                    _listaCarrinho = new List<carrinho_model>();
                }
                return _listaCarrinho;
            }
        }

        /// <summary>
        /// Adiciona um novo carrinho
        /// </summary>
        /// <param name="user">Usuário associado ao carrinho</param>
        /// <returns></returns>
        [ProducesResponseType(200), ProducesResponseType(500), ProducesResponseType(404)]
        [HttpPost, Route("")]
        public IActionResult Post([FromBody] usuario_model user)
        {
            if (AutorizacaoUsuario(user.idusuario))
            {
                ListaCarrinho.Add(new carrinho_model() { idcarrinho = ListaCarrinho.Count + 1, idusuario = user.idusuario });
                return Ok(ListaCarrinho.Count - 1);
            }

            return Unauthorized();
        }

        /// <summary>
        /// Adciona um item em um carrinho já existente
        /// </summary>
        /// <param name="idcarrinho">Id do carrinho que se deseja adicionar o item</param>
        /// <param name="livro">Livro que será adicionado no carrinho</param>
        /// <returns></returns>
        [ProducesResponseType(200), ProducesResponseType(500), ProducesResponseType(404),
        ProducesResponseType(400)]
        [HttpPost, Route("{idcarrinho}/item")]
        public IActionResult Post(int idcarrinho, [FromBody] livro_model livro)
        {
            if (ListaCarrinho.Count - 1 < idcarrinho || idcarrinho < 0)
                return BadRequest("O parametro não representa um código de carrinho válido.");
            else if (LivroController.ListaLivro.Count < 0 || LivroController.ListaLivro.Where(x => x.isbn.ToLower() == livro.isbn.ToLower()) == null)
            {
                return BadRequest("O parametro isbn não representa um código de livro válido.");
            }
            livro_model livroFind = LivroController.ListaLivro.Find(x => x.isbn.ToLower() == livro.isbn.ToLower());

            if (livroFind == null)
                return NotFound("Livro não encontrado");

            ListaCarrinho[idcarrinho].itens.Add(livroFind);
            return Ok("Cadastrado!");
        }

        /// <summary>
        /// Retorna todos os itens existentes em um carrinho 
        /// </summary>
        /// <param name="idcarrinho">Código do carrinho</param>
        /// <returns></returns>
        [ProducesResponseType(200), ProducesResponseType(500), ProducesResponseType(404)]
        [HttpGet, Route("{idcarrinho}/item")]
        public IActionResult Get(int idcarrinho)
        {
            if (ListaCarrinho.Count - 1 < idcarrinho || idcarrinho < 0)
                return BadRequest("O parametro não representa um código de carrinho válido.");

            return Ok(ListaCarrinho[idcarrinho - 1].itens);
        }

        private bool AutorizacaoUsuario(long idusuario)
        {
            //Fazer requisição para API_AUTENTICACAO;
            return true;
        }
    }
}