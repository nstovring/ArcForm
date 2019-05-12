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
    public delegate void ClickAction();
    public event ClickAction OnClicked;
    [Header("Unitoken Settings")]
    public int arcCount;
    public float tokenRotation;
    public bool isInactive = true;
    public UnitokenDeleteButton deleteButton;


    [Header("Unitoken Refs")]
    public SpriteRenderer spriteRenderer;
    Camera mCamera;
    public Color DefaultColor;
    public Color HoverColor;
    [Header("Unitoken Collections")]
    public Dictionary<string, Property> myPropertiesFromConceptNet;
    public Dictionary<string, ArcCollection> myArcCollections;




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
        //myUnitokenStruct = token;

        myLabel.text = token.Label;
        this.TransientPosition = token.TransientPosition;
        //myType = Type.Unitoken;
        myArcs = new List<Arc>();
        mCamera = Camera.main;
    }

    public void Initialize(string label, Vector3 transientPosition, string uri){
        //myUnitokenStruct = new unitoken{URI = uri, TransientPosition = transientPosition, Label = label};
        myLabel.text = label;
        this.TransientPosition = transientPosition;
       // myType = Type.Unitoken;
        myArcs = new List<Arc>();
        mCamera = Camera.main;
    }

  
    internal void SetSprite(Sprite collectionIconSprite)
    {
        spriteRenderer.sprite = collectionIconSprite;
    }

    //void Update(){
    //  
    //   if(isInactive == false){//collider active and hoverOver is active.
    //    transform.GetComponent<CapsuleCollider2D>().enabled = true;
    //   }
    //   else{//collider disabled and dragging is occuring
    //    transform.GetComponent<CapsuleCollider2D>().enabled = false;
    //   }
    //}

    public bool SetActive(bool state)
    {
        return isInactive = state;
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
