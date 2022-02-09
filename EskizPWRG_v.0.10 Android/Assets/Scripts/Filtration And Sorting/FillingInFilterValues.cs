using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static itemMaterialForFilt;

public class FillingInFilterValues : MonoBehaviour
{
    public void FilterOnlyWallpaper()
    {

        for (int i = 0; i < GlobalApplicationManager.Wallpapers.Count; i++)
        {
            if (!WallpaperFilt.cost.Contains(GlobalApplicationManager.Wallpapers[i].cost))
            {
                WallpaperFilt.cost.Add(GlobalApplicationManager.Wallpapers[i].cost);
            }
            //if (!WallpaperFilt.color.Contains(GlobalApplicationManager.Wallpapers[i].color))
            //{
            //    WallpaperFilt.color.Add(GlobalApplicationManager.Wallpapers[i].color);
            //}
            if (!WallpaperFilt.manufacturer_company.Contains(GlobalApplicationManager.Wallpapers[i].custom_properties.manufacturer_company))
            {
                WallpaperFilt.manufacturer_company.Add(GlobalApplicationManager.Wallpapers[i].custom_properties.manufacturer_company);
            }
            if (!WallpaperFilt.manufacturer_country.Contains(GlobalApplicationManager.Wallpapers[i].custom_properties.manufacturer_country))
            {
                WallpaperFilt.manufacturer_country.Add(GlobalApplicationManager.Wallpapers[i].custom_properties.manufacturer_country);
            }
            if (!WallpaperFilt.collection.Contains(GlobalApplicationManager.Wallpapers[i].custom_properties.collection) && GlobalApplicationManager.Wallpapers[i].custom_properties.collection != "")
            {
                WallpaperFilt.collection.Add(GlobalApplicationManager.Wallpapers[i].custom_properties.collection);
            }
            if (!WallpaperFilt.pack_dimensions.x.Contains(GlobalApplicationManager.Wallpapers[i].pack_dimensions.x))
            {
                WallpaperFilt.pack_dimensions.x.Add(GlobalApplicationManager.Wallpapers[i].pack_dimensions.x);
            }
            if (!WallpaperFilt.pack_dimensions.y.Contains(GlobalApplicationManager.Wallpapers[i].pack_dimensions.y))
            {
                WallpaperFilt.pack_dimensions.y.Add(GlobalApplicationManager.Wallpapers[i].pack_dimensions.y);
            }
            for (int j = 0; j < GlobalApplicationManager.Wallpapers[i].custom_properties.color.Length; j++)
            {
                if (!WallpaperFilt.color.Contains(GlobalApplicationManager.Wallpapers[i].custom_properties.color[j]))
                {
                    WallpaperFilt.color.Add(GlobalApplicationManager.Wallpapers[i].custom_properties.color[j]);
                }
            }
        }
    }

    public void FilterOnlyWallpaperForPaintings()
    {
        
        for(int i=0; i < GlobalApplicationManager.WallpapersForPainting.Count; i++)
        {
            if (!WallpaperForPaintingFilt.cost.Contains(GlobalApplicationManager.WallpapersForPainting[i].cost))
            {
                WallpaperForPaintingFilt.cost.Add(GlobalApplicationManager.WallpapersForPainting[i].cost);
            }
            if (!WallpaperForPaintingFilt.manufacturer_company.Contains(GlobalApplicationManager.WallpapersForPainting[i].custom_properties.manufacturer_company))
            {
                WallpaperForPaintingFilt.manufacturer_company.Add(GlobalApplicationManager.WallpapersForPainting[i].custom_properties.manufacturer_company);
            }
            if (!WallpaperForPaintingFilt.manufacturer_country.Contains(GlobalApplicationManager.WallpapersForPainting[i].custom_properties.manufacturer_country))
            {
                WallpaperForPaintingFilt.manufacturer_country.Add(GlobalApplicationManager.WallpapersForPainting[i].custom_properties.manufacturer_country);
            }
            if (!WallpaperForPaintingFilt.collection.Contains(GlobalApplicationManager.WallpapersForPainting[i].custom_properties.collection) && GlobalApplicationManager.WallpapersForPainting[i].custom_properties.collection != "")
            {
                WallpaperForPaintingFilt.collection.Add(GlobalApplicationManager.WallpapersForPainting[i].custom_properties.collection);
            }
            if (!WallpaperForPaintingFilt.pack_dimensions.x.Contains(GlobalApplicationManager.WallpapersForPainting[i].pack_dimensions.x))
            {
                WallpaperForPaintingFilt.pack_dimensions.x.Add(GlobalApplicationManager.WallpapersForPainting[i].pack_dimensions.x);
            }
            if (!WallpaperForPaintingFilt.pack_dimensions.y.Contains(GlobalApplicationManager.WallpapersForPainting[i].pack_dimensions.y))
            {
                WallpaperForPaintingFilt.pack_dimensions.y.Add(GlobalApplicationManager.WallpapersForPainting[i].pack_dimensions.y);
            }
        }
    }

