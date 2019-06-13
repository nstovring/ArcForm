using ArcToolConstants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArcCollection : Fragment
{
    public int CollectionType;
    public SpriteRenderer spriteRend;


    
    public void SetType(int type)
    {
        CollectionType = type;
        UpdateSpriteColor();
        Debug.Log("Setting ColletionType : " + CollectionType);
    }

    [ContextMenu("UpdateSpriteColor")]
    public void UpdateSpriteColor()
    {
        spriteRend.color = ColourBehaviour.Instance.ColorTokens[CollectionType].Colors[1];
    }

    public Camera mCamera;
    // Start is called before the first frame update
    void Start()
    {
        mCamera = Camera.main;
        if (myArcs == null)
        {
            myArcs = new List<Arc>();
        }
    }

    public ArcCollection AddToCollection(List<ArcMenuSubItem> subItems)
    {
        //Add items to collection
        foreach (ArcMenuSubItem x in subItems)
        {
            AddToCollection(x);
        }
        return this;
    }




    public ArcCollection AddToCollection(ArcMenuSubItem item)
    {
        //Unitoken target = TokenFactory.Instance.AddNewToken(item.text.text, ArcMapManager.Instance.GetAwayVector(this));
        Unitoken target = TokenFactory.Instance.AddNewToken(item.text.text, transform.position);

        target.deleteButton.OnClicked += item.OnClick;
        target.Source = this;
        target.deleteButton.key = item.key;
        target.deleteButton.label = item.label;

        Arc arc = ArcFactory.Instance.AddNewArc(this, " ", target);
        arc.SetLabel(" ");
        target.DefaultColor = item.buttonStateHandler.EditedColor;
        target.HoverColor = item.buttonStateHandler.DefaultColor;
        //target.SetColorToken(ColourBehaviour.Instance.ColorTokens[CollectionType]);
        target.spriteRenderer.color = spriteRend.color;
        item.SetConnections(target, arc, this);
        target.isInactive = false;
        target.transform.parent = arc.transform;
        arc.transform.parent = transform;

        myArcs.Add(arc);

        return this;
    }

    public void OnMouseEnter()
    {
        spriteRend.color = ColourBehaviour.Instance.ColorTokens[CollectionType].Colors[0];
        hoverIcon.GetComponent<SpriteRenderer>().color = spriteRend.color;
    }

    public void OnMouseExit()
    {
        spriteRend.color = ColourBehaviour.Instance.ColorTokens[CollectionType].Colors[1];
        hoverIcon.GetComponent<SpriteRenderer>().color = Color.black;
    }

    public void OnMouseDown()
    {
        Mouselistener.Instance.ExecuteQuery((Unitoken)Source);
        ArcMapGrid.Instance.RemoveFromMap(this);
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

        transform.position = new Vector3(lastMousePos.x, lastMousePos.y, 0);
        TransientPosition = transform.position;
    }

    public ArcCollection RemoveFromCollection(ArcMenuSubItem item)
    {
        string label = item.label;
        Arc temp = null;
        foreach(Arc arc in myArcs)
        {
            if(arc.target.myLabel.text == label)
            {
                temp = arc;
            }
        }

        if(temp != null)
        {
            myArcs.Remove(temp);
            ArcMapManager.Instance.DestroyArc(temp);
            item.ClearConnections();
        }
        return this;
    }


    public ArcCollection RemoveFromCollection(Unitoken item)
    {
        string label = item.myLabel.text;
        Arc temp = null;
        foreach (Arc arc in myArcs)
        {
            if (arc.target.myLabel.text == label)
            {
                temp = arc;
            }
        }

        if (temp != null)
        {
            myArcs.Remove(temp);
            ArcMapManager.Instance.DestroyArc(temp);
            //item.ClearConnections();
        }
        return this;
    }
}
