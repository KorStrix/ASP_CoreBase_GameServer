using System.Collections;
using System.Collections.Generic;

namespace Common
{
    public interface IRestServerSender
    {
        IEnumerator OnSendRestServer(string strURI, byte[] arrSendByte, System.Action<ReturnCodeEnum, string> OnRecvJson);
    }

    public class RestServerSenderFactory : IEnumerable<IRestServerSender>
    {
        public static RestServerSender_Unity Unity => new RestServerSender_Unity();
        public static RestServerSender_ForVSTest Test => new RestServerSender_ForVSTest();

        private readonly List<IRestServerSender> _listSender = new List<IRestServerSender>();

        public RestServerSenderFactory()
        {
        }

        public RestServerSenderFactory(params IRestServerSender[] arrSender)
        {
            _listSender.AddRange(arrSender);
        }

        public IEnumerator<IRestServerSender> GetEnumerator()
        {
            return _listSender.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _listSender.GetEnumerator();
        }
    }
}
