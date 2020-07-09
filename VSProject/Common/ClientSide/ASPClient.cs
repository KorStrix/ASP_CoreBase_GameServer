#region Header

/*	============================================
 *	Author 			    	: strix
 *	Initial Creation Date 	: 2020-07-08
 *	Summary 		        : 
 *  Template 		        : New Behaviour For ReSharper
   ============================================ */

#endregion Header

using System.Collections;
using System.Collections.Generic;

namespace Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ASPClient
    {
        /* const & readonly declaration             */

        /* enum & struct declaration                */

        /* public - Field declaration               */

        public string strIP_IncludePort { get; private set; }


        /* protected & private - Field declaration  */

        private readonly List<IRestServerSender> _listServerSender;

        // ========================================================================== //

        /* public - [Do~Something] Function 	        */

        public ASPClient(string strIP_IncludePort, RestServerSenderFactory pSenderFactory)
        {
            this.strIP_IncludePort = strIP_IncludePort;
            _listServerSender = new List<IRestServerSender>(pSenderFactory);
        }

        public IEnumerator DoSendRestServer<T>(IRestPacket2S pSendPacket, System.Action<ReturnCodeEnum, T> OnRecv)
        {
            T tRecvPacket = default(T);

            RestCombinePacket2S pPacket2S = new RestCombinePacket2S(typeof(T).Name, PacketExtractor.DoConvert_ObjectToJson(pSendPacket));
            string strSendJson = PacketExtractor.DoConvert_ObjectToJson(pPacket2S);
            byte[] arrByte = System.Text.Encoding.UTF8.GetBytes(strSendJson);

            foreach (IRestServerSender pSendHow in _listServerSender)
            {
                yield return pSendHow.OnSendRestServer(strIP_IncludePort, pSendPacket.strURI, arrByte,
                    OnRecvJson: (pReturnCode, strRecvJson) =>
                    {
                        if (pReturnCode != ReturnCodeEnum.None)
                        {
                            OnRecv?.Invoke(pReturnCode, tRecvPacket);
                            return;
                        }

                        if (PacketExtractor.DoTryConvert_JsonToPacket(strRecvJson, out tRecvPacket) == false)
                        {
                            OnRecv?.Invoke(pReturnCode | ReturnCodeEnum.JsonParseError, tRecvPacket);
                            return;
                        }


                        OnRecv?.Invoke(pReturnCode, tRecvPacket);
                    });
            }
        }

        // ========================================================================== //

        /* protected - [Override & Unity API]       */

        /* protected - [abstract & virtual]         */


        // ========================================================================== //

        #region Private

        private static bool Check_IsValid_URI(string strText)
        {
            return strText.Contains("404") && strText.Contains("not") && strText.Contains("found");
        }

        #endregion Private
    }
}