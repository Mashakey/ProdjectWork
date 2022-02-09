using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class GlossyPaintsPageFiller : MonoBehaviour
{
    [SerializeField]
    Button applyButton;
    [SerializeField]
    GameObject paintGlossyPrefab;
    [SerializeField]
    public Sprite inactiveApplyButton;

    List<Transform> paintFields = new List<Transform>();

    public void CreatePaintFields(List<MaterialJSON> paintsJsons)
    {
        applyButton.interactable = false;
        applyButton.GetComponent<Image>().sprite = inactiveApplyButton;
        Transform panelSafeArea = gameObject.transform.Find("PanelSafeArea");
        Transform scrollContentPaint = panelSafeArea.transform.Find("ScrolContentPaints");
        Transform paintsPage = scrollContentPaint.transform.Find("PaintsPg");

        GetComponent<Canvas>().enabled = true;
        GetComponent<CanvasScaler>().enabled = true;

        ClearPaintFields();
        paintFields = new List<Transform>();

        foreach (var paint in paintsJsons)
        {
            GameObject paintField = Instantiate(paintGlossyPrefab, paintsPage.transform);
            paintField.name = paint.id;
            paintFields.Add(paintField.transform);
            Transform objectField = paintField.transform.Find("Object");
            Transform nameField = paintField.transform.Find("Name");
            nameField.GetComponent<Text>().text = paint.name;
            Transform blurField = objectField.transform.Find("Blur");
            Transform priceField = blurField.transform.Find("Price");
            priceField.GetComponent<Text>().text = paint.cost.ToString() + " \u20BD/ë";
            nameField.GetComponent<Text>().text = paint.name;
            Transform paintImage = objectField.transform.Find("paintImage");
            Texture2D texturePreview = DataCacher.GetPaintPreviewFromCache(paint);
            Sprite preview = Sprite.Create(texturePreview, new Rect(0.0f, 0.0f, texturePreview.width, texturePreview.height), new Vector2(0.5f, 0.5f), 100.0f);
            paintImage.GetComponent<Image>().sprite = preview;
            SetHeartSpriteStatus(paintField, paint);
        }
    }

    public void SetHeartSpriteStatus(GameObject paintField, MaterialJSON materialJson)
    {
        if (FavoritesStorage.IsMaterialInFavorites(materialJson))
        {
            paintField.GetComponent<PaintLikeButtonBehaviour>().SetHeartRedWithoutAdding();
            //PaintLikeButtonBehaviour likeField = GetComponentInChildren<PaintLikeButtonBehaviour>();
            //likeField.SetHeartRedWithoutAdding();
        }
    }

    public void ClearPaintFields()
	{
        foreach(var paint in paintFields)
		{
            Destroy(paint.gameObject);
		}
        paintFields.Clear();
	}
}
