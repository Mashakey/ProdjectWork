using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureUpdater : MonoBehaviour
{
    public void UpdateTexture(Material updateMaterial)
	{
		if (gameObject != null)
		{
			Debug.LogWarning("We are in textureUpdater");
			Debug.LogWarning("object is '" + name + "'");
			Debug.LogWarning("material is '" + updateMaterial.name + "'");
			//MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
			//renderer.material = updateMaterial;
			//Vector2 vanillaTextureScale = updateMaterial.mainTextureScale;
			//Vector2 objectSize = GetAttachedObjectSize();
			//Vector2 textureScale = new Vector2(objectSize.x * vanillaTextureScale.x, objectSize.y * vanillaTextureScale.y);
			//renderer.material.SetTextureScale(Shader.PropertyToID("_MainTex"), textureScale);
			//renderer.material.SetTextureScale(Shader.PropertyToID("_BumpMap"), textureScale);
			//renderer.material.SetTextureScale(Shader.PropertyToID("_GlossMap"), textureScale);

			if (transform.GetComponent<Wall>() != null)
			{
				Wall wall = transform.GetComponent<Wall>();
				string previousMaterialId = wall.materialId;
				string newMaterialId = updateMaterial.name;
				HistoryAction historyAction = new HistoryAction();
				historyAction.CreateChangeMaterialHistoryAction(wall.UpdateMaterial, previousMaterialId, newMaterialId);
				HistoryChangesStack historyChangesStack = FindObjectOfType<HistoryChangesStack>();
				historyChangesStack.AddHistoryAction(historyAction);
				//transform.GetComponent<Wall>().oldMaterialId = updateMaterial.name;
				transform.GetComponent<Wall>().UpdateMaterial(updateMaterial);
			}
			else if (transform.GetComponent<Floor>() != null)
			{
				Room room = GetComponentInParent<Room>();
				if (name == "ceiling")
				{
					room.ceilingMaterialId = updateMaterial.name;
				}
				else
				{
					room.floorMaterialId = updateMaterial.name;

				}

				Floor floor = transform.GetComponent<Floor>();
				string previousMaterialId = floor.materialId;
				string newMaterialId = updateMaterial.name;
				HistoryAction historyAction = new HistoryAction();
				historyAction.CreateChangeMaterialHistoryAction(floor.UpdateMaterial, previousMaterialId, newMaterialId);
				HistoryChangesStack historyChangesStack = FindObjectOfType<HistoryChangesStack>();
				historyChangesStack.AddHistoryAction(historyAction);
				//transform.GetComponent<Floor>().oldMaterialId = updateMaterial.name;
				//FindObjectOfType<Room>().floorMaterialId = updateMaterial.name;
				transform.GetComponent<Floor>().UpdateMaterial(updateMaterial);

			}
			else if (transform.GetComponent<Door>() != null)
			{
				Door door = transform.GetComponent<Door>();
				string previousMaterialId = door.MaterialId;
				string newMaterialId = updateMaterial.name;
				HistoryAction historyAction = new HistoryAction();
				historyAction.CreateChangeMaterialHistoryAction(door.UpdateMaterial, previousMaterialId, newMaterialId);
				HistoryChangesStack historyChangesStack = FindObjectOfType<HistoryChangesStack>();
				historyChangesStack.AddHistoryAction(historyAction);

				transform.GetComponent<Door>().UpdateMaterial(updateMaterial);
			}
		}
	}

	Vector2 GetAttachedObjectSize()
	{
		Vector2 objectSize = new Vector2(1, 1);
		if (transform.GetComponent<Wall>() != null)
		{
			objectSize.x = transform.GetComponent<Wall>().Length;
			objectSize.y = transform.GetComponent<Wall>().Height;
		}
		else if (transform.GetComponent<Floor>() != null)
		{
			objectSize.x = transform.GetComponent<Floor>().Length;
			objectSize.y = transform.GetComponent<Floor>().Width;
		}
		return (objectSize);
	}
}
