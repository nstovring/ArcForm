using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitokenDeleteButton : MonoBehaviour
{
    // Start is called before the first frame update

    public delegate void ClickAction();
    public event ClickAction OnClicked;
    public Unitoken myToken;
    public ButtonStateHandler buttonStateHandler;
    public SpriteRenderer rend;

    void Start()
    {
        
    }

    public void OnMouseDown()
    {
        OnClicked?.Invoke();
    }


    public void ClickTest()
    {
        Debug.Log("BUtton Clicked!");
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
