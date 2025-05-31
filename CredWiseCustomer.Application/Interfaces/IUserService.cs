using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredWiseCustomer.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto> GetByEmailAsync(string email);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
        Task<UserDto> UpdateUserAsync(int id, UpdateUserDto updateUserDto);
        Task<bool> SoftDeleteUserAsync(int id);
        Task<bool> RestoreUserAsync(int id);
        Task<bool> EmailExistsAsync(string email);
        Task<User> GetUserEntityByEmailAsync(string email);
    }
}
