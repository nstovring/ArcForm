using System;
using System.Collections.Generic;
using JsonLD.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using NUnit.Framework;
using UnityEngine;
using UnityEngine.Assertions;
//using Xunit;

public class Deserialization : MonoBehaviour
{
    public static Deserialization Instance;
    void Start(){
        Instance = this;
    }

public partial class Person
    {
        [JsonProperty("@context")]
        public Uri[] Context { get; set; }

        [JsonProperty("@id")]
        public string Id { get; set; }

        [JsonProperty("edges")]
        public Edge[] Edges { get; set; }

        [JsonProperty("view")]
        public View View { get; set; }
    }

    public partial class Edge
    {
        [JsonProperty("@id")]
        public string Id { get; set; }

        [JsonProperty("dataset")]
        public string Dataset { get; set; }

        [JsonProperty("end")]
        public End End { get; set; }

        [JsonProperty("license")]
        public string License { get; set; }

        [JsonProperty("rel")]
        public Rel Rel { get; set; }

        [JsonProperty("sources")]
        public Source[] Sources { get; set; }

        [JsonProperty("start")]
        public End Start { get; set; }

        [JsonProperty("surfaceText")]
        public string SurfaceText { get; set; }

        [JsonProperty("weight")]
        public long Weight { get; set; }

        [JsonProperty("@context")]
        public Uri[] Context { get; set; }
    }

    public partial class End
    {
        [JsonProperty("@id")]
        public string Id { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("term")]
        public string Term { get; set; }
    }

    public partial class Rel
    {
        [JsonProperty("@id")]
        public string Id { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public partial class Source
    {
        [JsonProperty("@id")]
        public string Id { get; set; }

        [JsonProperty("activity")]
        public string Activity { get; set; }

        [JsonProperty("contributor")]
        public string Contributor { get; set; }
    }

    public partial class View
    {
        [JsonProperty("@id")]
        public string Id { get; set; }

        [JsonProperty("firstPage")]
        public string FirstPage { get; set; }

        [JsonProperty("nextPage")]
        public string NextPage { get; set; }

        [JsonProperty("paginatedProperty")]
        public string PaginatedProperty { get; set; }
    }

 //    public void Can_deserialize_with_existing_structure()
 //  {
 //      // given
 //      var json = JObject.Parse(@"
 //      {
 //          '@context': {
 //             'foaf': 'http://xmlns.com/foaf/0.1/',
 //             'name': 'foaf:name',
 //             'lastName': 'foaf:familyName',
 //             'Person': 'foaf:Person'
 //          },
 //          '@id': 'http://t-code.pl/#tomasz',
 //          '@type': 'Person',
 //          'name': 'Tomasz',
 //          'lastName': 'Pluskiewicz'
 //      }");

 //      // when
 //      IEntitySerializer serializer = new EntitySerializer(new StaticContextProvider());
 //      var person = serializer.Deserialize<Person>(json);

 //      // then
 //      Debug.Log(person.context);
 //      Debug.Log(person.LastName);
 //      Debug.Log(person.Id);
 //    
 //      Assert.AreEqual("Tomasz", person.Name);
 //      Assert.AreEqual("Pluskiewicz", person.LastName);
 //      Assert.AreEqual(new Uri("http://t-code.pl/#tomasz"), person.Id);
 //  }


//[Fact]
    public void Can_deserialize_with_existing_structure(string rJObject)
    {
        // given
        var json = JObject.Parse(rJObject);

        // when
        IEntitySerializer serializer = new EntitySerializer(new StaticContextProvider());
        var person = serializer.Deserialize<Person>(json);

        // then
        //Debug.Log(person.Name);
        //Debug.Log(person.LastName);
        //Debug.Log(person.Id);
          foreach(Edge x in person.Edges){
            Debug.Log(x.SurfaceText);
        }
        //Assert.AreEqual("Tomasz", person.Name);
        //Assert.AreEqual("Pluskiewicz", person.LastName);
        //Assert.AreEqual(new Uri("http://t-code.pl/#tomasz"), person.Id);
    }

}
