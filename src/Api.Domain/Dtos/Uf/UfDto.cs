using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Dtos.Uf
{
    public class UfDto
    {
        public Guid Id { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
    }
}