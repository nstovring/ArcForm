﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mouselistener : MonoBehaviour
{
    //ArcMapManager.Instance.SelectUnitoken(this);
    Camera mCamera;
    public Unitoken hoveredOverToken;
    public Arc hoveredOverArc;
    Unitoken hoveredStore;
    
    public bool isDraging;
    public bool startVectorStored;
    public bool consoleSingleMessage = false;

    public Vector3 mouseDelta;
    public static Mouselistener Instance;

    public static Vector3 mousePositionInSpace;
    public Vector3 unitokenStartPosVector;

    public Unitoken endPoint;
    public bool draggingFromBackground = false;
    public bool draggingFromToken = false;
    public bool draggingFromArk = false;

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 1.5f;
    float holdtime = 0f;

    public bool DoubleClick(){
        if (Input.GetMouseButton(0)){
            clicked++;
            if (clicked == 1) clicktime = Time.time;
        }
        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            return true;
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
        return false;
    }
    public bool ClickAndHold(){
        if(Input.GetMouseButton(0)){
            holdtime += Time.deltaTime;
        }
        else{
            holdtime = 0;
            //Debug.Log("Reset the counter");
        
        }
        if(holdtime >= clickdelay){
            //Debug.Log("I Am Working!");
            return true;
        }

        return false;
    }

    public bool tokenSpawn;

    public bool arcSpawn;

    public bool LeftMouseButtonHeld(){
        return Input.GetMouseButton(0);
    }
    public bool RightMouseButtonHeld(){
        return Input.GetMouseButton(1);
    }
    public bool MiddleMouseButtonHeld(){
        return Input.GetMouseButton(2);
    }

    // Start is called before the first frame update
    void Start()
    {
        mCamera = Camera.main;
        Instance = this;
        
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPos = mCamera.ScreenToWorldPoint(Input.mousePosition);
        float h = mouseWorldPos.x;
        float v = mouseWorldPos.y;
        mouseDelta = new Vector3(h,v,0);
        CheckOnClick(); //Checks if clicked on background, Arc or Token
        ClickAndHold(); //Checks if mouseIsHeld
        //DragFromBackground(); //Checks if dragging from background
        DragFromToken(); //Checks if dragging from Token
        //DragFromArk(); //Checks if dragging from Arc
        OnDraggedRelease(); //Determines what is dragged and instatiated
        
    
        if (endPoint != null){ // Makes sure the instance is updated until mouseRelease();
            endPoint.transform.position = mouseDelta;
        }
       
        // Click, Drag and Release
        //if (Input.GetMouseButtonDown(0) && hoveredOverToken != null){ //Store info from hover token
        //    endPoint = TokenFactory.Instance.AddNewToken(mouseDelta);
        //    hoveredStore = hoveredOverToken;
        //    ArcFactory.Instance.AddNewArc(hoveredOverToken, "Test", endPoint);
        //    isDraging = true;
        //    
        //}
        

        // Instantiate new Unitoken, 
        if (Input.GetMouseButtonUp(0)){ // Drag from Token to new Token
            if(isDraging){
                OnDraggedRelease();
            }
        }

        if(Input.GetMouseButtonUp(1)){
        
                ArcMapManager.Instance.selectedUnitoken = TokenFactory.Instance.AddNewToken(mouseDelta);
                ArcMapManager.Instance.SelectUnitoken(ArcMapManager.Instance.selectedUnitoken);
                Debug.Log(Input.mousePosition);
                Debug.Log(mouseWorldPos);
        
            }
        
        

        
    }
    
    public void CheckOnClick(){
        
        if(Input.GetMouseButtonDown(0) == true){
            
            if(hoveredOverToken == null && hoveredOverArc == null){//clicked on background
                Debug.Log("Clicked on background");
                draggingFromBackground = true;
                unitokenStartPosVector = new Vector3(mousePositionInSpace.x, mousePositionInSpace.y, 0);
                //tokenSpawn = true;
            }
            if(hoveredOverToken != null && hoveredOverArc == null){//clicked on Token
                Debug.Log("Clicked on Token");
                draggingFromToken = true;
                hoveredStore = hoveredOverToken;
                tokenSpawn = true;

            }
            if(hoveredOverToken == null && hoveredOverArc != null){//clicked on Arc
                Debug.Log("Clicked on Arc");
                draggingFromArk = true;
                arcSpawn = true;
            }
        }
    }

    public void DragFromBackground(){
        if(ClickAndHold() == true && draggingFromBackground == true){
                Debug.Log("Dragging from background");
                //StoreStart vector from empty space
                startVectorStored = true;
                ArcFactory.Instance.AddNewArc(TokenFactory.Instance.AddNewToken(unitokenStartPosVector), "Test", hoveredOverToken);
                isDraging = true;
        }       
    }
    public void DragFromToken(){
        if(ClickAndHold() == true && draggingFromToken == true){
                Debug.Log("Dragging from Token");
                if(tokenSpawn==true){
                    endPoint = TokenFactory.Instance.AddNewToken(mouseDelta);
                    ArcFactory.Instance.AddNewArc(hoveredStore, "Test", endPoint);
                    tokenSpawn = false;
                }
                
                isDraging = true;
                           
        }   
    }
    public void DragFromArk(){
        if(ClickAndHold() == true && draggingFromArk == true){
            if(consoleSingleMessage == true){
                Debug.Log("Dragging from Ark");
                consoleSingleMessage = false;
                }           
        }


    }

    public void OnDraggedRelease(){
            if(Input.GetMouseButtonUp(0) == true && draggingFromBackground == true && isDraging == true){
                Debug.Log("Not draging from Background anymore");
                draggingFromBackground = false;
                consoleSingleMessage = true;

                isDraging = false;    
                draggingFromBackground = false;
        
            }

            if(Input.GetMouseButtonUp(0) == true && draggingFromToken == true && isDraging == true){
                Debug.Log("Not draging from Token anymore");
                draggingFromToken = false;
                consoleSingleMessage = true;
                endPoint = null;
                hoveredStore = null;


                isDraging = false;    
                draggingFromToken = false;
        
            }
            
            //if(startVectorStored == true && hoveredOverToken != null){ // Drag from background New Token to existing Token
            //    
            //    
            //    hoveredOverToken=null;
            //    startVectorStored = false;
            //     
            //}

            //if(hoveredStore != null && startVectorStored==false && hoveredOverToken == null){ // Drag from existing Token to new
            //    endPoint = null;
            //    hoveredStore = null;
//
//
            //}
            
            //if(hoveredStore != null && startVectorStored==false && hoveredOverToken != null){ // Drag from existing Token to another existing
//
            //    ArcFactory.Instance.AddNewArc(hoveredStore, "Test", hoveredOverToken);
            //    hoveredStore = null;
//
            //}
        
        
    }
    

    public void FollowMouse(Transform trs){
        Vector3 mouseWorldPos = mCamera.ScreenToWorldPoint(Input.mousePosition);
        float h = mouseWorldPos.x;
        float v = mouseWorldPos.y;
        Vector3 mouseDelta = new Vector3(h,v,0);
        trs.position = mouseDelta;
    }

    //void OnMouseOver()
    //{
    //    //If your mouse hovers over the GameObject with the script attached, output this message
    //    if(Input.GetMouseButtonDown(0)){
    //        ArcMapManager.Instance.SelectUnitoken(this);
    //    }
    //    //Debug.Log("Mouse is over GameObject.");
    //    SetHoverActive(true);
    //}

    //void OnMouseExit()
    //{
    //    //The mouse is no longer hovering over the GameObject so output this message each frame
    //    //Debug.Log("Mouse is no longer on GameObject.");
    //    SetHoverActive(isSelected);
    //}

    //public bool isSelected;
    //public bool SetHoverActive(bool state){
    //    hoverIcon.gameObject.SetActive(state);
    //    return state;
    //}

     void OnGUI(){
        //Vector3 point = new Vector3();
        Event   currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = mCamera.pixelHeight - currentEvent.mousePosition.y;

        mousePositionInSpace = mCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mCamera.nearClipPlane + 5));
        //point = mCamera.ScreenToWorldPoint(Input.mousePosition);

        

        GUIStyle gsTest = new GUIStyle();
        gsTest.normal.textColor = Color.black;
        GUILayout.BeginArea(new Rect(20, 20, 250, 120),gsTest);
        GUILayout.Label("Screen pixels: " + mCamera.pixelWidth + ":" + mCamera.pixelHeight, gsTest);
        GUILayout.Label("Mouse position: " + mousePos, gsTest);
        GUILayout.Label("World position: " + mousePositionInSpace.ToString("F3"), gsTest);
        GUILayout.EndArea();
    }

}
