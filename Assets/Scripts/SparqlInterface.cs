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

using StructureContainer;
public class SparqlInterface : MonoBehaviour
{
    // Start is called before the first frame update
    public static SparqlInterface Instance;
    void Start(){
        Instance = this;
        Initialize();
        //StartLoading();
    }  
    string testURI ="http://dbpedia.org/resource/The_Lord_of_the_Rings";
    string testDataset ="http://dbpedia.org/data/Albert_Einstein.rdf";
    string defaultResourceURI ="<http://dbpedia.org/resource/Barack_Obama>";
    public List<predicate> myPredicates;
    public void Initialize(){
        subjects = new List<string>();
        //_worker = new BackgroundWorker ();
        //_worker.DoWork += (object sender, DoWorkEventArgs e) => Main();
        //myPredicates = GetPredicates(defaultResourceURI);
    }

 
    const int queryColCount = 2;
    public List<predicate> GetPredicates(string resourceURI){
        TripleStore store = new TripleStore();
        List<predicate> predicates = new List<predicate>();
	

 //   PREFIX dbpedia: <http://dbpedia.org/resource/>
 //   PREFIX foaf: <http://xmlns.com/foaf/0.1/>
 //   PREFIX dc: <http://purl.org/dc/elements/1.1/>
 //   PREFIX mo: <http://purl.org/ontology/mo/>

		//Create a Parameterized String

		SparqlParameterizedString queryString = new SparqlParameterizedString();
        //Add a namespace declaration

        queryString.Namespaces.AddNamespace("dbpedia", new Uri("http://dbpedia.org/resource/"));
        queryString.Namespaces.AddNamespace("foaf", new Uri(" http://xmlns.com/foaf/0.1/"));
        queryString.Namespaces.AddNamespace("dc", new Uri("http://purl.org/dc/elements/1.1/"));
        queryString.Namespaces.AddNamespace("mo", new Uri("http://purl.org/ontology/mo/"));
        queryString.Namespaces.AddNamespace("rdf", new Uri("http://www.w3.org/1999/02/22-rdf-syntax-ns#"));

        Debug.Log("Added NameSpaces");


        /* queryString.CommandText = " CONSTRUCT { ?album dc:creator dbpedia:The_Beatles .";
        queryString.CommandText += "  ?track dc:creator dbpedia:The_Beatles . }";
        queryString.CommandText += " WHERE { dbpedia:The_Beatles foaf:made ?album .";
        queryString.CommandText += " ?album mo:record ?record .";
        queryString.CommandText += "?record mo:track ?track . }";
         */
        
        string strSELECT = "SELECT";
        string strDISTINCT = "DISTINCT";

        queryString.CommandText = "SELECT DISTINCT ?property ?value WHERE {<"+resourceURI+"> ?property ?value . filter ( ?property in ( rdf:type) ) } LIMIT 5";

        SparqlQueryParser sparqlparser = new SparqlQueryParser();
		SparqlQuery query = sparqlparser.ParseFromString(queryString);
        Debug.Log("Created query string");

        SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("https://dbpedia.org/sparql"), "http://dbpedia.org");
        Debug.Log("Created Endpoint");

        //Get the Query processor
		ISparqlQueryProcessor processor = new RemoteQueryProcessor(endpoint);
        Debug.Log("Created Processor");


         try{
            object results = processor.ProcessQuery(query);
            Debug.Log("Processing Query");

            if (results is SparqlResultSet)
		    {
                SparqlResultSet rset = (SparqlResultSet)results;

                string msg = rset.Count + " results:";

                Debug.Log(msg);
		    	//Print out the Results
		    	  int cnt = 0;
                foreach (SparqlResult r in rset)
                {
                    cnt++;
                    msg = "#" + cnt + ": " + r.ToString();
                    //r.Variables

                    string targetLabel = r.ToString();
                    List<string> values = new List<string>();
                    foreach(string x in r.Variables){
                        values.Add(r.Value(x).ToString());
                    }
                    predicate predicate = new predicate{property = values[1], value = values[0]};
                    predicates.Add(predicate);
                    //Debug.Log(values.Count);
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
                    
                    Debug.Log(msg);
                    //Do whatever you want with each Triple
                }
            } else
            {
                //If you don't get a SparqlResutlSet or IGraph something went wrong 
                //but didn't throw an exception so you should handle it here
                string msg = "ERROR, or no results";
                Debug.Log(msg);
            }
        }catch (RdfQueryException queryEx)
        {
            //There was an error executing the query so handle it here
            Debug.Log(queryEx.Message);
            if (queryEx.InnerException != null)
            Console.WriteLine("Inner exception: {0}", queryEx.InnerException);
      
        }

		return predicates;
    }

 
    public void DebugResults(object results){
          try{
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
                    //ArcMapSaver.unitoken target = new ArcMapSaver.unitoken();
                    //target.Label =targetLabel;
                    //Unitoken utarget = ArcMapManager.Instance.tokenFactory.AddNewToken(target);

                    //ArcMapManager.Instance.arcFactory.AddNewArc(uSource,"HomePages known",utarget);

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
        // lock (_actions) 
        //{
        //    if( _actions.Count > 0 )
        //    {
        //        _actions.Dequeue().Invoke();
        //    }
        //}
    }

    public void StartLoading()
    {
        //_worker.RunWorkerAsync ();
    }
    void OnDestroy()
    {
        //_worker.Dispose ();
    }
}
