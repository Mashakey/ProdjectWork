using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;

public class PreviewMaterialPageFiller : MonoBehaviour
{
    [SerializeField]
    WallpaperPreviewPage wallpaperPreviewPage;
    [SerializeField]
    WallpaperForPaintingPreviewPage wallpaperForPaintingPreviewPage;
    [SerializeField]
    LaminatePreviewPage laminatePreviewPage;
    [SerializeField]
    LinoleumPreviewPage linoleumPreviewPage;
    [SerializeField]
    PVHPreviewPage pvhPreviewPage;
    [SerializeField]
    BaseboardPreviewPage baseboardPreviewPage;
    [SerializeField]
    DoorPreviewPage doorPreviewPage;

    public void EnableMaterialPreviewPage(MaterialJSON materialJson)
	{
        if (materialJson.type == "wallpaper")
		{
            wallpaperPreviewPage.FillPage(materialJson);
		}
        else if (materialJson.type == "wallpaper_for_painting")
		{
            wallpaperForPaintingPreviewPage.FillPage(materialJson);
        }
        else if (materialJson.type == "laminate")
		{
            laminatePreviewPage.FillPage(materialJson);
        }
        else if (materialJson.type == "linoleum")
		{
            linoleumPreviewPage.FillPage(materialJson);
        }
        else if (materialJson.type == "pvc")
		{
            pvhPreviewPage.FillPage(materialJson);
        }
        else if (materialJson.type == "baseboard")
		{
            baseboardPreviewPage.FillPage(materialJson);
        }
        else if (materialJson.type == "door")
		{
            doorPreviewPage.FillPage(materialJson);
        }
        else if (materialJson.type == "paint")
		{

		}
        else
		{
            Debug.LogError("Unknown material type " + materialJson.type);
		}
	}
}
