using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouselistener : MonoBehaviour
{
    Camera mCamera;
    public Unitoken hoveredOverToken;
    Unitoken hoveredStore;
    public bool isDraging;
    public bool startVectorStored;
    public static Mouselistener Instance;

    public static Vector3 mousePositionInSpace;
    public Vector3 unitokenStartPosVector;

    // Start is called before the first frame update
    void Start()
    {
        mCamera = Camera.main;
        Instance = this;
        Vector3 endPositionVector;
    }

    
    // Update is called once per frame
    void Update()
    {
         Vector3 mouseWorldPos = mCamera.ScreenToWorldPoint(Input.mousePosition);
                float h = mouseWorldPos.x;
                float v = mouseWorldPos.y;
                Vector3 mouseDelta = new Vector3(h,v,0);
        if(Input.GetMouseButtonUp(1)){
                

                ArcMapManager.Instance.selectedUnitoken = TokenFactory.Instance.AddNewToken(mouseDelta);
                ArcMapManager.Instance.SelectUnitoken(ArcMapManager.Instance.selectedUnitoken);
                Debug.Log(Input.mousePosition);
                Debug.Log(mouseWorldPos);

            }
        
        // Click, Drag and Release
        if (Input.GetMouseButtonDown(0) && hoveredOverToken != null){ //Store info from hover token
            isDraging = true;
            hoveredStore = hoveredOverToken;
        }
        if (Input.GetMouseButtonDown(0) && hoveredOverToken == null){ //StoreStart vector from empty space
            unitokenStartPosVector = new Vector3(mousePositionInSpace.x, mousePositionInSpace.y, 0);
            startVectorStored = true;
            isDraging = true;
        }

        // Instantiate new Unitoken, 
        if (Input.GetMouseButtonUp(0)){ // Drag from Token to new Token
            if(isDraging){
                OnDraggedRelease();
            }
           
          

        }

        
    }


    public void OnDraggedRelease(){ 
            if(hoveredStore != null && startVectorStored==false && hoveredOverToken == null){ // Drag from existing Token to new

                Vector3 temp = new Vector3(mousePositionInSpace.x, mousePositionInSpace.y, 0);
                ArcFactory.Instance.AddNewArc(hoveredStore, "Test", TokenFactory.Instance.AddNewToken(temp));
                hoveredStore = null;

            }
            if(startVectorStored == true && hoveredOverToken != null){ // Drag from new Token to existing Token
                
                ArcFactory.Instance.AddNewArc(TokenFactory.Instance.AddNewToken(unitokenStartPosVector), "Test", hoveredOverToken);
                hoveredOverToken=null;
                startVectorStored = false;
                 
            }
            if(hoveredStore != null && startVectorStored==false && hoveredOverToken != null){ // Drag from existing Token to another existing

                ArcFactory.Instance.AddNewArc(hoveredStore, "Test", hoveredOverToken);
                hoveredStore = null;

            }

        isDraging = false;
    }


    //public void FollowMouse(Transform trs){
    //    Vector3 mouseWorldPos = mCamera.ScreenToWorldPoint(Input.mousePosition);
    //    float h = mouseWorldPos.x;
    //    float v = mouseWorldPos.y;
    //    Vector3 mouseDelta = new Vector3(h,v,0);
    //    trs.position = mouseDelta;
    //}

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
