using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConceptNetJsonHolder;
using UnityEngine.UI;
using ArcToolConstants;
using System;
using StructureContainer;

public class ArcMenuSubItem : MonoBehaviour{
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

    public ArcMenuItem arcCollectionItem;

    public ArcMenuSubItem(Edge x)
    {
        this.edge = x;
        this.label = x.End.Label;
    }

    public void Refresh(ArcMenuSubItem x, string topic){
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
        target.isActive = false;
        Arc arc = ArcFactory.Instance.AddNewArc(core, "", target);
    }

    public void SetCore(Unitoken core)
    {
        myCore = core;
    }

    void Start(){
        text.text = label;
        

        button.onClick.AddListener(delegate{
            SetActive(!isActive);
            //Property focus = ArcMapManager.Instance.GetFocusedToken().GetProperty(key);
            //Find relation
            int count = 0;
            //Get Unitoken Connected unitoken
            Unitoken focus = ArcMapManager.Instance.GetFocusedToken();
            ArcToolUIManager.ArcDataUtility.SetRelation(ArcMapManager.Instance.GetFocusedToken(), key, label, isActive);
            ArcToolUIManager.ArcUIUtility.UpdatePropertyMenuFromUnitoken(focus);
            ArcToolUIManager.Instance.ToggleSubMenuItem(this);
        });
    }


    public void SetActive(bool active)
    {
        isActive = active;
        buttonToggle.Toggle(active);
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