using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;

public class MaterialBuilder : MonoBehaviour
{

    public static Material GetMaterial(MaterialJSON material)
    {
        Material materialFromTextures = new Material(Shader.Find("Pavel/CustomPBS"));

        DataCacher.GetTexturesFromCache(material);
        Vector2 textureScale = new Vector2(material.texture_dimensions.x, material.texture_dimensions.y);
        if (material != null)
        {
            materialFromTextures.name = material.id;

            DataCacher.GetTexturesFromCache(material);
            if (material.tex.tex_diffuse != "")
            {
                materialFromTextures.SetTexture(Shader.PropertyToID("_MainTex"), material.applyedTextures.tex_diffuse);
                materialFromTextures.SetTextureScale(Shader.PropertyToID("_MainTex"), textureScale);
            }
            if (material.tex.tex_normal != "")
            {
                materialFromTextures.SetTexture(Shader.PropertyToID("_BumpMap"), material.applyedTextures.tex_normal);
                materialFromTextures.SetTextureScale(Shader.PropertyToID("_BumpMap"), textureScale);
            }
            if (material.tex.tex_roughness != "")
            {
                materialFromTextures.SetTexture(Shader.PropertyToID("_GlossMap"), material.applyedTextures.tex_roughness);
                materialFromTextures.SetTextureScale(Shader.PropertyToID("_GlossMap"), textureScale);
            }
            material.ClearApplyedTextures();
        }
        return (materialFromTextures);
    }

    public static Material GetMaterial(string materialId)
    {
        Material materialFromTextures = new Material(Shader.Find("Pavel/CustomPBS"));
        if (materialId == "default" || materialId == "DefaultMaterial" || materialId == "Default")
        {
            materialFromTextures = Resources.Load<Material>("Default/DefaultMaterial");
            return materialFromTextures;
        }
        MaterialJSON material = DataCacher.GetMaterialById(materialId);
        if (material != null)
        {
            Debug.LogWarning("We are in material builder");
            materialFromTextures.name = materialId;

            Vector2 textureScale = new Vector2(material.texture_dimensions.x, material.texture_dimensions.y);
            DataCacher.GetTexturesFromCache(material);
            if (material.tex.tex_diffuse != "")
            {
                materialFromTextures.SetTexture(Shader.PropertyToID("_MainTex"), material.applyedTextures.tex_diffuse);
                materialFromTextures.SetTextureScale(Shader.PropertyToID("_MainTex"), textureScale);
            }
            if (material.tex.tex_normal != "")
            {
                materialFromTextures.SetTexture(Shader.PropertyToID("_BumpMap"), material.applyedTextures.tex_normal);
                materialFromTextures.SetTextureScale(Shader.PropertyToID("_BumpMap"), textureScale);
            }
            if (material.tex.tex_roughness != "")
            {
                materialFromTextures.SetTexture(Shader.PropertyToID("_GlossMap"), material.applyedTextures.tex_roughness);
                materialFromTextures.SetTextureScale(Shader.PropertyToID("_GlossMap"), textureScale);
            }
            material.ClearApplyedTextures();
        }
        return (materialFromTextures);
    }

    public static Material GetMaterial(string materialId, string objectType)
    {
        Material materialFromTextures = new Material(Shader.Find("Pavel/CustomPBS"));
        if (objectType == "baseboard")
        {
            materialFromTextures = new Material(Shader.Find("Custom/CustomPBSBaseBoard"));
        }
        else
        {
            materialFromTextures = new Material(Shader.Find("Pavel/CustomPBS"));

        }
        if (materialId == "default" || materialId == "DefaultMaterial")
        {
            materialFromTextures = Resources.Load<Material>("Default/DefaultMaterial");
            return materialFromTextures;
        }
        MaterialJSON material = DataCacher.GetMaterialById(materialId);

        if (material != null)
        {
            materialFromTextures.name = materialId;

            Vector2 textureScale = new Vector2(material.texture_dimensions.x, material.texture_dimensions.y);
            DataCacher.GetTexturesFromCache(material);
            if (material.tex.tex_diffuse != "")
            {
                materialFromTextures.SetTexture(Shader.PropertyToID("_MainTex"), material.applyedTextures.tex_diffuse);
                materialFromTextures.SetTextureScale(Shader.PropertyToID("_MainTex"), textureScale);
            }
            if (material.tex.tex_normal != "")
            {
                materialFromTextures.SetTexture(Shader.PropertyToID("_BumpMap"), material.applyedTextures.tex_normal);
                materialFromTextures.SetTextureScale(Shader.PropertyToID("_BumpMap"), textureScale);
            }
            if (material.tex.tex_roughness != "")
            {
                materialFromTextures.SetTexture(Shader.PropertyToID("_GlossMap"), material.applyedTextures.tex_roughness);
                materialFromTextures.SetTextureScale(Shader.PropertyToID("_GlossMap"), textureScale);
            }
            material.ClearApplyedTextures();
        }
        return (materialFromTextures);
    }

