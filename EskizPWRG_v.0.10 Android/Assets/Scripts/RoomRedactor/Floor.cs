using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;

public class Floor : MonoBehaviour
{
	public Material wallMaterial;
	public string Name;
	public string materialId = "";
	public MaterialJSON CurrentMaterialJson;
	//public Vector2[] RoomCorners;
	public float Length;
	public float Width;

	public float VanillaTextureScaleX = 0f;
	public float VanillaTextureScaleY = 0f;

	public float TextureScaleX = 0f;
	public float TextureScaleY = 0f;

	public float ManuallyTextureScaleX = 0f;
	public float ManuallyTextureScaleY = 0f;
	public float ManuallyEndedX = 0f;
	public float ManuallyEndedY = 0f;

	public void CreateFloor(Vector2 downLeftCoord, Vector2 upperRigthCoord, float height)
	{
		//RoomCorners = new Vector2[roomCorners.Length];
		//roomCorners.CopyTo(RoomCorners, 0);

		Vector2 lengthVector = new Vector2(downLeftCoord.x, upperRigthCoord.y);
		var heading = lengthVector - downLeftCoord;
		var distance = heading.magnitude;
		Length = distance;
		heading = upperRigthCoord - lengthVector;
		distance = heading.magnitude;
		Width = distance;
		Mesh mesh = new Mesh();
		//Vector2[] RoomCorners = { new Vector2(0f, 0f), new Vector2(0f, 5f), new Vector2(5f, 5f), new Vector2(5f, 0f) };

		Vector3[] vertices = new Vector3[4]
	   {
			new Vector3(downLeftCoord.x, height, downLeftCoord.y),
			new Vector3(downLeftCoord.x, height, upperRigthCoord.y),
			new Vector3(upperRigthCoord.x, height, upperRigthCoord.y),
			new Vector3(upperRigthCoord.x, height, downLeftCoord.y)
	   };
		mesh.vertices = vertices;

		int[] tris;
		if (height == 0)
		{
			tris = new int[6]
				{
			// lower left triangle
			0, 1, 2,
			// upper right triangle
			0, 2, 3
			};
		}
		else
		{
			tris = new int[6]
			{
				// lower left triangle
				2, 1, 0,
				// upper right triangle
				3, 2, 0
			};
		}

		mesh.triangles = tris;

		Vector3[] normals = new Vector3[4]
		{
			-Vector3.forward,
			-Vector3.forward,
			-Vector3.forward,
			-Vector3.forward
		};
		mesh.normals = normals;

		Vector2[] uv = new Vector2[4]
		{
			new Vector2(0, 0),
			new Vector2(1, 0),
			new Vector2(1, 1),
			new Vector2(0, 1)
		};
		mesh.uv = uv;

		gameObject.GetComponent<MeshFilter>().mesh = mesh;
		gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;

		wallMaterial = Resources.Load<Material>("Default/DefaultMaterial");
		UpdateMaterial(wallMaterial);
		//UpdateMaterial();
	}

	public void CreateFloor(Vector2[] roomCorners, float height)
	{
		//RoomCorners = new Vector2[roomCorners.Length];
		//roomCorners.CopyTo(RoomCorners, 0);
		var heading = roomCorners[1] - roomCorners[0];
		var distance = heading.magnitude;
		Length = distance;
		heading = roomCorners[2] - roomCorners[1];
		distance = heading.magnitude;
		Width = distance;
		Mesh mesh = new Mesh();

		Vector2[] RoomCorners = { new Vector2(0f, 0f), new Vector2(0f, 5f), new Vector2(5f, 5f), new Vector2(5f, 0f) };

		Vector3[] vertices = new Vector3[4]
	   {
			new Vector3(roomCorners[0].x, height, roomCorners[0].y),
			new Vector3(roomCorners[1].x, height, roomCorners[1].y),
			new Vector3(roomCorners[2].x, height, roomCorners[2].y),
			new Vector3(roomCorners[3].x, height, roomCorners[3].y)
	   };
		mesh.vertices = vertices;

		int[] tris;
		if (height == 0)
		{
			tris = new int[6]
				{
			// lower left triangle
			0, 1, 2,
			// upper right triangle
			0, 2, 3
			};
		}
		else
		{
			tris = new int[6]
			{
				// lower left triangle
				2, 1, 0,
				// upper right triangle
				3, 2, 0
			};
		}
		mesh.triangles = tris;

		Vector3[] normals = new Vector3[4]
		{
			-Vector3.forward,
			-Vector3.forward,
			-Vector3.forward,
			-Vector3.forward
		};
		mesh.normals = normals;

		Vector2[] uv = new Vector2[4]
		{
			new Vector2(0, 0),
			new Vector2(1, 0),
			new Vector2(1, 1),
			new Vector2(0, 1)
		};
		mesh.uv = uv;

		gameObject.GetComponent<MeshFilter>().mesh = mesh;
		gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;

		wallMaterial = Resources.Load<Material>("Default/DefaultMaterial");
		UpdateMaterial(wallMaterial);
		//UpdateMaterial();
	}

	public void UpdateMaterial(Material updateMaterial)
	{
		if (updateMaterial == null)
			return;
		wallMaterial = updateMaterial;
		materialId = updateMaterial.name;
		gameObject.GetComponent<MeshRenderer>().material = updateMaterial;
		MaterialJSON materialJson = GlobalApplicationManager.GetMaterialJsonById(materialId);
		CurrentMaterialJson = materialJson;
		UpdateTextureScale();

		RecalculateNormals();
	}

	void UpdateTextureScale()
	{
		float textureScaleX = 1f;
		float textureScaleY = 1f;
		if (CurrentMaterialJson != null)
		{
			if (CurrentMaterialJson.id != "DefaultFloor" && CurrentMaterialJson.id != "Default")
			{
				textureScaleX = CurrentMaterialJson.texture_dimensions.x;
				textureScaleY = CurrentMaterialJson.texture_dimensions.y;
			}	
		}
		Vector2 textureScale = new Vector2(Length * (1f / textureScaleX), Width * (1f / textureScaleY));
		gameObject.GetComponent<MeshRenderer>().material.SetTextureScale(Shader.PropertyToID("_MainTex"), textureScale);
		gameObject.GetComponent<MeshRenderer>().material.SetTextureScale(Shader.PropertyToID("_BumpMap"), textureScale);
		gameObject.GetComponent<MeshRenderer>().material.SetTextureScale(Shader.PropertyToID("_GlossMap"), textureScale);

		TextureScaleX = textureScale.x;
		TextureScaleY = textureScale.y;
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
}
