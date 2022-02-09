using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using static DataTypes;

public class DrumScroll : MonoBehaviour
{
	[SerializeField]
	Sprite BlankPreviewImage;
	[SerializeField]
	GameObject contentFieldPrefab;
	[SerializeField]
	Scrollbar scrollbar;

	[SerializeField]
	private Transform contentPage;

	private int ContentFieldWidth = 340;
	private int ContentFieldHeight = 89;
	private const int Spacing = 0;
	private const int TopIndent = 0;
	private const int BottomIndent = 0;
	private const int MaxContentFieldsCount = 12;

	List<MaterialJSON> materialJsonsToDisplay;
	public List<MaterialField> contentFields;

	RectTransform contentPageRectTransform;

	public bool filtration = false;
	public List<MaterialJSON> filtrationList = new List<MaterialJSON>();
	public List<GameObject> InstantiatePrefabs = new List<GameObject>();

	public Button filters;
	public GameObject popUpNullMaterial;
	public int selectFilt;
	public TwinSlider twinSlider;

	private void Awake()
	{
		contentFields = new List<MaterialField>();
		contentPageRectTransform = contentPage.GetComponent<RectTransform>();
	}

	public void ResetScrollbar()
	{
		scrollbar.value = 1f;
		contentPageRectTransform.anchoredPosition = new Vector2(contentPageRectTransform.sizeDelta.x, 0f);
		ClearContentFields();
	}

	float contentPageVerticalPosition;
	int previousFrameSkippedContentFieldsCount = 0;

	void CalculateContentFieldsRelocation()
	{
		contentPageVerticalPosition = contentPageRectTransform.anchoredPosition.y - Spacing;
		if (contentPageVerticalPosition < 0)
		{
			return;
		}
		int skippedContentFieldsCount = Mathf.FloorToInt(contentPageVerticalPosition / (ContentFieldHeight + Spacing));
		//Debug.LogErrorFormat($"Skipped contentFieldsCount = {skippedContentFieldsCount}\n Previos skipped count = {previousFrameSkippedContentFieldsCount}");
		if (previousFrameSkippedContentFieldsCount == skippedContentFieldsCount)
		{
			return;
		}
		else if (skippedContentFieldsCount > previousFrameSkippedContentFieldsCount)
		{
			//Debug.LogWarningFormat("Contentpage vert pos = {0}", contentPageVerticalPosition);
			//Debug.LogWarningFormat($"Skipped {skippedContentFieldsCount}\nPreviousCount {previousFrameSkippedContentFieldsCount} Adding to end");
			//for (int i = 0; i < contentFields.Count; i++)
			//{
			//	Debug.LogWarningFormat($"Field[{i}] pos = {contentFields[i].GetComponent<RectTransform>().anchoredPosition}");
			//}
			AddContentFieldsToEnd(skippedContentFieldsCount);
		}
		else
		{
			//Debug.LogWarningFormat($"Skipped {skippedContentFieldsCount}\nPreviousCount {previousFrameSkippedContentFieldsCount} Adding to start");

			AddContentFieldsToStart(skippedContentFieldsCount);
		}
		previousFrameSkippedContentFieldsCount = skippedContentFieldsCount;
	}

	void AddContentFieldsToStart(int skippedContentFieldsCount)
	{
		int deltaCount = skippedContentFieldsCount - previousFrameSkippedContentFieldsCount;
		//Debug.LogWarning("UP");

		//Debug.LogErrorFormat($"Skipped = {skippedContentFieldsCount}  precious = {previousFrameSkippedContentFieldsCount}");
		for (int i = skippedContentFieldsCount; i < previousFrameSkippedContentFieldsCount && i < materialJsonsToDisplay.Count; i++)
		{
			int ElementFieldIndex = i % contentFields.Count;
			if (ElementFieldIndex < contentFields.Count)
			{
				Debug.LogErrorFormat($"Element index = {i}  elementFieldIndex = {ElementFieldIndex}");
				RectTransform contentFieldRectTransform;
				contentFieldRectTransform = contentFields[ElementFieldIndex].GetComponent<RectTransform>();
				float newFieldVerticalPosition = -(TopIndent + i * Spacing + i * ContentFieldHeight);
				//newFieldVerticalPosition -= ContentFieldHeight - 44.5f;

				contentFieldRectTransform.anchoredPosition = new Vector2(contentFieldRectTransform.anchoredPosition.x, newFieldVerticalPosition);
				contentFields[ElementFieldIndex].transform.Find("ImMat").Find("DownImageMaterial").GetComponent<Image>().sprite = BlankPreviewImage;

				//contentFields[ElementFieldIndex].GetComponent<MaterialField>().FillFieldWithMaterialData(materialJsonsToDisplay[i]);
				UpdateContentFields();

			}
		}
		Resources.UnloadUnusedAssets();

	}

