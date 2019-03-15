using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Unitoken : Thought, ILabelable
{

    List<LineRenderer> lineList = new List<LineRenderer>();

    public Vector3 rotationVector;
    public float tokenRotation;

    public TextMeshPro myLabel;
    public TextMeshPro MyLabel => myLabel;

    public TMP_InputField InputField => ArcMapManager.Instance.inputField;

    public List<Arc> myArcs;

    Camera mCamera;
    // Start is called before the first frame update
    void Start()
    {
        ArcMapManager.AddToken(this);

        mCamera = Camera.main;

        if(myArcs == null){
            myArcs = new List<Arc>();
        }
//        if(myTargets == null){
//            myTargets = new List<Unitoken>();
        //}
    }

    public void Initialize(){
        //mySources = new List<Unitoken>();
        myArcs = new List<Arc>();
        mCamera = Camera.main;
    }




    void OnMouseDown()
    {
        //Show options
        //Change label - Add labels
        //Create new join arc
        //
        if(Input.GetKey(KeyCode.Space)){
            AddNewToken();
        }
        else {
            ShowInputField();
        }
    }

    void OnMouseDrag()
    {
        Vector3 mouseWorldPos = mCamera.ScreenToWorldPoint(Input.mousePosition);
        float h = mouseWorldPos.x;
        float v = mouseWorldPos.y;
        Vector3 mouseDelta = new Vector3(h,v,0);
        transform.position = mouseDelta;
    }

   

    public virtual void ShowInputField()
    {
        ArcMapManager.Instance.ShowInputField(transform.position, this);
    }

    public virtual void SetLabel(string label)
    {
        MyLabel.text = label;
    }
}
