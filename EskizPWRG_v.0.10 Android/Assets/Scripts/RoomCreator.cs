using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using static DataTypes;

public class RoomCreator : MonoBehaviour
{
	[SerializeField]
	ManagerType RoomTypeScript;
	[SerializeField]
	LoadingScreen loadingScreen;

	public MaterialBuilder materialBuilder;
	private Vector2[] roomCorners = { new Vector2(0f, 0f), new Vector2(0f, 3f), new Vector2(-3f, 3f), new Vector2(-3f, 6f), new Vector2(3f, 6f), new Vector2(3f, 0f) };
	static GameObject Room = null;

	public void ClearHistoryChanges()
	{
		HistoryChangesStack historyChangesStack = FindObjectOfType<HistoryChangesStack>();
		historyChangesStack.ClearHistory();
	}

	public void CreateRoom(RoomData roomData)
	{
		Debug.LogWarning("Starting to create room");
		RoomDischarger.CreateEmptyRoom(roomData);
		ClearHistoryChanges();
		GlobalApplicationManager.ClearSelectedObjects();

		Coroutine updateMaterialCoroutine;

		DestroyRoom();
		RoomTypeScript.typeMyRoom = GlobalApplicationManager.GetStringRoomTypeFromEnum(roomData.Type);
		//Camera.main.transform.position = roomData.CameraPosition;
		FindObjectOfType<MiddleRoomPoint>().SetMiddlePointAndDollyTrackPosition(roomData.CameraPosition);
		GameObject room = new GameObject(roomData.Name, typeof(Room));
		Room = room;
		Room roomScript = room.GetComponent<Room>();
		roomScript.Cost = roomData.Cost;
		roomScript.Interior = roomData.Interior;
		roomScript.Type = roomData.Type;
		roomScript.Height = roomData.Height;

		roomScript.Name = roomData.Name;
		for (int i = 0; i < roomData.RoomCorners.Count; i++)
		{
			roomScript.RoomCorners.Add(roomData.RoomCorners[i]);
		}
		for (int i = 0; i < roomData.RoomCorners.Count; i++)
		{
			int j = i + 1;
			if (j >= roomData.RoomCorners.Count)
				j = 0;
			Debug.LogWarning("Creating wall");

			string name = "Wall" + i;
			GameObject wall = new GameObject(name, typeof(Wall), typeof(TextureUpdater), typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider));
			wall.transform.SetParent(room.transform);
			Wall wallScript = wall.GetComponent<Wall>();
			wallScript.Name = name;
			wallScript.CreateWall(roomData.RoomCorners[i], roomData.RoomCorners[j], roomData.Height);
			foreach (WindowData window in roomData.Walls[i].Windows)
			{
				wallScript.CreateWindow(window.Type, window.Position, window.Rotation, window.Scale);
			}
			foreach (DoorData door in roomData.Walls[i].Doors)
			{
				MaterialJSON doorMaterialJson = GlobalApplicationManager.GetMaterialJsonById(door.MaterialId);
				if (doorMaterialJson == null)
				{
					doorMaterialJson = new MaterialJSON();
					doorMaterialJson.name = "DefaultDoor";
					doorMaterialJson.id = "DefaultDoor";

				}
				Door createdDoor = wallScript.CreateDoor(door.MaterialId, door.Position, door.Rotation);
				if (doorMaterialJson != null)
				{
					//StartCoroutine(materialBuilder.UpdateMeshMaterial(doorMaterialJson, createdDoor.UpdateMaterial));
					updateMaterialCoroutine = StartCoroutine(materialBuilder.UpdateMeshMaterial(doorMaterialJson, createdDoor.UpdateMaterial));
					StartCoroutine(ResetChangesHistorAfterEndingUpdate(updateMaterialCoroutine));

				}
			}
			//Debug.LogWarning("We are in room creator");
			//Debug.LogWarning("wall material id is '" + roomData.Walls[i].MaterialId + "'");
			//wallScript.UpdateMaterial(MaterialBuilder.GetMaterial(roomData.Walls[i].MaterialId));

			//MaterialJSON materialJson = DataCacher.GetMaterialById(roomData.Walls[i].MaterialId);

			if (roomData.Walls[i].MaterialId.StartsWith("UserPhoto"))
			{
				Debug.LogWarningFormat($"Wall texture is user photo. Name is '{roomData.Walls[i].MaterialId}'");
				updateMaterialCoroutine = StartCoroutine(materialBuilder.UpdateMeshMaterialWithUserImage(roomData.Walls[i].MaterialId, wallScript.UpdateMaterial));
				StartCoroutine(ResetChangesHistorAfterEndingUpdate(updateMaterialCoroutine));
			}
			else
			{
				MaterialJSON materialJson = GlobalApplicationManager.GetMaterialJsonById(roomData.Walls[i].MaterialId);
				if (materialJson == null)
				{
					materialJson = new MaterialJSON();
					materialJson.name = "Default";
					materialJson.id = "DefaultWall";
				}
				//StartCoroutine(materialBuilder.UpdateMeshMaterial(materialJson, wallScript.UpdateMaterial));
				updateMaterialCoroutine = StartCoroutine(materialBuilder.UpdateMeshMaterial(materialJson, wallScript.UpdateMaterial));
				StartCoroutine(ResetChangesHistorAfterEndingUpdate(updateMaterialCoroutine));
			}

			if (roomData.Baseboard != null)
			{
				Baseboard createdBaseboard = wallScript.CreateBaseBoard(roomData.Baseboard.MaterialId);
				roomScript.baseBoardMaterialId = roomData.Baseboard.MaterialId;
				createdBaseboard.AttachedWall = wallScript;
				MaterialJSON baseboardMaterialJson = GlobalApplicationManager.GetMaterialJsonById(roomData.Baseboard.MaterialId);
				updateMaterialCoroutine = StartCoroutine(materialBuilder.UpdateMeshMaterial(baseboardMaterialJson, createdBaseboard.UpdateMaterial));
				StartCoroutine(ResetChangesHistorAfterEndingUpdate(updateMaterialCoroutine));
				//StartCoroutine(materialBuilder.UpdateMeshMaterial(baseboardMaterialJson, createdBaseboard.UpdateMaterial));
				wallScript.Baseboard = createdBaseboard;

			}
			roomScript.AddWall(wallScript);
		}
		Debug.LogWarning("Creating floor");

