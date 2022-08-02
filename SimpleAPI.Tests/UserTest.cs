using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using SimpleAPI.Controllers;
using SimpleAPI.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimpleAPI.Tests
{
    public class UserTest
    {
        private readonly IUserRepository _userRepository;
        private readonly UserController _userController;

        public UserTest()
        {

            _userRepository = new UserRepository();
            _userController = new UserController(_userRepository);

        }

        [Fact]
        public Task<bool> GetAllAsync()
        {
           
          
        }
    }
}