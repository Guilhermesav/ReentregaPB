using System;
using System.Collections.Generic;
using System.Text;

namespace ReentregaPB.Dominio.Model.Entity
{
    public class PostEntity
    {
        public int Id { get; set; }

        public string Categoria { get; set; }

        public string Texto { get; set; }

        public string UrlFoto { get; set; }

        public string Poster { get; set; }

        public List<PostEntity> Comentarios { get; set; }

    }
}
