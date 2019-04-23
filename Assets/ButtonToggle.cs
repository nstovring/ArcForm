using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonToggle : MonoBehaviour
{
    public bool toggled;
    public Animator animator;
    private Button button;
    public ArcCollectionItem arcCollectionItem;
    //public string someName;
    //public GameObject subMenuTitle = GameObject.Find("SubMenuTitle");



    void Start()
    {
        if(animator == null){
        animator = GetComponent<Animator>();
        }
        if(button == null){
        button = GetComponent<Button>();
        }
        //if(arcCollectionItem == null){
        //arcCollectionItem = GetComponent<ArcCollectionItem>();
        //}
        //someName = arcCollectionItem.textField.text;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Toggle(bool state)
    {
        animator.SetBool("Toggled", true);
        if (state == true)
        {
            SubItemController.toggleCounter += 1;
            SubItemController.CheckForToggle();
            //subMenuTitle.GetComponent<SubMenuRename>().SetProperty(someName);

        }
        if (state != true)
        {
            animator.SetBool("Toggled", false);
            SubItemController.toggleCounter -= 1;
            Debug.Log(SubItemController.toggleCounter);
            SubItemController.CheckForToggle();
            //arcCollectionItem.SetProperty(someName);
        }
    }

    public void TaskOnClick()
    {
        toggled = !toggled;
        if(toggled == true){
            animator.SetBool("Toggled", true);
            SubItemController.toggleCounter += 1;
            Debug.Log(SubItemController.toggleCounter);
            SubItemController.CheckForToggle();
            //subMenuTitle.GetComponent<SubMenuRename>().SetProperty(someName);
            
        }
        if(toggled != true){
            animator.SetBool("Toggled", false);
            SubItemController.toggleCounter -= 1;
            Debug.Log(SubItemController.toggleCounter);
            SubItemController.CheckForToggle();
            //arcCollectionItem.SetProperty(someName);
        }
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");
    }
}