using ABCBrasil.Hackathon.Api.Domain.DataContracts.Requests;
using ABCBrasil.Hackathon.Api.Domain.Entities;
using ABCBrasil.Hackathon.Api.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ABCBrasil.Hackathon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Cria um usuário
        /// </summary>
        /// <param name="id">O identificador do usuário</param>
        /// <param name="request">O payload com os dados do usuário</param>
        /// <returns>Uma resposta padrão com os dados do usuário criado</returns>
        /// <response code="201">Uma resposta padrão com os dados do usuário criado</response>
        /// <response code="400">Payload inválido.</response>
        /// <response code="422">Erro de negócio.</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <response code="503">Um ou mais serviços internos indisponíveis</response>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(User))]
        public async Task<IActionResult> Post([FromBody] UserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Payload inválido.");

            var user = new User
            {
                Email = request.Email,
                Name = request.Name,
                Password = request.Password,
            };

            await _userRepository.Insert(user);

            return Created(string.Empty, user);
        }

        /// <summary>
        /// Obter um usuário pelo id
        /// </summary>
        /// <param name="id">Identificador do Usuário</param>
        /// <returns>Retorna o usuário criado</returns>
        /// <response code="200">Retorna o Usuário</response>
        /// <response code="404">Usuário não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <response code="503">Um ou mais serviços internos indisponíveis</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var user = await _userRepository.GetById(id);

            if (user is null)
                return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Atualiza um usuário
        /// </summary>
        /// <param name="id">O identificador do usuário</param>
        /// <param name="request">O payload com os dados do usuário</param>
        /// <returns>Uma resposta padrão com os dados do usuário atualizado</returns>
        /// <response code="200">Uma resposta padrão com os dados do usuário atualizado</response>
        /// <response code="400">Payload inválido.</response>
        /// <response code="404">Usuário não encontrado</response>
        /// <response code="422">Erro de negócio.</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <response code="503">Um ou mais serviços internos indisponíveis</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UserRequest request)
        {
            var user = await _userRepository.GetById(id);

            if (user is null)
                return NotFound();

            user.Name = request.Name;
            user.Email = request.Email;
            user.Password = request.Password;

            await _userRepository.Update(user);

            return Ok(user);
        }

        /// <summary>
        /// Exclui um usuário
        /// </summary>
        /// <param name="id">O identificador do usuário</param>
        /// <returns>Uma resposta padrão com os dados do usuário deletado</returns>
        /// <response code="200">Uma resposta padrão com os dados do usuário deletado</response>
        /// <response code="404">Usuário não encontrado</response>
        /// <response code="422">Erro de negócio.</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <response code="503">Um ou mais serviços internos indisponíveis</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var user = await _userRepository.GetById(id);

            if (user is null)
                return NotFound();

            await _userRepository.Delete(user.Id);

            return Ok(user);
        }
    }
}
