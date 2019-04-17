using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FragmentResources;
using StructureContainer;
using System;

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

    public enum UnitokenState {Loaded, Preview, Placed, Removed}
    public enum UnitokenVisibility {Invisible, SemiVisible, Opaque}

    public List<ArcCollection> Arcbranches;

    UnitokenState unitokenState;
    UnitokenVisibility unitokenVisibility;
    Camera mCamera;
    // Start is called before the first frame update
    void Start()
    {
        mCamera = Camera.main;

        if(myArcs == null){
            myArcs = new List<Arc>();
        }
        Arcbranches = new List<ArcCollection>();
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

    public void SetVisibility(UnitokenVisibility visibility){
         switch(visibility){
            case UnitokenVisibility.Invisible:
            spriteRenderer.color = new Color(1,1,1,0f);
            break;
            case UnitokenVisibility.Opaque:
            spriteRenderer.color = new Color(1,1,1,1);
            break;
            case UnitokenVisibility.SemiVisible:
            spriteRenderer.color = new Color(1,1,1,0.5f);
            break;
        }
        unitokenVisibility = visibility;
    }

    public void SetState(UnitokenState state){
        switch(state){
            case UnitokenState.Loaded:
            SetVisibility(UnitokenVisibility.Invisible);
            break;
            case UnitokenState.Placed:
            SetVisibility(UnitokenVisibility.Opaque);
            break;
            case UnitokenState.Preview:
            SetVisibility(UnitokenVisibility.SemiVisible);
            break;
            case UnitokenState.Removed:
            SetVisibility(UnitokenVisibility.Invisible);
            break;        
        }
        unitokenState = state;
    }

    void UpdateVisibility(){
        switch(unitokenVisibility){
            case UnitokenVisibility.Invisible:
            spriteRenderer.color = new Color(1,1,1,0f);
            break;
            case UnitokenVisibility.Opaque:
            spriteRenderer.color = new Color(1,1,1,1);
            break;
            case UnitokenVisibility.SemiVisible:
            spriteRenderer.color = new Color(1,1,1,0.5f);
            break;
        }
    }

    internal void SetSprite(Sprite collectionIconSprite)
    {
        spriteRenderer.sprite = collectionIconSprite;
    }

    internal void AddCollection(ArcCollection subBranch)
    {
        Arcbranches.Add(subBranch);
    }

    void Update(){
      
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
