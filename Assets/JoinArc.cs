﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinArc : Arc
{
   
    public Material lineMaterial;
    public Transform arrowSprite;
    LineRenderer myLine; 
    public EdgeCollider2D myEdgeCollider;

    public Vector3 newDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTokens(Unitoken source, Unitoken target){
        source.SetTarget(target);
        target.AddSource(source);
        this.source = source;
        this.target = target;
    }

    public float myPadding;
    public void ShowArc(float padding){
                myLine = new GameObject().AddComponent<LineRenderer>();
                Vector3[] points = new Vector3[2];


                points[0] = source.transform.position;
                points[1] = target.transform.position;

                Vector3 sourceToTarg = (points[1] - points[0]).normalized;
                points[0] += sourceToTarg * padding;
                points[1] -= sourceToTarg * padding;

                
                //Debug.Log(points[0] );
                

                myLine.SetPositions(points);
                myLine.material = lineMaterial;
                myLine.startColor = Color.black;
                myLine.endColor = Color.black;
                myLine.startWidth = 0.2f;
                myLine.endWidth = 0.2f;
                myLine.transform.name = "JoinArcLine";
                myLine.transform.parent = transform;

                myPadding = padding;

                UpdateArrowSprite(points[1], sourceToTarg);
                //lineList.Add(line);
                UpdateCollider(points);
                SetCenter();
                UpdateLabelPosition(center);

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

    public void SetCenter(){
        center = target.transform.position - (target.transform.position - source.transform.position)/2.0f;
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
        //if(Input.GetKey(KeyCode.Space)){
        //    AddNewToken();
        //}
        //else {
        ShowInputField();
        //}
        Debug.Log("Clicked");
        //Change Label
        //Request inputfield from Arcmapmanager
    }

    public void RefreshArc(){

                Vector3[] points = new Vector3[2];

                points[0] = source.transform.position;
                points[1] = target.transform.position;

                Vector3 sourceToTarg = (points[1] - points[0]).normalized;
                points[0] += sourceToTarg * ArcMapManager.Instance.linePadding;
                points[1] -= sourceToTarg * ArcMapManager.Instance.linePadding;

                myLine.SetPositions(points);
                UpdateCollider(points);
                SetCenter();
                UpdateLabelPosition(center);
                UpdateArrowSprite(points[1], sourceToTarg);
    }

    // Update is called once per frame
    void Update()
    {
        if(source != null && target != null)
            CheckHasChanged();
       
    }

    void CheckHasChanged(){
         if (source.transform.hasChanged || target.transform.hasChanged)
        {
            RefreshArc();
            source.transform.hasChanged = false;
            target.transform.hasChanged = false;
        }
    }
}
