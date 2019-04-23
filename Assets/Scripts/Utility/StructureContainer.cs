using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StructureContainer
{
    
    [System.Serializable]
    public struct predicate{
        public string property;
        public string value;

        public string URI;

        public bool HasURI(){
            if(string.IsNullOrWhiteSpace(URI))
            return false;
            else
            return true;
        }
    }
    [System.Serializable]
    public struct unitoken{
        public int id;
        public Vector3 TransientPosition;
        public string Label;

        public string URI;

        public bool HasURI(){
            if(string.IsNullOrWhiteSpace(URI))
                return false;
            else
                return true;
        }

    }

    public class StructConstructor{
        public static unitoken CreateUnitokenStruct(SearchResultElement x){
            return new unitoken {Label = x.elementText.text, URI = x.URI};
        }

        public static unitoken CreateUnitokenStruct(string name){
            return new unitoken {Label = name};
        }
    }

    [System.Serializable]
    public struct arc{
        public int id;
        public int source;
        public int target;
        public string Label;

        public string URI;
        public bool HasURI(){
            if(string.IsNullOrWhiteSpace(URI))
            return false;
            else
            return true;
        }
    }


    //Structs
    public struct Relation
    {
        public string Label;
        public bool isActive;
        public Unitoken token;

        public void SetActive(bool active)
        {
            isActive = active;
        }
    }

    public struct Property
    {
        public string Label;
        public bool isActive;
        public List<Relation> Relations;
    }


}
