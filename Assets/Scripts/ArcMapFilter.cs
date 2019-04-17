using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcMapFilter : MonoBehaviour
{
    
    string toggledCategories;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update(){
        toggledCategories = "";
        string[] relations = ConceptNetInterface.relationURIs;


        //Check if label is within toggled array
        for(int i = 0; i < relations.Length; i++){
        string edgeUnitokenLabel = relations[i];
        bool state = PropertyMenu.Instance.Filter[i];
        ConceptNetProperty c = PropertyMenu.Instance.GetProperty(edgeUnitokenLabel);
            if(state){
                //Debug.Log("")
                toggledCategories += edgeUnitokenLabel +": ";
            }
        }

    }
    // Update is called once per frame
    void OnGUI(){
        Event   currentEvent = Event.current;
        
        GUIStyle gsTest = new GUIStyle();
        gsTest.normal.textColor = Color.black;
        GUILayout.BeginArea(new Rect(20, 20, 450, 120),gsTest);
        GUILayout.Label("Toggled categories: " +toggledCategories, gsTest);
        GUILayout.EndArea();
    }
}
