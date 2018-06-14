using System.Collections.Generic;
using Api_Livraria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api_Livraria.Controllers.v1
{
    [Route("api/publico/v1/carrinho")]
    public class CarrinhoController : ControllerBase
    {
        private static List<carrinho> _listaCarrinho;
        private static List<carrinho> ListaCarrinho
        {
            get
            {
                if (_listaCarrinho == null)
                {
                    _listaCarrinho = new List<carrinho>();
                }
                return _listaCarrinho;
            }
        }

        [HttpPost, Route("")]
        public IActionResult Post([FromBody] usuario user)
        {
            ListaCarrinho.Add(new carrinho() { idcarrinho = ListaCarrinho.Count + 1, idusuario = user.idusuario });
            return Ok(ListaCarrinho.Count - 1);
        }

        [HttpPost, Route("{idcarrinho}/item")]
        public IActionResult Post(int idcarrinho, [FromBody] string isbn)
        {
            if (ListaCarrinho.Count - 1 < idcarrinho || idcarrinho < 0)
                return BadRequest("O parametro não representa um código de carrinho válido.");
            else if(LivroController.ListaLivro.Count < 0 || LivroController.ListaLivro.Where(x => x.isbn.ToLower() == isbn.ToLower()) == null){
                return BadRequest("O parametro isbn não representa um código de livro válido.");
            }            

            ListaCarrinho[idcarrinho - 1].itens.Add(LivroController.ListaLivro.Where(x => x.isbn.ToLower() == isbn.ToLower()).FirstOrDefault());
            return Ok("Cadastrado!");
        }

        [HttpGet, Route("{idcarrinho}/item")]
        public IActionResult Get(int idcarrinho)
        {
            if (ListaCarrinho.Count - 1 < idcarrinho || idcarrinho < 0)
                return BadRequest("O parametro não representa um código de carrinho válido.");

            return Ok(ListaCarrinho[idcarrinho - 1].itens);
        }
    }
}