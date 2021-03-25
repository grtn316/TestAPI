﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestAPI.API.DataContracts;
using TestAPI.API.DataContracts.Requests;
using TestAPI.Services.Contracts;
using StackExchange.Redis;
using S = TestAPI.Services.Model;
using Repositories.Redis;
using Microsoft.Extensions.Options;
//using Microsoft.Extensions.Options;

namespace TestAPI.API.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private readonly IRedisConnectionFactory _fact;

#pragma warning disable CS1591
        public UserController(IUserService service, IMapper mapper, IRedisConnectionFactory factory)
        {
            _service = service;
            _mapper = mapper;
            _fact = factory;
            
            
        }
#pragma warning restore CS1591

        #region GET
        /// <summary>
        /// Comments and descriptions can be added to every endpoint using XML comments.
        /// </summary>
        /// <remarks>
        /// XML comments included in controllers will be extracted and injected in Swagger/OpenAPI file.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<User> Get(string id)
        {
            var data = await _service.GetAsync(id);

            var db = _fact.Connection().GetDatabase();
            db.StringSet("StackExchange.Redis.Key", "Stack Exchange Redis is Awesome");
            
            if (data != null)
                return _mapper.Map<User>(data);
            else
                return null;
        }
        #endregion

        #region POST
        [HttpPost]
        public async Task<User> CreateUser([FromBody] UserCreationRequest value)
        {

            //TODO: include exception management policy according to technical specifications
            if (value == null)
                throw new ArgumentNullException("value");

            if (value == null)
                throw new ArgumentNullException("value.User");


            var data = await _service.CreateAsync(_mapper.Map<S.User>(value.User));

            if (data != null)
                return _mapper.Map<User>(data);
            else
                return null;

        }
        #endregion

        #region PUT
        [HttpPut()]
        public async Task<bool> UpdateUser(User parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            return await _service.UpdateAsync(_mapper.Map<S.User>(parameter));
        }
        #endregion

        #region DELETE
        [HttpDelete("{id}")]
        public async Task<bool> DeleteDevice(string id)
        {
            return await _service.DeleteAsync(id);
        }
        #endregion
    }
}