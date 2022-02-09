using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static DataTypes;
using Newtonsoft.Json;
using System;

public class Wall : MonoBehaviour
{
	public Material wallMaterial;
	public Material doorMaterial;
	public List<Window> Windows = new List<Window>();
	public List<Door> Doors = new List<Door>();
	public Baseboard Baseboard;
	//public BaseBoardData BaseBoard = null;
	//GameObject BaseBoard = null;
	GameObject Door = null;
	public string Name;
	public string materialId = "";
	public Vector2 StartCoord;
	public Vector2 EndCoord;
	public float Height = 5;
	public float Length;
	DateTime mouseDownTime;
	Vector3 mouseDownPosition;

	bool isLongClickValid;

	private void Awake()
	{
		if (GetComponent<WallGrid>() == null)
		{
			gameObject.AddComponent<WallGrid>();
			//Debug.LogError("Wallgrid is created");
		}
	}

	private void Start()
	{
		foreach (var window in Windows)
		{
			//window.GetComponent<WindowTouchScaler>().ScaleWindow(window.Scale);
		}
	}

	public void OnMouseDown()
	{
		if (FindObjectOfType<Room>().oppened3DButtons == null)
		{
			isLongClickValid = true;
		}
		else
		{
			isLongClickValid = false;
		}
		mouseDownTime = DateTime.Now;
		mouseDownPosition = Input.mousePosition;

	}

	public void OnMouseDrag()
	{
		var distance = (Input.mousePosition - mouseDownPosition).magnitude;
		if (distance > 100)
		{
			isLongClickValid = false;
		}
		float mouseDownDeltaTime = (float)(DateTime.Now - mouseDownTime).TotalMilliseconds;
		if (mouseDownDeltaTime > 1000 && isLongClickValid)
		{
			if (!CheckClickUI.IsClikedOnUI() && Screen.orientation == ScreenOrientation.Portrait)
			{
				isLongClickValid = false;
				ClickOnWall();
			}
		}
	}

	public void OnMouseUp()
	{
		var distance = (Input.mousePosition - mouseDownPosition).magnitude;
		float mouseDownDeltaTime = (float)(DateTime.Now - mouseDownTime).TotalMilliseconds;
		//if (mouseDownDeltaTime > 300 && distance < 100)
		//{

		//	if (!CheckClickUI.IsClikedOnUI() && Screen.orientation == ScreenOrientation.Portrait)
		//	{
		//		ClickOnWall();
		//	}

		//}
		if (!CheckClickUI.IsClikedOnUI())
		{
			Room room = FindObjectOfType<Room>();
			room.DeleteOppened3DButtons();
			NotePage notePage = FindObjectOfType<NotePage>();
			if (notePage)
			{
				notePage.CloseNoteWindow();
			}
		}
	}

	private bool CheckTachUI()
	{
		foreach (Touch touch in Input.touches)
		{
			int id = touch.fingerId;
			if (EventSystem.current.IsPointerOverGameObject(id))
			{
				// ui touched
				return true;
			}
		}
		return false;
	}

	public void ClickOnWall()
	{
		OnWallMaterialsHandler onWallMaterialsHandler = FindObjectOfType<OnWallMaterialsHandler>();
		if (onWallMaterialsHandler == null)
		{
			Debug.LogError("OnWallMaterialsHandlerIsNotFound");
			return;
		}
		else
		{
			ActiveWindowKeeper.IsRedactorActive = false;
			onWallMaterialsHandler.transform.GetComponent<Canvas>().enabled = true;
			onWallMaterialsHandler.transform.GetComponent<CanvasScaler>().enabled = true;
			ScrollbarBackButton scrollbarBackButton = FindObjectOfType<ScrollbarBackButton>();
			scrollbarBackButton.SetPrevousPageAsEditor();
			WallBackButton wallBackButton = FindObjectOfType<WallBackButton>();
			wallBackButton.SetPrevousPageAsEditor();
		}
		GlobalApplicationManager.AddSelectedWall(gameObject.transform);
		GlobalApplicationManager.AddSelectedObject(gameObject.transform);
	}

