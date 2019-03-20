using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcMapSaver : MonoBehaviour
{
    // Start is called before the first frame update
    public struct unitoken{
        public int id;
        public Vector3 TransientPosition;
        public string Label;
    }

    public struct arc{
        public int id;
        public int source;
        public int target;
        public string Label;
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

    public static void LoadMap(){
        unitoken[] tokens = LoadUnitokens();
        arc[] arcs = LoadArcs();
        //Clear tokens?
        foreach(unitoken x in tokens){
            //x.transform.position = x.TransientPosition;
            ArcMapManager.Instance.AddNewToken(x);
        }

        foreach(arc x in arcs){
            //x.transform.position = x.TransientPosition;
            ArcMapManager.Instance.AddNewArc(x);
        }
    }

    public static void SaveMap(List<Unitoken> unitokens, List<Arc> arcs){
        SaveUnitokens(unitokens);
        SaveArcs(arcs);
    }
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

    public static arc[] LoadArcs(){


        arcLabels =PlayerPrefsX.GetStringArray(arcKeys[0]);
        sources = PlayerPrefsX.GetIntArray(arcKeys[1]);
        targets = PlayerPrefsX.GetIntArray(arcKeys[2]);

        arc[] loadedArcs = new arc[arcLabels.Length];

        for(int i = 0; i < loadedArcs.Length; i++){
            arc newArc = new arc();
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

    public static unitoken[] LoadUnitokens(){
        transientPositions = PlayerPrefsX.GetVector3Array(keys[0]);
        unitokenLabels = PlayerPrefsX.GetStringArray(keys[1]);


        Random.InitState(42);

        unitoken[] loadedTokens = new unitoken[transientPositions.Length];

        for(int i = 0; i < loadedTokens.Length; i++){
            unitoken newToken = new unitoken();
            newToken.Label = unitokenLabels[i];
            newToken.TransientPosition = transientPositions[i] + new Vector3(0,0, Random.Range(-1.0f, 1.0f));
            //newToken.Initialize(unitokenLabels[i],transientPositions[i]);

            loadedTokens[i] = newToken;

        }
        return loadedTokens;
    }
 
}
