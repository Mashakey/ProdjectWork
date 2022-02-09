using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserPhotosPageFiller : MonoBehaviour
{
	[SerializeField]
	GameObject UserPhotoFieldPrefab;
	[SerializeField]
	GameObject UserPhotosPage;
	[SerializeField]
	GameObject GalleryPage;
	[SerializeField]
	Text OpenUserPhotosPageButtonText;
	[SerializeField]
	Button ProceedButton;
	[SerializeField]
	Sprite ActiveProceedButtonSprite;
	[SerializeField]
	Sprite DeactivatedProceedButtonSprite;
	[SerializeField]
	GameObject ActiveSelectionFrame;

	public UserPhotoField activeUserPhotoField = null;

	List<UserPhotoField> photoFields = new List<UserPhotoField>();

	bool isEnabled = false;

	public void SwitchGalleryPageState()
	{
		if (!isEnabled)
		{
			isEnabled = true;
			EnablePhotosPage();
		}
		else
		{
			isEnabled = false;
			DisablePhotosPage();
		}
	}

	public void EnablePhotosPage()
	{
		Color orange;
		ColorUtility.TryParseHtmlString("#FF9900", out orange); //orange
		OpenUserPhotosPageButtonText.color = orange;

		GalleryPage.SetActive(true);
		FillUserPhotosPage();
		DeactivateProceedButton();
		isEnabled = true;
	}

	public void DisablePhotosPage()
	{
		Color grey;
		ColorUtility.TryParseHtmlString("#555555", out grey); //orange
		OpenUserPhotosPageButtonText.color = grey;
		ActiveSelectionFrame.transform.SetParent(transform);
		ActiveSelectionFrame.transform.position = new Vector3(-1000f, -1000f, -1000f);
		GalleryPage.SetActive(false);
		isEnabled = false;
	}

	public void ResetSelection()
	{
		ActiveSelectionFrame.transform.SetParent(transform);
		ActiveSelectionFrame.transform.position = new Vector3(-1000f, -1000f, -1000f);
	}

	public void FillUserPhotosPage()
	{
		ClearFieldsPage();
		List<string> userPhotosNames = DataCacher.GetCachedUserPhotosNames();
		foreach (var userPhoto in userPhotosNames)
		{
			GameObject userPhotoField = Instantiate(UserPhotoFieldPrefab, UserPhotosPage.transform);
			userPhotoField.name = userPhoto;

			var photoFieldScript = userPhotoField.GetComponent<UserPhotoField>();
			photoFields.Add(photoFieldScript);

			photoFieldScript.SetPreviewAndTexture();
		}
	}

	public void ClearFieldsPage()
	{
		foreach (var photoField in photoFields)
		{
			Destroy(photoField.gameObject);
		}
		photoFields.Clear();
	}

	public void SelectPhotoField(UserPhotoField selectedField)
	{
		activeUserPhotoField = null;
		activeUserPhotoField = selectedField;
		ActivateProceedButton();
	}

	public void ActivateProceedButton()
	{
		ProceedButton.interactable = true;
		ProceedButton.GetComponent<Image>().sprite = ActiveProceedButtonSprite;
	}

	public void DeactivateProceedButton()
	{
		ProceedButton.interactable = false;
		ProceedButton.GetComponent<Image>().sprite = DeactivatedProceedButtonSprite;
	}

	public void OnProceedButtonClick()
	{
		MaterialBuilder materialBuilder = Transform.FindObjectOfType<MaterialBuilder>();
		List<Transform> selectedObjects = GlobalApplicationManager.GetAndPopSelectedObjects();
		if (materialBuilder != null)
		{
			Debug.LogWarningFormat($"Trying to apply user material '{activeUserPhotoField.photoTexture.name}' to surface");
			foreach (var selectedObject in selectedObjects)
			{
				Debug.LogError("We are in PreviewPage. Selected object is '" + selectedObject.name + "'");
				materialBuilder.StartCoroutine(materialBuilder.UpdateMeshMaterialWithUserImage(activeUserPhotoField.photoTexture.name, selectedObject.GetComponent<TextureUpdater>().UpdateTexture));
			}
		}
	}
}
