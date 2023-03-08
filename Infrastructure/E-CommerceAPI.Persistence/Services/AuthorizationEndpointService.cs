using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.Abstractions.Services.Configurations;
using E_CommerceAPI.Application.Repositories.EndpointRepository;
using E_CommerceAPI.Application.Repositories.MenuRepository;
using E_CommerceAPI.Domain.Entities;
using E_CommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        private readonly IApplicationService _applicationService;
        private readonly IEndpointReadRepository _endpointReadRepository;
        private readonly IEndpointWriteRepository _endpointWriteRepository;
        private readonly IMenuReadRepository _menuReadRepository;
        private readonly IMenuWriteRepository _menuWriteRepository;
        private readonly RoleManager<AppRole> _roleManager;
        public AuthorizationEndpointService(IApplicationService applicationService, IEndpointReadRepository endpointReadRepository, IEndpointWriteRepository endpointWriteRepository, IMenuWriteRepository menuWriteRepository, IMenuReadRepository menuReadRepository, RoleManager<AppRole> roleManager)
        {
            _applicationService = applicationService;
            _endpointReadRepository = endpointReadRepository;
            _endpointWriteRepository = endpointWriteRepository;
            _menuWriteRepository = menuWriteRepository;
            _menuReadRepository = menuReadRepository;
            _roleManager = roleManager;
        }

        public async Task AssignRoleEndpointAsync(string[] roles, string menu, string code, Type type)
        {

            Menu _menu = await _menuReadRepository.GetSingleAsync(m => m.Name == menu);

            if(_menu == null)
            {
                _menu = new Menu()
                {
                    Id = Guid.NewGuid(),
                    Name = menu,
                };
                await _menuWriteRepository.AddAsync(_menu);

                await _menuWriteRepository.SaveAsync();
            }

            Endpoint? endpoint = await _endpointReadRepository.Table.Include(e => e.Menu).Include(e => e.Roles).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);

            if(endpoint == null)
            {

               var action = _applicationService.GetAuthorizeDefinitonEndPoints(type)
                    .FirstOrDefault(m => m.Name == menu)?
                    .Actions.FirstOrDefault(e => e.Code == code);

                endpoint = new Endpoint()
                            {
                                Id = Guid.NewGuid(),
                                Code = code,
                                HttpType = action.HttpType,
                                Definition = action.Definition,
                                ActionType = action.ActionType,
                                Menu= _menu,
                            };


                await _endpointWriteRepository.AddAsync(endpoint);

                await _endpointWriteRepository.SaveAsync();

            }

            foreach (var role in endpoint.Roles)
            {
                endpoint.Roles.Remove(role);

            }

            var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

            foreach (var role in appRoles)
            {
                endpoint.Roles.Add(role);

            }

            await _endpointWriteRepository.SaveAsync();



        }

        public async Task<List<string>> GetRolesToEndpointAsync(string code, string menu)
        {
            Endpoint? endpoint = await _endpointReadRepository.Table.Include(e => e.Roles).Include(e => e.Menu).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);

            if(endpoint == null)
            {
                return new List<string>();
            }

           return endpoint.Roles.Select(r => r.Name).ToList();
        }
    }
}
