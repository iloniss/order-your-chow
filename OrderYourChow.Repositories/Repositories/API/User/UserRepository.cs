
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderYourChow.CORE.Contracts.API.User;
using OrderYourChow.CORE.Models.API.User;
using OrderYourChow.DAL.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderYourChow.Repositories.Repositories.API.User
{
    public class UserRepository : IUserRepository
    {
        private readonly OrderYourChowContext _orderYourChowContext;
        private readonly IMapper _mapper;

        public UserRepository(OrderYourChowContext orderYourChowContext, IMapper mapper)
        {
            _orderYourChowContext = orderYourChowContext;
            _mapper = mapper;
        }
        public async Task<UserDTO> AddAsync([FromBody] UserDTO userDTO)
        {       
            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                var result = _mapper.Map<DUser>(userDTO);
                await _orderYourChowContext.AddAsync(result);
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();

                return _mapper.Map<UserDTO>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
