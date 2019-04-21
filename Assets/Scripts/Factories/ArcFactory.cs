using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StructureContainer;
public class ArcFactory : MonoBehaviour
{
    public Transform joinArcPrefab;
    public static ArcFactory Instance;
    internal void Initialize()
    {
        Instance = this;
        //throw new NotImplementedException();
    }
    public void CollapseArc(){
        string label = ArcMapManager.Instance.selectedArc.Collapse();
        Vector3 center = ArcMapManager.Instance.selectedArc.TransientPosition;
        unitoken token = new unitoken();
        token.Label = label;
        token.TransientPosition = center;
        
        TokenFactory.Instance.AddNewToken(token);
    }


   public void AddNewArc(Unitoken source){
       Unitoken target = TokenFactory.Instance.AddNewToken();
       Arc arc = CreateJoinArc(source,target);
       source.transform.parent = arc.transform;
       target.transform.parent = arc.transform;
       ArcMapManager.Instance.selectedUnitoken = target;
       //Fragment frag = new GameObject("Fragment").AddComponent<Fragment>();
       //frag.Initialize(arc);

   }

    public Arc AddNewArc(Fragment source, string label, Fragment target)
    {
        Arc arc = CreateJoinArc(source, target);
        arc.SetLabel(label);
        ArcMapManager.Instance.selectedUnitoken = target;
        return arc;
    }


    public Arc AddNewArc(Unitoken Source, string Predicate, Unitoken Target){
       Unitoken source = Source;
       Unitoken target = Target;
       source.arcCount += 1;
       target.arcCount += 1;


       Arc arc = CreateJoinArc(source,target);

       arc.SetLabel(Predicate);

       source.transform.parent = arc.transform;
       target.transform.parent = arc.transform;
       ArcMapManager.Instance.selectedUnitoken = target;
       return arc;
   }


    public void AddNewArc(Unitoken Source, string Predicate, unitoken Target){
       //Unitoken target = AddNewToken();
       Unitoken source = Source;
       Unitoken target = TokenFactory.Instance.AddNewToken(Target);
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

        ArcMapManager.Instance.AddArcToList(newJoinArc);
        return newJoinArc;
    }

}
