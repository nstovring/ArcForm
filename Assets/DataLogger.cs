using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLogger : MonoBehaviour
{
    public string ParticipantNr;

    public float TestTime;
    public float TaskTime;
    public float ActionTime;
    public float InstructionTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnGUI()
    {

        List<string> debugString = new List<string>();
        GUIStyle gsTest = new GUIStyle();
        gsTest.normal.textColor = Color.black;
        GUILayout.BeginArea(new Rect(Screen.width - 250,0 , 250, 250), gsTest);
        foreach (string x in debugString)
        {
            GUILayout.Label(x, gsTest);
        }
        GUILayout.EndArea();
    }

}
