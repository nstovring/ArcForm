using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Arc : Fragment
{
    //public TextMeshPro MyLabel { get => myLabel;}


    public Material lineMaterial;
    public Transform arrowSprite;
    LineRenderer myLine; 
    public EdgeCollider2D myEdgeCollider;

    public Vector3 newDirection;
    public Vector3 center;

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

    public override void ShowInputField()
    {
        //UIManager.Instance.ShowInputField(TransientPosition, this);
    }
    public override void SetLabel(string label)
    {
        if(myLabel == null){
            throw new MissingReferenceException();
        }
        myLabel.text = label;
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
        
        this.source = source;
        this.target = target;
    }

    public float myPadding;
    public void ShowArc(float padding){
        myPadding = padding;
        Vector3[] points = InitializeLine();

        Vector3 sourceToTarg = (points[1] - points[0]).normalized;

        Vector3 closestTargetVector;
        Vector3 closestSourceVector;
        //if (target.GetType() == typeof(Unitoken))
        //{
        closestTargetVector = target.myCollider.ClosestPoint(source.TransientPosition);
        closestSourceVector = source.myCollider.ClosestPoint(target.TransientPosition);

        points[0] += closestTargetVector * padding;
        points[1] -= closestSourceVector * padding;

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
        center =  points[1] + (points[0] - points[1])/2.0f;

        myLine.SetPositions(points);
        //myLine.material = lineMaterial;
        //Color tempColor = myLine.endColor;
        //myLine.startColor = myLine.startColor;
        //myLine.endColor = tempColor;
        myLine.endWidth = ArcMapManager.Instance.arcEndWidth;
        myLine.startWidth = ArcMapManager.Instance.arcStartWidth;
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
  
    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
             
        Mouselistener.Instance.hoveredOverArc = this;
        //Debug.Log("Mouse is over GameObject.");
        SetHoverActive(true);
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        //Debug.Log("Mouse is no longer on GameObject.");
        SetHoverActive(false);
        Mouselistener.Instance.hoveredOverArc = null;
    }

    public float circlePadding1 = 0.8f;
    public float circlePadding2 = 0.8f;
    public void RefreshArc(){
        if(source.GetType() == typeof(ArcCollection))
        {
            myLine.endColor = ((ArcCollection)source).spriteRend.color;
        }


        Vector3[] points = new Vector3[2];
        if(myLine == null){
            points = InitializeLine();
        }else{
            myLine.GetPositions(points);
        }

        myLine.endWidth = ArcMapManager.Instance.arcEndWidth;
        myLine.startWidth = ArcMapManager.Instance.arcStartWidth;

        Vector3 closestTargetVector;
        Vector3 closestSourceVector;
        float targetPaddingOffset = 0;
        float sourcePaddingOffset = 0;
        if (target.myCollider.GetType() == typeof(CircleCollider2D))
        {
            closestTargetVector = target.TransientPosition;
            targetPaddingOffset = circlePadding1;
        }
        else
        {
            closestTargetVector = target.myCollider.ClosestPoint(source.TransientPosition);
            targetPaddingOffset = 0;
        }

        if (source.myCollider.GetType() == typeof(CircleCollider2D))
        {
            closestSourceVector = source.TransientPosition;
            sourcePaddingOffset = circlePadding2;
        }
        else
        {
            closestSourceVector = source.myCollider.ClosestPoint(target.TransientPosition);
            sourcePaddingOffset = 0;
        }

        points[0] = closestSourceVector;

        if(typeof(Arc) == target.GetType()){
            Arc arcSource = (Arc)target;
            points[1] = arcSource.center;

        }else{
            points[1] = closestTargetVector;
        }

        Vector3 sourceToTarg = (points[1] - points[0]).normalized;
        points[0] += sourceToTarg * (ArcMapManager.Instance.linePadding + sourcePaddingOffset);
        points[1] -= sourceToTarg * (ArcMapManager.Instance.linePadding + targetPaddingOffset);

        center =  points[1] + ( points[0] - points[1])/2.0f;
        
        myLine.SetPositions(points);
        UpdateCollider(points);
        SetTransientPosition();
        UpdateLabelPosition(TransientPosition);
        UpdateArrowSprite(points[0], sourceToTarg);

        DebugArc();
    }

    void DebugArc()
    {
        Vector3 closestTargetVector;
        Vector3 closestSourceVector;
        closestTargetVector = target.myCollider.ClosestPoint(source.TransientPosition);
        closestSourceVector = source.myCollider.ClosestPoint(target.TransientPosition);
        Debug.DrawLine(closestSourceVector, closestTargetVector);
        //}
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
