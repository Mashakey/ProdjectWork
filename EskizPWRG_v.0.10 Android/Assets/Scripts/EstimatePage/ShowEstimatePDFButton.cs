using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEstimatePDFButton : MonoBehaviour
{
	[SerializeField]
	GameObject estimatePDFLoadingScreen;

    public void OnShowEstimateButtonClick()
	{
		estimatePDFLoadingScreen.SetActive(true);
		EstimateToPDFConverter pdfConverter = FindObjectOfType<EstimateToPDFConverter>();
		pdfConverter.DownloadAndOpenPDF();	
	}
}
