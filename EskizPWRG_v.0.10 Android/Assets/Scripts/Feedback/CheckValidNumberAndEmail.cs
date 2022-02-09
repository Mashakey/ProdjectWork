using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
public class CheckValidNumberAndEmail : MonoBehaviour
{
	[SerializeField]
	InputField numberInputField;
	[SerializeField]
	InputField emailInputField;

	Color colorOutline;

	Outline outlineEmail;
	Outline outlineNamber;

	private void Awake()
	{
		outlineEmail = GameObject.Find("Email").GetComponent<Outline>();
		outlineNamber = GameObject.Find("Namber").GetComponent<Outline>();
		ColorUtility.TryParseHtmlString("#D25353", out colorOutline);
	}

	public void ChekNumberFormat()
	{
		string phoneNumber = numberInputField.text;
		Debug.LogWarning("phone = " + phoneNumber);
		//string numberPattern = @"^\+?[78][-\(]?\d{3}\)?-?\d{3}-?\d{2}-?\d{2}$";
		string numberPattern = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";
		if (Regex.IsMatch(phoneNumber, numberPattern, RegexOptions.IgnoreCase))
		{
			Debug.LogWarning("Phone is valid");
		}
		else
		{
			Debug.LogWarning("Phone is not valid");
			numberInputField.text = "Некорректный номер телефона. Введите снова.";
			outlineNamber.effectColor = colorOutline;
		}
	}

	public void CheckEmailFormat()
	{
		string email = emailInputField.text;
		if (email.Contains("@"))
		{
			Debug.LogWarning("Email is valid");
		}
		else
		{
			Debug.LogWarning("Email is not valid");
			emailInputField.text = "Некорректный Email. Введите снова.";
			outlineEmail.effectColor = colorOutline;
		}
	}
}
