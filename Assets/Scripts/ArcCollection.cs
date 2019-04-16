using System.Collections;
using System.Collections.Generic;
using ConceptNetJsonHolder;
using UnityEngine;

public class ArcCollection : Fragment{
        //Arc state and type and cetegory
        public Unitoken core;
        public string Label;
        List<Arc> arcs;
        List<List<Edge>> ConceptEdges;
        List<ArcCollection> Branches;

        public void Initialize(){
            arcs = new List<Arc>();
            Branches = new List<ArcCollection>();
        }

        public void SetCore(Unitoken c){
            core = c;
        }

        public ArcCollection AddBranch(ArcCollection arcBranch){
            if(arcs == null)
                arcs = new List<Arc>();

            //Create connection from core to core
            Branches.Add(arcBranch);

            //Vector3 rngVector = new Vector3(Random.Range(-2.0f, 2.0f),Random.Range(-2.0f, 2.0f));
            //Unitoken target  = TokenFactory.Instance.AddNewToken(arcBranch.Label, core.transform.position + rngVector);
            ////target.SetState(state);
            ////target.isSoft = false;
            //Arc arc = ArcFactory.Instance.AddNewArc(core, "", target);
            //arcs.Add(arc);

            return arcBranch;
        }

        public Arc AddEdge(Edge edge){
            if(arcs == null)
                arcs = new List<Arc>();

            Vector3 rngVector = new Vector3(Random.Range(-2.0f, 2.0f),Random.Range(-2.0f, 2.0f));
            Unitoken target  = TokenFactory.Instance.AddNewToken(edge.End.Label, core.transform.position + rngVector);
            //target.SetState(state);
            target.isSoft = false;
            Arc arc = ArcFactory.Instance.AddNewArc(core, "", target);
            arcs.Add(arc);

            return arc;
        }
}