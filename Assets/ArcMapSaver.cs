using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcMapSaver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static readonly string[] keys = {"unitokenTransientPositions", "unitokenLabels"};
    static readonly string[] arcKeys = {"label", "sourceID", "targetID"};

    //void SaveArc(Arc arc, string key){
    //    PlayerPrefsX.SetVector3Array(key + acn)
    static Vector3[] transientPositions;
    static string[] unitokenLabels;
    static string[] arcLabels;
    static int[] sources;
    static int[] targets;


    public static void SaveArcs(List<Arc> arcs){
        arcLabels = new string[arcs.Count-1];
        sources = new int[arcs.Count-1];
        targets = new int[arcs.Count-1];

        for(int i = 0; i < arcs.Count -1; i++){
            Arc arc = arcs[i];
            sources[i] = arc.source.id;
            targets[i] = arc.target.id;
            arcLabels[i] = arc.myLabel.text;
        }
       PlayerPrefsX.SetStringArray(arcKeys[0], arcLabels);
       PlayerPrefsX.SetIntArray(arcKeys[1], sources);
       PlayerPrefsX.SetIntArray(arcKeys[2], targets);

    }

    public static ArcMapManager.arc[] LoadArcs(){


        arcLabels =PlayerPrefsX.GetStringArray(arcKeys[0]);
        sources = PlayerPrefsX.GetIntArray(arcKeys[1]);
        targets = PlayerPrefsX.GetIntArray(arcKeys[2]);

        ArcMapManager.arc[] loadedArcs = new ArcMapManager.arc[arcLabels.Length];

        for(int i = 0; i < loadedArcs.Length; i++){
            ArcMapManager.arc newArc = new ArcMapManager.arc();
            newArc.Label = arcLabels[i];
            newArc.source = sources[i];
            newArc.target = targets[i];

            //newToken.Initialize(unitokenLabels[i],transientPositions[i]);

            loadedArcs[i] = newArc;

        }
        return loadedArcs;
    }
 
    public static void SaveUnitokens(List<Unitoken> unitokens){
        transientPositions = new Vector3[unitokens.Count-1];
        unitokenLabels = new string[unitokens.Count-1];

        for(int i = 0; i < unitokens.Count -1; i++){
            Unitoken token = unitokens[i];
            transientPositions[i] = token.TransientPosition;
            unitokenLabels[i] = token.MyLabel.text;
        }
       PlayerPrefsX.SetVector3Array(keys[0], transientPositions);
       PlayerPrefsX.SetStringArray(keys[1], unitokenLabels);

    }

    public static ArcMapManager.unitoken[] LoadUnitokens(){
        transientPositions = PlayerPrefsX.GetVector3Array(keys[0]);
        unitokenLabels = PlayerPrefsX.GetStringArray(keys[1]);

        ArcMapManager.unitoken[] loadedTokens = new ArcMapManager.unitoken[transientPositions.Length];

        for(int i = 0; i < loadedTokens.Length; i++){
            ArcMapManager.unitoken newToken = new ArcMapManager.unitoken();
            newToken.Label = unitokenLabels[i];
            newToken.TransientPosition = transientPositions[i];
            //newToken.Initialize(unitokenLabels[i],transientPositions[i]);

            loadedTokens[i] = newToken;

        }
        return loadedTokens;
    }
 
}
