using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConceptNetJsonHolder;
using UnityEngine.UI;
using ArcToolConstants;
public class ArcCollectionSubItem : MonoBehaviour{
    public Text text;
    public Button button;
    public Fragment myCore;
    public string mytopic;
    string label;
    public Edge edge;
  
    public ArcCollectionSubItem(Edge x)
    {
        this.edge = x;
        this.label = x.End.Label;
    }

    public void Refresh(ArcCollectionSubItem x, string topic){
        this.edge = x.edge;
        this.label = x.label;
        text.text = label;
        mytopic = topic;
    }

    public void PlaceOnMap(Fragment core)
    {
        //if (ArcMapManager.Instance.focusedToken == null)
        //{
        //    throw new MissingReferenceException();
        //}
        //Unitoken core = ArcMapManager.Instance.focusedToken;
        Unitoken target  = TokenFactory.Instance.AddNewToken(edge.End.Label, core.transform.position + StaticConstants.rngVector());
        //target.SetState(state);
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
            if (myCore == null)
            {
                throw new MissingReferenceException();
            }
            PlaceOnMap(myCore);
        });
    }
}