    public void FilterOnlyLaminate()
    {

        for (int i = 0; i < GlobalApplicationManager.Laminates.Count; i++)
        {
            if (!LaminateFilt.cost.Contains(GlobalApplicationManager.Laminates[i].cost))
            {
                LaminateFilt.cost.Add(GlobalApplicationManager.Laminates[i].cost);
            }
            //if (!LaminateFilt.color.Contains(GlobalApplicationManager.Laminates[i].color))
            //{
            //    LaminateFilt.color.Add(GlobalApplicationManager.Laminates[i].color);
            //}
            if (!LaminateFilt.manufacturer_company.Contains(GlobalApplicationManager.Laminates[i].custom_properties.manufacturer_company))
            {
                LaminateFilt.manufacturer_company.Add(GlobalApplicationManager.Laminates[i].custom_properties.manufacturer_company);
            }
            if (!LaminateFilt.manufacturer_country.Contains(GlobalApplicationManager.Laminates[i].custom_properties.manufacturer_country))
            {
                LaminateFilt.manufacturer_country.Add(GlobalApplicationManager.Laminates[i].custom_properties.manufacturer_country);
            }
            if (!LaminateFilt.collection.Contains(GlobalApplicationManager.Laminates[i].custom_properties.collection))
            {
                LaminateFilt.collection.Add(GlobalApplicationManager.Laminates[i].custom_properties.collection);
            }
            if (!LaminateFilt.board_thickness.Contains(GlobalApplicationManager.Laminates[i].custom_properties.board_thickness))
            {
                LaminateFilt.board_thickness.Add(GlobalApplicationManager.Laminates[i].custom_properties.board_thickness);
            }
            if (!LaminateFilt.chamfer.Contains(GlobalApplicationManager.Laminates[i].custom_properties.chamfer))
            {
                LaminateFilt.chamfer.Add(GlobalApplicationManager.Laminates[i].custom_properties.chamfer);
            }
            if (!LaminateFilt.moisture_resistant.Contains(GlobalApplicationManager.Laminates[i].custom_properties.moisture_resistant))
            {
                LaminateFilt.moisture_resistant.Add(GlobalApplicationManager.Laminates[i].custom_properties.moisture_resistant);
            }
            for (int j = 0; j < GlobalApplicationManager.Laminates[i].custom_properties.color.Length; j++)
            {
                if (!LaminateFilt.color.Contains(GlobalApplicationManager.Laminates[i].custom_properties.color[j]))
                {
                    LaminateFilt.color.Add(GlobalApplicationManager.Laminates[i].custom_properties.color[j]);
                }
            }
        }
    }

