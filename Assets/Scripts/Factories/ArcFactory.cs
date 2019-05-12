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
        
        //TokenFactory.Instance.AddNewToken(token);
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

       //source.transform.parent = arc.transform;
       //target.transform.parent = arc.transform;
       ArcMapManager.Instance.selectedUnitoken = target;
       return arc;
   }


    Arc CreateJoinArc(Fragment source, Fragment target){
        Arc newJoinArc = Instantiate(joinArcPrefab, Vector3.zero, Quaternion.identity, transform.parent).GetComponent<Arc>();
        newJoinArc.Initialize(source,target);

        ArcMapManager.Instance.AddArcToList(newJoinArc);
        return newJoinArc;
    }

}
