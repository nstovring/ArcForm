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
    
    public ActionGroupAnimate[] actionGroupChildren = new ActionGroupAnimate[4];
    
    
    void Start()
    {
        foreach (ActionGroupAnimate agc in actionGroupChildren){
        agc.gameObject.SetActive(false);

        }
        smallCollider.radius = smallRadius;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isHoveredOver == true && animationIsout == true){
            if(activateActionGroup == false){
                foreach (ActionGroupAnimate agc in actionGroupChildren){
                agc.gameObject.SetActive(true);
                
                }
            activateActionGroup = true;
            }
            
            foreach (ActionGroupAnimate agc in actionGroupChildren)
            {
                agc.AnimateOut();
            }
            animationIsout = false;
        }
        if(isHoveredOver != true && animationIsout !=true){
            foreach (ActionGroupAnimate agc in actionGroupChildren)
            {
                agc.AnimateIn();
            }
            animationIsout = true;
        }
        
        
    }

    void GroupStart(){
        
        //GetComponentInChildren<ActionGroupAnimate>().AnimateOut;
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
