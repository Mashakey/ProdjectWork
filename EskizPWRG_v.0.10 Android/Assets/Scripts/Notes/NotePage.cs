using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotePage : MonoBehaviour
{
	[SerializeField]
	GameObject NotePrefab;
	[SerializeField]
	InputField noteInputField;

	public Note activeNote = null;

	public void ChangeInputFieldText(string text)
	{
		noteInputField.text = text;
	}

	public void ChangeNote()
	{
		ActiveWindowKeeper.IsRedactorActive = true;

		activeNote.contentText = noteInputField.text;
		CloseNoteWindow();
	}

	public void CreateNote()
	{
		ActiveWindowKeeper.IsRedactorActive = true;

		GameObject note = Instantiate(NotePrefab);
		activeNote = note.GetComponent<Note>();
		activeNote.MoveNoteInLookDirection();
		activeNote.contentText = noteInputField.text;
		Room room = FindObjectOfType<Room>();
		room.AddNote(activeNote);
		activeNote.transform.SetParent(room.transform);
		CloseNoteWindow();
	}

	public void DeleteNote()
	{
		if (activeNote != null)
		{
			ActiveWindowKeeper.IsRedactorActive = true;

			Room room = FindObjectOfType<Room>();
			room.DeleteNote(activeNote);
			CloseNoteWindow();
		}
	}

	public void CloseNoteWindow()
	{
		Debug.LogWarning("Closing note window");
		ActiveWindowKeeper.IsRedactorActive = true;
		Destroy(gameObject);
	}
}
