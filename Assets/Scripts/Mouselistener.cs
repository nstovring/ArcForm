using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouselistener : MonoBehaviour
{
    Camera mCamera;
    public Unitoken hoveredOverToken;
    Unitoken hoveredStore;
    bool isDraging;
    public static Mouselistener Instance;

    public static Vector3 mousePositionInSpace;

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
        // Mouseposition and vector3 storage
       

        // Click, Drag and Release
        if (Input.GetMouseButton(0) && hoveredOverToken != null){
            // Debug.Log("Pressed primary button is held");
            isDraging = true;
            hoveredStore = hoveredOverToken;
        }
        if (Input.GetMouseButton(0) && hoveredOverToken == null){
            
        //    // Debug.Log("Pressed primary button is held");
        //    isDraging = true;
        //    hoveredStore = hoveredOverToken;
        }

        else{isDraging = false;}

        // Instantiate new Unitoken, 
        if (Input.GetMouseButtonUp(0)){
            if(hoveredStore != null){
                
                ArcFactory.Instance.AddNewArc(hoveredStore, "Test", TokenFactory.Instance.AddNewToken(mousePositionInSpace));
                hoveredStore = null;
            }

        }

        //if (Input.GetMouseButton(1))
        //    Debug.Log("Pressed secondary button.");
        
        //if (Input.GetMouseButton(2))
        //    Debug.Log("Pressed middle click.");

        
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
