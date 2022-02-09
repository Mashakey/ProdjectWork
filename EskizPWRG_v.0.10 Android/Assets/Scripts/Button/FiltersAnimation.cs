using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FiltersAnimation : MonoBehaviour
{
    public GameObject objectAnimator;
    public string animationOne;
    public string animationTwo;
    public Sprite arrowUp;
    public Sprite arrowDown;
    int indexClick = 0;

    Animator animator;
    public bool click = true;

    public ManagerFiltration managerFiltration;

    public GameObject CostRoom;
    public GameObject TypeRoom;


    private void Update()
    {
    }

    private void Start()
    {

        if (objectAnimator!= null)
        {
            animator = objectAnimator.GetComponent<Animator>();
        }
    }

    public void ClickAnim()
    {       
        if (click == true)
        {
            animator.Play(animationOne);
            click = false;
            GameObject.FindGameObjectWithTag("arrow").GetComponent<Image>().sprite = arrowUp;
        }
        else
        {
            animator.Play(animationTwo);
            click = true;
            GameObject.FindGameObjectWithTag("arrow").GetComponent<Image>().sprite = arrowDown;
        }       
    }

    public void AnimationFilterRoom()
    {
        if (click == true)
        {
            animator.Play(animationOne);
            click = false;
        }
        else
        {
            animator.Play(animationTwo);
            click = true;
            CostRoom.GetComponent<Image>().sprite = arrowDown;
            TypeRoom.GetComponent<Image>().sprite = arrowDown;
        }
    }

    public void AnimationFiltersPaint()
    {
        if (click == true)
        {
            animator.Play(animationOne);
            click = false;
        }
        else
        {
            animator.Play(animationTwo);
            click = true;
        }
    }

    public void AnimationFilterMaterials()
    {
        animator = objectAnimator.GetComponent<Animator>();


        if (click == true)
        {
            animator.Play(animationOne);           
            click = false;
            managerFiltration.CloseAllFilters();
            managerFiltration.resetFiltersForBack();
        }
        else
        {
            animator.Play(animationTwo);
            indexClick++;
            indexManager();
            click = true;
        }

    }

    public void indexManager()
    {
        managerFiltration.prefabCost = GameObject.FindGameObjectWithTag("CostPrefab");
        foreach (Transform child in objectAnimator.transform)
        {
            if (child.gameObject.activeSelf == true && child.name != "cost" && child.name != "ImageUp" && child.name != "ImageDown")
            {
                managerFiltration.materialsChildrens.Add(child.gameObject);

            }
        }
        
    }

    public void FiltersAnimationForBack()
    {
        managerFiltration.CloseAllFilters();
        managerFiltration.resetFiltersForBack();
        if (animator != null)
        {
            animator.Play(animationOne);
        }
        click = false;
    }

    public void FiltersAnimationForBackPaint()
    {
        if (animator != null)
        {
            animator.Play(animationOne);
        }
        click = false;
    }

}
