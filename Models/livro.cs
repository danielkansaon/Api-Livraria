using System.Collections.Generic;

namespace Api_Livraria.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class livro
    {
        public string isbn { get; set; }
        public string nome { get; set; }

        public List<comentario> comentarios { get; set; }
    }
}