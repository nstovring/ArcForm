using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubItemController : MonoBehaviour
{
    public static int toggleCounter = 0;
    public GameObject arcSubItemMenu;

    public static SubItemController Instance { get; internal set; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        if(arcSubItemMenu == null){
        arcSubItemMenu = GameObject.Find("ArcCollectionSubItemMenu");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //ToggleSubMenu(); //hides ArcCollectionSubItemMenu gameobject set active
        
    }

    public static bool CheckForToggle(){
    if(toggleCounter == 0){
        return true;
    }
    else return false;
    
    }

     public void ToggleSubMenu(){
        
        if(CheckForToggle() == true){
            arcSubItemMenu.SetActive(false);
            

        }
        if(CheckForToggle() != true){
            arcSubItemMenu.SetActive(true);
            
        }
    }
}
