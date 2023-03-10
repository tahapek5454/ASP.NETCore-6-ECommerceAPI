using E_CommerceAPI.Application.DTOs.Users;
using E_CommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO model);
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessToken);
        Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        Task<List<ListUserDTO>> GetAllUsersAsync(int page, int size);
        int TotaUsersCount { get; }
        Task AssignRoleToUser(string userId, string[] roles);
        Task<string[]> GetRolesToUserAsync(string idOrUserName);
        Task<bool> HasRolePermissionToEndpointAsync(string userName, string code);
    }
}
