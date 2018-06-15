using System;
using System.Collections.Generic;
using Api_Livraria.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api_Livraria.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
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

/// <summary>
/// 
/// </summary>
/// <param name="isbn"></param>
/// <param name="idusuario"></param>
/// <param name="datainicio"></param>
/// <param name="datafim"></param>
/// <returns></returns>
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
/// <summary>
/// 
/// </summary>
/// <param name="idpedido"></param>
/// <returns></returns>
        [HttpGet, Route("{idpedido}")]
        public IActionResult Get(long idpedido)
        {
            return Ok(ListaPedido.Find(x => x.idpedido == idpedido));
        }
    }
}