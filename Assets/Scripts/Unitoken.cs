using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FragmentResources;
using StructureContainer;
using System;

public class Unitoken : Fragment
{
    public int arcCount;

    public float tokenRotation;

    public delegate void ClickAction();
    public event ClickAction OnClicked;

    public Transform softChild;

    public Transform hardChild;

    public SpriteRenderer spriteRenderer;
    public unitoken myUnitokenStruct;

    public Dictionary<string, Property> myPropertiesFromConceptNet;

    public Dictionary<string, ArcCollection> myArcCollections;

    public bool isInactive = true;


    public MapState myMapState = MapState.Preview;
    public InteractState myInteractState = InteractState.Unselected;

    public enum UnitokenState {Loaded, Preview, Placed, Removed}
    public enum UnitokenVisibility {Invisible, SemiVisible, Opaque}


    UnitokenState unitokenState;
    UnitokenVisibility unitokenVisibility;
    Camera mCamera;

    public Color DefaultColor;
    public Color HoverColor;

    public UnitokenDeleteButton deleteButton;

    // Start is called before the first frame update
    void Start()
    {
        mCamera = Camera.main;
        myArcCollections = new Dictionary<string, ArcCollection>();
        if (myArcs == null){
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

    void Update(){
      
       if(isInactive == false){//collider active and hoverOver is active.
        transform.GetComponent<CapsuleCollider2D>().enabled = true;
       }
       else{//collider disabled and dragging is occuring
        transform.GetComponent<CapsuleCollider2D>().enabled = false;
       }
    }

    public bool SetActive(bool state)
    {
        return isInactive = state;
    }

    public void UpdateUI()
    {

    }

    void OnMouseDrag()
    {
        //FollowMouse();
    }


    internal void StoreProperties(Dictionary<string, Property> properties)
    {
        myPropertiesFromConceptNet = properties;
    }

    internal Property GetProperty(string key)
    {
        Property property;
        myPropertiesFromConceptNet.TryGetValue(key, out property);
        return property;
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

    public override bool SetHoverActive(bool state)
    {
        if (state)
        {
            spriteRenderer.color = HoverColor;
        }
        else
        {
            spriteRenderer.color = DefaultColor;
        }
        return base.SetHoverActive(state);
    }



    public override void ShowInputField()
    {
        //UIManager.Instance.ShowInputField(transform.position, this);
    }

    public override void SetLabel(string label)
    {
        myLabel.text = label;
    }
}