    public static Material GetMaterialFromTexture(MaterialJSON material, Texture2D texture)
    {
        Material materialFromTexture = new Material(Shader.Find("Pavel/CustomPBS"));
        Vector2 textureScale = new Vector2(material.texture_dimensions.x, material.texture_dimensions.y);
        materialFromTexture.SetTexture(Shader.PropertyToID("_MainTex"), texture);
        materialFromTexture.SetTextureScale(Shader.PropertyToID("_MainTex"), textureScale);
        return (materialFromTexture);
    }

    public static Texture2D MakePreviewForPaint(MaterialJSON materialJson)
    {
        Texture2D previewTexture = new Texture2D(200, 200);
        Color paintColor = new Color();
        string name = "preview_icon.png";
        if (materialJson.type == "paint" || materialJson.type == "ceiling")
        {
            string colorRGB;
            if (materialJson.type == "paint")
            {
                colorRGB = materialJson.custom_properties.tinting_color;
            }
            else
            {
                colorRGB = materialJson.color;
            }
            if (ColorUtility.TryParseHtmlString(colorRGB, out paintColor))
            {
                //float H, S, V;
                //Color.RGBToHSV(paintColor, out H, out S, out V);
                //S = 0.44f;
                //paintColor = Color.HSVToRGB(H, S, V);

                for (int y = 0; y < previewTexture.height; y++)
                {
                    for (int x = 0; x < previewTexture.width; x++)
                    {
                        previewTexture.SetPixel(x, y, paintColor);
                    }
                }

            }
        }
        previewTexture.name = name;
        previewTexture.Apply();
        materialJson.preview_icon = name;
        materialJson.tex.tex_diffuse = name;
        return (previewTexture);
    }

    public class MaterialKeeper
    {
        public Material material = new Material(Shader.Find("Pavel/CustomPBS"));
    }

    public IEnumerator UpdateMeshMaterial(MaterialJSON materialJson, UpdateMeshMaterialDelegate updateMeshMaterial)
    {
        //Debug.LogWarning("We are in material builder");
        //Material meshMaterial = new Material(Shader.Find("Pavel/CustomPBS"));
        if (materialJson != null)
        {
            if (GlobalApplicationManager.GetMaterialJsonById(materialJson.id).id == "Default")
            {
                materialJson = null;
            }
        }
        MaterialKeeper meshMaterial = new MaterialKeeper();
        if (materialJson != null && materialJson.name != "Default")
        {
            yield return (StartCoroutine(DownloadOrGetTextureAndCreateMaterial(materialJson, meshMaterial)));
        }
        if (meshMaterial.material != null && materialJson != null && !IsMaterialJsonDefault(materialJson))
        {
            meshMaterial.material.name = materialJson.id;
            //Debug.LogWarning("Material is not null");
            //Debug.LogWarning("Material name is '" + meshMaterial.material.name + "'");
            Vector2 textureScale = new Vector2(materialJson.texture_dimensions.x, materialJson.texture_dimensions.y);
            meshMaterial.material.SetTextureScale(Shader.PropertyToID("_MainTex"), textureScale);
            meshMaterial.material.SetTextureScale(Shader.PropertyToID("_BumpMap"), textureScale);
            meshMaterial.material.SetTextureScale(Shader.PropertyToID("_GlossMap"), textureScale);
            updateMeshMaterial(meshMaterial.material);
        }
        else if (materialJson == null)
        {
            Debug.LogError("Material Json is null");
            meshMaterial.material = Resources.Load<Material>("Default/DefaultMaterial");
            meshMaterial.material.name = "Default";
            updateMeshMaterial(meshMaterial.material);

        }
        else
        {

            //Debug.LogError("Material is null");
            meshMaterial.material = new Material(Shader.Find("Pavel/CustomPBS"));
            if (materialJson.id == "DefaultWall")
            {
                meshMaterial.material = Resources.Load<Material>("Default/DefaultWallMaterial");
                meshMaterial.material.name = "DefaultWall";

            }
            else if (materialJson.id == "DefaultFloor")
            {
                meshMaterial.material = Resources.Load<Material>("Default/DefaultFloorMaterial");
                meshMaterial.material.name = "DefaultFloor";
            }
            else if (materialJson.id == "DefaultDoor")
            {
                meshMaterial.material = Resources.Load<Material>("Default/DefaultDoorMaterial");
                meshMaterial.material.name = "DefaultDoor";
            }
            else
            {
                meshMaterial.material = Resources.Load<Material>("Default/DefaultMaterial");
                meshMaterial.material.name = "Default";
            }
            //meshMaterial.material.name = "Default";
            updateMeshMaterial(meshMaterial.material);
        }
    }

    public IEnumerator UpdateMeshMaterialWithUserImage(string userImageName, UpdateMeshMaterialDelegate updateMeshMaterial)
	{
        TextureKeeper textureKeeper = new TextureKeeper();
        yield return (StartCoroutine(DataCacher.GetUserPhotoTextureCoroutine(userImageName, textureKeeper)));
        Material materialFromUserPhoto = new Material(Shader.Find("Pavel/CustomPBS"));
        materialFromUserPhoto.name = userImageName;
        materialFromUserPhoto.SetTexture(Shader.PropertyToID("_MainTex"), textureKeeper.texture);
        Debug.LogWarning("Material from user photo is created");
        updateMeshMaterial(materialFromUserPhoto);
        yield break;
    } 