	public void CreateWall(Vector2 start, Vector2 end, float height)
	{
		StartCoord = start;
		EndCoord = end;
		Height = height;
		var heading = EndCoord - StartCoord;
		var distance = heading.magnitude;
		Length = distance;
		Mesh mesh = new Mesh();

		Vector3[] vertices = new Vector3[4]
	   {
			new Vector3(start.x, 0, start.y),
			new Vector3(end.x, 0, end.y),
			new Vector3(start.x, height, start.y),
			new Vector3(end.x, height, end.y)
	   };
		mesh.vertices = vertices;

		int[] tris = new int[6]
		{
			// lower left triangle
			0, 2, 1,
			// upper right triangle
			2, 3, 1
		};
		mesh.triangles = tris;

		//Vector3[] normals = new Vector3[4]
		//{
		//	-Vector3.forward,
		//	-Vector3.forward,
		//	-Vector3.forward,
		//	-Vector3.forward
		//};
		Vector3[] normals = new Vector3[4]
		{
			Vector3.forward,
			Vector3.forward,
			Vector3.forward,
			Vector3.forward
		};
		mesh.normals = normals;

		Vector2[] uv = new Vector2[4]
		{
			new Vector2(0, 0),
			new Vector2(1, 0),
			new Vector2(0, 1),
			new Vector2(1, 1)
		};
		mesh.uv = uv;

		gameObject.GetComponent<MeshFilter>().mesh = mesh;
		gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
		//wallMaterial = Resources.Load<Material>("Default/DefaultMaterial");
		UpdateMaterial(MaterialBuilder.GetMaterial("default"));
	}

	public void UpdateMaterial(Material updateMaterial)
	{
		wallMaterial = updateMaterial;
		materialId = updateMaterial.name;
		gameObject.GetComponent<MeshRenderer>().material = updateMaterial;
		UpdateTextureScale();

		RecalculateNormals();
	}

	void UpdateTextureScale()
	{
		Vector2 textureScale = new Vector2(1f * Length, 1f * Height);
		MaterialJSON wallMaterialJson = GlobalApplicationManager.GetMaterialJsonById(materialId);
		//Debug.LogWarning("UODATING WALL MATERIAL IS = '" + wallMaterialJson.name + "' '" + wallMaterialJson.id + "'");
		if (wallMaterialJson.id != "Default" && wallMaterialJson.id != "DefaultWall")
		{
			//Debug.LogWarningFormat($"texDimX = {wallMaterialJson.texture_dimensions.x} texDimY = {wallMaterialJson.texture_dimensions.y} height = {Height} lenght = {Length}");
			//textureScale = new Vector2(wallMaterialJson.texture_dimensions.x * Height, wallMaterialJson.texture_dimensions.y * Length);
			textureScale = new Vector2((1f / wallMaterialJson.texture_dimensions.x) * Length, (1f / wallMaterialJson.texture_dimensions.y) * Height);
		}
		Vector2 vanillaTextureScale = wallMaterial.mainTextureScale;
		//Vector2 textureScale = new Vector2(Length * vanillaTextureScale.x, Height * vanillaTextureScale.y);
		//textureScale = new Vector2(Height * vanillaTextureScale.x, Length * vanillaTextureScale.y);
		gameObject.GetComponent<MeshRenderer>().material.SetTextureScale(Shader.PropertyToID("_MainTex"), textureScale);
		gameObject.GetComponent<MeshRenderer>().material.SetTextureScale(Shader.PropertyToID("_BumpMap"), textureScale);
		gameObject.GetComponent<MeshRenderer>().material.SetTextureScale(Shader.PropertyToID("_GlossMap"), textureScale);
	}

	public void RecalculateNormals()
	{
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		if (meshFilter != null)
		{
			Mesh mesh = meshFilter.mesh;
			if (mesh != null)
			{
				mesh.RecalculateNormals();
				mesh.RecalculateTangents();
				Debug.Log("Normals are recalculated");
			}
		}
	}

	public Baseboard CreateBaseBoard(string materialId)
	{
		if (Baseboard != null)
		{
			Debug.LogError("DestroingBaseboard");
			Destroy(Baseboard.gameObject);
		}
		//float baseBoardOffset = 0.025f;
		float baseBoardOffset = 0.01f;
		float baseBoardHeight = 0.06f;

		var heading = new Vector3(EndCoord.x, 0, EndCoord.y) - new Vector3(StartCoord.x, 0, StartCoord.y);
		GameObject baseBoard = new GameObject("baseBord " + Name, typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider), typeof(Baseboard));
		//BaseBoardData baseBoardData = new BaseBoardData();
		//baseBoardData.MaterialId = oldMaterialId;
		//BaseBoard = baseBoardData;
		Mesh mesh = new Mesh();
		heading = heading.normalized;
		//Debug.LogError(heading.x + " " + heading.y + " " + heading.z);
		Vector3[] vertices = new Vector3[4]
	   {
			new Vector3(StartCoord.x + heading.z * baseBoardOffset - heading.x * baseBoardOffset, 0, StartCoord.y - heading.x * baseBoardOffset - heading.z * baseBoardOffset),
			new Vector3(EndCoord.x + heading.z * baseBoardOffset + heading.x * baseBoardOffset, 0, EndCoord.y - heading.x * baseBoardOffset + heading.z * baseBoardOffset),
			new Vector3(StartCoord.x, baseBoardHeight, StartCoord.y),
			new Vector3(EndCoord.x, baseBoardHeight, EndCoord.y)
	   };
		mesh.vertices = vertices;

