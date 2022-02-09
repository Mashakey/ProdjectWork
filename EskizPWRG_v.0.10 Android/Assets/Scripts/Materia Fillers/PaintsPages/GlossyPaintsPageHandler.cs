using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class GlossyPaintsPageHandler : MonoBehaviour
{
	[SerializeField]
	Button applyButton;
	[SerializeField]
	GameObject activeFieldFramePrefab;

	Transform activeField = null;
	GameObject activeFieldFrame = null;

	public Sprite activeApplyButton;
	[SerializeField]
	SortingPainting sortingPainting;

	public void DeactivatePaintsPageCanvas()
	{
		GetComponent<Canvas>().enabled = false;
		GetComponent<CanvasScaler>().enabled = false;
		sortingPainting.ResetSortingButton();
	}

	public void SetPaintFieldActive(Transform paintField)
	{
		if (activeField != null)
		{
			Destroy(activeFieldFrame);
		}
		activeField = paintField;
		GameObject frame = Instantiate(activeFieldFramePrefab, activeField);
		GameObject imagePaint = paintField.Find("Object").gameObject;
		frame.GetComponent<RectTransform>().sizeDelta = new Vector2(imagePaint.GetComponent<RectTransform>().rect.width, imagePaint.GetComponent<RectTransform>().rect.height);
		frame.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 12f);
		activeFieldFrame = frame;
		applyButton.interactable = true;
		applyButton.GetComponent<Image>().sprite = activeApplyButton;
	}

	public void OnApplyButtonClick()
	{
		MaterialBuilder materialBuilder = Transform.FindObjectOfType<MaterialBuilder>();
		List<Transform> selectedObjects = GlobalApplicationManager.GetAndPopSelectedObjects();
		if (materialBuilder != null)
		{
			Debug.LogErrorFormat($"Trying to apply material '{activeField.name}' to surface");
			MaterialJSON currentMaterialJson = GlobalApplicationManager.GetMaterialJsonById(activeField.name);
			foreach (var selectedObject in selectedObjects)
			{
				Debug.LogError("We are in PreviewPage. Selected object is '" + selectedObject.name + "'");
				materialBuilder.StartCoroutine(materialBuilder.UpdateMeshMaterial(currentMaterialJson, selectedObject.GetComponent<TextureUpdater>().UpdateTexture));
			}
		}
		DeactivatePaintsPageCanvas();
	}
}

