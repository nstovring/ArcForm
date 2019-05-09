using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitokenDeleteButton : MonoBehaviour
{
    // Start is called before the first frame update

    public delegate void ClickAction();
    public event ClickAction OnClicked;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
