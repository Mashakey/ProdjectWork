using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;

public class UserPhotoField : MonoBehaviour
{

	public Texture2D photoTexture;

	public void SelectPhotoField()
	{
		UserPhotosPageFiller userPhotosPageFiller = GetComponentInParent<UserPhotosPageFiller>();
		userPhotosPageFiller.SelectPhotoField(this);
	}

	public void SetPreviewAndTexture()
	{
		string photoName = transform.name;
		StartCoroutine(GetAndSetPreviewAndTexture(photoName));
	}

	public IEnumerator GetAndSetPreviewAndTexture(string photoName)
	{
		TextureKeeper textureKeeper = new TextureKeeper();
		yield return (StartCoroutine(DataCacher.GetUserPhotoTextureCoroutine(photoName, textureKeeper)));
		photoTexture = textureKeeper.texture;
		photoTexture.name = photoName;
		Sprite preview = Sprite.Create(textureKeeper.texture, new Rect(0.0f, 0.0f, textureKeeper.texture.width, textureKeeper.texture.height), new Vector2(0.5f, 0.5f), 100.0f);
		GetComponent<UnityEngine.UI.Image>().sprite = preview;
		Resources.UnloadUnusedAssets();
		yield break;
	}
}
