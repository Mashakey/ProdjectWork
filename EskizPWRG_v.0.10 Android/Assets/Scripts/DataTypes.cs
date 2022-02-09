using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using Newtonsoft.Json;


public static class DataTypes
{
	public delegate void UpdateMeshMaterialDelegate(Material material);
	public delegate void MoveObjectOnWallDelegate(Transform movingObject, Vector2 coordinates, Vector2Int objectSizeInCells);
	public delegate void AddObjectOnWall(Transform movingObject, Vector2 coordinates, Vector2Int objectSizeInCells);

	public class TextureDownloadingStatus
	{
		public bool isNormalDownloaded = false;
		public bool isRoughnessDownloaded = false;
	}

	public class HTTPHeader
	{
		public string Key;
		public string Value;
	}

	[System.Serializable]
	public class DeviceObj
	{
		public string deviceId;
		public string deviceModel;
		public string deviceOS;

		public DeviceObj(string id, string model, string oS)
		{
			deviceId = id;
			deviceModel = model;
			deviceOS = oS;
		}
	}

	[System.Serializable]
	public class EventObj
	{
		public string deviceId;
		public int @event;
		public string addition;

		public EventObj(string id, int eventId, string _addition)
		{
			deviceId = id;
			@event = eventId;
			addition = _addition;
		}
	}

	[System.Serializable]
	public class FurnitureObjectData
	{
		public string Name;
		public Vector3 Position;
		public Quaternion Rotation;
	}

	[System.Serializable]
	public enum FurnitureType
	{
		Bed,
		Table,
		Chair,
		Sofa,
		Cupboard
	}

	[System.Serializable]
	public enum MaterialType
	{
		Wallpaper,
		WallpaperForPainting,
		Laminate,
		Linoleum,
		PVC,
		Door,
		Baseboard,
		Paint
	}

	[System.Serializable]
	public enum HistoryActionType
	{
		ChangeSurfaceTexture,
		MoveObjectOnWall,
		DeleteObject,
		CreateObject,
		CreateBaseboard,
		ChangeBaseboard,
		DeleteBaseboard,
		ScaleWindow,
		ChangeWindowType,
		MoveFurnitureObject,
		RotateFurnitureObject,
		DeleteFurnitureObject,
		CreateFurnitureObject
	}

	[System.Serializable]
	public enum InteriorType
	{
		LivingRoom,
		Bedroom,
		Hallway,
		Nursery
	}

	[System.Serializable]
	public enum RoomType
	{
		Rectangle,
		G_type,
		T_type,
		Trapezoidal,
		Z_type
	}

	[System.Serializable]
	public enum WindowType
	{
		double_leaf_window,
		tricuspid_window,
		balcony_right_door,
		balcony_left_door
	}

	[System.Serializable]
	public class TextureKeeper
	{
		public Texture2D texture = new Texture2D(256, 256);
	}

	[System.Serializable]
	public class ColorJson
	{
		public string _id;
		public string name;
		public string color;
		public string createdAt;
		public string updatedAt;
		public float __v;
		public string id;
	}

	[System.Serializable]
	public class Favorites
	{
		public List<MaterialJSON> Wallpapers = new List<MaterialJSON>();
		public List<MaterialJSON> Laminates = new List<MaterialJSON>();
		public List<MaterialJSON> Linoleums = new List<MaterialJSON>();
		public List<MaterialJSON> Doors = new List<MaterialJSON>();
		public List<MaterialJSON> PVCs = new List<MaterialJSON>();
		public List<MaterialJSON> Baseboards = new List<MaterialJSON>();
		public List<MaterialJSON> WallpapersForPainting = new List<MaterialJSON>();
		public List<MaterialJSON> Paints = new List<MaterialJSON>();
	}

	[System.Serializable]
	public class PdfData
	{
		public string title;
		public string city;
		public PdfRoomData room;
		public PdfMaterialsData materials;
	}

	[System.Serializable]
	public class PdfRoomData
	{
		public string title;
		public string width;
		public string length;
		public string heigh;
		public string doors;
		public string windows;
	}

