using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderYourChow.CORE.Contracts.API.User;
using OrderYourChow.CORE.Models.API.User;
using System.Threading.Tasks;

namespace OrderYourChow.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Add([FromBody] UserDTO userDTO)
        { 
           var result = await _userRepository.AddAsync(userDTO);

            if (result == null)
                return BadRequest();

            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
