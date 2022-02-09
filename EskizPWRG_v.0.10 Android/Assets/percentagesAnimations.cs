using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class percentagesAnimations : MonoBehaviour
{
	public string animationUp;
	public string animationDown;

	bool isOpen;

    public void precentOnClick()
	{
		if (isOpen)
		{
			gameObject.GetComponentInParent<Animator>().Play(animationDown);
			isOpen = false;
		}
		else
		{
			gameObject.GetComponentInParent<Animator>().Play(animationUp);
			isOpen = true;
			StartCoroutine(CloseDescriptionAfterThreeSeconds());
		}

	}

	public IEnumerator CloseDescriptionAfterThreeSeconds()
	{
		yield return new WaitForSeconds(3);
		if (isOpen)
		{
			gameObject.GetComponentInParent<Animator>().Play(animationDown);
			isOpen = false;
		}
	}
}
