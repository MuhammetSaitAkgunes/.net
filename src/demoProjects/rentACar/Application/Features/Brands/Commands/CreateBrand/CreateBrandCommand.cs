using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand:IRequest<CreatedBrandDto>
    {
        public string Name { get; set; }

        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandDto> //request olarak command alıp onu dto olarak geri döndürme bildirimi.
        {
            //işlem yapılacak kısımların enjekte edilmesi
            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;
            private readonly BrandBusinessRules _brandBusinessRules;

            public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRules = brandBusinessRules;
            }

            public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                //rule tarafında yazıldığı ve tüm commandler buradan geçiyor. tüm proje için tek try catch.
                await _brandBusinessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);

                //request, command'e denk geliyor. command de crud işlemlerimizi temsil ediyor. burada requesti commande mapliyoruz.
                Brand mappedBrand = _mapper.Map<Brand>(request);
                //oluşan requesti async şekilde db'ye kaydediyoruz.
                Brand createdBrand = await _brandRepository.AddAsync(mappedBrand);
                //create...commandhandler classını tanımlarken command alıp o commandın dto'sunu çevireceğimizi söyledik. burada da db'ye eklenen brandi dto'ya çevirdik.
                CreatedBrandDto createdBrandDto = _mapper.Map<CreatedBrandDto>(createdBrand);

                //dto return'ü.
                return createdBrandDto;

            }
        }
    }
}
