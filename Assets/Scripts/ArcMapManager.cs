﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(ArcMapLayout))]
public class ArcMapManager : MonoBehaviour
{
    //This class is responsible for creating Tokens, Arcs and Thoughts

    public static ArcMapManager Instance;
    public TokenFactory tokenFactory;
    public ArcFactory arcFactory;
    public ArcMapLayout arcMapLayout;
    public List<Unitoken> unitokens;
    public List<Arc> Arcs;

    public Transform unitokenPrefab;
    public Transform joinArcPrefab;
    public TMP_InputField inputField;

    public float linePadding = 0.2f;
    public float mapScale = 2.0f;

    public Arc selectedArc;

    Camera mCamera;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        unitokens = new List<Unitoken>();
        Arcs = new List<Arc>();
        
        arcMapLayout = GetComponent<ArcMapLayout>();
        if(tokenFactory == null)
            tokenFactory = GetComponent<TokenFactory>();

        if(arcFactory == null)
            arcFactory = GetComponent<ArcFactory>();

        tokenFactory.Initialize();
        arcFactory.Initialize();
        mCamera = Camera.main;
    }

    public void ShowInputField(Vector3 worldPos, ILabelable asker){

        inputField.transform.gameObject.SetActive(true);

        inputField.onEndEdit.RemoveAllListeners();


        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPos);
        RectTransform rectTransform =  inputField.GetComponent<RectTransform>();
        rectTransform.position = screenPos;

        inputField.onEndEdit.AddListener(delegate{
            asker.SetLabel(inputField.text);
            inputField.transform.gameObject.SetActive(false);
        });

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
            Debug.Log("Failed to add token");
            return false;
        }
    }
    public void OnClickAddNewToken(){
        tokenFactory.AddNewToken();
    }
   


    public Unitoken selectedUnitoken;
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
    public void MoveUnitoken(){
        if(selectedUnitoken != null){
            FollowMouse(selectedUnitoken.transform);
            if(Input.GetMouseButtonUp(0)){
                selectedUnitoken = null;
            }
        }
    }

    public void FollowMouse(Transform trs){
        Vector3 mouseWorldPos = mCamera.ScreenToWorldPoint(Input.mousePosition);
        float h = mouseWorldPos.x;
        float v = mouseWorldPos.y;
        Vector3 mouseDelta = new Vector3(h,v,0);
        trs.position = mouseDelta;
    }
 

    // Update is called once per frame
    void Update()
    {
        if(selectedUnitoken == null){
            if(Input.GetMouseButtonUp(1)){
                 Vector3 mouseWorldPos = mCamera.ScreenToWorldPoint(Input.mousePosition);
                float h = mouseWorldPos.x;
                float v = mouseWorldPos.y;
                Vector3 mouseDelta = new Vector3(h,v,0);

                selectedUnitoken = tokenFactory.AddNewToken(mouseDelta);
                SelectUnitoken(selectedUnitoken);
                Debug.Log(mouseDelta);
            }
        }

        if(Input.GetKeyUp(KeyCode.Space) && selectedUnitoken != null){
            //ArcMapManager.Instance.AddNewToken(this);
            arcFactory.AddNewArc(selectedUnitoken);
        }
        //MoveUnitoken();
        MoveMap();

        if(unitokens != null && unitokens.Count > 0){
            UpdateMap();
        }
    }

    public float zoomScale = 1;

    public void MoveMap(){
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float z = Input.mouseScrollDelta.y * zoomScale;
        Vector3 mouseDelta = new Vector3(h,v,z);
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


    public bool FlattenMap = false;
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
}
