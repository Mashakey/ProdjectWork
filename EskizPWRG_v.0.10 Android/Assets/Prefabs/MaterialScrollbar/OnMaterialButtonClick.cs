using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;

public class OnMaterialButtonClick : MonoBehaviour
{
    public void OnClick()
	{
        managerMaterial managerMaterialScript = GameObject.FindObjectOfType<managerMaterial>();
        Wall parentWall = managerMaterialScript.GetParentWall();
        //Debug.LogError("THIS MATERIAL ID IS " + gameObject.name);
        
        MaterialJSON material = DataCacher.GetMaterialById(gameObject.name);
        if (DataCacher.IsTextureExist(material.id, material.tex.tex_diffuse))
        {
            Debug.LogError("Texture is found");
            Texture2D texture = DataCacher.GetTextureFromCache(material.id, material.tex.tex_diffuse);
            parentWall.UpdateMaterial(MaterialBuilder.GetMaterialFromTexture(material, texture));
        }
        else
		{
            Debug.LogError("Texture is not found, downloading");

            StartCoroutine(ServerDataDownloaderCoroutine.GetMaterialDiffuseRightNow(material, parentWall.UpdateMaterial));
		}
        managerMaterialScript.SetCanvasActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
