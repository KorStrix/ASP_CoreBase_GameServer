using Newtonsoft.Json;
using System;

namespace Common
{
    public static class PacketExtractor
    {
        public static bool DoTryConvert_JsonToPacket<T>(string strJson, out T tResult, System.Action<Exception> OnError = null)
        {
            bool bIsSuccess = true;
            tResult = default(T);

            try
            {
                tResult = JsonConvert.DeserializeObject<T>(strJson);
            }
            catch (Exception e)
            {
                bIsSuccess = false;
                OnError?.Invoke(e);
            }

            return bIsSuccess;
        }

        public static string DoConvert_ObjectToJson(object pObject, System.Action<Exception> OnError = null)
        {
            string strJson = string.Empty;

            try
            {
                strJson = JsonConvert.SerializeObject(pObject);
            }
            catch (Exception e)
            {
                OnError?.Invoke(e);
            }

            return strJson;
        }
    }
}
