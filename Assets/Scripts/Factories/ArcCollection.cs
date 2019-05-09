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
    // Start is called before the first frame update
    void Start()
    {
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
        Unitoken target = TokenFactory.Instance.AddNewToken(item.text.text, ArcMapManager.Instance.GetAwayVector(this));
        target.deleteButton.OnClicked += item.OnClick;
        Arc arc = ArcFactory.Instance.AddNewArc(this, " ", target);
        arc.SetLabel(" ");

        item.SetConnections(target, arc, this);
        target.isInactive = false;
        target.transform.parent = arc.transform;
        arc.transform.parent = transform;

        myArcs.Add(arc);

        return this;
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
}
