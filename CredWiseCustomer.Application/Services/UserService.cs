using AutoMapper;
using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Application.Interfaces;
using CredWiseCustomer.Core.Entities;

namespace CredWiseCustomer.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        //public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        //{
        //    if (await EmailExistsAsync(createUserDto.Email))
        //        throw new Exception("A user with this email already exists.");

        //    var user = _mapper.Map<User>(createUserDto);
        //    user.Password = _passwordHasher.HashPassword(createUserDto.Password);
        //    user.IsActive = true;

        //    var createdUser = await _userRepository.AddAsync(user);
        //    return _mapper.Map<UserDto>(createdUser);
        //}

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            if (await EmailExistsAsync(createUserDto.Email))
                throw new Exception("A user with this email already exists.");

            var user = _mapper.Map<User>(createUserDto);
            user.Password = _passwordHasher.HashPassword(createUserDto.Password);
            user.IsActive = true;

            // Set role based on CreatedBy
            user.Role = createUserDto.CreatedBy.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                ? "Admin"
                : "Customer";

            var createdUser = await _userRepository.AddAsync(user);
            return _mapper.Map<UserDto>(createdUser);
        }
        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id)
                       ?? throw new Exception($"User with ID {id} not found.");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email)
                       ?? throw new Exception($"User with email {email} not found.");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(id)
                                ?? throw new Exception("User not found.");

            _mapper.Map(updateUserDto, existingUser);

            if (!string.IsNullOrWhiteSpace(updateUserDto.Password))
            {
                existingUser.Password = _passwordHasher.HashPassword(updateUserDto.Password);
            }

            var updatedUser = await _userRepository.UpdateAsync(existingUser);
            return _mapper.Map<UserDto>(updatedUser);
        }

        public async Task<bool> SoftDeleteUserAsync(int id)
        {
            return await _userRepository.SoftDeleteAsync(id);
        }

        public async Task<bool> RestoreUserAsync(int id)
        {
            return await _userRepository.RestoreAsync(id);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user != null;
        }

        public async Task<User> GetUserEntityByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                throw new Exception($"User with email {email} not found.");
            return user;
        }
    }
}
