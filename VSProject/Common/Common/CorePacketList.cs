using System;
using System.Runtime.InteropServices;

namespace Common
{
    public interface IRestPacket2S
    {
        string strURI { get; }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class RestCombinePacket2S
    {
        public string strPacketName { get; set; } = "";
        public string strPacketContents { get; set; } = "";

        public RestCombinePacket2S()
        {
        }

        public RestCombinePacket2S(string strPacketName, string strPacketContents)
        {
            this.strPacketName = strPacketName;
            this.strPacketContents = strPacketContents;
        }
    }
}
