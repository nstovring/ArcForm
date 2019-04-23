using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConceptNetJsonHolder;
using UnityEngine.UI;
using ArcToolConstants;
using System;
using StructureContainer;

public class ArcCollectionSubItem : MonoBehaviour{
    public Text text;
    public Button button;
    public ButtonToggle buttonToggle;
    public Fragment myCore;
    public Fragment myUnitoken;
    public Fragment myArc;
    public string key;
    string label;
    public Edge edge;
    public bool isActive = false;
  
    public ArcCollectionSubItem(Edge x)
    {
        this.edge = x;
        this.label = x.End.Label;
    }

    public void Refresh(ArcCollectionSubItem x, string topic){
        this.edge = x.edge;
        this.label = x.label;
        text.text = label;
        key = topic;
    }

    public void Refresh(Relation x, string topic)
    {
        //this.edge = x.edge;
        this.label = x.Label;
        text.text = label;
        key = topic;
    }

    public void PlaceOnMap(Fragment core)
    {
        Unitoken target  = TokenFactory.Instance.AddNewToken(edge.End.Label, core.transform.position + StaticConstants.rngVector());
        target.isSoft = false;
        Arc arc = ArcFactory.Instance.AddNewArc(core, "", target);
    }

    public void SetCore(Unitoken core)
    {
        myCore = core;
    }

    void Start(){
        text.text = label;
        

        button.onClick.AddListener(delegate{
            isActive = !isActive;
            //Get Related ArcCollection item
            ArcCollectionItem aci = ArcCollectionToggleMenu.Instance.GetArcCollectionItem(this);
            if (isActive)
            {
                //If ArcCollection item .MyCollectionArc == Null
                if(aci.myArcCollection == null)
                {
                    // Add new Collection and add this item
                    aci.myArcCollection = ArcCollectionFactory.Instance.AddNewCollection(ArcMapManager.Instance.GetFocusedToken(), key, this);
                }
                else //Else Add to current collection
                {
                    aci.myArcCollection.AddToCollection(this);
                }

            }else
            {
               //if this is final subitem present in collection destroy the entire collection
               // else destroy my arc
               //if (aci.myArcCollection == null)
               //{
               //    // Add new Collection and add this item
               //    aci.myArcCollection = ArcCollectionFactory.Instance.AddNewCollection(ArcMapManager.Instance.GetFocusedToken(), mytopic, this);
               //}
               //else //Else Add to current collection
               //{
               //    aci.myArcCollection.AddToCollection(this);
               //}
               //ArcCollectionFactory.Instance.RemoveFromCollection(ArcMapManager.Instance.GetFocusedCollection(), this);
            }
           
        });
    }

    public void SetActive(bool active)
    {
        isActive = active;
    }

    internal void SetConnections(Unitoken target, Arc arc, ArcCollection ac)
    {
        myArc = arc;
        myCore = ac;
        myUnitoken = target;
    }
    internal void ClearConnections()
    {
        ArcMapManager.Instance.DestroyFragment(myArc);
        myArc = null;
        myCore = null;
        myUnitoken = null;
    }
}