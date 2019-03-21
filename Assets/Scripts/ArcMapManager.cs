using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(ArcMapLayout))]
public class ArcMapManager : MonoBehaviour
{
    //This class is responsible for creating Tokens, Arcs and Thoughts

    public static ArcMapManager Instance;
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
        AddNewToken();
    }
    public Unitoken AddNewToken(){
        Vector3 mouseWorldPos = mCamera.ScreenToWorldPoint(Input.mousePosition);
        float h = mouseWorldPos.x;
        float v = mouseWorldPos.y;
        Vector3 mouseDelta = new Vector3(h,v,0);
        //transform.position = mouseDelta;


        Unitoken newToken = Instantiate(unitokenPrefab, mouseDelta, Quaternion.identity, transform.parent).GetComponent<Unitoken>();
        newToken.transform.name = "Unitoken";
        newToken.Initialize("Label", mouseDelta);

        AddToken(newToken);

        return newToken;
    }


    public void AddNewToken(ArcMapSaver.unitoken token){
        Vector3 mouseWorldPos = mCamera.ScreenToWorldPoint(Input.mousePosition);
        float h = mouseWorldPos.x;
        float v = mouseWorldPos.y;
        Vector3 mouseDelta = new Vector3(h,v,0);
        //transform.position = mouseDelta;
        Unitoken newToken = Instantiate(unitokenPrefab, token.TransientPosition, Quaternion.identity, transform.parent).GetComponent<Unitoken>();

        newToken.transform.position = token.TransientPosition;
        newToken.transform.name = "Unitoken";
        newToken.Initialize(token.Label, token.TransientPosition);

        AddToken(newToken);

        //return newToken;
    }

    public Unitoken AddNewToken(Vector3 position){
        //source.tokenRotation = (360.0f/(source.myArcs.Count + 1.0f));
        //Vector3 rotationVector = new Vector3(Mathf.Sin(source.tokenRotation * Mathf.Deg2Rad), Mathf.Cos(source.tokenRotation * Mathf.Deg2Rad), 0);
        ///Vector3 offset = rotationVector * ArcMapManager.Instance.mapScale;

        Unitoken newToken = Instantiate(unitokenPrefab, position, Quaternion.identity, transform.parent).GetComponent<Unitoken>();
        newToken.transform.name = "Unitoken";
        newToken.Initialize("Label", position);

        AddToken(newToken);

        return newToken;

        //CreateJoinArc(source, newToken);
    }


    public void CollapseArc(){
        string label = selectedArc.Collapse();
        Vector3 center = selectedArc.TransientPosition;
        ArcMapSaver.unitoken token = new ArcMapSaver.unitoken();
        token.Label = label;
        token.TransientPosition = center;
        
        AddNewToken(token);
    }


   public void AddNewArc(Unitoken source){
       Unitoken target = AddNewToken();
       Arc arc = CreateJoinArc(source,target);
       source.transform.parent = arc.transform;
       target.transform.parent = arc.transform;
       selectedUnitoken = target;
       //Fragment frag = new GameObject("Fragment").AddComponent<Fragment>();
       //frag.Initialize(arc);

   }


    public void AddNewArc(ArcMapSaver.arc newArc){
       //Unitoken target = AddNewToken();
       Unitoken source = unitokens[newArc.source];
       Unitoken target = unitokens[newArc.target];

       Arc arc = CreateJoinArc(source,target);

       arc.SetLabel(newArc.Label);

       source.transform.parent = arc.transform;
       target.transform.parent = arc.transform;
       selectedUnitoken = target;
       Debug.Log("Creating arcs");


       //Fragment frag = new GameObject("Fragment").AddComponent<Fragment>();
       //frag.Initialize(arc);

   }
    Arc CreateJoinArc(Fragment source, Fragment target){
        Arc newJoinArc = Instantiate(joinArcPrefab, Vector3.zero, Quaternion.identity, transform.parent).GetComponent<Arc>();
        newJoinArc.Initialize(source,target);

        AddArc(newJoinArc);
        return newJoinArc;
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

                selectedUnitoken = AddNewToken(mouseDelta);
                SelectUnitoken(selectedUnitoken);
                Debug.Log(mouseDelta);
            }
        }

        if(Input.GetKeyUp(KeyCode.Space) && selectedUnitoken != null){
            //ArcMapManager.Instance.AddNewToken(this);
            AddNewArc(selectedUnitoken);
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
        //Vector3[] tokenforces = arcMapLayout.GetUnitokenForceVectors(unitokens);
        //Vector3[] arcforces = arcMapLayout.GetArcForceVectors(Arcs);
//
        //for(int i = 0; i < tokenforces.Length; i++){
        //    unitokens[i].transform.position +=  tokenforces[i] * Time.deltaTime;
        //    unitokens[i].TransientPosition = unitokens[i].transform.position;
        //    Debug.DrawRay(unitokens[i].TransientPosition, tokenforces[i], Color.red);
        //    //Debug.DrawRay(Vector3.zero, forces[i] * 10, Color.red);
        //}
//
        //for(int i = 0; i < arcforces.Length; i++){
        //    Fragment source = Arcs[i].source;
        //    Fragment target = Arcs[i].target;
        //    source.transform.position +=  arcforces[i] * Time.deltaTime * 0.5f;
        //    source.TransientPosition = source.transform.position;
//
        //    target.transform.position +=  arcforces[i] * Time.deltaTime * 0.5f;
        //    target.TransientPosition = target.transform.position;
        //    //unitokens[i].transform.position +=  tokenforces[i] * Time.deltaTime;
        //    //unitokens[i].TransientPosition = unitokens[i].transform.position;
        //    Debug.DrawRay(Arcs[i].TransientPosition, arcforces[i], Color.blue);
        //    //Debug.DrawRay(Vector3.zero, forces[i] * 10, Color.red);
        //}

   }

    public void ClearMap(){
        unitokens.Clear();

    }
}
