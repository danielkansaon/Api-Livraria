using System.Collections.Generic;

namespace Api_Livraria.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class carrinho
    {
        public long idcarrinho { get; set; }
        public long idusuario { get; set; }
        public List<livro> itens { get; set; }
        public usuario user { get; set; }
    }
}