using System.Collections;
using System.Collections.Generic;
using ConceptNetJsonHolder;
using UnityEngine;

public class ArcCollection : Fragment{
        //Arc state and type and cetegory
        public Unitoken core;
        public string Label;
        List<Fragment> Connections;

        public void Initialize(){
            Connections = new List<Fragment>();
        }

        public void SetCore(Unitoken c){
            core = c;
        }

        public void AddConnection(Fragment f){
            Connections.Add(f);
        }

        public Arc AddEdge(Edge edge){
            if(Connections == null)
                Connections = new List<Fragment>();

            Vector3 rngVector = new Vector3(Random.Range(-2.0f, 2.0f),Random.Range(-2.0f, 2.0f));
            Unitoken target  = TokenFactory.Instance.AddNewToken(edge.End.Label, core.transform.position + rngVector);
            //target.SetState(state);
            target.isSoft = false;
            Arc arc = ArcFactory.Instance.AddNewArc(core, "", target);
            Connections.Add(arc);

            return arc;
        }
}