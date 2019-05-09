using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using StructureContainer;
using ArcToolConstants;

[RequireComponent(typeof(ArcMapLayout))]
[RequireComponent(typeof(ArcFactory))]
[RequireComponent(typeof(TokenFactory))]
[RequireComponent(typeof(ArcCollectionFactory))]
public class ArcMapManager : MonoBehaviour
{
    //This class is responsible for creating Tokens, Arcs and Thoughts
    [Header("Managers")]
    public static ArcMapManager Instance;
    private TokenFactory tokenFactory;
    private ArcFactory arcFactory;
    private ArcMapLayout arcMapLayout;
    [Header("Elements")]
    public List<Fragment> unitokens;
    public List<Fragment> Arcs;
    public List<Fragment> ArcCollections;

    [Header("Prefabs")]
    public Transform unitokenPrefab;
    public Transform joinArcPrefab;
   
    [Header("Settings")]
    public float linePadding = 0.2f;
    public float mapScale = 2.0f;
    public float zoomScale = 1;
    public int minZoom = 5;
    public int maxZoom = 50;

    [Header("Refs")]
    public Arc selectedArc;
    public Unitoken focusedToken;
    public Fragment selectedUnitoken;
    public ArcCollection selectedArcCollection;

    public Camera mCamera;

    [Header("Booleans")]
    public bool FlattenMap = false;
    public bool AutoMoveCamera = false;
    public bool debugCollection = true;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        unitokens = new List<Fragment>();
        Arcs = new List<Fragment>();

        if(arcMapLayout == null)
            arcMapLayout = GetComponent<ArcMapLayout>();

        if(tokenFactory == null)
            tokenFactory = GetComponent<TokenFactory>();

        if(arcFactory == null)
            arcFactory = GetComponent<ArcFactory>();

        StaticConstants.IntializeDictionaries();

