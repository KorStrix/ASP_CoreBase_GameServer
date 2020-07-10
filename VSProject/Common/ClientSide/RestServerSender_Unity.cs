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

    public IEnumerator OnSendRestServer(string strURI, byte[] arrSendByte, System.Action<ReturnCodeEnum, string> OnRecvJson)
    {
        UnityWebRequest pRequest = Create_WebRequest(strURI);
        pRequest.uploadHandler = new UploadHandlerRaw(arrSendByte);

        yield return pRequest.SendWebRequest();
        ReturnCodeEnum pReturnCode = ReturnCodeEnum.None;

        if (pRequest.isNetworkError)
            pReturnCode = ReturnCodeEnum.NetworkError.DoAddContents(pRequest.error);

        OnRecvJson?.Invoke(pReturnCode, pRequest.downloadHandler.text);
    }

    private UnityWebRequest Create_WebRequest(string strURI)
    {
        UnityWebRequest request = new UnityWebRequest();

        if (strURI.Contains("https"))
            request.certificateHandler = new DummyCertificateHandler();

        request.url = strURI + "/" + strURI;
        request.method = "POST";
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        return request;
    }
}