using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FragmentResources;
using StructureContainer;
public class Unitoken : Fragment, ILabelable
{
    public int arcCount;

    public float tokenRotation;

    public TextMeshPro myLabel;
    public TextMeshPro MyLabel => myLabel;

    public Transform hoverIcon;
    public Transform softChild;

    public Transform hardChild;

    public SpriteRenderer spriteRenderer;
    public unitoken myUnitokenStruct;

    public bool isHoveredOver = false;

    public bool isSoft = true;


    public MapState myMapState = MapState.Preview;
    public InteractState myInteractState = InteractState.Unselected;

    Camera mCamera;
    // Start is called before the first frame update
    void Start()
    {
        mCamera = Camera.main;

        if(myArcs == null){
            myArcs = new List<Arc>();
        }
    }

    public void Initialize(unitoken token){
        myUnitokenStruct = token;

        myLabel.text = token.Label;
        this.TransientPosition = token.TransientPosition;
        myType = Type.Unitoken;
        myArcs = new List<Arc>();
        mCamera = Camera.main;
    }

    public void Initialize(string label, Vector3 transientPosition, string uri){
        myUnitokenStruct = new unitoken{URI = uri, TransientPosition = transientPosition, Label = label};
        myLabel.text = label;
        this.TransientPosition = transientPosition;
        myType = Type.Unitoken;
        myArcs = new List<Arc>();
        mCamera = Camera.main;
    }

    public void CheckMapState(){

    }

    public void CheckInteractState(){

    }


    void Update(){
        if(myMapState == MapState.Preview){
            spriteRenderer.color = new Color(1,1,1,0.5f);
        }else{
            spriteRenderer.color = new Color(1,1,1,1);

        }
        
       if(isSoft == false){//collider active and hoverOver is active.
        transform.GetComponent<CircleCollider2D>().enabled = true;
       }
       else{//collider disabled and dragging is occuring
        transform.GetComponent<CircleCollider2D>().enabled = false;
       }
    }

    void OnMouseDrag()
    {
        //FollowMouse();
    }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
             
        Mouselistener.Instance.hoveredOverToken = this;
        //Debug.Log("Mouse is over GameObject.");
        SetHoverActive(true);
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        //Debug.Log("Mouse is no longer on GameObject.");
        SetHoverActive(false);
        Mouselistener.Instance.hoveredOverToken = null;
    }

    public bool isSelected;
    public bool SetHoverActive(bool state){
        hoverIcon.gameObject.SetActive(state);
        isHoveredOver = (state);
        return state;
    }

   
    public virtual void ShowInputField()
    {
        //UIManager.Instance.ShowInputField(transform.position, this);
    }

    public virtual void SetLabel(string label)
    {
        MyLabel.text = label;
    }
}