    public IEnumerator DownloadOrGetTextureAndCreateMaterial(MaterialJSON materialJson, MaterialKeeper materialToReturn)
    {
        bool isDiffuseInCache = false;
        Material materialFromTextures;
        Vector2 textureScale = new Vector2(materialJson.texture_dimensions.x, materialJson.texture_dimensions.y);
        if (materialJson.type == "baseboard")
        {
            materialFromTextures = new Material(Shader.Find("Custom/CustomPBSBaseBoard"));
        }
        else
        {
            materialFromTextures = new Material(Shader.Find("Pavel/CustomPBS"));

        }
        TextureKeeper textureDiffuse = new TextureKeeper();
        //Texture2D textureDiffuse = new Texture2D(256, 256);

        if (materialJson.type == "paint")
        {
            textureDiffuse.texture = DataCacher.GetPaintPreviewFromCache(materialJson);
            materialFromTextures.SetTexture(Shader.PropertyToID("_MainTex"), textureDiffuse.texture);
            materialFromTextures.SetTextureScale(Shader.PropertyToID("_MainTex"), textureScale);

        }
        else if (DataCacher.IsTextureExist(materialJson.id, materialJson.tex.tex_diffuse))
        {
            isDiffuseInCache = true;
            Debug.LogWarning("Texture is exist " + materialJson.id + " " + materialJson.name);
            textureDiffuse.texture = DataCacher.GetTextureFromCache(materialJson.id, materialJson.tex.tex_diffuse);
            materialFromTextures.SetTexture(Shader.PropertyToID("_MainTex"), textureDiffuse.texture);
            materialFromTextures.SetTextureScale(Shader.PropertyToID("_MainTex"), textureScale);
        }
        else
        {
            Debug.LogError("downloading texture from server");
            yield return (StartCoroutine(TextureDownloader.DownloadTexture(materialJson, materialJson.tex.tex_diffuse, textureDiffuse)));
            if (textureDiffuse != null)
            {

                materialFromTextures.SetTexture(Shader.PropertyToID("_MainTex"), textureDiffuse.texture);
                materialFromTextures.SetTextureScale(Shader.PropertyToID("_MainTex"), textureScale);
            }
            else
            {
                Debug.LogError("Diffuse is null on material '" + materialJson.id + "'");
                //materialToReturn = null;
                yield break;
            }
        }
        if (materialJson.tex.tex_normal != "")
        {
            //Texture2D textureNormal = new Texture2D(256, 256);
            TextureKeeper textureNormal = new TextureKeeper();
            if (DataCacher.IsTextureExist(materialJson.id, materialJson.tex.tex_normal) || isDiffuseInCache)
            {
                textureNormal.texture = DataCacher.GetTextureFromCache(materialJson.id, materialJson.tex.tex_normal);
            }
            else
            {
                yield return (StartCoroutine(TextureDownloader.DownloadTexture(materialJson, materialJson.tex.tex_normal, textureNormal)));
                if (textureNormal != null)
                {
                    materialFromTextures.SetTexture(Shader.PropertyToID("_BumpMap"), textureNormal.texture);
                    materialFromTextures.SetTextureScale(Shader.PropertyToID("_BumpMap"), textureScale);
                }
            }

        }
        if (materialJson.tex.tex_roughness != "")
        {
            //Texture2D textureRoughness = new Texture2D(256, 256);
            TextureKeeper textureRoughness = new TextureKeeper();
            if (DataCacher.IsTextureExist(materialJson.id, materialJson.tex.tex_roughness) || isDiffuseInCache)
            {
                textureRoughness.texture = DataCacher.GetTextureFromCache(materialJson.id, materialJson.tex.tex_roughness);
            }
            else
            {
                yield return (StartCoroutine(TextureDownloader.DownloadTexture(materialJson, materialJson.tex.tex_roughness, textureRoughness)));
                if (textureRoughness != null)
                {
                    materialFromTextures.SetTexture(Shader.PropertyToID("_GlossMap"), textureRoughness.texture);
                    materialFromTextures.SetTextureScale(Shader.PropertyToID("_GlossMap"), textureScale);
                }
            }
        }
        materialToReturn.material = materialFromTextures;

        yield break;
    }

    public bool IsMaterialJsonDefault(MaterialJSON materialJSON)
    {
        if (materialJSON.id == "DefaultWall" || materialJSON.name == "DefaultWall")
        {
            return (true);
        }
        else if (materialJSON.id == "DefaultFloor" || materialJSON.name == "DefaultFloor")
        {
            return (true);
        }
        else if (materialJSON.id == "DefaultDoor" || materialJSON.name == "DefaultDoor")
        {
            return (true);
        }
        else
        {
            return (false);
        }

    }
}