	[System.Serializable]
	public class PdfMaterialsData
	{
		public string title;
		public string[] headers;
		public List<PdfLine> lines;
		public string total;
		public string sum;
	}

	[System.Serializable]
	public class PdfLine
	{
		public string[] line;
	}

	[System.Serializable]
	public class RoomData
	{
		public string Name;
		public RoomType Type;
		public InteriorType Interior;
		public float Height;
		public float Area;
		public float Cost;
		public List<Vector2> RoomCorners = new List<Vector2>();
		public List<WallData> Walls = new List<WallData>();
		public List<NoteData> Notes = new List<NoteData>();
		public List<FurnitureObjectData> Furniture = new List<FurnitureObjectData>();
		//public Baseboard Baseboard;
		public BaseBoardData Baseboard;
		public FloorData Floor;
		public CeilingData Ceiling;
		public Vector3 CameraPosition;
	}

	[System.Serializable]
	public class NoteData
	{
		public Vector3 position = new Vector3(0f, 0f, 0f);
		public string contentText = "";
	}

	[System.Serializable]
	public class WallData
	{
		public Vector2 startCoord;
		public Vector2 endCoord;
		public string MaterialId;
		public List<WindowData> Windows = new List<WindowData>();
		public List<DoorData> Doors = new List<DoorData>();

		public WallData(Vector2 startCoord, Vector2 endCoord)
		{
			this.startCoord = startCoord;
			this.endCoord = endCoord;
		}
	}

	[System.Serializable]
	public class WindowData
	{
		public Vector3 Position;
		public Quaternion Rotation;
		public WindowType Type;
		public float Scale;

	}

	[System.Serializable]
	public class DoorData
	{
		public Vector3 Position;
		public Quaternion Rotation;
		public string MaterialId;
	}

	[System.Serializable]
	public class BaseBoardData
	{
		public string MaterialId;
	}

	[System.Serializable]
	public class FloorData
	{
		public string MaterialId;
	}

	[System.Serializable]
	public class CeilingData
	{
		public string MaterialId;
	}


	[System.Serializable]
	public class LegacyRoomData
	{
		public LegacyFloorData floor = new LegacyFloorData();
		public LegacyWallData[] walls = new LegacyWallData[4];
		public MaterialJSON plinth = new MaterialJSON();
		//public LegacyPlinthData plinth = new LegacyPlinthData();
		public LegacyCeilingData ceiling = new LegacyCeilingData();

		public string name;
		public float length;
		public float width;
		public float height;
		public float estimate;
		public string image_normal;
		public string preview_icon;
		public string id;
	}

	[System.Serializable]
	public class LegacyFloorData
	{
		public MaterialJSON material;
	}

	[System.Serializable]
	public class LegacyWallData
	{
		public LegacyDoorData door;
		public string window_type;
		public MaterialJSON material;
	}

	[System.Serializable]
	public class LegacyDoorData
	{
		public string name;
		public string id;
	}

	[System.Serializable]
	public class LegacyPlinthData
	{
		public MaterialJSON material;
		public string id;
	}

	[System.Serializable]
	public class LegacyCeilingData
	{
		public MaterialJSON material;
		public string id;
	}

	[System.Serializable]
	public class DownloadedMaterialsWithHeader
	{
		public int total = 0;
		public int limit = 0;
		public int skip = 0;
		public List<MaterialJSON> data = new List<MaterialJSON>();
	}

	[System.Serializable]
	public class ShopsWithHeader
	{
		public int total;
		public int limit;
		public List<ShopJson> data = new List<ShopJson>();
	}

	[System.Serializable]
	public class ShopJson
	{
		public string _id;
		public string id;
		public ShopAddress address = new ShopAddress();
		public bool storeConfirmed;
		public bool emailConfirmed;
		public bool phoneConfirmed;
		public List<string> goods = new List<string>();
		public string zoneinfo;
		public string name;
		public string phone;
		public string email;
		public string website;
		public string description;
		public string owner;
		public string createdAt;
		public string updatedAt;
	}

