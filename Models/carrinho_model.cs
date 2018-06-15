using System.Collections.Generic;

namespace Api_Livraria.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class carrinho_model
    {
        public long idcarrinho { get; set; }
        public long idusuario { get; set; }
        public List<livro_model> itens { get; set; }
        public usuario_model user { get; set; }

        public carrinho_model()
        {
            itens = new List<livro_model>();
        }
    }
}