    public void FilterOnlyLinoleums()
    {

        for (int i = 0; i < GlobalApplicationManager.Linoleums.Count; i++)
        {
            if (!LinoleumFilt.cost.Contains(GlobalApplicationManager.Linoleums[i].cost))
            {
                LinoleumFilt.cost.Add(GlobalApplicationManager.Linoleums[i].cost);
            }
            //if (!LinoleumFilt.color.Contains(GlobalApplicationManager.Linoleums[i].color))
            //{
            //    LinoleumFilt.color.Add(GlobalApplicationManager.Linoleums[i].color);
            //}
            if (!LinoleumFilt.manufacturer_company.Contains(GlobalApplicationManager.Linoleums[i].custom_properties.manufacturer_company))
            {
                LinoleumFilt.manufacturer_company.Add(GlobalApplicationManager.Linoleums[i].custom_properties.manufacturer_company);
            }
            if (!LinoleumFilt.manufacturer_country.Contains(GlobalApplicationManager.Linoleums[i].custom_properties.manufacturer_country))
            {
                LinoleumFilt.manufacturer_country.Add(GlobalApplicationManager.Linoleums[i].custom_properties.manufacturer_country);
            }
            if (!LinoleumFilt.collection.Contains(GlobalApplicationManager.Linoleums[i].custom_properties.collection))
            {
                LinoleumFilt.collection.Add(GlobalApplicationManager.Linoleums[i].custom_properties.collection);
            }
            //if (!LinoleumFilt.width_list.Contains(GlobalApplicationManager.Linoleums[i].custom_properties.weight))
            //{
            //    LinoleumFilt.weight.Add(GlobalApplicationManager.Linoleums[i].custom_properties.weight);
            //}
            if (!LinoleumFilt.total_thickness.Contains(GlobalApplicationManager.Linoleums[i].custom_properties.total_thickness))
            {
                LinoleumFilt.total_thickness.Add(GlobalApplicationManager.Linoleums[i].custom_properties.total_thickness);
            }
            if (!LinoleumFilt.zs_thickness.Contains(GlobalApplicationManager.Linoleums[i].custom_properties.zs_thickness))
            {
                LinoleumFilt.zs_thickness.Add(GlobalApplicationManager.Linoleums[i].custom_properties.zs_thickness);
            }
            if (!LinoleumFilt.design_type.Contains((GlobalApplicationManager.Linoleums[i].custom_properties.design_type)))
            {
                LinoleumFilt.design_type.Add(GlobalApplicationManager.Linoleums[i].custom_properties.design_type);
            }
            if (!LinoleumFilt.use.Contains(GlobalApplicationManager.Linoleums[i].custom_properties.use))
            {
                LinoleumFilt.use.Add(GlobalApplicationManager.Linoleums[i].custom_properties.use);
            }
            if (!LinoleumFilt.basis.Contains(GlobalApplicationManager.Linoleums[i].custom_properties.basis))
            {
                LinoleumFilt.basis.Add(GlobalApplicationManager.Linoleums[i].custom_properties.basis);
            }
            for (int j = 0; j < GlobalApplicationManager.Linoleums[i].custom_properties.color.Length; j++)
            {
                if (!LinoleumFilt.color.Contains(GlobalApplicationManager.Linoleums[i].custom_properties.color[j]))
                {
                    LinoleumFilt.color.Add(GlobalApplicationManager.Linoleums[i].custom_properties.color[j]);
                }
            }

        }
    }

    public void FilterOnlyPVCs()
    {

        for (int i = 0; i < GlobalApplicationManager.PVCs.Count; i++)
        {
            if (!PVCsFilt.cost.Contains(GlobalApplicationManager.PVCs[i].cost))
            {
                PVCsFilt.cost.Add(GlobalApplicationManager.PVCs[i].cost);
            }
            //if (!PVCsFilt.color.Contains(GlobalApplicationManager.PVCs[i].color))
            //{
            //    PVCsFilt.color.Add(GlobalApplicationManager.PVCs[i].color);
            //}
            if (!PVCsFilt.manufacturer_company.Contains(GlobalApplicationManager.PVCs[i].custom_properties.manufacturer_company))
            {
                PVCsFilt.manufacturer_company.Add(GlobalApplicationManager.PVCs[i].custom_properties.manufacturer_company);
            }
            if (!PVCsFilt.manufacturer_country.Contains(GlobalApplicationManager.PVCs[i].custom_properties.manufacturer_country))
            {
                PVCsFilt.manufacturer_country.Add(GlobalApplicationManager.PVCs[i].custom_properties.manufacturer_country);
            }
            if (!PVCsFilt.collection.Contains(GlobalApplicationManager.PVCs[i].custom_properties.collection))
            {
                PVCsFilt.collection.Add(GlobalApplicationManager.PVCs[i].custom_properties.collection);
            }
            if (!PVCsFilt.chamfer.Contains(GlobalApplicationManager.PVCs[i].custom_properties.chamfer))
            {
                PVCsFilt.chamfer.Add(GlobalApplicationManager.PVCs[i].custom_properties.chamfer);
            }
            if (!PVCsFilt.protective_layer_thickness.Contains(GlobalApplicationManager.PVCs[i].custom_properties.protective_layer_thickness))
            {
                PVCsFilt.protective_layer_thickness.Add(GlobalApplicationManager.PVCs[i].custom_properties.protective_layer_thickness);
            }
            if (!PVCsFilt.board_thickness.Contains(GlobalApplicationManager.PVCs[i].custom_properties.board_thickness))
            {
                PVCsFilt.board_thickness.Add(GlobalApplicationManager.PVCs[i].custom_properties.board_thickness);
            }
            for (int j = 0; j < GlobalApplicationManager.PVCs[i].custom_properties.color.Length; j++)
            {
                if (!PVCsFilt.color.Contains(GlobalApplicationManager.PVCs[i].custom_properties.color[j]))
                {
                    PVCsFilt.color.Add(GlobalApplicationManager.PVCs[i].custom_properties.color[j]);
                }
            }
        }
    }
    public void FilterOnlyBaseboards()
    {

        for (int i = 0; i < GlobalApplicationManager.Baseboards.Count; i++)
        {
            if (!BaseboardFilt.cost.Contains(GlobalApplicationManager.Baseboards[i].cost))
            {
                BaseboardFilt.cost.Add(GlobalApplicationManager.Baseboards[i].cost);
            }
            if (!BaseboardFilt.manufacturer_company.Contains(GlobalApplicationManager.Baseboards[i].custom_properties.manufacturer_company))
            {
                BaseboardFilt.manufacturer_company.Add(GlobalApplicationManager.Baseboards[i].custom_properties.manufacturer_company);
            }
            if (!BaseboardFilt.manufacturer_country.Contains(GlobalApplicationManager.Baseboards[i].custom_properties.manufacturer_country))
            {
                BaseboardFilt.manufacturer_country.Add(GlobalApplicationManager.Baseboards[i].custom_properties.manufacturer_country);
            }
            if (!BaseboardFilt.collection.Contains(GlobalApplicationManager.Baseboards[i].custom_properties.collection))
            {
                BaseboardFilt.collection.Add(GlobalApplicationManager.Baseboards[i].custom_properties.collection);
            }
            if (!BaseboardFilt.material.Contains(GlobalApplicationManager.Baseboards[i].custom_properties.material) && GlobalApplicationManager.Baseboards[i].custom_properties.material != "")
            {
                BaseboardFilt.material.Add(GlobalApplicationManager.Baseboards[i].custom_properties.material);
            }
            if (!BaseboardFilt.length.Contains(GlobalApplicationManager.Baseboards[i].custom_properties.length))
            {
                BaseboardFilt.length.Add(GlobalApplicationManager.Baseboards[i].custom_properties.length);
            }
            if (!BaseboardFilt.height.Contains(GlobalApplicationManager.Baseboards[i].custom_properties.height))
            {
                BaseboardFilt.height.Add(GlobalApplicationManager.Baseboards[i].custom_properties.height);
            }
        }
    }

