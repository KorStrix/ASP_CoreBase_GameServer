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

namespace WebServerStudy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EchoController : ControllerBase
    {
        public class User
        {
            [Required]
            public int Id { get; set; }

            [StringLength(100, MinimumLength = 2)]
            public string Name { get; private set; }

            public string Phone { get; set; }
            public string Address { get; set; }

            public override string ToString()
            {
                return $"Echo Id : {Id} // Name : {Name} // Phone : {Phone} // Address : {Address}";
            }
        }

        [HttpGet]
        public string Get(string a, string b, string c)
        {
            return $"Get a : {a}, b : {b}, c : {c}";
        }

        [HttpPost]
        public string Post([FromBody] RestCombinePacket2S pReqCombinePacket)
        {
            if (PacketExtractor.DoTryConvert_JsonToPacket(pReqCombinePacket.strPacketContents, out Rest_CommonPacket2C pReqPacket))
                return PacketExtractor.DoConvert_ObjectToJson(pReqPacket);

            Rest_EchoPacket2C pPacket = new Rest_EchoPacket2C();
            pPacket.strRecvPacket = pReqCombinePacket.strPacketContents;
            return PacketExtractor.DoConvert_ObjectToJson(pPacket);
        }
    }
}
