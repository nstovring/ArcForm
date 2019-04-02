using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeTester : MonoBehaviour
{
    // Start is called before the first frame update
    //[Header("Health Settings")]

    public Transform testObject;

    private Camera mCamera;
    void Start()
    {
        mCamera = ArcMapManager.Instance.mCamera;
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse(testObject);
    }

       public void FollowMouse(Transform trs){
        
        trs.position = point;
    }
 
    Vector3 point;
    void OnGUI(){
        //Vector3 point = new Vector3();
        Event   currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = mCamera.pixelHeight - currentEvent.mousePosition.y;

        point = mCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mCamera.nearClipPlane));
        //point = mCamera.ScreenToWorldPoint(Input.mousePosition);

        

        GUIStyle gsTest = new GUIStyle();
        gsTest.normal.textColor = Color.black;
        GUILayout.BeginArea(new Rect(20, 20, 250, 120),gsTest);
        GUILayout.Label("Screen pixels: " + mCamera.pixelWidth + ":" + mCamera.pixelHeight, gsTest);
        GUILayout.Label("Mouse position: " + mousePos, gsTest);
        GUILayout.Label("World position: " + point.ToString("F3"), gsTest);
        GUILayout.EndArea();
    }
}
