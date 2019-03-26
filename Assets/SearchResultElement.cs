using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchResultElement : MonoBehaviour
{
    // Start is called before the first frame update
    public Button elementButton;
    public Text elementText;
    void Start()
    {
        if(elementButton == null)
            throw new UnassignedReferenceException();
            
        if(elementText == null)
            throw new UnassignedReferenceException();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
