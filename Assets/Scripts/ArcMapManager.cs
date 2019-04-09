using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(ArcMapLayout))]
[RequireComponent(typeof(ArcFactory))]
[RequireComponent(typeof(TokenFactory))]
public class ArcMapManager : MonoBehaviour
{
    //This class is responsible for creating Tokens, Arcs and Thoughts
    [Header("Managers")]
    public static ArcMapManager Instance;
    private TokenFactory tokenFactory;
    private ArcFactory arcFactory;
    private ArcMapLayout arcMapLayout;
    [Header("Elements")]
    public List<Unitoken> unitokens;
    public List<Arc> Arcs;

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
    public Unitoken selectedUnitoken;

    public Camera mCamera;

    [Header("Booleans")]
    public bool FlattenMap = false;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        unitokens = new List<Unitoken>();
        Arcs = new List<Arc>();

        if(arcMapLayout == null)
            arcMapLayout = GetComponent<ArcMapLayout>();

        if(tokenFactory == null)
            tokenFactory = GetComponent<TokenFactory>();

        if(arcFactory == null)
            arcFactory = GetComponent<ArcFactory>();

        tokenFactory.Initialize();
        arcFactory.Initialize();
        mCamera = Camera.main;
    }


    public bool AddToken(Unitoken token){
        if(!unitokens.Contains(token)){
            token.id = unitokens.Count;
            unitokens.Add(token);
            return true;
        }else{
            Debug.Log("Failed to add token");
            return false;
        }
    }

    public bool AddArc(Arc arc){
         if(!Arcs.Contains(arc)){
            arc.id = Arcs.Count;
            Arcs.Add(arc);
            return true;
        }else{
            Debug.Log("Failed to add arc");
            return false;
        }
    }
    public void OnClickAddNewToken(){
        tokenFactory.AddNewToken();
    }
   
    public void SelectUnitoken(Unitoken selected){
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
    }

    public void RemoveToken(Unitoken token){
        unitokens.Remove(token);
        Destroy(token.transform.gameObject);
    }

    public void RemoveArc(Arc arc){
        Arcs.Remove(arc);
        Destroy(arc);
    }
 
    public void MoveMap(){
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
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

  


}
