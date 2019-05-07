using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonToggle : MonoBehaviour
{
    public bool toggled;
    public Animator animator;
    private Button button;
    public ArcMenuItem arcCollectionItem;


    void Start()
    {
        if(animator == null){
        animator = GetComponent<Animator>();
        }
        if(button == null){
        button = GetComponent<Button>();
        }
    }

    public bool edited;

    public void ToggleEdited(bool state)
    {
        if (state == true)
        {
            animator.SetBool("Edited", true);
            //SubItemController.toggleCounter += 1;
            //Debug.Log(SubItemController.toggleCounter);
            SubItemController.CheckForToggle();
            //subMenuTitle.GetComponent<SubMenuRename>().SetProperty(someName);

        }
        if (state != true)
        {
            animator.SetBool("Edited", false);
            //SubItemController.toggleCounter -= 1;
            //Debug.Log(SubItemController.toggleCounter);
            SubItemController.CheckForToggle();
            //arcCollectionItem.SetProperty(someName);
        }
        edited = state;
    }

    public void TogglePressed(bool state)
    {
        if (state == true)
        {
            animator.SetBool("Toggled", true);
            //SubItemController.toggleCounter += 1;
            //Debug.Log(SubItemController.toggleCounter);
            SubItemController.CheckForToggle();
            //subMenuTitle.GetComponent<SubMenuRename>().SetProperty(someName);

        }
        if (state != true)
        {
            animator.SetBool("Toggled", false);
            //SubItemController.toggleCounter -= 1;
            //Debug.Log(SubItemController.toggleCounter);
            SubItemController.CheckForToggle();
            //arcCollectionItem.SetProperty(someName);
        }
        toggled = state;
    }

    public void TaskOnClick()
    {
        toggled = !toggled;
        if(toggled == true){
            animator.SetBool("Toggled", true);
            //SubItemController.toggleCounter += 1;
            //Debug.Log(SubItemController.toggleCounter);
            SubItemController.CheckForToggle();
            //subMenuTitle.GetComponent<SubMenuRename>().SetProperty(someName);
            
        }
        if(toggled != true){
            animator.SetBool("Toggled", false);
            //SubItemController.toggleCounter -= 1;
            //Debug.Log(SubItemController.toggleCounter);
            SubItemController.CheckForToggle();
            //arcCollectionItem.SetProperty(someName);
        }
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");
    }
}