		GameObject floor = new GameObject("floor", typeof(Floor), typeof(ClickOnSurfaceHandler), typeof(TextureUpdater), typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider));
		floor.transform.SetParent(room.transform);
		Floor floorScript = floor.GetComponent<Floor>();
		floorScript.CreateFloor(FindDownLeftCoord(roomData.RoomCorners.ToArray()), FindUpperRigthCoord(roomData.RoomCorners.ToArray()), 0);
		//if (roomData.Floor != null)
		//{
		MaterialJSON floorMaterialJson = null;
		if (roomData.Floor != null)
		{
			//Debug.LogError("####Roomdata floor " + roomData.Floor.MaterialId);

			floorMaterialJson = GlobalApplicationManager.GetMaterialJsonById(roomData.Floor.MaterialId);
		}
		if (floorMaterialJson == null)
		{
			floorMaterialJson = new MaterialJSON();
			floorMaterialJson.name = "Default";
			floorMaterialJson.id = "DefaultFloor";
		}


		//StartCoroutine(materialBuilder.UpdateMeshMaterial(floorMaterialJson, floorScript.GetComponent<TextureUpdater>().UpdateTexture));
		updateMaterialCoroutine = StartCoroutine(materialBuilder.UpdateMeshMaterial(floorMaterialJson, floorScript.GetComponent<TextureUpdater>().UpdateTexture));

		StartCoroutine(ResetChangesHistorAfterEndingUpdate(updateMaterialCoroutine));


		//floorScript.UpdateMaterial(MaterialBuilder.GetMaterial(roomData.Floor.MaterialId));
		//}
		//else
		//{
		//	Debug.LogError("FLOOR IS NULL");
		//}
		roomScript.floorMaterialId = floorScript.materialId;


		GameObject ceiling = new GameObject("ceiling", typeof(Floor), typeof(ClickOnSurfaceHandler), typeof(TextureUpdater), typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider));
		ceiling.transform.SetParent(room.transform);
		Floor ceilingScript = ceiling.GetComponent<Floor>();
		ceilingScript.CreateFloor(FindDownLeftCoord(roomData.RoomCorners.ToArray()), FindUpperRigthCoord(roomData.RoomCorners.ToArray()), roomData.Height);
		if (roomData.Ceiling != null)
		{
			MaterialJSON ceilingMaterialJson = GlobalApplicationManager.GetMaterialJsonById(roomData.Ceiling.MaterialId);
			if (ceilingMaterialJson == null)
			{
				ceilingMaterialJson = new MaterialJSON();
				ceilingMaterialJson.name = "Default";
				ceilingMaterialJson.id = "Default";
			}
			//StartCoroutine(materialBuilder.UpdateMeshMaterial(ceilingMaterialJson, ceilingScript.UpdateMaterial));
			updateMaterialCoroutine = StartCoroutine(materialBuilder.UpdateMeshMaterial(ceilingMaterialJson, ceilingScript.GetComponent<TextureUpdater>().UpdateTexture));
			StartCoroutine(ResetChangesHistorAfterEndingUpdate(updateMaterialCoroutine));

			//ceilingScript.UpdateMaterial(MaterialBuilder.GetMaterial(materialJson));
		}
		roomScript.ceilingMaterialId = ceilingScript.materialId;

		foreach (NoteData noteData in roomData.Notes)
		{
			PrefabContainer prefabContainer = FindObjectOfType<PrefabContainer>();
			Note note = Instantiate(prefabContainer.Note, noteData.position, Quaternion.identity).GetComponent<Note>();
			roomScript.AddNote(note);
			note.contentText = noteData.contentText;
			note.transform.SetParent(roomScript.transform);
			Debug.LogWarning("Note is created");
		}

		foreach (FurnitureObjectData furnitureData in roomData.Furniture)
		{
			PrefabContainer prefabContainer = FindObjectOfType<PrefabContainer>();
			GameObject furniture = prefabContainer.GetFurnitureObjectByName(furnitureData.Name);
			furniture = Instantiate(furniture, furnitureData.Position, furnitureData.Rotation, room.transform);
			furniture.name = furnitureData.Name;

			//FurnitureObject furnitureObject = Instantiate(furniture, furnitureData.Position, furnitureData.Rotation, room.transform).GetComponent<FurnitureObject>();
			
			roomScript.Furniture.Add(furniture.GetComponent<FurnitureObject>());
			//roomScript.Furniture.Add(furnitureObject);
			Debug.LogWarning("Furniture object is created");

		}

		//StartCoroutine(WaitForSecondsAndResetChangesHistory());
		//PrintDataToFile(roomScript);

	}


	public void CreateRoomFromLegacy(LegacyRoomData legacyRoomData)
	{
		ClearHistoryChanges();

		DestroyRoom();
		GlobalApplicationManager.ClearSelectedObjects();

		Coroutine updateMaterialCoroutine;

		//PrintDataToFile(JsonConvert.SerializeObject(legacyRoomData));
		float length = legacyRoomData.length;
		float width = legacyRoomData.width;
		float height = legacyRoomData.height;
		string baseBoardMaterialId = "";
		
		Vector2[] roomCorners = { new Vector2(0f, 0f), new Vector2(0f, width), new Vector2(length, width), new Vector2(length, 0f) };
		//Debug.LogError(length);
		GameObject room = new GameObject(legacyRoomData.name, typeof(Room));
		Room = room;
		Room roomScript = room.GetComponent<Room>();
		//roomScript.Name = legacyRoomData.name;
		roomScript.Name = DataCacher.GetAvailableRoomName(legacyRoomData.name);
		Room.name = roomScript.Name;
		roomScript.Height = legacyRoomData.height;
		roomScript.Type = RoomType.Rectangle;
		for (int i = 0; i < roomCorners.Length; i++)
		{
			roomScript.RoomCorners.Add(roomCorners[i]);
		}
		if (legacyRoomData.plinth.id != null && legacyRoomData.plinth.id != "")
		{
			//Debug.LogError(GlobalApplicationManager.GetMaterialJsonById)
			baseBoardMaterialId = legacyRoomData.plinth.id;
			roomScript.baseBoardMaterialId = baseBoardMaterialId;
		}
		for (int i = 0; i < roomCorners.Length; i++)
		{
			int j = i + 1;
			if (j >= roomCorners.Length)
				j = 0;

			string name = "Wall" + i;
			GameObject wall = new GameObject(name, typeof(Wall), typeof(TextureUpdater), typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider));
			wall.transform.SetParent(room.transform);
			Wall wallScript = wall.GetComponent<Wall>();
			wallScript.Name = name;
			wallScript.CreateWall(roomCorners[i], roomCorners[j], height);


			if (baseBoardMaterialId != "")
			{

				Baseboard createdBaseboard = wallScript.CreateBaseBoard(baseBoardMaterialId);
				createdBaseboard.AttachedWall = wallScript;

				//BaseBoardData baseBoardData = new BaseBoardData();
				createdBaseboard.MaterialId = legacyRoomData.plinth.id;
				MaterialJSON baseboardMaterialJson = GlobalApplicationManager.GetMaterialJsonById(legacyRoomData.plinth.id);

				updateMaterialCoroutine = StartCoroutine(materialBuilder.UpdateMeshMaterial(baseboardMaterialJson, createdBaseboard.UpdateMaterial));
				StartCoroutine(ResetChangesHistorAfterEndingUpdate(updateMaterialCoroutine));

				//StartCoroutine(materialBuilder.UpdateMeshMaterial(baseboardMaterialJson, createdBaseboard.UpdateMaterial));
				wallScript.Baseboard = createdBaseboard;



			}
			if (legacyRoomData.walls[i].window_type != "none")
			{
				wallScript.CreateWindow(legacyRoomData.walls[i].window_type);
			}
			if (legacyRoomData.walls[i].door != null)
			{
				if (legacyRoomData.walls[i].door.name != "none")
				{
					//wallScript.CreateDoor(legacyRoomData.walls[i].door.id);
					Debug.LogWarning("We are in legacyRoomCreator. door material is '" + legacyRoomData.walls[i].door.id + "'");
					Door createdDoor = wallScript.CreateDoor(legacyRoomData.walls[i].door.id);
					MaterialJSON doorMaterialJson = GlobalApplicationManager.GetMaterialJsonById(legacyRoomData.walls[i].door.id);


					updateMaterialCoroutine = StartCoroutine(materialBuilder.UpdateMeshMaterial(doorMaterialJson, createdDoor.UpdateMaterial));
					StartCoroutine(ResetChangesHistorAfterEndingUpdate(updateMaterialCoroutine));
					//StartCoroutine(materialBuilder.UpdateMeshMaterial(doorMaterialJson, createdDoor.UpdateMaterial));
				}
			}
			Debug.LogError(legacyRoomData.walls[i].material.id);
			if (legacyRoomData.walls[i].material != null)
			{
				updateMaterialCoroutine = StartCoroutine(materialBuilder.UpdateMeshMaterial(legacyRoomData.walls[i].material, wallScript.UpdateMaterial));
				StartCoroutine(ResetChangesHistorAfterEndingUpdate(updateMaterialCoroutine));
				//StartCoroutine(materialBuilder.UpdateMeshMaterial(legacyRoomData.walls[i].material, wallScript.UpdateMaterial));
			}
			roomScript.AddWall(wallScript);
		}
		GameObject floor = new GameObject("floor", typeof(Floor), typeof(ClickOnSurfaceHandler), typeof(TextureUpdater), typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider));
		floor.transform.SetParent(room.transform);
		Floor floorScript = floor.GetComponent<Floor>();
		floorScript.CreateFloor(roomCorners, 0);
		if (legacyRoomData.floor != null)
		{
			updateMaterialCoroutine = StartCoroutine(materialBuilder.UpdateMeshMaterial(legacyRoomData.floor.material, floorScript.GetComponent<TextureUpdater>().UpdateTexture));
			StartCoroutine(ResetChangesHistorAfterEndingUpdate(updateMaterialCoroutine));
			//StartCoroutine(materialBuilder.UpdateMeshMaterial(legacyRoomData.floor.material, floorScript.GetComponent<TextureUpdater>().UpdateTexture));
		}
		roomScript.floorMaterialId = floorScript.materialId;

		GameObject ceiling = new GameObject("ceiling", typeof(Floor), typeof(ClickOnSurfaceHandler), typeof(TextureUpdater), typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider));
		ceiling.transform.SetParent(room.transform);
		Floor ceilingScript = ceiling.GetComponent<Floor>();
		ceilingScript.CreateFloor(roomCorners, height);
		if (legacyRoomData.ceiling != null)
		{
			updateMaterialCoroutine = StartCoroutine(materialBuilder.UpdateMeshMaterial(legacyRoomData.ceiling.material, ceilingScript.UpdateMaterial));
			StartCoroutine(ResetChangesHistorAfterEndingUpdate(updateMaterialCoroutine));
			//StartCoroutine(materialBuilder.UpdateMeshMaterial(legacyRoomData.ceiling.material, ceilingScript.UpdateMaterial));

		}
		roomScript.ceilingMaterialId = ceilingScript.materialId;
		Vector3 middleRoomPosition = new Vector3(length / 2, height / 2, width / 2);
		//Camera.main.transform.position = middleRoomPosition;
		FindObjectOfType<MiddleRoomPoint>().SetMiddlePointAndDollyTrackPosition(middleRoomPosition);

		//RoomCostCalculator.CalculateRoomCost(Room.GetComponent<Room>());

	}

	public void DestroyRoom()
	{
		if (Room != null)
			Destroy(Room);
		Room = null;
		Debug.LogWarning("Room destroed");

	}

	Vector2 FindDownLeftCoord(Vector2[] roomCorners)
	{
		float x = 0f;
		float y = 0f;
		for (int i = 0; i < roomCorners.Length; i++)
		{
			if (roomCorners[i].x < x)
				x = roomCorners[i].x;
			if (roomCorners[i].y < y)
				y = roomCorners[i].y;
		}
		return (new Vector2(x, y));
	}

	Vector2 FindUpperRigthCoord(Vector2[] roomCorners)
	{
		float x = 0f;
		float y = 0f;
		for (int i = 0; i < roomCorners.Length; i++)
		{
			if (roomCorners[i].x > x)
				x = roomCorners[i].x;
			if (roomCorners[i].y > y)
				y = roomCorners[i].y;
		}
		return (new Vector2(x, y));
	}

	public void PrintDataToFile(string text)
	{
		using (StreamWriter sw = new StreamWriter("D:\\test\\logRoom.txt", true, System.Text.Encoding.Default))
		{
			sw.WriteLine(text);
			sw.WriteLine();
		}
	}

	public string ConvertRoomToJSON(Room room)
	{
		RoomData roomData = new RoomData();
		roomData.CameraPosition = FindObjectOfType<MiddleRoomPoint>().transform.position;
		//roomData.CameraPosition = Camera.main.transform.position;
		roomData.Name = room.Name;
		roomData.Cost = room.Cost;
		roomData.Interior = room.Interior;
		roomData.Type = room.Type;
		roomData.Height = room.Height;

		roomData.RoomCorners = room.RoomCorners;
		foreach(Wall wall in room.Walls)
		{
			WallData wallData = new WallData(wall.StartCoord, wall.EndCoord);
			wallData.MaterialId = wall.materialId;
			foreach (Window window in wall.Windows)
			{
				WindowData windowData = new WindowData();
				windowData.Position = window.Position;
				windowData.Rotation = window.Rotation;
				windowData.Type = window.Type;
				windowData.Scale = window.Scale;
				wallData.Windows.Add(windowData);
			}
			foreach (Door door in wall.Doors)
			{
				DoorData doorData = new DoorData();
				doorData.Position = door.Position;
				doorData.Rotation = door.Rotation;
				doorData.MaterialId = door.MaterialId;
				wallData.Doors.Add(doorData);
			}
			if (wall.Baseboard != null)
			{
				//Baseboard baseboard = new Baseboard();
				BaseBoardData baseboard = new BaseBoardData();
				baseboard.MaterialId = wall.Baseboard.MaterialId;
				roomData.Baseboard = baseboard;
			}
			roomData.Walls.Add(wallData);
		}
		FloorData floor = new FloorData();
		floor.MaterialId = room.floorMaterialId;
		Debug.LogWarning("We are in room converter to json. Floor material is '" + floor.MaterialId + "'");
		roomData.Floor = floor;
		CeilingData ceiling = new CeilingData();
		ceiling.MaterialId = room.ceilingMaterialId;
		roomData.Ceiling = ceiling;
		foreach(Note note in room.Notes)
		{
			NoteData noteData = new NoteData();
			noteData.position = note.transform.position;
			noteData.contentText = note.contentText;
			roomData.Notes.Add(noteData);
		}
		foreach(FurnitureObject furniture in room.Furniture)
		{
			FurnitureObjectData furnitureData = new FurnitureObjectData();
			furnitureData.Name = furniture.gameObject.name;
			furnitureData.Position = furniture.transform.position;
			furnitureData.Rotation = furniture.transform.rotation;
			roomData.Furniture.Add(furnitureData);
		}

		var settings = new Newtonsoft.Json.JsonSerializerSettings();
		settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
		return (JsonConvert.SerializeObject(roomData, settings));
	}

	public IEnumerator ResetChangesHistorAfterEndingUpdate(Coroutine coroutineWaitFor)
	{
		yield return coroutineWaitFor;
		HistoryChangesStack historyChangesStack = FindObjectOfType<HistoryChangesStack>();
		historyChangesStack.ClearHistory();
		yield break;
	}
}
