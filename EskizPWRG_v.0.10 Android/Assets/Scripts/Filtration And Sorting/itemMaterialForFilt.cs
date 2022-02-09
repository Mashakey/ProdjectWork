using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using Newtonsoft.Json;

public static class itemMaterialForFilt
{
    [System.Serializable]
    public class WallpaperFilt
    {
        public static List<float> cost = new List<float>();
        public static List<string> color = new List<string>();
        public static List<string> manufacturer_company = new List<string>();
        public static List<string> manufacturer_country = new List<string>();
        public static List<string> collection = new List<string>();
        public static class pack_dimensions
        {
            public static List<float> x = new List<float>();
            public static List<float> y = new List<float>();
        }
    }

    [System.Serializable]
    public static class WallpaperForPaintingFilt
    {
        public static List<string> color = new List<string>();
        public static List<float> cost = new List<float>();
        public static List<string> manufacturer_company = new List<string>();
        public static List<string> manufacturer_country = new List<string>();
        public static List<string> collection = new List<string>();
        public static class pack_dimensions
        {
            public static List<float> x = new List<float>();
            public static List<float> y = new List<float>();
        }
    }

    [System.Serializable]
    public static class LaminateFilt
    {
        public static List<float> cost = new List<float>();
        public static List<string> color = new List<string>();
        public static List<string> manufacturer_company = new List<string>();
        public static List<string> manufacturer_country = new List<string>();
        public static List<string> collection = new List<string>();
        public static List<float> board_thickness = new List<float>();
        public static List<bool> chamfer = new List<bool>();
        public static List<bool> moisture_resistant= new List<bool>();
    }

    [System.Serializable]
    public static class LinoleumFilt
    {
        public static List<float> cost = new List<float>();
        public static List<string> color = new List<string>();
        public static List<string> manufacturer_company = new List<string>();
        public static List<string> manufacturer_country = new List<string>();
        public static List<string> collection = new List<string>();
        public static List<float> width_list = new List<float>();
        public static List<float> total_thickness = new List<float>();
        public static List<float> zs_thickness = new List<float>();
        public static List<string> design_type = new List<string>();
        public static List<string> use = new List<string>();
        public static List<string> basis = new List<string>();
    }

    [System.Serializable]
    public static class PVCsFilt
    {
        public static List<float> cost = new List<float>();
        public static List<string> color = new List<string>();
        public static List<string> manufacturer_company = new List<string>();
        public static List<string> manufacturer_country = new List<string>();
        public static List<string> collection = new List<string>();
        public static List<bool> chamfer = new List<bool>();
        public static List<float> protective_layer_thickness = new List<float>();
        public static List<float> board_thickness = new List<float>();
    }

    [System.Serializable]
    public static class BaseboardFilt
    {
        public static List<float> cost = new List<float>();
        public static List<string> manufacturer_company = new List<string>();
        public static List<string> manufacturer_country = new List<string>();
        public static List<string> collection = new List<string>();
        public static List<string> material = new List<string>();
        public static List<float> length = new List<float>();
        public static List<float> height = new List<float>();
    }

    [System.Serializable]
    public static class DoorFilt
    {
        public static List<float> cost = new List<float>();
        public static List<string> color = new List<string>();
        public static List<string> manufacturer_company = new List<string>();
        public static List<string> manufacturer_country = new List<string>();
        public static List<string> collection = new List<string>();
        public static List<string> coating_type = new List<string>();
        public static List<string> model = new List<string>();
    }

}
