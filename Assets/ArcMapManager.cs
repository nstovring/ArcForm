using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ArcMapManager : MonoBehaviour
{
    //This class is responsible for creating Tokens, Arcs and Thoughts
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
    public static ArcMapManager Instance;
    public List<Unitoken> unitokens;
    public List<Arc> Arcs;

    public Transform unitokenPrefab;
    public Transform joinArcPrefab;
    public TMP_InputField inputField;

    public float linePadding = 0.2f;
    public float mapScale = 2.0f;

    Camera mCamera;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        unitokens = new List<Unitoken>();
        Arcs = new List<Arc>();

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


    public void AddNewToken(unitoken token){
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

    public void CollapseArc(Arc arc){
        string label = "";
        Fragment source = arc.source;
        Fragment target = arc.target;
        List<Fragment> Left = new List<Fragment>();
        List<Fragment> Right = new List<Fragment>();
        while(source.myType == Fragment.Type.Arc){
            source = ((Arc)source).source;
            if(source.myType == Fragment.Type.Unitoken){
                label += source.name;
            }
        }
        while(target.myType == Fragment.Type.Arc){
            target = ((Arc)target).target;
            if(target.myType == Fragment.Type.Unitoken){
                label += target.name;
            }
        }
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


    public void AddNewArc(arc newArc){
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
        if(selectedUnitoken == null){
            selectedUnitoken = selected;
        }
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
                Debug.Log(mouseDelta);
            }
        }
        //MoveUnitoken();
    }

    public void SaveMap(){
        ArcMapSaver.SaveUnitokens(unitokens);
        ArcMapSaver.SaveArcs(Arcs);
    }

    public void LoadMap(){
        unitoken[] tokens = ArcMapSaver.LoadUnitokens();
        arc[] arcs = ArcMapSaver.LoadArcs();
        //Clear tokens?
        foreach(unitoken x in tokens){
            //x.transform.position = x.TransientPosition;
            AddNewToken(x);
        }

        foreach(arc x in arcs){
            //x.transform.position = x.TransientPosition;
            AddNewArc(x);
        }
    }

    public void ClearMap(){
        unitokens.Clear();

    }
}
