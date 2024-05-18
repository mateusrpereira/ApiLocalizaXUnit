using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class MunicipioEntity : BaseEntity
    {
        [Required]
        [MaxLength(60)]
        public string Nome { get; set; }

        public int CodIBGE { get; set; }

        [Required]
        public Guid UfId { get; set; }

        public UfEntity Uf { get; set; }

        public IEnumerable<CepEntity> Ceps { get; set; }

    }
}