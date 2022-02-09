using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class HistoryChangesStack : MonoBehaviour
{
	public Button UndoButton;
	public Button RedoButton;
	public Button ResetRoomButton;

	Stack<HistoryAction> UndoActions = new Stack<HistoryAction>();
	Stack<HistoryAction> RedoActions = new Stack<HistoryAction>();
	Stack<HistoryAction> ReUndoActions = new Stack<HistoryAction>();

	public void AddHistoryAction(HistoryAction historyAction)
	{
		ResetRoomButton.interactable = true;
		UndoButton.interactable = true;
		if (UndoActions.Count >= 14)
		{
			HistoryAction[] tempArray = UndoActions.ToArray();
			UndoActions.Clear();
			for (int i = tempArray.Length - 2; i >=0; i--)
			{
				UndoActions.Push(tempArray[i]);
			}
		}
		UndoActions.Push(historyAction);

		RedoActions.Clear();
		ReUndoActions.Clear();
		RedoButton.interactable = false;
	}

	public void AddUndoActionFromReUndo(HistoryAction historyAction)
	{
		UndoButton.interactable = true;
		UndoActions.Push(historyAction);
	}

	public void PopAndInvokeHistoryAction()
	{

		HistoryAction undoAction = UndoActions.Pop();
		undoAction.CreateAndAddRedoAction();
		ReUndoActions.Push(undoAction);

		undoAction.InvokeHistoryAction();
		//UndoActions.Pop().InvokeHistoryAction();
		if (UndoActions.Count < 1)
		{
			UndoButton.interactable = false;
		}
	}

	public void PopAndInvokeRedoAction()
	{
		Debug.LogErrorFormat($"ReUndo acctions count = {ReUndoActions.Count} Redo actions count = {RedoActions.Count}");

		AddUndoActionFromReUndo(ReUndoActions.Pop());
		RedoActions.Pop().InvokeHistoryAction();
		Debug.LogErrorFormat($"ReUndo acctions count = {ReUndoActions.Count} Redo actions count = {RedoActions.Count}");

		if (RedoActions.Count < 1)
		{
			RedoButton.interactable = false;
		}
	}

	public void AddRedoAction(HistoryAction historyAction)
	{
		RedoActions.Push(historyAction);
		RedoButton.interactable = true;
	}

	public void AddReundoAction(HistoryAction historyAction)
	{
		ReUndoActions.Push(historyAction);
	}

	public void ClearHistory()
	{
		ResetRoomButton.interactable = false;
		UndoButton.interactable = false;
		UndoActions.Clear();
	}
}
