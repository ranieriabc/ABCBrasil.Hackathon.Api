using ABCBrasil.Hackathon.Api.Controllers;
using ABCBrasil.Hackathon.Api.Domain.Entities;
using ABCBrasil.Hackathon.Api.Domain.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ABCBrasil.Hackathon.Api.UnitTests.Controllers
{
    public class UsersControllerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _controller = new UsersController(_userRepositoryMock.Object);
        }

        [Fact(DisplayName = "Criação do Usuário com Sucesso")]
        public async Task Post_ShouldReturn201Created_WhenRequestIsValid()
        {
            var request = SourcesBogus.GenerateCreateUserRequest();
            var user = SourcesBogus.GenerateCreateUser(request.Name, request.Email, request.Password);

            _userRepositoryMock.Setup(a => a.Insert(user));

            var result = (await _controller.Post(request)) as CreatedResult;

            result.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status201Created);
            var response = result?.Value as User;
            response.Should().NotBeNull();
            response?.Id.Should().Be(0);
            response?.CreatedIn.Should().NotBe(DateTime.MinValue);
            response?.Name.Should().Be(request.Name);
            response?.Email.Should().Be(request.Email);
            response?.Password.Should().Be(request.Password);
        }

        [Fact(DisplayName = "Payload Inválido")]
        public async Task Post_ShouldReturn400BadRequest_WhenModelStateIsInvalid()
        {
            var request = SourcesBogus.GenerateCreateUserRequest();

            request.Name = null;

            _controller.ModelState.AddModelError("Name", "The Name field is required.");

            var result = await _controller.Post(request);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<string>(badRequestResult.Value);
            badRequestResult.Value.Should().Be("Payload inválido.");
        }

        [Fact(DisplayName = "Consulta o Usuário com Sucesso")]
        public async Task Get_ShouldReturn200OK_WhenIdIsValid()
        {
            var user = SourcesBogus.GenerateCreateUser();

            _userRepositoryMock
                .Setup(a => a.GetById(user.Id))
                .ReturnsAsync(user);

            var result = await _controller.Get(user.Id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var userResult = Assert.IsType<User>(okResult.Value);

            userResult.Should().NotBeNull();
            userResult.Id.Should().Be(user.Id);
            userResult.CreatedIn.Should().Be(user.CreatedIn);
            userResult.Name.Should().Be(user.Name);
            userResult.Email.Should().Be(user.Email);
            userResult.Password.Should().Be(user.Password);
        }

        [Fact(DisplayName = "Consulta o Usuário não Existente")]
        public async Task Get_ShouldReturnNotFound_WhenIdNotExists()
        {
            User user = null;
            var id = 1;

            _userRepositoryMock
                .Setup(a => a.GetById(id))
                .ReturnsAsync(user);

            var result = await _controller.Get(id);

            var okResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
