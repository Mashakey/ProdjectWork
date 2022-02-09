using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGridMoverOneAxis : MonoBehaviour
{
    Vector2Int objectSizeInCells = Vector2Int.zero;
    Vector3 objectSizeInUnits = Vector3.zero;
    Transform parentWall = null;
    bool isActive = false;

    void CalculateObjectSizeInCells()
    {
        Collider collider = GetComponent<Collider>();
        Vector3 minPoint = transform.TransformPoint(collider.bounds.min);
        Vector3 maxPoint = transform.TransformPoint(collider.bounds.max);

        objectSizeInUnits = maxPoint - minPoint;
        Debug.LogWarning("Door size in units = " + objectSizeInUnits);
        WallGrid wallGrid = GetComponentInParent<WallGrid>();
        float objectSizeInCellsX = Mathf.Abs(objectSizeInUnits.x / wallGrid.GetCellSize().x);
        float objectSizeInCellsY = Mathf.Abs(objectSizeInUnits.y / wallGrid.GetCellSize().y);
        objectSizeInCells = new Vector2Int((int)Math.Ceiling(objectSizeInCellsX), (int)Math.Ceiling(objectSizeInCellsY));
        if (objectSizeInCells.x % 2 == 0)
            objectSizeInCells.x++;
        //if (objectSizeInCells.y % 2 == 0)
        //    objectSizeInCells.y++;
        objectSizeInCells.y = wallGrid.WallGridArray.GetLength(1);
        //objectSizeInCells.y--;
        //objectSizeInCells.y = 2;
        Debug.LogWarning("Door size in cells = " + objectSizeInCells);

    }

    public Vector3 GetObjectPositionOnWall()
    {
        Transform previousParent = transform.parent;
        transform.SetParent(transform.parent.Find("Grid"));
        Vector3 positionOnWall = transform.localPosition;
        transform.SetParent(previousParent);
        return (positionOnWall);
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
        position.x = position.x / grid.GetCellSize().x;
        position.x = Mathf.Round(position.x);
        position.x = position.x * grid.GetCellSize().x;
        position.y = position.y / grid.GetCellSize().y;
        position.y = Mathf.Round(position.y);
        position.y = position.y * grid.GetCellSize().y;

        return (position);
    }

    void AddObjectToGrid()
    {
        WallGrid wallGrid = transform.GetComponentInParent<WallGrid>();
        transform.SetParent(transform.parent.Find("Grid"));
        Vector2 positionWithFakeY = new Vector2(transform.localPosition.x, 0f);
        //Vector2 celledCoordinates = RoundPositionToCellSize(transform.localPosition);
        Vector2 celledCoordinates = RoundPositionToCellSize(positionWithFakeY);
        Debug.LogError("Grid mover door object local position = " + transform.localPosition);
        Debug.LogError("Celled coordinates = " + celledCoordinates);
        //WallGrid wallGrid = transform.GetComponentInParent<WallGrid>();
        wallGrid.MoveObjectOneDimention(gameObject.transform, celledCoordinates, objectSizeInCells);
    }

    void MoveObjectOnCells()
    {
        Vector2 localMousePosition = GetLocalMousePosition();
        Vector3 globalMousePosition = GetGlobalMousePosition();
        Debug.LogError("Global mouse position = " + globalMousePosition);
        Debug.LogError("Local mouse position = " + localMousePosition);
        Vector2 celledCoordinates = RoundPositionToCellSize(localMousePosition);
        Vector2 positionWithFakeY = new Vector2(celledCoordinates.x, 0f);
        WallGrid wallGrid = transform.GetComponentInParent<WallGrid>();
        wallGrid.MoveObjectOneDimention(gameObject.transform, positionWithFakeY, objectSizeInCells);
    }

    void Start()
    {
        parentWall = transform.parent;
        CalculateObjectSizeInCells();
        AddObjectToGrid();
        Debug.LogWarning("Door pos wallgrid" + transform.position);
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
        Debug.LogErrorFormat($"History door coord is {GetObjectPositionOnWall()} {RoundPositionToCellSize(GetObjectPositionOnWall())}");

        historyAction.CreateMoveObjectOnWallHistoryAction(wallGrid.MoveObjectOneDimention, transform, RoundPositionToCellSize(GetObjectPositionOnWall()), objectSizeInCells);
        HistoryChangesStack historyChangesStack = FindObjectOfType<HistoryChangesStack>();
        historyChangesStack.AddHistoryAction(historyAction);
    }

    public void AddHistoryActionCreate()
    {
        HistoryAction historyAction = new HistoryAction();
        WallGrid wallGrid = transform.GetComponentInParent<WallGrid>();
        //GameObject cloneObject = Instantiate(gameObject, transform.parent);
        //cloneObject.SetActive(false);
        Debug.LogErrorFormat($"History door coord is {GetObjectPositionOnWall()} {RoundPositionToCellSize(GetObjectPositionOnWall())}");
        historyAction.CreateAddObjectOnWallHistoryAction(wallGrid.MoveObjectOneDimention, transform, RoundPositionToCellSize(GetObjectPositionOnWall()), objectSizeInCells);
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
}
