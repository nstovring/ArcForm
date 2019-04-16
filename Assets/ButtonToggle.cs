using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonToggle : MonoBehaviour
{
    public bool toggled;

    public Animator animator;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        if(animator == null){
        animator = GetComponent<Animator>();
        }
        if(button == null){
        button = GetComponent<Button>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TaskOnClick()
    {
        toggled = !toggled;
        if(toggled == true){
            animator.SetBool("Toggled", true);
            
        }
        if(toggled != true){
            animator.SetBool("Toggled", false);
        }
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");
    }
}