using System;
using System.Collections.Generic;
using Api_Livraria.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api_Livraria.Controllers.v1
{
    /// <summary>
    /// Recurso para gerenciamento de pedido de livros
    /// </summary>
    [Route("api/publico/v1/pedido")]
    public class PedidoController : ControllerBase
    {
        private static List<pedido_model> _listaPedido;
        private static List<pedido_model> ListaPedido
        {
            get
            {
                if (_listaPedido == null)
                    _listaPedido = new List<pedido_model>();

                return _listaPedido;
            }
        }

        /// <summary>
        /// Realiza uma reserva de um livro
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        [ProducesResponseType(200), ProducesResponseType(500), ProducesResponseType(400),
        ProducesResponseType(404)]
        [HttpPost, Route("")]
        public IActionResult Post([FromBody] pedido_model pedido)
        {
            livro_model pLivro = LivroController.ListaLivro.Find(x => x.isbn.ToUpper() == pedido.livro.isbn.ToUpper());

            if (pLivro != null)
            {
                if (ListaPedido.Exists(x => (x.livro.isbn.ToUpper() == pLivro.isbn.ToUpper())
                              && pedido.datainicio < x.datainicio))
                {
                    return BadRequest("Data inválida");
                }

                ListaPedido.Add(new pedido_model()
                {
                    idpedido = ListaPedido.Count,
                    livro = pLivro,
                    datainicio = pedido.datainicio,
                    datafim = pedido.datafim
                });
            }
            else
            {
                return NotFound("ISBN não encontrado");
            }

            return Ok(ListaPedido.Count);
        }

        /// <summary>
        /// Retorn uma reserva já realizada
        /// </summary>
        /// <param name="idpedido">Id da reserva</param>
        /// <returns></returns>
         [ProducesResponseType(200), ProducesResponseType(500), ProducesResponseType(404)]
        [HttpGet, Route("{idpedido}")]
        public IActionResult Get(long idpedido)
        {
            return Ok(ListaPedido.Find(x => x.idpedido == idpedido));
        }
    }
}