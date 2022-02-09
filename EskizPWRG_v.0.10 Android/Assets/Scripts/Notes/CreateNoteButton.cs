using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNoteButton : MonoBehaviour
{
    [SerializeField]
    GameObject noteWindowPrefab;

    public void OnCreateNoteButtonClick()
	{
        ActiveWindowKeeper.IsRedactorActive = false;
        GameObject editorRoom = GameObject.Find("EditorRoom");
        Debug.LogWarning("Creating note window");
        GameObject notePage = Instantiate(noteWindowPrefab, editorRoom.transform);
        notePage.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
    }
}
