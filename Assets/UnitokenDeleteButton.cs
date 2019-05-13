using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitokenDeleteButton : MonoBehaviour
{
    // Start is called before the first frame update

    public delegate void ClickAction();
    public event ClickAction OnClicked;
    public Unitoken myToken;
    public Unitoken focus;
    public ButtonStateHandler buttonStateHandler;
    public SpriteRenderer rend;

    void Start()
    {
        
    }
    public string key;
    public string label;
    public void OnMouseDown()
    {
        DataLogger.Instance.LogAction("Delete token with mouse click");

        Unitoken mainToken = (Unitoken)myToken.Source.Source;
        ArcCollection myCollection = (ArcCollection)myToken.Source;

        ArcToolUIManager.ArcDataUtility.SetRelation(mainToken, key, label, false);

        ArcToolUIManager.ArcUIUtility.UpdatePropertyMenuFromUnitoken(mainToken);

        ArcToolUIManager.Instance.RemoveTokenFromCollection(mainToken, myCollection, myToken);
        //myCollection.RemoveFromCollection(myToken);
        //ArcToolUIManager.Instance.ToggleSubMenuItem(item);

        //OnClicked?.Invoke();
    }

    public Unitoken SourceToken;


    public void ClickTest()
    {
        //Unitoken focus = ArcMapManager.Instance.GetFocusedToken();
        //ArcToolUIManager.ArcDataUtility.SetRelation(SourceToken, myToken.key, label, isActive);
        //ArcToolUIManager.ArcUIUtility.UpdatePropertyMenuFromUnitoken(focus);
    }

    public void OnMouseOver()
    {
        rend.color = buttonStateHandler.SelectedColor;
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b,1);
        myToken.SetHoverActive(true);
    }

    public void OnMouseExit()
    {
        rend.color = buttonStateHandler.DefaultColor;
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