    public void FilterOnlyDoors()
    {

        for (int i = 0; i < GlobalApplicationManager.Doors.Count; i++)
        {
            if (!DoorFilt.cost.Contains(GlobalApplicationManager.Doors[i].cost))
            {
                DoorFilt.cost.Add(GlobalApplicationManager.Doors[i].cost);
            }
            //if (!DoorFilt.color.Contains(GlobalApplicationManager.Doors[i].color))
            //{
            //    DoorFilt.color.Add(GlobalApplicationManager.Doors[i].color);
            //}
            if (!DoorFilt.manufacturer_company.Contains(GlobalApplicationManager.Doors[i].custom_properties.manufacturer_company))
            {
                DoorFilt.manufacturer_company.Add(GlobalApplicationManager.Doors[i].custom_properties.manufacturer_company);
            }
            if (!DoorFilt.manufacturer_country.Contains(GlobalApplicationManager.Doors[i].custom_properties.manufacturer_country))
            {
                DoorFilt.manufacturer_country.Add(GlobalApplicationManager.Doors[i].custom_properties.manufacturer_country);
            }
            if (!DoorFilt.collection.Contains(GlobalApplicationManager.Doors[i].custom_properties.collection))
            {
                DoorFilt.collection.Add(GlobalApplicationManager.Doors[i].custom_properties.collection);
            }
            if (!DoorFilt.coating_type.Contains(GlobalApplicationManager.Doors[i].custom_properties.coating_type))
            {
                DoorFilt.coating_type.Add(GlobalApplicationManager.Doors[i].custom_properties.coating_type);
            }
            if (!DoorFilt.model.Contains(GlobalApplicationManager.Doors[i].custom_properties.model))
            {
                DoorFilt.model.Add(GlobalApplicationManager.Doors[i].custom_properties.model);
            }
            for (int j = 0; j < GlobalApplicationManager.Doors[i].custom_properties.color.Length; j++)
            {
                if (!DoorFilt.color.Contains(GlobalApplicationManager.Doors[i].custom_properties.color[j]))
                {
                    DoorFilt.color.Add(GlobalApplicationManager.Doors[i].custom_properties.color[j]);
                }
            }

        }
    }
}
