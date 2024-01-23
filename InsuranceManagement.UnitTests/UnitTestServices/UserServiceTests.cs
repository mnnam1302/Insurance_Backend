using AutoMapper;
using backend.DTO.User;
using backend.IRepositories;
using backend.Models;
using backend.Profiles;
using backend.Responses;
using backend.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.SignalR.Protocol;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace InsuranceManagement.UnitTests.UnitTestServices
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly IMapper _mapper;

        public UserServiceTests()
        {
            _userRepository = new Mock<IUserRepository>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async void GetUserById_Returns_User()
        {
            // ARRANGE
            var userList = GetListUser();

            // setup a mock state repo to return some fake data in our target method
            _userRepository.Setup(x => x.Get(It.IsAny<int>()))
                        .ReturnsAsync(userList[1]);

            // create userService by injecting our mock repository
            var userService = new UserService(_userRepository.Object, _mapper);

            // ACT - call our method under test
            var result = await userService.GetUserById(2);

            // ASSERT - we got the result we expected
            result.Should().NotBeNull();
            result.Should().BeOfType<UserDTO>();
            result.UserId.Should().Be(2);
        }

        [Fact]
        public async void GetUserByEmail_Returns_User()
        {
            // ARRANGE
            var userList = GetListUser();

            // setup a mock state repo to return some fake data in our target method
            _userRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>()))
                        .ReturnsAsync(userList[1]);

            // create userService by injecting our mock repository
            var userService = new UserService(_userRepository.Object, _mapper);

            // ACT - call our method under test
            var result = await userService.GetUserByEmail("test2@gmail.com");

            // ASSERT - we got the result we expected
            result.Should().NotBeNull();
            result.Should().BeOfType<UserDTO>();

            result.UserId.Should().Be(2);
            result.Email.Should().Be("test2@gmail.com");
    
        }

        [Fact]
        public async void GetAllPaging_BasePagingResponse()
        {
            // ARRANGE
            var userList = GetListUser();
            int page = 1;
            int pageSize = 5;

            _userRepository.Setup(x => x.GetMultiPaging(
               It.IsAny<Expression<Func<User, bool>>>(),
                   out It.Ref<int>.IsAny,
                   out It.Ref<int>.IsAny,
                   out It.Ref<int>.IsAny,
                   page,
                   pageSize,
                   null
            )).Returns(userList);

            var userService = new UserService(_userRepository.Object, _mapper);

            // ACT
            var result = userService.GetAllPaging(page, pageSize);

            // ASSERT
            result.Should().NotBeNull();
            result.Should().BeOfType<BasePagingResponse<UserDTO>>();
            result.Data.Count.Should().Be(2);
            result.Data[0].UserId.Should().Be(1);
            result.Data[1].UserId.Should().Be(2);
        }

        [Fact]
        public async void CheckUserExits_Return_True()
        {
            // ARRANGE
            var userId = 1;
            var userList = GetListUser();

            _userRepository.Setup(x => x.Exists(It.IsAny<int>()))
                   .ReturnsAsync((int userId) =>
                   {
                       var user = userList.FirstOrDefault(u => u.UserId == userId);
                       return user != null;
                   });

            // ACT
            var userService = new UserService(_userRepository.Object, _mapper);

            var result = await userService.CheckUserExists(userId);

            // ASSERT
            result.Should().BeTrue();
        }

        [Fact]
        public async void CheckUserExits_Return_False()
        {
            // ARRANGE
            var userId = 3;
            var userList = GetListUser();

            _userRepository.Setup(x => x.Exists(It.IsAny<int>()))
                   .ReturnsAsync((int userId) =>
                   {
                       var user = userList.FirstOrDefault(u => u.UserId == userId);
                       return user != null;
                   });
            // ACT
            var userService = new UserService(_userRepository.Object, _mapper);

            var result = await userService.CheckUserExists(userId);

            // ASSERT
            result.Should().BeFalse();
        }

        [Fact]
        public async void Register_Success()
        {
            // ARRANGE
            var userList = GetListUser();

            var userDto = new CreateUserDTO
            {
                Email = "test3@gmail.com",
                Password = "123456",
                FullName = "Test 3",
                Phone = "0969958902",
                Sex = "Nam",
                DateOfBirth = DateTime.Now.AddYears(-20),
                CardIdentification = "123456789",
            };

            var user = _mapper.Map<User>(userDto);
            _userRepository.Setup(x => x.CreateUser(It.IsAny<User>()))
                .ReturnsAsync((User user) =>
                {
                    userList.Add(user);
                    return user;
                });

            var userService = new UserService(_userRepository.Object, _mapper);

            // ACT
            var result = await userService.Register(userDto);

            // ASSERT
            result.Should().NotBeNull();
            result.Should().BeOfType<BaseCommandResponse>();
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async void Register_ExistsingEmail_Failed()
        {
            // ARRANGE
            var userList = GetListUser();

            var userDto = new CreateUserDTO
            {
                Email = "test2@gmail.com",
                Password = "123456",
                FullName = "Test 2",
                Phone = "0969958902",
                Sex = "Nam",
                DateOfBirth = DateTime.Now.AddYears(-20),
                CardIdentification = "123456789",
            };

            var user = _mapper.Map<User>(userDto);
            _userRepository.Setup(x => x.CreateUser(It.IsAny<User>()))
                .ReturnsAsync((User user) =>
                {
                    userList.Add(user);
                    return user;
                });

            _userRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>()))
                .ReturnsAsync(userList.FirstOrDefault(u => u.Email == userDto.Email));

            var userService = new UserService(_userRepository.Object, _mapper);

            // ACT
            var result = await userService.Register(userDto);

            // ASSERT
            result.Should().NotBeNull();
            result.Should().BeOfType<BaseCommandResponse>();
            userList.Count.Should().Be(2);
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("Email already exists.");
        }

        [Fact]
        public async void Register_LessAge18_False()
        {
            // ARRANGE
            var userList = GetListUser();

            var userDto = new CreateUserDTO
            {
                Email = "test3@gmail.com",
                Password = "123456",
                FullName = "Test 3",
                Phone = "0969958902",
                Sex = "Nam",
                DateOfBirth = DateTime.Now.AddYears(-5),
                CardIdentification = "123456789",
            };

            var user = _mapper.Map<User>(userDto);
            _userRepository.Setup(x => x.CreateUser(It.IsAny<User>()))
                .ReturnsAsync((User user) =>
                {
                    userList.Add(user);
                    return user;
                });

            // ACT
            var userService = new UserService(_userRepository.Object, _mapper);
            var result = await userService.Register(userDto);

            // ASSERT
            result.Should().NotBeNull();
            result.Should().BeOfType<BaseCommandResponse>();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("User must be equal or greater than 18 years old.");
        }

        private List<User> GetListUser()
        {
            List<User> users = new List<User>
            {
                new User
                {
                     UserId = 1,
                     Email = "test1@gmail.com",
                     Password = "123456",
                     FullName = "Test 1",
                     Phone = "0969957900",
                     Sex = "Nam",
                     DateOfBirth = DateTime.Now.AddYears(-25),
                     CardIdentification = "123456789",
                     CreatedDate = DateTime.Now,
                     IsAdmin = false,
                     Status = true
                },
                new User
                {
                     UserId = 2,
                     Email = "test2@gmail.com",
                     Password = "123456",
                     FullName = "Test 2",
                     Phone = "0969957901",
                     Sex = "Nữ",
                     DateOfBirth = DateTime.Now.AddYears(-24),
                     CardIdentification = "123456789",
                     CreatedDate = DateTime.Now,
                     IsAdmin = true,
                     Status = true
                }
            };

            return users;
        }
    }
}