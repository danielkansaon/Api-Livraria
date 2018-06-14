using System.Collections.Generic;

namespace Api_Livraria.Models
{
    public class pedido
    {
        public long idpedido { get; set; }
        public List<livro> livros { get; set; }
    }
}