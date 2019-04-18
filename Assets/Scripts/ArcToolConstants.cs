using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcToolConstants
{
    public class StaticConstants
    {
        public static string[] RelationURIs = { "/r/RelatedTo", "/r/ExternalURL", "/r/FormOf", "/r/IsA", "/r/PartOf", "/r/HasA", "/r/UsedFor", "/r/CapableOf", "/r/AtLocation", "/r/Causes", "/r/HasSubevent", "/r/HasFirstSubevent", "/r/HasLastSubevent", "/r/HasPrerequisite", "/r/HasProperty", "/r/MotivatedByGoal", "/r/ObstructedBy", "/r/Desires", "/r/CreatedBy", "/r/Synonym", "/r/Antonym", "/r/DistinctFrom", "/r/DerivedFrom", "/r/SymbolOf", "/r/DefinedAs", "/r/Entails", "/r/MannerOf", "/r/LocatedNear", "/r/HasContext", "/r/SimilarTo", "/r/EtymologicallyRelatedTo", "/r/EtymologicallyDerivedFrom", "/r/CausesDesire", "/r/MadeOf", "/r/ReceivesAction", "/r/InstanceOf" };
        public static string[] relationsNaming = { "is related to ", "External URL ", "form of ", "is a ", "part of ", "has a ", "used for ", "capable of ", "at location ", "causes ", "has subevent ", "has first subevent ", "has last subevent ", "has prerequisite ", "has property ", "motivated by goal ", "obstructed by ", "desires ", "created by ", "synonym ", "antonym ", "distinct from ", "derived from ", "symbol of ", "defined as ", "entails ", "manner of ", "located near ", "has context ", "similar to ", "etymologically related to ", "etymologically derived from ", "causes desire ", "made of ", "receives action ", "instance of " };

        public static Vector3 rngVector()
        {
            return new Vector3(Random.RandomRange(-2.0f, 2.0f), Random.RandomRange(-2.0f, 2.0f), 0);
        }
    }
}
