using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

using static DataTypes;

public class Room : MonoBehaviour
{
	public List<Vector2> RoomCorners = new List<Vector2>();
	public List<Wall> Walls = new List<Wall>();
	public List<Note> Notes = new List<Note>();
	public List<FurnitureObject> Furniture = new List<FurnitureObject>();
	public float Height;
	public string floorMaterialId = "";
	public string ceilingMaterialId = "";
	public string baseBoardMaterialId = "";
	public string Name = "Без имени";
	public float Area = 0;
	public float Cost = 0;
	public RoomType Type;
	public InteriorType Interior;

	public GameObject oppened3DButtons = null;

	public float CalculateArea()
	{
		if (Type == RoomType.Rectangle)
		{
			Area = Walls[0].Length * Walls[1].Length;
			return (Area);
		}
		else if (Type == RoomType.G_type)
		{
			Area = (Walls[0].Length * Walls[1].Length) - (Walls[3].Length * Walls[4].Length);
			return (Area);
		}
		else if (Type == RoomType.T_type)
		{
			Area = (Walls[0].Length * Walls[1].Length) +(Walls[4].Length * Walls[5].Length);
			return (Area);
		}
		else if (Type == RoomType.Trapezoidal)
		{
			Area = (Walls[0].Length * Walls[1].Length) + (Walls[4].Length - Walls[1].Length) * ((Walls[0].Length + Walls[3].Length) / 2);
			return (Area);
		}
		else if (Type == RoomType.Z_type)
		{
			Area = ((Walls[0].Length + Walls[2].Length) * (Walls[1].Length + Walls[3].Length)) - (Walls[1].Length * Walls[2].Length + Walls[5].Length * Walls[6].Length);
			return (Area);
		}
		else
			return 0;
	}

	public void calculateCost()
	{
		RoomCostCalculator.CalculateRoomCost(this);
	}

	public void AddWall(Wall wall)
	{
		Walls.Add(wall);
	}

	public void CreateBaseboard(MaterialJSON baseboardMaterialJson)
	{
		MaterialBuilder materialBuilder = Transform.FindObjectOfType<MaterialBuilder>();
		baseBoardMaterialId = baseboardMaterialJson.id;
		foreach (var wall in Walls)
		{
			Baseboard baseboard = wall.CreateBaseBoard(baseboardMaterialJson.id);
			wall.Baseboard = baseboard;
			materialBuilder.StartCoroutine(materialBuilder.UpdateMeshMaterial(baseboardMaterialJson, baseboard.UpdateMaterial));
		}
	}

	public void DeleteBaseboard()
	{
		foreach (var wall in Walls)
		{
			Destroy(wall.Baseboard.gameObject);
			wall.Baseboard = null;
		}
		baseBoardMaterialId = "";
	}

	public void DeleteOppened3DButtons()
	{
		Camera.main.GetComponent<CameraRotation>().UnfreezeCamera();
		if (oppened3DButtons != null)
		{
			if (oppened3DButtons.GetComponent<Buttons3DWindow>() != null)
			{
				oppened3DButtons.GetComponent<Buttons3DWindow>().TurnOffMoveButton();
				oppened3DButtons.GetComponent<Buttons3DWindow>().TurnOffScaleButton();
			}
			else if (oppened3DButtons.GetComponent<Buttons3DDoor>() != null)
			{
				oppened3DButtons.GetComponent<Buttons3DDoor>().TurnOffMoveButton();
			}
			else if (oppened3DButtons.GetComponent<Buttons3DFurniture>() != null)
			{
				oppened3DButtons.GetComponent<Buttons3DFurniture>().TurnOffMoveButton();
				oppened3DButtons.GetComponent<Buttons3DFurniture>().TurnOffRotateButton();
			}
			Destroy(oppened3DButtons);
		}
		oppened3DButtons = null;
	}

	public void AddNote(Note note)
	{
		if (!Notes.Contains(note))
		{
			Notes.Add(note);
		}
	}

	public void DeleteNote(Note note)
	{
		if (Notes.Contains(note))
		{
			Destroy(note.gameObject);
			Notes.Remove(note);
		}
	}
}