	void AddContentFieldsToEnd(int skippedContentFieldsCount)
	{
		int deltaCount = skippedContentFieldsCount - previousFrameSkippedContentFieldsCount;
		int skippedCount = skippedContentFieldsCount;
		Debug.LogWarning("Skipped count = " + deltaCount + " " + skippedContentFieldsCount);
		for (int i = previousFrameSkippedContentFieldsCount + 1; i <= skippedCount; i++)
		{

			int ElementFieldIndex = i % contentFields.Count;
			ElementFieldIndex--;
			if (ElementFieldIndex < 0)
			{
				ElementFieldIndex = contentFields.Count - 1;
			}
			int newDisplayedElementIndex = i + contentFields.Count;
			newDisplayedElementIndex--;
			Debug.LogWarningFormat($"new element index = {newDisplayedElementIndex} listIndex = {ElementFieldIndex}");
			if (newDisplayedElementIndex < materialJsonsToDisplay.Count)
			{
				RectTransform contentFieldRectTransform;
				contentFieldRectTransform = contentFields[ElementFieldIndex].GetComponent<RectTransform>();
				float newFieldVerticalPosition = -(TopIndent + newDisplayedElementIndex * Spacing + newDisplayedElementIndex * ContentFieldHeight);
				//newFieldVerticalPosition += ContentFieldHeight + 44.5f;
				//ewFieldVerticalPosition += ContentFieldHeight + 89f;
				Debug.LogErrorFormat("New field position = " + newFieldVerticalPosition);
				contentFieldRectTransform.anchoredPosition = new Vector2(contentFieldRectTransform.anchoredPosition.x, newFieldVerticalPosition);
				Debug.LogWarningFormat($"{contentFieldRectTransform.gameObject.name} position = {contentFieldRectTransform.anchoredPosition}");
				Debug.LogWarningFormat($"parent = {contentFieldRectTransform.gameObject.transform.parent.name}");
				contentFields[ElementFieldIndex].transform.Find("ImMat").Find("DownImageMaterial").GetComponent<Image>().sprite = BlankPreviewImage;
				//contentFields[ElementFieldIndex].GetComponent<MaterialField>().FillFieldWithMaterialData(materialJsonsToDisplay[newDisplayedElementIndex]);
				Debug.LogWarningFormat($"{contentFieldRectTransform.gameObject.name} position = {contentFieldRectTransform.localPosition}");
				UpdateContentFields();
			}
		}
		Resources.UnloadUnusedAssets();
	}


	public void UpdateContentFields()
	{
		foreach (var contentField in contentFields)
		{
			contentField.GetComponent<MaterialField>().FillFieldWithMaterialData(GlobalApplicationManager.GetMaterialJsonById(contentField.name));
		}
	}



	void Update()
	{
		if (materialJsonsToDisplay != null && materialJsonsToDisplay.Count > 0)
		{
			CalculateContentFieldsRelocation();
		}
	}

	public void SetRectTransormSettings(List<MaterialJSON> materialJsons)
	{
		Canvas canvas = gameObject.transform.GetComponentInParent<Canvas>();
		CanvasScaler canvasScaler = gameObject.transform.GetComponentInParent<CanvasScaler>();
		canvas.enabled = true;
		canvasScaler.enabled = true;
		if (contentFields != null)
		{
			ClearContentFields();
		}
		materialJsonsToDisplay = materialJsons;
		SetContentPageSize(materialJsonsToDisplay.Count);
		SetContentPagePosition();
		CreateContentFields(materialJsons);
	}

	public void nullSearchResult()
    {
        if (contentFields.Count == 0)
        {
			popUpNullMaterial.SetActive(true);
		}
		else
			popUpNullMaterial.SetActive(false);
	}

	public void ClearContentFields()
	{
		foreach (var contentField in contentFields)
		{
			Destroy(contentField.gameObject);
		}
		contentFields.Clear();
	}

	void SetContentPageSize(int contentCount)
	{
		float contentPageRectTransformHeight = ContentFieldHeight * contentCount + TopIndent + BottomIndent + (contentCount * Spacing);
		contentPageRectTransform.sizeDelta = new Vector2(contentPageRectTransform.sizeDelta.x, contentPageRectTransformHeight);
	}

	void SetContentPagePosition()
	{
		float yOffset = 0f;
		contentPageRectTransform.anchoredPosition = new Vector2(contentPageRectTransform.anchoredPosition.x, yOffset);
	}

	public void CreateContentFields(List<MaterialJSON> materialJsons)
	{
		Debug.Log("Creating content fields");
		for (int i = 0; i < MaxContentFieldsCount && i < materialJsons.Count; i++)
		{
			Vector2 contentFieldPosition = new Vector2(0f, -i * (ContentFieldHeight + Spacing));
			GameObject contentField = Instantiate(contentFieldPrefab, contentPage.transform);
			contentField.GetComponent<RectTransform>().anchoredPosition = contentFieldPosition;
			Debug.LogWarningFormat($"Field[{i}] pos = {contentFieldPosition} anchored = {contentField.GetComponent<RectTransform>().anchoredPosition}");

			contentField.GetComponent<MaterialField>().FillFieldWithMaterialData(materialJsons[i]);
			contentFields.Add(contentField.GetComponent<MaterialField>());
		}
		Debug.LogError("Content fields count = " + contentFields.Count);
	}
}
