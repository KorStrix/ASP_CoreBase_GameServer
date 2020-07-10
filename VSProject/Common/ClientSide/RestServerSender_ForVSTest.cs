using System;
using System.Collections;

namespace Common
{
    public class RestServerSender_ForVSTest : IRestServerSender
    {
        public IEnumerator OnSendRestServer(string strURI, byte[] arrSendByte, Action<ReturnCodeEnum, string> OnRecvJson)
        {


            yield break;
        }
    }
}