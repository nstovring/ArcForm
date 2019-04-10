using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Arc : Fragment, ILabelable
{
    public TextMeshPro myLabel;
    public TextMeshPro MyLabel { get => myLabel;}


    public Material lineMaterial;
    public Transform arrowSprite;
    LineRenderer myLine; 
    public EdgeCollider2D myEdgeCollider;

    public Vector3 newDirection;


    public Fragment source;
    public Fragment target;

    public enum ThoughtState {Folded, Unfolded};

    public bool isLabelled;
    public string Label   { get => GetThoughtLabel();}
    public Transform getTransform { get => transform;}

    public string GetThoughtLabel(){
        return "";
    }
    public virtual void Initialize(){
        myType = Type.Arc;
    }

    public virtual void ShowInputField()
    {
        //UIManager.Instance.ShowInputField(TransientPosition, this);
    }
    public virtual void SetLabel(string label)
    {
        if(MyLabel == null){
            throw new System.NotImplementedException();
        }
        MyLabel.text = label;
    }

    public string Collapse(){
       //myLabel.text = CollapseArc(this);
       return CollapseArc(this);
    }
    public string CollapseArc(Arc arc){
        string left = "";
        string right = "";

        if(source.myType == Fragment.Type.Arc){
            left += CollapseArc((Arc)source);
        }else{
            left += source.name;
        }
        if(target.myType == Fragment.Type.Arc){
            right += CollapseArc((Arc)target);
        }else{
            right += source.name;
        }
        //returns a unitoken which inherits the arcs and the label is concatenated
        return left + myLabel.text + right;
    }

 
    public void Initialize(Fragment source, Fragment target){
        SetTokens(source,target);
        ShowArc(ArcMapManager.Instance.linePadding);
    }

    public void SetTokens(Fragment source, Fragment target){
        
//        source.SetTarget(target);
//        target.AddSource(source);
        source.myArcs.Add(this);
        target.myArcs.Add(this);

        this.source = source;
        this.target = target;
    }

    public float myPadding;
    public void ShowArc(float padding){
                

                myPadding = padding;
                Vector3[] points = InitializeLine();

                Vector3 sourceToTarg = (points[1] - points[0]).normalized;
                points[0] += sourceToTarg * padding;
                points[1] -= sourceToTarg * padding;

                UpdateArrowSprite(points[1], sourceToTarg);
                //lineList.Add(line);
                UpdateCollider(points);
                SetTransientPosition();
                UpdateLabelPosition(TransientPosition);

    }

    Vector3[] InitializeLine(){
                myLine =  GetComponent<LineRenderer>();
                Vector3[] points = new Vector3[2];


                points[0] = source.transform.position;
                points[1] = target.transform.position;

               

                
                //Debug.Log(points[0] );
                

                myLine.SetPositions(points);
                myLine.material = lineMaterial;
                myLine.startColor = Color.black;
                myLine.endColor = Color.black;
                myLine.startWidth = 0.0f;
                myLine.endWidth = 0.2f;
                myLine.transform.name = "JoinArcLine";
                myLine.transform.parent = transform;

                return points;
    }
    public void UpdateArrowSprite(Vector3 pos, Vector3 dir){
        arrowSprite.position = pos;
        PointSpriteInDirection(dir, newDirection);
    }

    public void PointSpriteInDirection(Vector3 direction, Vector3 newDirection){
        float angle = Vector3.SignedAngle(newDirection, direction, Vector3.forward);
        arrowSprite.Rotate(0,0,angle, Space.World);
        this.newDirection = direction;
    }

    public void SetTransientPosition(){
        TransientPosition = target.transform.position - (target.transform.position - source.transform.position)/2.0f;
    }

    public void UpdateCollider(Vector3[] points){
        Vector2[] colliderPoints = {points[0], points[1]};
        myEdgeCollider.points = colliderPoints;
    }

    public void UpdateLabelPosition(Vector3 pos){
        myLabel.transform.position = pos;
    }

    void OnMouseDown()
    {
        ShowInputField();
        //}
        Debug.Log("Clicked");
        //Change Label
        //Request inputfield from Arcmapmanager
    }
    public Transform hoverIcon;
    public bool isHoveredOver = false;
    public bool SetHoverActive(bool state){
        hoverIcon.gameObject.SetActive(state);
        isHoveredOver = (state);
        return state;
    }
    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
             
        Mouselistener.Instance.hoveredOverArc = this;
        Debug.Log("Mouse is over GameObject.");
        SetHoverActive(true);
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
        SetHoverActive(false);
        Mouselistener.Instance.hoveredOverArc = null;
    }

    public void RefreshArc(){

                Vector3[] points = new Vector3[2];
                if(myLine == null){
                    points = InitializeLine();
                }else{
                    myLine.GetPositions(points);
                }
                

                points[0] = source.transform.position;
                points[1] = target.transform.position;

                Vector3 sourceToTarg = (points[1] - points[0]).normalized;
                points[0] += sourceToTarg * ArcMapManager.Instance.linePadding;
                points[1] -= sourceToTarg * ArcMapManager.Instance.linePadding;

               
                myLine.SetPositions(points);
                UpdateCollider(points);
                SetTransientPosition();
                UpdateLabelPosition(TransientPosition);
                UpdateArrowSprite(points[1], sourceToTarg);
    }

    // Update is called once per frame
    void Update()
    {
        RefreshArc();
        //if(source != null && target != null)
            //CheckHasChanged();
       
    }

    void CheckHasChanged(){
         if (target != null && target.transform.hasChanged)
        {
            RefreshArc();
            source.transform.hasChanged = false;
            target.transform.hasChanged = false;
        }
        if (source != null && source.transform.hasChanged)
        {
            RefreshArc();
            source.transform.hasChanged = false;
            target.transform.hasChanged = false;
        }


    }
  
}
