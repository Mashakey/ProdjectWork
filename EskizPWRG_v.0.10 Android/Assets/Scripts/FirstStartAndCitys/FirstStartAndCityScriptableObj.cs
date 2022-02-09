using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class FirstStartAndCityScriptableObj : MonoBehaviour
{
    public Item item;

    private void Awake()
    {
        if (File.Exists(Application.persistentDataPath + "/firstStart.json"))
        {
            item = JsonUtility.FromJson<Item>(File.ReadAllText(Application.persistentDataPath + "/firstStart.json"));
        }
        else
		{
            item = new Item();
            item.numberRun = 0;
            item.city = "All city";

        }
    }


    public void SaveField()
    {
        File.WriteAllText(Application.persistentDataPath + "/firstStart.json", JsonUtility.ToJson(item));
    }

    [System.Serializable]
    public class Item
    {
        public int numberRun;
        public string city;
    }

}
