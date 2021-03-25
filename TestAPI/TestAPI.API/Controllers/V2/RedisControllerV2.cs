using AutoMapper;
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


namespace TestAPI.API.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/redis")]
    [ApiController]
    public class RedisController : Controller
    {
        //private readonly IUserService _service;
        //private readonly IMapper _mapper;
        private readonly IRedisConnectionFactory _fact;

#pragma warning disable CS1591
        public RedisController(IRedisConnectionFactory factory)
        {
            //_service = service;
            //_mapper = mapper;
            _fact = factory;

        }

        #region GET
        /// <summary>
        /// Comments and descriptions can be added to every endpoint using XML comments.
        /// </summary>
        /// <remarks>
        /// XML comments included in controllers will be extracted and injected in Swagger/OpenAPI file.
        /// </remarks>
        /// <param name="keyName"></param>
        /// <returns></returns>
        [HttpGet("{keyName}")]
        public async Task<ActionResult<RedisData>> GetKey(string keyName)
        {

            //TODO: include exception management policy according to technical specifications
            if (keyName == null)
                throw new ArgumentNullException("value");

            var db = _fact.Connection().GetDatabase();
            var data = await db.StringGetAsync(keyName);
            
            RedisData rObject = new RedisData();
            rObject.KeyName = keyName;
            rObject.KeyValue = data;

            if (data != RedisValue.Null)
                return Ok(rObject);
            else
                return NotFound();
        }
        #endregion

        #region POST
        [HttpPost]
        public async Task<ActionResult<RedisData>> CreateKey([FromBody] KeyCreationRequest value)
        {

            //TODO: include exception management policy according to technical specifications
            if (value == null)
                throw new ArgumentNullException("value");

            var db = _fact.Connection().GetDatabase();
            await db.StringSetAsync(value.KeyName, value.KeyValue);
            
            //var data = await _service.CreateAsync(_mapper.Map<S.User>(value.User));
            var data = await db.StringGetAsync(value.KeyName);

            RedisData rObject = new RedisData();
            rObject.KeyName = value.KeyName;
            rObject.KeyValue = data;

            if (data != RedisValue.Null)
                return Ok(rObject);
            else
                return NotFound();

        }
        #endregion
    }
}
