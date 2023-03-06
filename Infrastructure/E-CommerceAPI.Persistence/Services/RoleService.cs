using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRoleAsync(string name)
        {
           IdentityResult result = await _roleManager.CreateAsync(new AppRole() {Id=Guid.NewGuid().ToString() ,Name= name });

            return result.Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(string id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            IdentityResult result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

        public (object, int) GetAllRoles(int page, int size)
        {
            var count = _roleManager.Roles.Count();
            var datas = _roleManager.Roles.Skip(page * size).Take(size).Select(r => new { r.Id, r.Name });
            
            return (datas, count);
        }

        public async Task<(string id, string name)> GetRoleByIdAsync(string id)
        {
            string name = await _roleManager.GetRoleIdAsync(new AppRole() { Id = id });
            return (id, name);
        }

        public async Task<bool> UpdateRoleAsync(string id, string name)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            role.Name= name;
            IdentityResult result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }
    }
}
