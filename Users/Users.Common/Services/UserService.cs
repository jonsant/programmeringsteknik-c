using System;
using System.Collections.Generic;
using System.Security.Authentication;
using Users.Common.Models;

namespace Users.Common.Services
{
    public class UserService : IUserService
    {
        Dictionary<Guid, IUser> UsersByGuid = new Dictionary<Guid, IUser>();
        Dictionary<string, IUser> UsersByEmail = new Dictionary<string, IUser>();
        Dictionary<Guid, string> PasswordByGuid = new Dictionary<Guid, string>();

        public IUser Get(string email)
        {
            if (UsersByEmail.TryGetValue(email, out IUser user))
                return user;

            return null;
        }

        public IUser Get(Guid userId)
        {
            if (UsersByGuid.TryGetValue(userId, out IUser user))
                return user;

            return null;
        }

        public IServiceResponse Login(string email, string password)
        {
            ServiceResponse response = new ServiceResponse();

            if (UsersByEmail.TryGetValue(email, out IUser user))
            {
                Guid userId = user.Id;
                string registeredPassword = PasswordByGuid[userId];

                if (password == registeredPassword)
                {
                    response.Success = true;
                    response.Message = "Logged in!";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Incorrect password!";
                    response.Exception = new AuthenticationException();
                }
                    
                return response;
            }

            //User newUser = new User();
            //newUser.Email = email;

            response.Success = false;
            response.Message = "User not found!";
            response.Exception = new ArgumentException();

            return response;

        }

        public IServiceResponse Register(IUser user)
        {
            ServiceResponse response = new ServiceResponse();

            if (UsersByGuid.ContainsKey(user.Id))
            {
                response.Success = false;
                response.Message = "User already registered!";
                response.Exception = new ArgumentException();
            }
            else
            {
                UsersByEmail.Add(user.Email, user);
                UsersByGuid.Add(user.Id, user);

                response.Success = true;
                response.Message = "User registered successfully!";
            }

            return response;

        }

        public IServiceResponse SetPassword(Guid userId, string password)
        {
            ServiceResponse response = new ServiceResponse();

            PasswordByGuid[userId] = password;

            response.Success = true;
            response.Message = "Password set successfully!";

            return response;
        }
    }
}
