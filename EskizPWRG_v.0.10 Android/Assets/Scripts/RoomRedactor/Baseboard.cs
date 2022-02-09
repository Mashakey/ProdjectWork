using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baseboard : MonoBehaviour
{
    public Material BaseboardMaterial;
    public string MaterialId;
    public Wall AttachedWall;

    public void UpdateMaterial(Material updateMaterial)
    {
        Room room = FindObjectOfType<Room>();
        room.baseBoardMaterialId = updateMaterial.name;
        BaseboardMaterial = updateMaterial;
        MaterialId = updateMaterial.name;
        gameObject.GetComponent<MeshRenderer>().material = updateMaterial;
        UpdateTextureScale();
    }

    void UpdateTextureScale()
    {
        Vector2 vanillaTextureScale = BaseboardMaterial.mainTextureScale;
        //Vector2 textureScale = new Vector2(AttachedWall.Length * vanillaTextureScale.x, AttachedWall.Height * vanillaTextureScale.y);
        //Vector2 textureScale = new Vector2(AttachedWall.Length * vanillaTextureScale.y, AttachedWall.Height * vanillaTextureScale.x);
        Vector2 textureScale = new Vector2(1, 1);
        gameObject.GetComponent<MeshRenderer>().material.SetTextureScale(Shader.PropertyToID("_MainTex"), textureScale);
        gameObject.GetComponent<MeshRenderer>().material.SetTextureScale(Shader.PropertyToID("_BumpMap"), textureScale);
        gameObject.GetComponent<MeshRenderer>().material.SetTextureScale(Shader.PropertyToID("_GlossMap"), textureScale);
    }

}
