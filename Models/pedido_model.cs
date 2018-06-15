using System;
using System.Collections.Generic;

namespace Api_Livraria.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class pedido_model
    {
        public long idpedido { get; set; }
        public livro_model livro { get; set; }

        public DateTime datainicio { get; set; }

        public DateTime datafim { get; set; }
    }
}