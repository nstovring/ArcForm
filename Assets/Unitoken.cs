using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Unitoken : Thought, ILabelable
{
//    RectTransform rectTransform;
  


    public Transform unitokenPrefab;
    public Transform joinArcPrefab;


    List<LineRenderer> lineList = new List<LineRenderer>();

    public Vector3 rotationVector;
    public float tokenRotation;

    public TextMeshPro myLabel;
    public TextMeshPro MyLabel => myLabel;

    public TMP_InputField InputField => ArcMapManager.Instance.inputField;

    //public bool TextInputToggle = false;
    Camera mCamera;
    // Start is called before the first frame update
    void Start()
    {
        ArcMapManager.AddToken(this);

        mCamera = Camera.main;

        if(myArcs == null){
            myArcs = new List<Arc>();
        }
        if(myTargets == null){
            myTargets = new List<Unitoken>();
        }
    }

    public void Initialize(){
        //mySources = new List<Unitoken>();
        myTargets = new List<Unitoken>();
        mCamera = Camera.main;
    }




    void OnMouseDown()
    {
        //Show options
        //Change label - Add labels
        //Create new join arc
        //
        if(Input.GetKey(KeyCode.Space)){
            AddNewToken();
        }
        else {
            ShowInputField();
        }
    }

    void OnMouseDrag()
    {
        Vector3 mouseWorldPos = mCamera.ScreenToWorldPoint(Input.mousePosition);
        float h = mouseWorldPos.x;
        float v = mouseWorldPos.y;
        Vector3 mouseDelta = new Vector3(h,v,0);
        transform.position = mouseDelta;
    }

    public void AddNewToken(){
        tokenRotation = (360.0f/(myTargets.Count + 1.0f));
        rotationVector = new Vector3(Mathf.Sin(tokenRotation * Mathf.Deg2Rad), Mathf.Cos(tokenRotation * Mathf.Deg2Rad), 0);
        Vector3 offset = rotationVector * ArcMapManager.Instance.mapScale;

        Unitoken newToken = Instantiate(unitokenPrefab, transform.position + offset, Quaternion.identity, transform.parent).GetComponent<Unitoken>();
        newToken.transform.name = "Unitoken";
        newToken.Initialize();

        CreateJoinArc(newToken);
    }

   
    void CreateJoinArc(Unitoken newToken){
        JoinArc newJoinArc = Instantiate(joinArcPrefab, Vector3.zero, Quaternion.identity, transform.parent).GetComponent<JoinArc>();
        newJoinArc.SetTokens(this,newToken);
        newJoinArc.ShowArc(ArcMapManager.Instance.linePadding);
    }

    public void AddArc(Arc arc){
        myArcs.Add(arc);
    }
    // Update is called once per frame
    void Update()
    {
       
      
    }

    void OnGUI(){
    }

    public virtual void ShowInputField()
    {
        ArcMapManager.Instance.ShowInputField(transform.position, this);
    }

    public virtual void SetLabel(string label)
    {
        MyLabel.text = label;
    }
}
