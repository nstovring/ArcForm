using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConceptNetJsonHolder;
using UnityEngine.UI;
using ArcToolConstants;
using System;
using StructureContainer;
using UnityEngine.EventSystems;

public class ArcMenuSubItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    public Text text;
    public Button button;
    public Fragment myCore;
    public Fragment myUnitoken;
    public Fragment myArc;

    public ButtonStateHandler buttonStateHandler;   

    public string key;
    public string label;

    public Edge edge;
    public bool isActive = true;

    public ArcMenuItem arcCollectionItem;

    public enum ToggleState { Default, Hover, Selected, Edited }

    public ToggleState myState = ToggleState.Default;

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
        target.isInactive = false;
        Arc arc = ArcFactory.Instance.AddNewArc(core, "", target);
    }

    public void SetCore(Unitoken core)
    {
        myCore = core;
    }

    void Start(){
        text.text = label;
        

        button.onClick.AddListener(delegate{
            OnClick();
        });
    }

    public void OnClick()
    {
        SetActive(!isActive);
        Unitoken focus = ArcMapManager.Instance.GetFocusedToken();
        ArcToolUIManager.ArcDataUtility.SetRelation(focus, key, label, isActive);
        if (isActive)
        {
            //SetMenu Item To edited
            arcCollectionItem.buttonStateHandler.SetToEdited();
        }
        ArcToolUIManager.ArcUIUtility.UpdatePropertyMenuFromUnitoken(focus);
        ArcToolUIManager.Instance.ToggleSubMenuItem(this);

        DataLogger.Instance.LogToggle(this);
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        //throw new NotImplementedException();
        buttonStateHandler.SetToEdited();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!isActive)
            buttonStateHandler.SetToDefault();
    }
}