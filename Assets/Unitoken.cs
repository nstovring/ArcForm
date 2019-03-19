using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Unitoken : Fragment, ILabelable
{
    public Vector3 AnchoredPosition;
    public Vector3 TransientPosition;

    List<LineRenderer> lineList = new List<LineRenderer>();

    public Vector3 rotationVector;
    public float tokenRotation;

    public TextMeshPro myLabel;
    public TextMeshPro MyLabel => myLabel;

    public string label;

    public TMP_InputField InputField => ArcMapManager.Instance.inputField;

    public Transform getTransform { get => transform;}


    Camera mCamera;
    // Start is called before the first frame update
    void Start()
    {
        //ArcMapManager.AddToken(this);

        mCamera = Camera.main;

        if(myArcs == null){
            myArcs = new List<Arc>();
        }
//        if(myTargets == null){
//            myTargets = new List<Unitoken>();
        //}
    }

    public void Initialize(string Label, Vector3 TransientPosition){
        myLabel.text = Label;
        this.TransientPosition = TransientPosition;
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
   
      
            ShowInputField();
        
    }
    void Update(){
        if(Input.GetKeyUp(KeyCode.Space) && ArcMapManager.Instance.selectedUnitoken == this){
            //ArcMapManager.Instance.AddNewToken(this);
            ArcMapManager.Instance.AddNewArc(this);
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
