using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using sharpPDF;
//using sharpPDF.Enumerators;
using System.IO;
using UnityEngine.Networking;
using Paroxe.PdfRenderer;

using static DataTypes;

public class EstimateToPDFConverter : MonoBehaviour
{
    [SerializeField]
    Canvas pdfCanvas;
    [SerializeField]
    PDFViewer m_Viewer;
    [SerializeField]
    GameObject estimatePDFLoadingScreen;

    public const string QueryPrefix = @"?query=";
    public static string devUri = @"https://apidev.ezkiz.ru";
    public static string pdfSuffix = "/api/pdfs";
    public static string PDFURI = devUri + pdfSuffix;

    public IEnumerator LoadPdfRoutine(string json)
    {
        //_cachePath = "D:/EskizDevDataAsync/Resources/estimate.pdf";
        string path = Path.Combine(Application.persistentDataPath, "EskizRoomEstimate.pdf");
       // string path = "D:/EskizDevDataAsync/Resources/estimate.pdf";
        
        var query = QueryPrefix + json;

        var link = PDFURI + query;
        try
        {
#if !UNITY_WEBGL
            using (var www = UnityWebRequest.Get(link))
            {
                AsyncOperation waiting = www.SendWebRequest();
                yield return waiting;
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.LogWarning(www.error);
                    Debug.Log("file not saved!");
                }
                else
                {
                    byte[] bytes = www.downloadHandler.data;
                    if (File.Exists(path)) File.Delete(path);
                    File.WriteAllBytes(path, bytes);
                    Debug.Log("file saved! " + path);
                    StartCoroutine(OpenPdf(path));

                }
            }
#else
    Debug.Log("WebGL link: " + link);
    OpenLinkJSPlugin.OpenLink(link);
#endif
        }
        finally
        {
            //AdaptiveDecoration.Menu.WaitScreenUIController.I.SetWaitScreen(false);
        }
        yield break;
    }


    IEnumerator OpenPdf(string path)
    {
        pdfCanvas.enabled = true;
        pdfCanvas.GetComponent<CanvasScaler>().enabled = true;
		m_Viewer.LoadDocumentFromFile(path);
		while (!m_Viewer.IsLoaded)
			yield return null;
        estimatePDFLoadingScreen.SetActive(false);
		yield break;
    }

    public void DownloadAndOpenPDF()
	{
        Room room = FindObjectOfType<Room>();
        //StartCoroutine(LoadPdfRoutine(RoomCostCalculator.CreateEstimatePdfData(room)));
        StartCoroutine(LoadPdfRoutine(RoomCostCalculator.EstimatePdfJson));
	}
}
