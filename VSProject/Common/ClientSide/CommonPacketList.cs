using System;
using System.Runtime.InteropServices;

namespace Common
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Rest_CommonPacket2S : IRestPacket2S
    {
        public string strURI => "Echo"; // 나중에 바꿔야함

        public string stringValue;
        public int iValue;
        public float fValue;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Rest_CommonPacket2C
    {
        public string stringValue;
        public int iValue;
        public float fValue;

        public string str2SPacket;
    }
}
