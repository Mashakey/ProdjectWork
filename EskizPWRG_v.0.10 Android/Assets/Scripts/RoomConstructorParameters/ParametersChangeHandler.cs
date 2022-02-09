using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class ParametersChangeHandler : MonoBehaviour
{
    [SerializeField]
    InputField inputFieldRoomName;

    [SerializeField]
    InputField inputFieldLength;
    [SerializeField]
    InputField inputFieldHeight;

    [SerializeField]
    Transform PanelSafeArea;

    [SerializeField]
    RoomCreator roomCreator;

    [SerializeField]
    GameObject LoadingScreen;

    [SerializeField]
    RoomWallManager tTypeWallManager;
    [SerializeField]
    RoomWallManager rectangularWallManager;
    [SerializeField]
    RoomWallManager gTypeWallManager;
    [SerializeField]
    RoomWallManager zTypeWallManager;
    [SerializeField]
    RoomWallManager trapezoidalWallManager;

    RoomWallManager roomWallsManager;

    string activeRoomType = "";

    float previousLengthValue = 100f;

    float lengthFieldValueBeforeChanging = 100f;


    float miminumWallLengthCm = 100;
    float maximumWallLengthCm = 800;
    float minimumWallHeightCm = 210;
    float maximumWallHeightCm = 420;

    public void SetRoomNameFieldValue(string roomName)
	{
        inputFieldRoomName.text = roomName;
	}

    public void SetLengthFieldValue(float lengthInCm)
	{
        inputFieldLength.text = (((int)lengthInCm).ToString()) + " см";

    }

    public void SetHeightFieldValue(float heightInCm)
	{
        inputFieldHeight.text = heightInCm.ToString() + " см";

    }

    public void OnRoomNameChanged()
	{
        roomWallsManager.RoomName = inputFieldRoomName.text;
	}

    public void UpdateInteriorType(string type)
	{
        switch (type)
		{
            case "Гостиная":
                roomWallsManager.Interior = InteriorType.LivingRoom;
                break;
            case "Спальня":
                roomWallsManager.Interior = InteriorType.Bedroom;
                break;
            case "Прихожая":
                roomWallsManager.Interior = InteriorType.Hallway;
                break;
            case "Детская":
                roomWallsManager.Interior = InteriorType.Nursery;
                break;
            default:
                roomWallsManager.Interior = InteriorType.Bedroom;
                break;
		}
	}

    public void OnLengthChanged()
    {
        //Debug.LogError("Room type = " + roomWallsManager.GetType());
        //lengthFieldValueBeforeChanging = int.Parse(inputFieldLength.text);
        float length = 0;
        if (inputFieldLength.text.Length > 0)
        {
            length = int.Parse(inputFieldLength.text);
        }
        if (length < miminumWallLengthCm)
        {
            length = miminumWallLengthCm;
        }
        else if (length > maximumWallLengthCm)
        {
            length = maximumWallLengthCm;
        }
        if (roomWallsManager.Equals(zTypeWallManager))
		{
            ZtypeWallManager wallManager = (ZtypeWallManager)roomWallsManager;
            previousLengthValue = wallManager.GetActiveWallLengthInCm();
		}
        //Debug.LogError("Setting length value #1");
        
        SetLengthFieldValue(length);
        roomWallsManager.ChangeWallLength((int)length);
    }

    public void OnRevertLengthChange()
	{
        //Debug.LogWarningFormat("Reverting length. Previosulength = {0}", lengthFieldValueBeforeChanging);
        SetLengthFieldValue(lengthFieldValueBeforeChanging);
	}

    public void SetPreviuosLengthValue()
    {
        lengthFieldValueBeforeChanging = int.Parse(inputFieldLength.text);
        //Debug.LogError("# Previous length value = " + lengthFieldValueBeforeChanging);
    }

    public void SetPreviuosLengthValue(float value)
    {
        lengthFieldValueBeforeChanging = value;
        //Debug.LogError("# Previous length value = " + lengthFieldValueBeforeChanging);

    }

    public void SetPreviuosLengthValueInInputField()
	{
        //Debug.LogError("Previous length value = " + lengthFieldValueBeforeChanging);
        SetLengthFieldValue(lengthFieldValueBeforeChanging);
        //SetLengthFieldValue(previousLengthValue);
	}

    public void OnAddWindowClick(int count)
	{
        roomWallsManager.AddWindow(count);
    }

    public void OnAddDoorClick(int count)
	{
        roomWallsManager.AddDoor(count);
	}

    public void OnHeightChanged()
	{
        float height = 0;
        if (inputFieldHeight.text.Length > 0)
		{
            height = float.Parse(inputFieldHeight.text);
		}
        if (height < minimumWallHeightCm)
		{
            height = minimumWallHeightCm;
		}
        else if (height > maximumWallHeightCm)
		{
            height = maximumWallHeightCm;
		}
        SetHeightFieldValue(height);
        roomWallsManager.ChangeHeight(height);
    }

    public void OnEnterRoomButtonClick()
	{

        //roomWallsManager.SetCameraPositionOnMiddle();
        LoadingScreen loadingScreen = LoadingScreen.GetComponent<LoadingScreen>();
        loadingScreen.gameObject.SetActive(true);
        loadingScreen.StartCoroutine(loadingScreen.LoadingOnNewRoomCreating(roomWallsManager));
        //LoadingScreen.GetComponent<LoadingScreen>().StartLoading();
        //StartCoroutine(LoadingScreen.GetComponent<LoadingScreen>().Loading());
        //roomCreator.CreateRoom(roomWallsManager.CreateRoomDataFromConstructor());
    }

	private void OnEnable()
	{
        activeRoomType = GameObject.Find("EventSystem").GetComponent<ManagerType>().typeMyRoom;
        //Debug.LogError(activeRoomType);
        if (activeRoomType == "T-type")
        {
            //roomWallsManager = PanelSafeArea.transform.GetComponentInChildren<TtypeWallManager>();
            roomWallsManager = tTypeWallManager;
        }
        else if (activeRoomType == "Rectange")
        {
            //roomWallsManager = (RectangularWallManager)PanelSafeArea.transform.GetComponentInChildren<RectangularWallManager>();
            roomWallsManager = rectangularWallManager;
        }
        else if (activeRoomType == "G-type")
        {
            //roomWallsManager = PanelSafeArea.transform.GetComponentInChildren<GtypeWallManager>();
            roomWallsManager = gTypeWallManager;

        }
        else if (activeRoomType == "Trapezoidal")
        {
            //roomWallsManager = PanelSafeArea.transform.GetComponentInChildren<TrapezoidalWallManager>();
            roomWallsManager = trapezoidalWallManager;

        }
        else if (activeRoomType == "Z-type")
        {
            //roomWallsManager = PanelSafeArea.transform.GetComponentInChildren<ZtypeWallManager>();
            roomWallsManager = zTypeWallManager;

        }
        else
        {
            Debug.LogError("Unknown room type '" + activeRoomType + "'");
        }
        //Debug.LogError("Manager is " + roomWallsManager.GetType());
    }

	void Start()
    {
		activeRoomType = GameObject.Find("EventSystem").GetComponent<ManagerType>().typeMyRoom;
		//Debug.LogError(activeRoomType);
		if (activeRoomType == "T-type")
		{
			roomWallsManager = PanelSafeArea.transform.GetComponentInChildren<TtypeWallManager>();
		}
		else if (activeRoomType == "Rectange")
		{
			roomWallsManager = (RectangularWallManager)PanelSafeArea.transform.GetComponentInChildren<RectangularWallManager>();
		}
		else if (activeRoomType == "G-type")
		{
			roomWallsManager = PanelSafeArea.transform.GetComponentInChildren<GtypeWallManager>();
		}
		else if (activeRoomType == "Trapezoidal")
		{
			roomWallsManager = PanelSafeArea.transform.GetComponentInChildren<TrapezoidalWallManager>();
		}
		else if (activeRoomType == "Z-type")
		{
			roomWallsManager = PanelSafeArea.transform.GetComponentInChildren<ZtypeWallManager>();
		}
		else
		{
			Debug.LogError("Unknown room type '" + activeRoomType + "'");
		}
		//inputFieldHeight.text = (roomWallsManager.RoomHeight * 100).ToString();
	}
}
