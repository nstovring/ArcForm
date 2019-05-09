using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ColourBehaviour : MonoBehaviour
{   
    public ColorToken[] ColorTokens = new ColorToken[37];
    public SpriteRenderer mySpriteRenderer;
     // Default folder  
    static readonly string rootFolder = @"C:\Users\kjong\Github\ArcForm\Assets\Scripts\UI";  
    //Default file. MAKE SURE TO CHANGE THIS LOCATION AND FILE PATH TO YOUR FILE 
    static readonly string textFile = @"C:\Users\kjong\Github\ArcForm\Assets\Scripts\UI\HexColors.txt";  
      
    [System.Serializable]
    public struct ColorToken{

        public Color[] Colors;
                
    }

    [ContextMenu("Load")]
    void Load(){
      int count = 0;
    if (File.Exists(textFile))
    {
    // Read a text file line by line.
    string[] lines = File.ReadAllLines(textFile);
    for(int i = 0; i < ColorTokens.Length; i ++){
        ColorTokens[i].Colors = new Color[3];
        for(int j = 0; j < 3; j++){
            
            ColorTokens[i].Colors[j] = hexToRgb(lines[count]);
            count += 1;
            
            
        }
    }
    }
    }

    public static ColourBehaviour Instance;
    void Start(){
        Instance = this;
    }
    
    
    
    Color hexToRgb(string hex){
        Color myColor = new Color();
        ColorUtility.TryParseHtmlString (hex, out myColor);
        return myColor;
    }
    
}
    
