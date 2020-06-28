using System;
using System.Collections.Generic;
using System.Text;

namespace ReentregaPB.Dominio.Model.Options
{
    public class PostHttpOptions
    {
        public Uri ApiBaseUrl { get; set; }
        public string PostPath { get; set; }

        public string ComentarioPath { get; set; }
        public string Name { get; set; }
        public int Timeout { get; set; }
    }
}
