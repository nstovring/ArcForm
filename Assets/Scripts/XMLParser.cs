﻿   /* 
    Licensed under the Apache License, Version 2.0
    
    http://www.apache.org/licenses/LICENSE-2.0
    */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

namespace Xml2CSharp
{

    public class XMLParser : MonoBehaviour
    {
        string testPath = "http://lookup.dbpedia.org/api/search.asmx/PrefixSearch?QueryClass=&MaxHits=5&QueryString=nicolaus";
		public List<string> Elements;
		public List<string> Text;
		public List<string> URI;

		public ArrayOfResult result;
		public void Start(){
			Elements = new List<string>();
			Text = new List<string>();
			URI = new List<string>();
		}
        public void Do()
		{
			ReadLink(testPath);
		}
        public void ReadLink(string path){
            XmlTextReader reader = new XmlTextReader (path);
			XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfResult));
			result = (ArrayOfResult)serializer.Deserialize(reader);
			reader.Close();
			Debug.Log(result.Result.Count);
			foreach(Result x in result.Result){
				Elements.Add(x.Label);
				Text.Add(x.Description);
				URI.Add(x.URI);

				SearchBox.Instance.AddSearchResult(x.Label, x.Description, x.URI);
			}
            //while (reader.Read()) 
            //{
            //    switch (reader.NodeType) 
            //    {
            //        case XmlNodeType.Element: // The node is an element.
            //            //Debug.Log ("<" + reader.Name);
            //            //Debug.Log (">");
			//			Elements.Add(reader.Name);
            //            break;
//
            //        case XmlNodeType.Text: //Display the text in each element.
            //            //Debug.Log (reader.Value);
			//			Text.Add(reader.Value);
            //            break;
//
            //        case XmlNodeType. EndElement: //Display the end of the element.
            //            //Debug.Log ("</" + reader.Name);
            //            //Debug.Log (">");
			//			EndElement.Add(reader.Name);
            //            break;
            //    }
            //}
        }
    }
	[XmlRoot(ElementName="Class", Namespace="http://lookup.dbpedia.org/")]
	public class Class {
		[XmlElement(ElementName="Label", Namespace="http://lookup.dbpedia.org/")]
		public string Label { get; set; }
		[XmlElement(ElementName="URI", Namespace="http://lookup.dbpedia.org/")]
		public string URI { get; set; }
	}

	[XmlRoot(ElementName="Classes", Namespace="http://lookup.dbpedia.org/")]
	public class Classes {
		[XmlElement(ElementName="Class", Namespace="http://lookup.dbpedia.org/")]
		public List<Class> Class { get; set; }
	}

	[XmlRoot(ElementName="Category", Namespace="http://lookup.dbpedia.org/")]
	public class Category {
		[XmlElement(ElementName="Label", Namespace="http://lookup.dbpedia.org/")]
		public string Label { get; set; }
		[XmlElement(ElementName="URI", Namespace="http://lookup.dbpedia.org/")]
		public string URI { get; set; }
	}

	[XmlRoot(ElementName="Categories", Namespace="http://lookup.dbpedia.org/")]
	public class Categories {
		[XmlElement(ElementName="Category", Namespace="http://lookup.dbpedia.org/")]
		public List<Category> Category { get; set; }
	}

	[XmlRoot(ElementName="Result", Namespace="http://lookup.dbpedia.org/")]
	public class Result {
		[XmlElement(ElementName="Label", Namespace="http://lookup.dbpedia.org/")]
		public string Label { get; set; }
		[XmlElement(ElementName="URI", Namespace="http://lookup.dbpedia.org/")]
		public string URI { get; set; }
		[XmlElement(ElementName="Description", Namespace="http://lookup.dbpedia.org/")]
		public string Description { get; set; }
		[XmlElement(ElementName="Classes", Namespace="http://lookup.dbpedia.org/")]
		public Classes Classes { get; set; }
		[XmlElement(ElementName="Categories", Namespace="http://lookup.dbpedia.org/")]
		public Categories Categories { get; set; }
		[XmlElement(ElementName="Templates", Namespace="http://lookup.dbpedia.org/")]
		public string Templates { get; set; }
		[XmlElement(ElementName="Redirects", Namespace="http://lookup.dbpedia.org/")]
		public string Redirects { get; set; }
		[XmlElement(ElementName="Refcount", Namespace="http://lookup.dbpedia.org/")]
		public string Refcount { get; set; }
	}

	[XmlRoot(ElementName="ArrayOfResult", Namespace="http://lookup.dbpedia.org/")]
	public class ArrayOfResult {
		[XmlElement(ElementName="Result", Namespace="http://lookup.dbpedia.org/")]
		public List<Result> Result { get; set; }
		[XmlAttribute(AttributeName="xmlns")]
		public string Xmlns { get; set; }
		[XmlAttribute(AttributeName="xsd", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Xsd { get; set; }
		[XmlAttribute(AttributeName="xsi", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Xsi { get; set; }
	}


}
