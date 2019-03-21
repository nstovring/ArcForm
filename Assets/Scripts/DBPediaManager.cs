using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VDS.RDF;

public class DBPediaManager : MonoBehaviour
{
    // Start is called before the first frame update

    public void Start(){
        try{
            GetGraph();
        }catch(Exception ex){
            Debug.Log(ex.Message);
        }
    }  
    string testURI ="http://dbpedia.org/resource/The_Lord_of_the_Rings";
    void GetGraph()
    {
        IGraph g = new Graph();
        //g.BaseUri = new Uri("http://example.org/");

        IUriNode dotNetRDF = g.CreateUriNode(UriFactory.Create("http://www.dotnetrdf.org"));
        IUriNode says = g.CreateUriNode(UriFactory.Create("http://example.org/says"));
        ILiteralNode helloWorld = g.CreateLiteralNode("Hello World");
        ILiteralNode bonjourMonde = g.CreateLiteralNode("Bonjour tout le Monde", "fr");
//
        g.Assert(new Triple(dotNetRDF, says, helloWorld));
        g.Assert(new Triple(dotNetRDF, says, bonjourMonde));

        Debug.Log(g.Triples.Count);
        //List<Triple>() triples = (List<Triple>())g.Triples;
        //for(int i= 0; i < g.Triples.Count){
        //    g.Triples.
            //Console.WriteLine(t.ToString());
        //}

        foreach (Triple t in g.Triples) 
        {
            //Console.WriteLine(t.ToString());
            Debug.Log(t.ToString());
        }
        //Console.ReadLine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
