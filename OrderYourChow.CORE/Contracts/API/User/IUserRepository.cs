using Microsoft.AspNetCore.Mvc;
using OrderYourChow.CORE.Models.API.User;
using System;
using System.Threading.Tasks;

namespace OrderYourChow.CORE.Contracts.API.User
{
    public interface IUserRepository
    {
        Task<UserDTO> AddAsync([FromBody] UserDTO userDTO);
    }
}
