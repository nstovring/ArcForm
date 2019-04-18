using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConceptNetJsonHolder;
using UnityEngine.UI;

public class ArcCollectionSubItem : MonoBehaviour{
    public Text text;
    public Button button;
    
    string label;
    private Edge edge;
  
    public ArcCollectionSubItem(Edge x)
    {
        this.edge = x;
        this.label = x.End.Label;
    }

    public void Refresh(ArcCollectionSubItem x){
        this.edge = x.edge;
        this.label = x.label;
        text.text = label;
    }

    public void PlaceOnMap(){
        if (ArcMapManager.Instance.focusedToken == null)
        {
            throw new MissingReferenceException();
        }
        Unitoken core = ArcMapManager.Instance.focusedToken;
        Unitoken target  = TokenFactory.Instance.AddNewToken(edge.End.Label, core.transform.position + ArcCollectionFactory.rngVector());
        //target.SetState(state);
        target.isSoft = false;
        Arc arc = ArcFactory.Instance.AddNewArc(core, "", target);
    }

   

    void Start(){
        text.text = label;

        button.onClick.AddListener(delegate{
            PlaceOnMap();
        });
    }
}