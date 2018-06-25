using System.Collections.Generic;

namespace Api_Livraria.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class livro_model
    {
        public string isbn { get; set; }
        public string nome { get; set; }

        public double preco { get; set; }

        public List<comentario_model> comentarios { get; set; }

        public livro_model()
        {
            comentarios = new List<comentario_model>();
        }
    }
}