        tokenFactory.Initialize();
        arcFactory.Initialize();
        mCamera = Camera.main;
    }

    internal Unitoken GetFocusedToken()
    {
        return focusedToken;
    }

    public bool AddTokenToList(Unitoken token){
        if(!unitokens.Contains(token)){
            token.id = unitokens.Count;
            unitokens.Add(token);
            return true;
        }else{
            Debug.Log("Failed to add token, token already in list");
            return false;
        }
    }

    internal Unitoken SetFocusedToken(Unitoken focus)
    {
        focusedToken = focus;
        return focusedToken;
    }

    internal ArcCollection GetFocusedCollection()
    {
        return selectedArcCollection;
    }

    internal ArcCollection SetFocusedCollection(ArcCollection focus)
    {
        selectedArcCollection = focus;
        return selectedArcCollection;
    }

    public bool AddArcToList(Arc arc){
         if(!Arcs.Contains(arc)){
            arc.id = Arcs.Count;
            Arcs.Add(arc);
            return true;
        }else{
            Debug.Log("Failed to add arc, arc already in list");
            return false;
        }
    }

    public void DestroyToken(Fragment token){
        unitokens.Remove(token);

        //if(token.my != null)
        //{
        //
        //}
        Unitoken t = (Unitoken)token;
        if(t.myArcCollections != null && t.myArcCollections.Count > 0)
        {
            foreach(string x in StaticConstants.RelationURIs)
            {
                ArcCollection ac;
                bool exists = t.myArcCollections.TryGetValue(x,out ac);
                if (exists)
                {
                    DestroyCollection(t.myArcCollections[x]);
                }
            }
        }

        if(token != null)
        Destroy(token.transform.gameObject);
    }

    public void DestroyArc(Arc arc){
        Arcs.Remove(arc);

        //DestroyToken(arc.source);
        DestroyToken(arc.target);

        Destroy(arc.gameObject);
    }

    public void DestroyCollection(Fragment ac)
    {
        ArcCollections.Remove(ac);
        if (unitokens.Contains(ac))
        {
            unitokens.Remove(ac);
        }

        if (ac == null)
            return;

        if(ac.myArcs != null)
        foreach(Arc a in ac.myArcs)
        {
            DestroyArc(a);
        }

        Destroy(ac.gameObject);
    }

    public void DestroyFragment(Fragment f)
    {
        if(f is Unitoken)
        {
            DestroyToken((Unitoken)f);
        }
        else if (f is Arc)
        {
            DestroyArc((Arc)f);
        }
        else if(f is ArcCollection)
        {
            DestroyCollection((ArcCollection)f);
        }
    }



    public void OnClickAddNewToken(){
        tokenFactory.AddNewToken();
    }
   
    public void SelectUnitoken(Fragment selected){
        //Deselect
        if(selectedUnitoken != null){
            selectedUnitoken.SetHoverActive(false);
            selectedUnitoken.isSelected = false;
        }
        //Select
        selectedUnitoken = selected;

        selectedUnitoken.SetHoverActive(true);
        selectedUnitoken.isSelected = true;
    }

    // Update is called once per frame
    void Update()
    {
        //MoveUnitoken();
        MoveMap();

        if(unitokens != null && unitokens.Count > 0){
            UpdateMap();
        }
        if(AutoMoveCamera)
            MoveCamera();
    }

    public bool autoMoveInterrupt = false;
    public float autoMoveCameraSpeed = 2.0f;
    void MoveCamera(){
        if(focusedToken != null && !autoMoveInterrupt)
        {
            Vector3 centerOffset = mCamera.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2,0))-(focusedToken.transform.position);
            Vector3 center = mCamera.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2,0));
            Vector3 focusedTokenPos = focusedToken.transform.position;
            Debug.DrawLine( new Vector3(Screen.width/2, Screen.height/2,0), mCamera.WorldToScreenPoint(focusedToken.transform.position));
            centerOffset.z = 0;
            focusedTokenPos.z = 0;
            float distance = Vector3.Distance(focusedTokenPos, center);
            if(distance > 0.1f){
                mCamera.transform.position -= centerOffset * Time.deltaTime * autoMoveCameraSpeed;
            }
        }
    }

 
    public void MoveMap(){
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(Mathf.Abs(h) > 0.01f || Mathf.Abs(v) > 0.01f)
        {
            autoMoveInterrupt = true;
        }

        float z = Input.mouseScrollDelta.y * zoomScale;
        float zoomFactor = Mathf.Clamp( mCamera.orthographicSize - z , minZoom, maxZoom);
        Vector3 mouseDelta = new Vector3(h,v,0);
        mCamera.orthographicSize = zoomFactor;
        mCamera.transform.position += mouseDelta;
    }

    public void ZoomMap(){

    }

    public void SaveMap(){
        ArcMapSaver.SaveMap(unitokens, Arcs);
    }

    public void LoadMap(){
        ArcMapSaver.LoadMap();
    }



    public void UpdateMap(){
        if(!FlattenMap){
            arcMapLayout.AddForces(unitokens, Arcs);
        }else{
            arcMapLayout.AddFlattenForces(unitokens,Arcs);
        }
   }

    public void ClearMap(){
        unitokens.Clear();

    }

    public void ToggleFlatten(){
        FlattenMap = !FlattenMap;
    }

    void OnGUI()
    {
        if (!debugCollection)
            return;


        List<string> debugString = new List<string>();
        if(focusedToken != null && focusedToken.myPropertiesFromConceptNet != null)
        {
            Dictionary<string, Property> focusedProperties = focusedToken.myPropertiesFromConceptNet;
            string debugLine = "p.Label" + " : " + "r.Label" + " : " + "r.isActive";
            debugString.Add(debugLine);
            foreach (string x in StaticConstants.RelationURIs)
            {
                Property p;
                focusedProperties.TryGetValue(x, out p);
                if(p.Relations != null && p.Relations.Count > 0)
                {
                    foreach(Relation r in p.Relations)
                    {
                        debugLine = p.Key +" : " + r.Label + " : " + r.isActive;
                        debugString.Add(debugLine);
                    }
                }
            }
        }

        GUIStyle gsTest = new GUIStyle();
        gsTest.normal.textColor = Color.black;
        GUILayout.BeginArea(new Rect(Screen.width - 250, Screen.height - 600, 500, 400), gsTest);
        foreach(string x in debugString)
        {
            GUILayout.Label(x, gsTest);
        }
        GUILayout.EndArea();



        debugString = new List<string>();
        if (focusedToken != null && focusedToken.myPropertiesFromConceptNet != null)
        {
            Dictionary<string, ArcMenuItem> PropertyMenuItems = ArcToolUIManager.ArcDataUtility.PropertyMenuItems;
            Dictionary<string, Property> focusedProperties = focusedToken.myPropertiesFromConceptNet;

            string debugLine = "p.key" + " : " + "p.myButtonToggle"+ " : " + "edited";
            debugString.Add(debugLine);

            foreach (string x in StaticConstants.RelationURIs)
            {
                ArcMenuItem p;
                Property f;
                focusedProperties.TryGetValue(x, out f);
                PropertyMenuItems.TryGetValue(x, out p);
                if (f.Relations != null && f.Relations.Count > 0)
                {
                    debugLine = p.key + " : Toggle State :" + p.buttonStateHandler.myState.ToString("F");
                    debugString.Add(debugLine);
                }
            }
        }
        gsTest.normal.textColor = Color.black;
        GUILayout.BeginArea(new Rect(Screen.width  - 250, Screen.height - 250, 250, 250), gsTest);
        foreach (string x in debugString)
        {
            GUILayout.Label(x, gsTest);
        }
        GUILayout.EndArea();
    }


}
