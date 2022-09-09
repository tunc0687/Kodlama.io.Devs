using Core.CrossCuttingConcerns.Exceptions;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

        public TechnologyBusinessRules(ITechnologyRepository technologyRepository, IProgrammingLanguageRepository programmingLanguageRepository)
        {
            _technologyRepository = technologyRepository;
            _programmingLanguageRepository = programmingLanguageRepository;
        }

        public async Task TechnologyNameCanNotBeDuplicatedWhenInserted(string name)
        {
            Technology? result = await _technologyRepository.GetAsync(x => x.Name == name);
            if (result != null) throw new BusinessException("Technology name exists.");
        }

        public async Task ProgrammingLanguageIdShouldExistWhenRequested(int programmingLanguageId)
        {
            ProgrammingLanguage? result = await _programmingLanguageRepository.GetAsync(x => x.Id == programmingLanguageId);
            if (result == null) throw new BusinessException("ProgrammingLanguage Id does not exists.");
        }

        public void TechnologyShouldExistWhenRequested(Technology technology)
        {
            if (technology == null) throw new BusinessException("Technology does not exists.");
        }
    }
}
