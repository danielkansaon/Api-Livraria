using System;
using System.Collections.Generic;
using Api_Livraria.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api_Livraria.Controllers.v1
{
    [Route("api/publico/v1/pedido")]
    public class PedidoController : ControllerBase
    {
        private static List<pedido> _listaPedido;
        private static List<pedido> ListaPedido
        {
            get
            {
                if (_listaPedido == null)
                    _listaPedido = new List<pedido>();

                return _listaPedido;
            }
        }


        [HttpPost, Route("")]
        public IActionResult Post(string isbn, long idusuario, DateTime datainicio, DateTime datafim)
        {
            livro pLivro = LivroController.ListaLivro.Find(x => x.isbn.ToUpper() == isbn.ToUpper());

            if (pLivro != null)
            {
                if (ListaPedido.Exists(x => (x.livros.isbn.ToUpper() == isbn.ToUpper())
                              && datainicio < x.DataInicio))
                {
                    return BadRequest("Data inválida");
                }

                ListaPedido.Add(new pedido()
                {
                    idpedido = ListaPedido.Count,
                    livros = pLivro,
                    DataInicio = datainicio,
                    DataFim = datafim
                });
            }
            else
            {
                return NotFound("Isbn não encontrado");
            }

            return Ok(ListaPedido.Count);
        }

        [HttpGet, Route("{idpedido}")]
        public IActionResult Get(long idpedido)
        {
            return Ok(ListaPedido.Find(x => x.idpedido == idpedido));
        }
    }
}