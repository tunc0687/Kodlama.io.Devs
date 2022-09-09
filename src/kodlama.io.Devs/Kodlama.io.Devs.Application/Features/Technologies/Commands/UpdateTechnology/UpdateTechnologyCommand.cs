using AutoMapper;
using Kodlama.io.Devs.Application.Features.Technologies.Dtos;
using Kodlama.io.Devs.Application.Features.Technologies.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommand : IRequest<UpdatedTechnologyDto>
    {
        public int Id { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }

        public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdatedTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public UpdateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<UpdatedTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
            {
                Technology? technology = await _technologyRepository.GetAsync(x => x.Id == request.Id);
                _technologyBusinessRules.TechnologyShouldExistWhenRequested(technology);

                await _technologyBusinessRules.TechnologyNameCanNotBeDuplicatedWhenInserted(request.Name);
                await _technologyBusinessRules.ProgrammingLanguageIdShouldExistWhenRequested(request.ProgrammingLanguageId);

                _mapper.Map(request, technology);
                Technology updatedTechnology = await _technologyRepository.UpdateAsync(technology);
                UpdatedTechnologyDto updateTechnologyDto = _mapper.Map<UpdatedTechnologyDto>(updatedTechnology);

                return updateTechnologyDto;
            }
        }
    }
}
