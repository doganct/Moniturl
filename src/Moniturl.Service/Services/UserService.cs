using AutoMapper;
using Moniturl.Core;
using Moniturl.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Moniturl.Service
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public async Task<ServiceResult> RegisterAsync(RegisterDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            ServiceResult loginCreditendial = await CheckLoginCredentials(model);

            if (!loginCreditendial.Success)
                return loginCreditendial;


            byte[] passwordHash, passwordSalt;
            CreatePasswordHashAndSalt(model.Password, out passwordHash, out passwordSalt);

            var user = _mapper.Map<User>(model);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.RoleId = Role.User.GetHashCode();

            await _userRepository.AddAsync(user);

            return new ServiceResult();
        }

        private async Task<ServiceResult> CheckLoginCredentials(RegisterDto model)
        {
            var errorList = new Dictionary<string, string>();

            var userSpecification = new UserSpecification(model.Email);

            var user = await _userRepository.GetBySpecAsync(userSpecification);

            bool isEmailDuplicated = user != null;

            if (isEmailDuplicated)
                errorList.Add(nameof(model.Email), Messages.ThisEmailAddressIsNotAvailable);

            return new ServiceResult { ErrorMessages = errorList };
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        private void CreatePasswordHashAndSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<ServiceResult<UserDto>> LoginAsync(LoginDto model)
        {
            var userSpecification = new UserSpecification(model.Email, true);

            var user = await _userRepository.GetBySpecAsync(userSpecification);

            if(user == null)
            {
                var result = new ServiceResult<UserDto>();
                result.ErrorMessages.Add(string.Empty, Messages.CheckYourEmailAddressOrPassword);
                return result;
            }

            if (!VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                var result = new ServiceResult<UserDto>();
                result.ErrorMessages.Add(string.Empty, Messages.CheckYourEmailAddressOrPassword);
                return result;
            }

            var userDto = _mapper.Map<UserDto>(user);

            return new ServiceResult<UserDto>
            {
                Result = userDto
            };
        }
    }
}
