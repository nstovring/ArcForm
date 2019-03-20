﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcMapLayout : MonoBehaviour
{
    // Start is called before the first frame update
    //List<Arc> Arcs;
    //List<Unitoken> Unitokens;

    public List<Vector3> unitokenForceList;
    public List<Vector3> arcForceList;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddForces(List<Unitoken> unitokens, List<Arc> arcs){
        Vector3[] tokenforces = GetUnitokenForceVectors(unitokens);
        Vector3[] arcforces = GetArcForceVectors(arcs);

        for(int i = 0; i < tokenforces.Length; i++){
            unitokens[i].transform.position +=  tokenforces[i] * Time.deltaTime;
            unitokens[i].TransientPosition = unitokens[i].transform.position;
            Debug.DrawRay(unitokens[i].TransientPosition, tokenforces[i], Color.red);
            //Debug.DrawRay(Vector3.zero, forces[i] * 10, Color.red);
        }

        for(int i = 0; i < arcforces.Length; i++){
            Fragment source = arcs[i].source;
            Fragment target = arcs[i].target;
            source.transform.position +=  arcforces[i] * Time.deltaTime * 0.5f;
            source.TransientPosition = source.transform.position;

            target.transform.position +=  arcforces[i] * Time.deltaTime * 0.5f;
            target.TransientPosition = target.transform.position;
            //unitokens[i].transform.position +=  tokenforces[i] * Time.deltaTime;
            //unitokens[i].TransientPosition = unitokens[i].transform.position;
            Debug.DrawRay(arcs[i].TransientPosition, arcforces[i], Color.blue);
            //Debug.DrawRay(Vector3.zero, forces[i] * 10, Color.red);
        }
    }


    public Vector3[] GetUnitokenForceVectors(List<Unitoken> unitokens){
        Vector3[] forces = new Vector3[unitokens.Count];
        unitokenForceList = new List<Vector3>();
        for(int i = 0; i < unitokens.Count; i++){
            Unitoken token = unitokens[i];
            for(int j = 0; j < unitokens.Count; j++ ){
                Unitoken neighbour = unitokens[j];
                float distance = Vector3.Distance(token.TransientPosition,neighbour.TransientPosition);

                if(distance > 1 && distance < 6.0f){
                    Vector3 dir = (token.TransientPosition - neighbour.TransientPosition)/distance;
                    forces[i] += dir;
                }
            }
            unitokenForceList.Add(forces[i]);
        }
        return forces;
    }

    public Vector3[] GetArcForceVectors(List<Arc> arcs){
        Vector3[] forces = new Vector3[arcs.Count];
        arcForceList = new List<Vector3>();
        for(int i = 0; i < arcs.Count; i++){
            Arc token = arcs[i];
            for(int j = 0; j < arcs.Count; j++ ){
                Arc neighbour = arcs[j];
                float distance = Vector3.Distance(token.TransientPosition,neighbour.TransientPosition);

                if(distance > 1 && distance < 6.0f){
                    Vector3 dir = (token.TransientPosition - neighbour.TransientPosition)/distance;
                    forces[i] += dir;
                }
            }
            arcForceList.Add(forces[i]);
        }
        return forces;
    }
}
