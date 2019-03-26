using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Unitoken : Fragment, ILabelable
{
    public Vector3 AnchoredPosition;
    
    public int arcCount;
    List<LineRenderer> lineList = new List<LineRenderer>();

    public Vector3 rotationVector;
    public float tokenRotation;

    public TextMeshPro myLabel;
    public TextMeshPro MyLabel => myLabel;

    public string label;

    public TMP_InputField InputField => UIManager.Instance.inputField;

    public Transform getTransform { get => transform;}
    public Transform hoverIcon;

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
   
        ArcMapManager.Instance.SelectUnitoken(this);
        ShowInputField();
        
    }
    void Update(){
       
    }

    void OnMouseDrag()
    {
        //FollowMouse();
    }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        if(Input.GetMouseButtonDown(0)){
            ArcMapManager.Instance.SelectUnitoken(this);
        }
        //Debug.Log("Mouse is over GameObject.");
        SetHoverActive(true);
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        //Debug.Log("Mouse is no longer on GameObject.");
        SetHoverActive(isSelected);
    }

    public bool isSelected;
    public bool SetHoverActive(bool state){
        hoverIcon.gameObject.SetActive(state);
        return state;
    }

   

    public virtual void ShowInputField()
    {
        UIManager.Instance.ShowInputField(transform.position, this);
    }

    public virtual void SetLabel(string label)
    {
        MyLabel.text = label;
    }
}