	[System.Serializable]
	public class ShopAddress
	{
		public string formatted;
		public ShopLocation loc = new ShopLocation();
		public string postalCode;
		public string country;
		public string region;
		public string locality;
		public string streetAddress;
	}

	[System.Serializable]
	public class ShopLocation
	{
		public string type;
		public double[] coordinates = new double[2];

	}

	[System.Serializable]
	public class MetaData
	{

	}

	[System.Serializable]
	public class Custom_properties
	{
		public bool chamfer;
		public bool moisture_resistant;

		public int count_items_per_pack;
		public int class_num;

		public float[] width_list;
		public float total_thickness;
		public float zs_thickness;
		public float protective_layer_thickness;
		public float weight;
		public float length;
		public float board_length;
		public float board_width;
		public float board_thickness;
		public float height;

		public string collection;
		public string coating_type;
		public string lock_type;
		public string use;
		public string manufacturer_country;
		public string manufacturer_company;
		public string material;
		public string production_mode;
		public string packing_type;
		public string rapport;
		public string design_type;
		public string usage_class;
		public string basis;
		public string vendor_code;
		public string[] color;
		public string tinting_color;
		public float tinting_price;
		public string model;
	}

	[System.Serializable]
	public class Textures
	{
		public string tex_diffuse = "";
		public string tex_normal = "";
		public string tex_roughness = "";
	}

	[System.Serializable]
	public class Pack_dimensions
	{
		public float x;
		public float y;
	}

	[System.Serializable]
	public class Texture_dimensions
	{
		public float x = 0;
		public float y = 0;
		public float z = 0;
	}

	//[System.Serializable]
	public class AplliedTextures
	{
		public Texture2D tex_diffuse = null;
		public Texture2D tex_normal = null;
		public Texture2D tex_roughness = null;
		public Texture2D tex_preview_icon = null;
	}

	[System.Serializable]
	public class MaterialJSON
	{
		[JsonIgnore]
		public AplliedTextures applyedTextures = new AplliedTextures();
		public Sprite previewSprite = null;

		public Textures tex = new Textures();
		public Pack_dimensions pack_dimensions = new Pack_dimensions();
		public Texture_dimensions texture_dimensions = new Texture_dimensions();
		public Custom_properties custom_properties = new Custom_properties();
		public MetaData metadata = new MetaData();

		public bool isValid = true;

		public string _id;
		public float[] width_list;
		public string name;
		public string coating;
		public string color;
		public string type;
		public float cost;
		public string units;

		public string some_new_field;
		public string preview_icon = "";
		public float shininess_scale;
		public float normal_scale;
		public float roughness_scale;
		public float pack_area;

		public string createdAt;
		public string updatedAt;
		public float __v;
		public string category;
		public string id;

		public void MakeMaterialPreview()
		{
			Texture2D previewTexture = new Texture2D(200, 200);
			Color paintColor = new Color();
			string name = "preview_icon.png";
			if (type == "paint")
			{
				if (ColorUtility.TryParseHtmlString(custom_properties.tinting_color, out paintColor))
				{
					for (int y = 0; y < previewTexture.height; y++)
					{
						for (int x = 0; x < previewTexture.width; x++)
						{
							previewTexture.SetPixel(x, y, paintColor);
						}
					}

				}
			}
			else if (type == "ceiling")
			{
				if (ColorUtility.TryParseHtmlString(color, out paintColor))
				{
					for (int y = 0; y < previewTexture.height; y++)
					{
						for (int x = 0; x < previewTexture.width; x++)
						{
							previewTexture.SetPixel(x, y, paintColor);
						}
					}
				}
			}
			preview_icon = name;
			tex.tex_diffuse = name;
			applyedTextures.tex_preview_icon = previewTexture;
			applyedTextures.tex_diffuse = previewTexture;
		}

		public void ClearApplyedTextures()
		{
			applyedTextures.tex_diffuse = null;
			applyedTextures.tex_normal = null;
			applyedTextures.tex_roughness = null;
			applyedTextures.tex_preview_icon = null;
		}
	}


}
