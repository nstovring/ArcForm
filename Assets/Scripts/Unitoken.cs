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
        //myPropertiesFromConceptNet = new Dictionary<string, Property>();
        if (myArcs == null){
            myArcs = new List<Arc>();
        }

        SetHoverActive(false);

        //for (int i = 0; i < 5; i++)
        //{
        //    string key = ArcToolConstants.StaticConstants.IndexToKey[i];
        //    Property p = new Property { Key = key };
        //    p.Relations = new List<Relation>();
        //    myPropertiesFromConceptNet.Add(key, p);
        //}

        //ArcToolUIManager.ArcUIUtility.UpdatePropertyMenuFromProperties(myPropertiesFromConceptNet);
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
    void OnMouseDown()
    {
        ArcMapGrid.Instance.RemoveFromMap(this);
        // load a new scene
        Debug.Log("Clicked");
    }

    void OnMouseUp()
    {
        MyCells = ArcMapGrid.Instance.FindEmptySpot(this, 3);
        transform.position = MyCells[1].DebugCube.center;
        Debug.Log("Clicked");
    }
    void OnMouseDrag()
    {
        if (Input.GetMouseButton(0))
        {
            FollowMouse();

        }
        else if (Input.GetMouseButton(1))
        {
            Debug.Log("Dragging right");
        }
        ArcMapManager.Instance.autoMoveInterrupt = true;
    }

    void FollowMouse()
    {
        Vector3 lastMousePos = mCamera.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector3(lastMousePos.x, lastMousePos.y,0);
        TransientPosition = transform.position;
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

    public void OnEnable()
    {
        spriteRenderer.color = DefaultColor;
    }
}