		int[] tris = new int[6]
		{
			// lower left triangle
			0, 2, 1,
			// upper right triangle
			2, 3, 1
		};
		mesh.triangles = tris;

		Vector3[] normals = new Vector3[4]
		{
			-Vector3.forward,
			-Vector3.forward,
			-Vector3.forward,
			-Vector3.forward
		};

		//Vector3[] normals = new Vector3[4]
		//{
		//	-Vector3.forward,
		//	-Vector3.forward,
		//	-Vector3.forward,
		//	-Vector3.forward
		//};

		mesh.normals = normals;

		Vector2[] uv = new Vector2[4]
		{
			new Vector2(0, 0),
			new Vector2(1, 0),
			new Vector2(0, 1),
			new Vector2(1, 1)
		};
		mesh.uv = uv;

		baseBoard.GetComponent<MeshFilter>().mesh = mesh;
		baseBoard.transform.SetParent(gameObject.transform);
		baseBoard.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Default/DefaultMaterial");
		Material materialToAttach = MaterialBuilder.GetMaterial(materialId, "baseboard");
		materialToAttach.mainTextureScale = new Vector2(1f, 1f);
		baseBoard.GetComponent<MeshRenderer>().material = materialToAttach;

		Baseboard baseboardScript = baseBoard.GetComponent<Baseboard>();
		Baseboard = baseboardScript;
		baseboardScript.AttachedWall = this;
		return (baseboardScript);
	}

	public void CreateWindow(string windowType)
	{
		Vector3 wallDirection = EndCoord - StartCoord;
		float windowHeight = 0;
		if (windowType == "balcony_left_door" || windowType == "balcony_right_door")
		{
			windowHeight = 1.095f;
		}
		else
		{
			windowHeight = 1.7f;
		}
		Vector3 middleWallCoord = new Vector3((StartCoord.x + EndCoord.x) / 2, windowHeight, (StartCoord.y + EndCoord.y) / 2);
		if (Windows.Count > 1)
		{
			Vector3 newWindowCoords = new Vector3((StartCoord.x + EndCoord.x) / 3, windowHeight, (StartCoord.y + EndCoord.y) / 3);
			Window existedWindowScript = Windows[0].GetComponent<Window>();
			existedWindowScript.UpdatePosition(newWindowCoords);
			middleWallCoord.x *= 2f;
			middleWallCoord.y *= 2f;
		}
		//Debug.LogError(Vector3.Angle(wallDirection, Vector3.left));
		//float windowRotationAngle = Vector3.Angle(wallDirection, Vector3.up) + 90f;
		float windowRotationAngle = Vector3.SignedAngle(wallDirection, Vector3.up, Vector3.forward) + 90f;
		Debug.LogError(gameObject.name + "  angle = " + windowRotationAngle);
		//Debug.LogError(wallDirection + " " + Vector3.left + " " + Vector3.up + "   " + windowRotationAngle);
		PrefabContainer container = GameObject.Find("PrefabContainer").GetComponent<PrefabContainer>();
		GameObject window = null;
		WindowType type = 0;
		if (windowType == "tricuspid_window")
		{
			window = Instantiate(container.GetWindow(WindowType.tricuspid_window), middleWallCoord, Quaternion.AngleAxis(windowRotationAngle, Vector3.up), gameObject.transform);
			type = WindowType.tricuspid_window;
		}
		else if (windowType == "double_leaf_window")
		{
			window = Instantiate(container.GetWindow(WindowType.double_leaf_window), middleWallCoord, Quaternion.AngleAxis(windowRotationAngle, Vector3.up), gameObject.transform);
			type = WindowType.double_leaf_window;
		}
		else if (windowType == "balcony_right_door")
		{
			window = Instantiate(container.GetWindow(WindowType.balcony_right_door), middleWallCoord, Quaternion.AngleAxis(windowRotationAngle, Vector3.up), gameObject.transform);
			type = WindowType.balcony_right_door;
		}
		else if (windowType == "balcony_left_door")
		{
			window = Instantiate(container.GetWindow(WindowType.balcony_left_door), middleWallCoord, Quaternion.AngleAxis(windowRotationAngle, Vector3.up), gameObject.transform);
			type = WindowType.balcony_left_door;
		}
		window.AddComponent<Window>();
		Window windowScript = window.GetComponent<Window>();
		windowScript.Type = type;
		windowScript.Position = window.transform.position;
		windowScript.Rotation = window.transform.rotation;
		Windows.Add(windowScript);
	}

	public Transform CreateWindow(WindowType type, Vector3 position, Quaternion rotation, float scale = 1f)
	{
		//Debug.LogError("Room creator window position " + position);
		if (scale == 0f)
		{
			scale = 1f;
		}
		Vector3 wallDirection = EndCoord - StartCoord;
		Vector3 middleWallCoord = new Vector3((StartCoord.x + EndCoord.x) / 2, 1.7f, (StartCoord.y + EndCoord.y) / 2);

		PrefabContainer container = GameObject.Find("PrefabContainer").GetComponent<PrefabContainer>();
		GameObject window = Instantiate(container.GetWindow(type), position, rotation, gameObject.transform);
		//Debug.LogError("parent is " + transform.name);
		//Debug.LogError("windowObject is " + window.name);
		window.transform.SetParent(transform);
		window.AddComponent<Window>();
		Window windowScript = window.GetComponent<Window>();
		windowScript.Scale = scale;
		//window.AddComponent<WindowTouchScaler>();
		if (window.GetComponent<WallGridMoverTwoAxis>() == null)
		{
			window.AddComponent<WallGridMoverTwoAxis>();
		}
		windowScript.Type = type;
		windowScript.Position = window.transform.position;
		windowScript.Rotation = window.transform.rotation;
		windowScript.Scale = scale;
		//window.GetComponent<WindowTouchScaler>().ScaleWindow(scale);
		Windows.Add(windowScript);
		return (window.transform);
	}



	public Door CreateDoor(string materialId)
	{
		Vector3 wallDirection = EndCoord - StartCoord;
		Vector3 middleWallCoord = new Vector3((StartCoord.x + EndCoord.x) / 2, 0f, (StartCoord.y + EndCoord.y) / 2);
		if (Windows.Count > 1)
		{
			Vector3 newDoorCoords = new Vector3((StartCoord.x + EndCoord.x) / 3, 1.7f, (StartCoord.y + EndCoord.y) / 3);
			Door oldDoorScript = Doors[0].GetComponent<Door>();
			oldDoorScript.UpdatePosition(newDoorCoords);
			middleWallCoord.x *= 2f;
			middleWallCoord.y *= 2f;
		}
		//Debug.LogError(Vector3.Angle(wallDirection, Vector3.left));
		float doorRotationAngle = Vector3.SignedAngle(wallDirection, Vector3.up, Vector3.forward) + 90f;
		Quaternion doorRotation = Quaternion.AngleAxis(doorRotationAngle, Vector3.up);
		//float doorRotationAngle = Vector3.Angle(wallDirection, Vector3.left);

		PrefabContainer container = GameObject.Find("PrefabContainer").GetComponent<PrefabContainer>();
		//GameObject door = Instantiate(container.door, middleWallCoord, Quaternion.AngleAxis(doorRotationAngle, Vector3.up), gameObject.transform);
		GameObject door = Instantiate(container.door, middleWallCoord, doorRotation, gameObject.transform);
		door.AddComponent<Door>();
		Door doorScript = door.GetComponent<Door>();
		Door = door;
		Transform doorModel = door.transform.Find("DoorSurface");
		Material materialToAttach = MaterialBuilder.GetMaterial(materialId);
		doorMaterial = materialToAttach;
		materialToAttach.mainTextureScale = new Vector2(1f, 1f);
		doorModel.GetComponent<MeshRenderer>().material = materialToAttach;
		doorScript.Position = door.transform.position;
		doorScript.Rotation = door.transform.rotation;
		doorScript.MaterialId = materialToAttach.name;
		Doors.Add(doorScript);
		return (doorScript);
	}

	public Door CreateDoor(string materialId, Vector3 position, Quaternion rotation)
	{
		//Debug.LogError("Room creator door position " + position);
		Vector3 wallDirection = EndCoord - StartCoord;
		Vector3 middleWallCoord = new Vector3((StartCoord.x + EndCoord.x) / 2, 0f, (StartCoord.y + EndCoord.y) / 2);
		//Debug.LogError(Vector3.Angle(wallDirection, Vector3.left));
		float doorRotationAngle = Vector3.Angle(wallDirection, Vector3.left);
		PrefabContainer container = GameObject.Find("PrefabContainer").GetComponent<PrefabContainer>();
		DoorData doorData = new DoorData();
		GameObject door = Instantiate(container.door, position, rotation, gameObject.transform);
		door.AddComponent<Door>();
		Door doorScript = door.GetComponent<Door>();
		Door = door;
		Transform doorModel = door.transform.Find("DoorSurface");
		Material materialToAttach = MaterialBuilder.GetMaterial(materialId);
		doorMaterial = materialToAttach;
		materialToAttach.mainTextureScale = new Vector2(1f, 1f);
		doorModel.GetComponent<MeshRenderer>().material = materialToAttach;
		doorScript.Position = door.transform.position;
		doorScript.Rotation = door.transform.rotation;
		doorScript.MaterialId = materialToAttach.name;
		Doors.Add(doorScript);
		return (doorScript);
	}
}
