using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcFactory : MonoBehaviour
{
    public Transform joinArcPrefab;

    internal void Initialize()
    {
        //throw new NotImplementedException();
    }
    public void CollapseArc(){
        string label = ArcMapManager.Instance.selectedArc.Collapse();
        Vector3 center = ArcMapManager.Instance.selectedArc.TransientPosition;
        ArcMapSaver.unitoken token = new ArcMapSaver.unitoken();
        token.Label = label;
        token.TransientPosition = center;
        
        ArcMapManager.Instance.tokenFactory.AddNewToken(token);
    }


   public void AddNewArc(Unitoken source){
       Unitoken target = ArcMapManager.Instance.tokenFactory.AddNewToken();
       Arc arc = CreateJoinArc(source,target);
       source.transform.parent = arc.transform;
       target.transform.parent = arc.transform;
       ArcMapManager.Instance.selectedUnitoken = target;
       //Fragment frag = new GameObject("Fragment").AddComponent<Fragment>();
       //frag.Initialize(arc);

   }


    public void AddNewArc(ArcMapSaver.arc newArc){
       //Unitoken target = AddNewToken();
       Unitoken source = ArcMapManager.Instance.unitokens[newArc.source];
       Unitoken target = ArcMapManager.Instance.unitokens[newArc.target];

       Arc arc = CreateJoinArc(source,target);

       arc.SetLabel(newArc.Label);

       source.transform.parent = arc.transform;
       target.transform.parent = arc.transform;
       ArcMapManager.Instance.selectedUnitoken = target;
       Debug.Log("Creating arcs");


       //Fragment frag = new GameObject("Fragment").AddComponent<Fragment>();
       //frag.Initialize(arc);

   }



    public void AddNewArc(Unitoken Source, string Predicate, Unitoken Target){
       //Unitoken target = AddNewToken();
       Unitoken source = Source;
       Unitoken target = Target;
       source.arcCount += 1;
       target.arcCount += 1;


        source.tokenRotation = (360.0f/(source.arcCount + 1.0f));
        Vector3 rotationVector = new Vector3(Mathf.Sin(source.tokenRotation * Mathf.Deg2Rad), Mathf.Cos(source.tokenRotation * Mathf.Deg2Rad), 0);
        Vector3 offset = rotationVector * ArcMapManager.Instance.mapScale;

        target.transform.position += offset;
       Arc arc = CreateJoinArc(source,target);

       arc.SetLabel(Predicate);

       source.transform.parent = arc.transform;
       target.transform.parent = arc.transform;
       ArcMapManager.Instance.selectedUnitoken = target;
       Debug.Log("Creating arcs");


       //Fragment frag = new GameObject("Fragment").AddComponent<Fragment>();
       //frag.Initialize(arc);

   }
    Arc CreateJoinArc(Fragment source, Fragment target){
        Arc newJoinArc = Instantiate(joinArcPrefab, Vector3.zero, Quaternion.identity, transform.parent).GetComponent<Arc>();
        newJoinArc.Initialize(source,target);

        ArcMapManager.Instance.AddArc(newJoinArc);
        return newJoinArc;
    }

}
