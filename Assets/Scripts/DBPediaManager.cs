using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Query.Datasets;
using VDS.RDF.Writing.Formatting;

public class DBPediaManager : MonoBehaviour
{
    // Start is called before the first frame update

    void Start(){
        Initialize();
        StartLoading();
    }  
    string testURI ="http://dbpedia.org/resource/The_Lord_of_the_Rings";
    string testDataset ="http://dig.csail.mit.edu/2008/webdav/timbl/foaf.rdf";

    public void Initialize(){
        subjects = new List<string>();
        _worker = new BackgroundWorker ();
        _worker.DoWork += (object sender, DoWorkEventArgs e) => Main();
    }


    public void Main()
	{
        TripleStore store = new TripleStore();
        
		//Create a Parameterized String
		SparqlParameterizedString queryString = new SparqlParameterizedString();

        InMemoryDataset ds = new InMemoryDataset(store, new Uri("http://dig.csail.mit.edu/2008/webdav/timbl/foaf.rdf"));

		//Add a namespace declaration
		queryString.Namespaces.AddNamespace("foaf", new Uri("http://xmlns.com/foaf/0.1/"));
		queryString.Namespaces.AddNamespace("card", new Uri("http://www.w3.org/People/Berners-Lee/card#"));

		//Set the SPARQL command
		//For more complex queries we can do this in multiple lines by using += on the
		//CommandText property
		//Note we can use @name style parameters here

		//Make a SELECT query against the Endpoint

        queryString.CommandText = "SELECT ?homepage WHERE { card:i foaf:knows ?known .";
        queryString.CommandText += "?known foaf:homepage ?homepage .";
        queryString.CommandText += "}";

        Unitoken uSource = ArcMapManager.Instance.tokenFactory.AddNewToken("Howard");

        SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://demo.openlinksw.com/sparql"), testDataset);

		
        //Get the Query processor
		ISparqlQueryProcessor processor = new RemoteQueryProcessor(endpoint);

		//When we call ToString() we get the full command text with namespaces appended as PREFIX
		//declarations and any parameters replaced with their declared values
		//Debug.Log(queryString.ToString());

		//We can turn this into a query by parsing it as in our previous example
        SparqlQueryParser sparqlparser = new SparqlQueryParser();
		SparqlQuery query = sparqlparser.ParseFromString(queryString);
		//results = processor.ProcessQuery(query);

        //g.ExecuteQuery(query);
        try{
            object results = processor.ProcessQuery(query);
            //SparqlResultSet res = (SparqlResultSet) results;

            if (results is SparqlResultSet)
		    {
                SparqlResultSet rset = (SparqlResultSet)results;

                string msg = rset.Count + " results:";

                //mSprqlResult.text += "\n" + msg;
                Debug.Log(msg);
		    	//Print out the Results
		    	  int cnt = 0;
                foreach (SparqlResult r in rset)
                {
                    cnt++;
                    msg = "#" + cnt + ": " + r.ToString();

                    string targetLabel = r.ToString();
                    ArcMapSaver.unitoken target = new ArcMapSaver.unitoken();
                    target.Label =targetLabel;
                    Unitoken utarget = ArcMapManager.Instance.tokenFactory.AddNewToken(target);

                    ArcMapManager.Instance.arcFactory.AddNewArc(uSource,"HomePages known",utarget);

                    Debug.Log(msg);
                    //mSprqlResult.text += "\n" + msg;
                    //Do whatever you want with each Result
                }
		    } else if (results is IGraph)
            {
                //CONSTRUCT/DESCRIBE queries give a IGraph
                IGraph resGraph = (IGraph)results;

                Debug.Log("\n" + "IGraph results:\n");
                Debug.Log(resGraph.IsEmpty);
                int cnt = 0;
                foreach (Triple t in resGraph.Triples)
                {
                    string msg = "Triple #" + ++cnt + ": " + t.ToString();
                    
                    //subjects.Add(t.Subject.ToString());
                    Debug.Log(msg);
                    //mSprqlResult.text += msg;
                    //Do whatever you want with each Triple
                }
            } else
            {
                //If you don't get a SparqlResutlSet or IGraph something went wrong 
                //but didn't throw an exception so you should handle it here
                string msg = "ERROR, or no results";
                Debug.Log(msg);
                //mSprqlResult.text += "\n" + msg;
            }
        }catch (RdfQueryException queryEx)
        {
            //There was an error executing the query so handle it here
            Debug.Log(queryEx.Message);
            //mSprqlResult.text += queryEx.Message     ;
        }

        
	}
    public List<string> subjects;
    BackgroundWorker _worker;
    Queue<Action> _actions = new Queue<Action>();

   

    // Update is called once per frame
    void Update()
    {
         lock (_actions) 
        {
            if( _actions.Count > 0 )
            {
                _actions.Dequeue().Invoke();
            }
        }
    }

      public void StartLoading()
    {
        _worker.RunWorkerAsync ();
    }
    void OnDestroy()
    {
        _worker.Dispose ();
    }
}
