using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos
{
    public class ProgrammingLanguageListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GetTechnologies> Technologies { get; set; }

        public class GetTechnologies
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
