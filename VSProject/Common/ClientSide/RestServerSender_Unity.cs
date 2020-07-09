#region Header

/*	============================================
 *	Author 			    	: strix
 *	Initial Creation Date 	: 2020-07-08
 *	Summary 		        : 
 *  Template 		        : New Behaviour For ReSharper
   ============================================ */

#endregion Header

using System.Collections;
using Common;

using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 
/// </summary>
public class RestServerSender_Unity : IRestServerSender
{
    public class DummyCertificateHandler : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }

    public IEnumerator OnSendRestServer(string strIP_IncludePort, string strURI, byte[] arrSendByte, System.Action<ReturnCodeEnum, string> OnRecvJson)
    {
        UnityWebRequest pRequest = Create_WebRequest(strIP_IncludePort, strURI);
        pRequest.uploadHandler = new UploadHandlerRaw(arrSendByte);

        yield return pRequest.SendWebRequest();
        ReturnCodeEnum pReturnCode = ReturnCodeEnum.None;

        if (pRequest.isNetworkError)
            pReturnCode = ReturnCodeEnum.NetworkError.DoAddContents(pRequest.error);

        OnRecvJson?.Invoke(pReturnCode, pRequest.downloadHandler.text);
    }

    private UnityWebRequest Create_WebRequest(string strIP_IncludePort, string strURI)
    {
        UnityWebRequest request = new UnityWebRequest();

        if (strIP_IncludePort.Contains("https"))
            request.certificateHandler = new DummyCertificateHandler();

        request.url = strIP_IncludePort + "/" + strURI;
        request.method = "POST";
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        return request;
    }
}