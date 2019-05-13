using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcToolConstants
{
    public class StaticConstants
    {
        public static string[] RelationURIs = { "/r/RelatedTo", "/r/ExternalURL", "/r/FormOf", "/r/IsA", "/r/PartOf", "/r/HasA", "/r/UsedFor", "/r/CapableOf", "/r/AtLocation", "/r/Causes", "/r/HasSubevent", "/r/HasFirstSubevent", "/r/HasLastSubevent", "/r/HasPrerequisite", "/r/HasProperty", "/r/MotivatedByGoal", "/r/ObstructedBy", "/r/Desires", "/r/CreatedBy", "/r/Synonym", "/r/Antonym", "/r/DistinctFrom", "/r/DerivedFrom", "/r/SymbolOf", "/r/DefinedAs", "/r/Entails", "/r/MannerOf", "/r/LocatedNear", "/r/HasContext", "/r/SimilarTo", "/r/EtymologicallyRelatedTo", "/r/EtymologicallyDerivedFrom", "/r/CausesDesire", "/r/MadeOf", "/r/ReceivesAction", "/r/InstanceOf" };
        public static string[] relationsNaming = { "is related to", "External URL", "form of", "is a", "part of", "has a", "used for", "capable of", "at location", "causes", "has subevent", "has first subevent", "has last subevent", "has prerequisite", "has property", "motivated by goal", "obstructed by", "desires", "created by", "synonym", "antonym", "distinct from", "derived from", "symbol of", "defined as", "entails", "manner of", "located near", "has context", "similar to", "etymologically related to", "etymologically derived from", "causes desire", "made of", "receives action", "instance of" };


        public static Dictionary<string, string> LabelToKey;
        public static Dictionary<string, string> KeyToLabel;
        public static Dictionary<string, int> KeyToIndex;
        public static Dictionary<int, string> IndexToKey;
        public static void IntializeDictionaries()
        {
            LabelToKey = new Dictionary<string, string>();
            KeyToLabel = new Dictionary<string, string>();
            KeyToIndex = new Dictionary<string, int>();
            IndexToKey = new Dictionary<int, string>();

            for (int i = 0; i < RelationURIs.Length; i++)
            {
                string key = RelationURIs[i];
                string label = relationsNaming[i];
                int index = i;
                KeyToIndex.Add(key, index);
                KeyToLabel.Add(key, label);
                LabelToKey.Add(label, key);
                IndexToKey.Add(index, key);
            }
        }

        //public static Vector3 densityVector()
        //{
        //
        //}

        public static Vector3 rngVector()
        {
            return new Vector3(rngFloat(), rngFloat(), 0);
        }

        public static Vector3 rngVector(int min, int max)
        {
            return new Vector3(rngFloat(min, max), rngFloat(min, max), 0);
        }

        public static float rngFloat()
        {
            return Random.Range(2.0f,4.0f) * GetRngSign();
        }

        public static float rngFloat(int min, int max)
        {
            return Random.Range(min, max) * GetRngSign();
        }

        public static float GetRngSign()
        {
            return Mathf.Sign(Random.Range(-2.0f, 2.0f));
        }
    }
}
