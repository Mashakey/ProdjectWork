using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;

public class ServerDownloaderSettings : MonoBehaviour
{
	[SerializeField]
	static int maximumTasksAtTime = 100;


	public static string tempUri = @"https://apidev.ezkiz.ru";
	public static string devUri = @"https://apidev.ezkiz.ru";
	public static string prodUri = @"https://api.ezkiz.ru";
	public static string prodUriPWRG = @"https://api-ezkiz.power-games.me";

	//public static string UsingURI = devUri; //JSON serialize error
	//public static string UsingURI = prodUriPWRG;
	//public static string UsingURI = prodUri;
	public static string UsingURI = devUri;
	public static string FeedbackURI = devUri;


	public const string get_products = @"/api/products";
	public const string get_categories = @"/api/categories";
	public const string get_ceilings = @"/api/ceilings";
	public const string get_shops = @"/api/stores";
	public const string get_colors = @"/api/colors?paginate=false";
	public const string post_orders = @"/api/orders";
	public const string api = @"/api/";
	public const string get_banner = @"/api/banner";
	public const string get_rooms = @"/api/rooms?paginate=false";
	public const string get_textures = @"/api/__uploads/";
	public const string send_feedback = @"/api/feedback?";
	public const string send_estimate_to_shop = @"/api/mails/estimate";

	const string authorizationHeaderKeyLegacy = "Authorization";
	const string authorizationHeaderValueLegacy = "Token ezkiz-jwt-secret";

	const string authorizationHeaderKeyPWRG = "Authorization";
	const string authorizationHeaderValuePWRG = "Token ezkiz-mobile-token";

	const string authorizationHeaderKey = authorizationHeaderKeyLegacy;

	const string authorizationHeaderValue = authorizationHeaderValueLegacy;
	//const string authorizationHeaderValue = authorizationHeaderValuePWRG;


	public static HTTPHeader GetAutorizationHeader()
	{
		HTTPHeader header = new HTTPHeader();
		header.Key = authorizationHeaderKey;
		header.Value = authorizationHeaderValue;
		return (header);
	}

	public static int GetMaximumWorkingCoroutinesAtTimeCount()
	{
		return (maximumTasksAtTime);
	}


	static int totalAmountOfMaterials = 0;
	static int workingTasksCount = 0;
	static object locker = new object();
	static int firstNotDownloadedMaterialIndex = 0;
	static int firstNotDownloadedTexturesIndex = 0;
	static int limitMaterialsDownloadPerCycle = 1;

	static List<MaterialJSON> materialsInNeedForTextures;
	static List<MaterialJSON> cachedMaterials;

	static bool isWaitingForEndingLoadingJsonsStarted = false;

	delegate void JsonDownloadCompletedHandler(string msg);
	delegate void TexturesDownloadCompletedHandler(string msg);
	event JsonDownloadCompletedHandler JsonDownloadCompleted;
	event TexturesDownloadCompletedHandler TexturesDownloadCompleted;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
