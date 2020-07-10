using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServerStudy.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class EchoController : ControllerBase
    {
        public interface IEchoRepo
        {
            string GetString();
        }

        public class EchoRepoTest : IEchoRepo
        {
            public string GetString()
            {
                return nameof(EchoRepoTest);
            }
        }

        private readonly IEchoRepo _sessionRepository;

        public EchoController(IEchoRepo sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public class Packet
        {
            public long UserID;
            public string stringValue;
        }

        [HttpGet]
        public string Get(string a, string b)
        {
            return $"Get a : {a}, b : {b}, {_sessionRepository.GetString()}";
        }

        [HttpPost]
        public Packet Post([FromBody] Packet pReqCombinePacket)
        {
            return pReqCombinePacket;
        }

        // Async도 언젠가 해야되는데 잘 안된다;
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Packet> Post_Async([FromBody] Packet pReqCombinePacket)
        {
            await Task.Delay(1000);

            return pReqCombinePacket;
        }

    }
}
