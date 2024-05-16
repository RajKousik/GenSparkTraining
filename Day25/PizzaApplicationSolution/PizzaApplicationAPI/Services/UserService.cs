using AutoMapper;
using Easy_Password_Validator;
using PizzaApplicationAPI.Exceptions.CommonExceptions;
using PizzaApplicationAPI.Exceptions.UserExceptions;
using PizzaApplicationAPI.Interfaces;
using PizzaApplicationAPI.Models;
using PizzaApplicationAPI.Models.DTOs;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;


namespace PizzaApplicationAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<int, User> _userRepo;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly PasswordValidatorService _passwordValidatorService;

        public UserService(IRepository<int, User> userRepo, IMapper mapper, ITokenService tokenService, PasswordValidatorService passwordValidatorService)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _tokenService = tokenService;
            _passwordValidatorService = passwordValidatorService;
        }

        public async Task<LoginReturnDTO> Login(UserLoginDTO loginDTO)
        {
            var userDB = (await _userRepo.GetAll()).SingleOrDefault(u => u.Email == loginDTO.Email);
            //var userDB = await _userRepo.Get(users.Id);
            if (userDB == null)
            {
                throw new UnauthorizedUserException("Invalid username or password");
            }

            HMACSHA512 hMACSHA = new HMACSHA512(userDB.PasswordHashKey);
            var encrypterPass = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            bool isPasswordSame = ComparePassword(encrypterPass, userDB.PasswordHash);

            if (isPasswordSame)
            {
                var loginReturnDTO = MapUserToLoginReturnDTO(userDB);
                loginReturnDTO.Token = _tokenService.GenerateToken(userDB);
                return loginReturnDTO;
            }

            throw new UnauthorizedUserException("Invalid username or password");

        }

        private LoginReturnDTO MapUserToLoginReturnDTO(User userDB)
        {
            LoginReturnDTO result = new LoginReturnDTO();
            result.UserId = userDB.Id;
            result.Username = userDB.Username;
            result.Email = userDB.Email;
            result.Token = _tokenService.GenerateToken(userDB);
            return result;
        }

        private bool ComparePassword(byte[] encrypterPass, byte[] password)
        {
            for (int i = 0; i < encrypterPass.Length; i++)
            {
                if (encrypterPass[i] != password[i])
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<UserRegisterDTO> Register(UserRegisterDTO registerDTO)
        {
            User user = null;
            try
            {
                var isEmailIdExists = (await _userRepo.GetAll()).SingleOrDefault(u => u.Email == registerDTO.Email);

                if (isEmailIdExists != null)
                {
                    throw new DuplicateUserException();
                }

                var isPasswordValid = _passwordValidatorService.TestAndScore(registerDTO.Password);

                if (!isPasswordValid)
                {
                    Debug.WriteLine("isPasswordValid", isPasswordValid);
                    throw new InvalidPasswordException();
                }


                user = MapRegiserDTOToUser(registerDTO);
                user = await _userRepo.Add(user);
                if (user == null)
                {
                    throw new UnableToRegisterException("Not able to register at this moment");
                }
                registerDTO.Password = Encoding.UTF8.GetString(user.PasswordHash);
                return registerDTO;
            }
            catch (Exception ex)
            {
                throw new UnableToRegisterException($"{ex.Message}");
            }

        }


        private User MapRegiserDTOToUser(UserRegisterDTO regiserDTO)
        {
            User user = new User();
            user.Username = regiserDTO.Username;
            user.Email = regiserDTO.Email;
            user.Age = regiserDTO.Age;
            HMACSHA512 hMACSHA = new HMACSHA512();
            user.PasswordHashKey = hMACSHA.Key;
            user.PasswordHash = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(regiserDTO.Password));
            return user;
        }
    }
}
