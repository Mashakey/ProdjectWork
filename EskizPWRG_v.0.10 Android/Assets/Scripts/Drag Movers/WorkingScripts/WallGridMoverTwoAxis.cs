using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WallGridMoverTwoAxis : MonoBehaviour
{
    Vector2Int objectSizeInCells = Vector2Int.zero;
    Vector3 objectSizeInUnits = Vector3.zero;
    Transform parentWall = null;
    bool isActive = false;

    public Vector2Int CalculateObjectSizeInCells()
    {
        Collider collider = GetComponent<Collider>();
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        //Vector3 minPoint = transform.TransformPoint(collider.bounds.min);
        //Vector3 maxPoint = transform.TransformPoint(collider.bounds.max);
        Vector3 minPoint = collider.bounds.min;
        Vector3 maxPoint = collider.bounds.max;
        //Debug.LogErrorFormat($"minPoint = {collider.bounds.min}  maxPoint = {collider.bounds.max}");
        objectSizeInUnits = maxPoint - minPoint;
        //objectSizeInUnits = new Vector3(boxCollider.size.x, boxCollider.size.y, boxCollider.size.z);
        float scaleFactor = GetComponent<Window>().Scale;
        objectSizeInUnits = new Vector3(boxCollider.size.x * scaleFactor, boxCollider.size.y * scaleFactor, boxCollider.size.z);
        //Debug.LogErrorFormat($"window size in units = {objectSizeInUnits}");
        //Debug.LogErrorFormat($"window box collider size in units = {boxCollider.size}");
        Debug.LogErrorFormat($"window scale = {scaleFactor}");
        WallGrid wallGrid = GetComponentInParent<WallGrid>();
        float objectSizeInCellsX = Mathf.Abs(objectSizeInUnits.x / wallGrid.GetCellSize().x);
        float objectSizeInCellsY = Mathf.Abs(objectSizeInUnits.y / wallGrid.GetCellSize().y);
        objectSizeInCells = new Vector2Int((int)Math.Ceiling(objectSizeInCellsX), (int)Math.Ceiling(objectSizeInCellsY));
        if (objectSizeInCells.x % 2 == 0)
            objectSizeInCells.x++;
        if (objectSizeInCells.y % 2 == 0)
            objectSizeInCells.y++;
        return (objectSizeInCells);
    }

    private Vector2 GetLocalMousePosition()
    {
        Wall wall = GetComponentInParent<Wall>();
        Mesh mesh = GetComponentInParent<MeshFilter>().mesh;
        Vector2 localMousePosition = Vector2.zero;
        Vector3 a = mesh.vertices[0];
        Vector3 b = mesh.vertices[1];
        Vector3 c = mesh.vertices[2];
        Plane plane = new Plane(a, b, c); //Making virtual plane parallel to wall
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            var hitPoint = ray.GetPoint(distance);
            Vector3 wallPosition = new Vector3(wall.StartCoord.x, 0f, wall.StartCoord.y);
            Vector3 position = wallPosition - hitPoint;
            localMousePosition = new Vector2(Mathf.Abs(position.x + position.z), Mathf.Abs(position.y));
            //Debug.LogError(localMousePosition);
        }
        return (localMousePosition);
    }

    private Vector3 GetGlobalMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask layerMask = LayerMask.GetMask("Wall");
        if (Physics.Raycast(ray, out hit, layerMask))
        {
            return (hit.point);
        }
        return (new Vector3(0f, 0f, 0f));
    }

    public Vector2 RoundPositionToCellSize(Vector2 position)
    {
        WallGrid grid = GetComponentInParent<WallGrid>();
        if (grid != null)
        {
            //Debug.LogError("WallGrid is not null");
        }
        else
        {
           // Debug.LogError("Wallgrid is null");
        }
        position.x = position.x / grid.GetCellSize().x;
        position.x = Mathf.Round(position.x);
        position.x = position.x * grid.GetCellSize().x;
        position.y = position.y / grid.GetCellSize().y;
        position.y = Mathf.Round(position.y);
        position.y = position.y * grid.GetCellSize().y;

        return (position);
    }

    public void AddObjectToGrid()
    {
        WallGrid wallGrid = transform.GetComponentInParent<WallGrid>();
        transform.SetParent(transform.parent.Find("Grid"));
        //transform.SetParent(wallGrid.transform);
        Vector2 celledCoordinates = RoundPositionToCellSize(transform.localPosition);
        Debug.LogWarning("###Grid mover window object local position = " + transform.localPosition);
        Debug.LogWarning("Celled coordinates = " + celledCoordinates);
        wallGrid.DeleteCollidingObjectAtCreatingNewOne(gameObject.transform, celledCoordinates, objectSizeInCells);
        wallGrid.MoveObject(gameObject.transform, celledCoordinates, objectSizeInCells);
    }

    public Vector3 GetWindowPositionOnWall()
	{
        Transform previousParent = transform.parent;
        transform.SetParent(transform.parent.Find("Grid"));
        Vector3 positionOnWall = transform.localPosition;
        transform.SetParent(previousParent);
        return (positionOnWall);
    }

    public bool IsWindowFit()
    {
        CalculateObjectSizeInCells();
        Debug.LogError("Object size in cells = " + objectSizeInCells);
        WallGrid wallGrid = transform.GetComponentInParent<WallGrid>();
        transform.SetParent(transform.parent.Find("Grid"));
        //transform.SetParent(wallGrid.transform);
        Vector2 celledCoordinates = RoundPositionToCellSize(transform.localPosition);
        bool isFit = wallGrid.CheckPositionAccesibility(transform, celledCoordinates, objectSizeInCells);
        transform.SetParent(wallGrid.transform);
        return (isFit);
    }

    void MoveObjectOnCells()
    {
        Vector2 localMousePosition = GetLocalMousePosition();
        Vector3 globalMousePosition = GetGlobalMousePosition();
        Debug.Log("Global mouse position = " + globalMousePosition);
        Debug.Log("Local mouse position = " + localMousePosition);
        Vector2 celledCoordinates = RoundPositionToCellSize(localMousePosition);
        //Debug.LogError("#################### CELLED COORDINATES = " + celledCoordinates);
        if (GetComponent<Window>().Type == DataTypes.WindowType.balcony_left_door || GetComponent<Window>().Type == DataTypes.WindowType.balcony_right_door)
		{
            celledCoordinates = new Vector2(celledCoordinates.x, 1f);
		}
        WallGrid wallGrid = transform.GetComponentInParent<WallGrid>();
        wallGrid.MoveObject(gameObject.transform, celledCoordinates, objectSizeInCells);
    }

    void Start()
    {
        parentWall = transform.parent;
        CalculateObjectSizeInCells();
        //Debug.LogError("OBJECT SIZE IN CELLS CALCULATED");
        AddObjectToGrid();
        GetComponent<WindowTouchScaler>().ScaleWindow(GetComponent<Window>().Scale);
        Debug.Log("Window pos wallgrid" + transform.position);
    }

    private void OnMouseDown()
    {
        if (isActive)
        {
            AddHistoryActionMove();
            Camera.main.GetComponent<CameraRotation>().FreezeCamera();
        }
    }

    private void OnMouseUp()
    {
        FindObjectOfType<CameraZoomer>().isActive = true;

        if (isActive)
        {
            Camera.main.GetComponent<CameraRotation>().UnfreezeCamera();
        }

    }

    private void OnMouseDrag()
    {

        if (isActive)
        {
            FindObjectOfType<CameraZoomer>().isActive = false;

            MoveObjectOnCells();
        }

    }

    public void AddHistoryActionMove()
    {
        HistoryAction historyAction = new HistoryAction();
        WallGrid wallGrid = transform.GetComponentInParent<WallGrid>();
        Debug.LogError("############Object name = " + wallGrid.name + "transform name = " + transform.name);
        //historyAction.CreateMoveObjectOnWallHistoryAction(wallGrid.MoveObject, transform, RoundPositionToCellSize(transform.localPosition), objectSizeInCells);
        historyAction.CreateMoveObjectOnWallHistoryAction(wallGrid.MoveObject, transform, RoundPositionToCellSize(GetWindowPositionOnWall()), objectSizeInCells);
        HistoryChangesStack historyChangesStack = FindObjectOfType<HistoryChangesStack>();
        historyChangesStack.AddHistoryAction(historyAction);
    }

    public void AddHistoryActionCreate()
    {
        HistoryAction historyAction = new HistoryAction();
        WallGrid wallGrid = transform.GetComponentInParent<WallGrid>();
        //GameObject cloneObject = Instantiate(gameObject, transform.parent);
        //cloneObject.SetActive(false);
        historyAction.CreateAddObjectOnWallHistoryAction(wallGrid.MoveObject, transform, RoundPositionToCellSize(transform.localPosition), objectSizeInCells);
        //historyAction.CreateAddObjectOnWallHistoryAction(wallGrid.MoveObject, transform, RoundPositionToCellSize(transform.localPosition), objectSizeInCells);
        HistoryChangesStack historyChangesStack = FindObjectOfType<HistoryChangesStack>();
        historyChangesStack.AddHistoryAction(historyAction);
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
