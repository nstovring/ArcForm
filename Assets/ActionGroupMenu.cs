using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGroupMenu : Fragment
{   
    public CircleCollider2D smallCollider;
    float smallRadius = 0.45f;
    float largeRadius = 1.4f;
    bool animationIsout = true;
    bool activateActionGroup = false;
    public Animator anim;
    public ActionGroupAnimate[] actionGroupChildren = new ActionGroupAnimate[4];
    
    
    void Start()
    {
        anim.SetBool("AnimateOut",true);

        foreach (ActionGroupAnimate agc in actionGroupChildren){
        agc.gameObject.SetActive(false);

        }
        smallCollider.radius = smallRadius;
    }

    // Update is called once per frame
    void Update()
    {
       if(isHoveredOver == true && anim.GetBool("AnimateIn") == true){
           Debug.Log("I am trying to AnimateOut");
           anim.SetBool("AnimateOut",true);
           anim.SetBool("AnimateIn",false);
       }
       else if(isHoveredOver != true && anim.GetBool("AnimateOut") == true){ 
           Debug.Log("I am trying to AnimateIN");
           anim.SetBool("AnimateIn", true);
           anim.SetBool("AnimateOut",false);}
       // if(isHoveredOver == true && animationIsout != true){
       //     if(activateActionGroup == false){
       //         foreach (ActionGroupAnimate agc in actionGroupChildren){
       //         agc.gameObject.SetActive(true);
       //         
       //         }
       //     activateActionGroup = true;
       //     }
       //     
       //     foreach (ActionGroupAnimate agc in actionGroupChildren)
       //     {
       //         agc.AnimateOut();
       //     }
       //     animationIsout = true;
       // }
       // if(isHoveredOver != true && animationIsout ==true){
       //     foreach (ActionGroupAnimate agc in actionGroupChildren)
       //     {
       //         agc.AnimateIn();
       //     }
       //     animationIsout = false;
       // }
        
        
    }

    void GroupStart(){
        
    }

    void GroupEnable(){
        
    }
    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        smallCollider.radius = largeRadius;
        Mouselistener.Instance.hoveredOverActionGroup = this;
        //Debug.Log("Mouse is over GameObject.");
        SetHoverActive(true);

        
    }
    void OnMouseExit()
    {
        smallCollider.radius = smallRadius;
        //The mouse is no longer hovering over the GameObject so output this message each frame
        //Debug.Log("Mouse is no longer on GameObject.");
        SetHoverActive(false);
        Mouselistener.Instance.hoveredOverActionGroup = null;
    }
}
