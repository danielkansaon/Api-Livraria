using Microsoft.AspNetCore.Mvc;
using Api_Livraria.Models;
using System.Collections.Generic;
using System.Linq;

namespace Api_Livraria.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/publico/v1/entrega")]
    public class EntregaController : ControllerBase
    {
        [HttpPost, Route("{idpedido}")]
        public ActionResult Post(long idPedido)
        {
            
            return Ok("Entregue!");
        }
    }
}