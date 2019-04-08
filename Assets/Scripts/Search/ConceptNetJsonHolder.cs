using System;
using System.Collections.Generic;
using JsonLD.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using NUnit.Framework;
using UnityEngine;
using UnityEngine.Assertions;
//using Xunit;

namespace ConceptNetJsonHolder
{
 
public partial class Concept
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


 

}
