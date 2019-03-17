using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Unitoken : Fragment, ILabelable
{

    List<LineRenderer> lineList = new List<LineRenderer>();

    public Vector3 rotationVector;
    public float tokenRotation;

    public TextMeshPro myLabel;
    public TextMeshPro MyLabel => myLabel;

    public TMP_InputField InputField => ArcMapManager.Instance.inputField;

    public Transform getTransform { get => transform;}


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
        myType = Type.Unitoken;
        myArcs = new List<Arc>();
        mCamera = Camera.main;
    }




    void OnMouseDown()
    {
        //Show options
        //Request action from manager depending on clicked type
        //Change label - Add labels
        //Create new join arc
        //
        if(Input.GetKey(KeyCode.Space)){
            //ArcMapManager.Instance.AddNewToken(this);
            ArcMapManager.Instance.AddNewArc(this);
        }
        else {
            ShowInputField();
        }
    }

    void OnMouseDrag()
    {
        //FollowMouse();
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
