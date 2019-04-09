using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StructureContainer;
using UnityEngine.UI;
using Xml2CSharp;
using ConceptNetJsonHolder;

public class ToggleBox : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform togglePrefab;
    public RectTransform myRectTransform;
    public SearchResultElement selectedSearchElement;
    public List<PredicateToggle> myPredicateToggles;
    public Dictionary<string, PredicateToggle> selectedPredicates;
    void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
        myPredicateToggles = new List<PredicateToggle>();
        selectedPredicates = new Dictionary<string, PredicateToggle>();
    }

    //Use xml data to create predicate
    public void CreatePredicates(SearchResultElement element){
        myPredicateToggles = new List<PredicateToggle>();
        selectedPredicates = new Dictionary<string, PredicateToggle>();

        foreach(Category x in element.XMLResult.Categories.Category){
            PredicateToggle predicateHolder = Instantiate(togglePrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<PredicateToggle>();
            predicateHolder.name = x.Label;
            predicateHolder.SetPredicate(x);
            myPredicateToggles.Add(predicateHolder);
            predicateHolder.myToggle.isOn = false;
            predicateHolder.myToggle.onValueChanged.AddListener(delegate(bool y){
                if(y == true){
                    StoreSelectedPredicate(predicateHolder);
                }else{
                    RemoveSelectedPredicate(predicateHolder);
                }
            });
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(myRectTransform);
    }

    
    public void CreatePredicates(Classes classes){
        myPredicateToggles = new List<PredicateToggle>();
        selectedPredicates = new Dictionary<string, PredicateToggle>();

        foreach(Class x in classes.Class){
            PredicateToggle predicateHolder = Instantiate(togglePrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<PredicateToggle>();
            predicateHolder.name = x.Label;
            predicateHolder.SetPredicate(x);
            myPredicateToggles.Add(predicateHolder);
            predicateHolder.myToggle.isOn = false;
            predicateHolder.myToggle.onValueChanged.AddListener(delegate(bool y){
                if(y == true){
                    StoreSelectedPredicate(predicateHolder);
                }else{
                    RemoveSelectedPredicate(predicateHolder);
                }
            });
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(myRectTransform);
    }

    
    public void CreatePredicates(Categories Categories){
        myPredicateToggles = new List<PredicateToggle>();
        selectedPredicates = new Dictionary<string, PredicateToggle>();

        foreach(Category x in Categories.Category){
            PredicateToggle predicateHolder = Instantiate(togglePrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<PredicateToggle>();
            predicateHolder.name = x.Label;
            predicateHolder.SetPredicate(x);
            myPredicateToggles.Add(predicateHolder);
            predicateHolder.myToggle.isOn = false;
            predicateHolder.myToggle.onValueChanged.AddListener(delegate(bool y){
                if(y == true){
                    StoreSelectedPredicate(predicateHolder);
                }else{
                    RemoveSelectedPredicate(predicateHolder);
                }
            });
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(myRectTransform);
    }
    //Use query data to create predicates


      public void CreatePredicates(Concept concept){
        myPredicateToggles = new List<PredicateToggle>();
        selectedPredicates = new Dictionary<string, PredicateToggle>();

        foreach(Edge x in concept.Edges){
            PredicateToggle predicateHolder = Instantiate(togglePrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<PredicateToggle>();
            predicateHolder.name = !string.IsNullOrWhiteSpace(x.SurfaceText) ? x.SurfaceText :  (x.Rel.Label +" " + x.End.Label).ToLower();
            predicateHolder.SetPredicate(x);
            myPredicateToggles.Add(predicateHolder);
            predicateHolder.myToggle.isOn = false;
            predicateHolder.myToggle.onValueChanged.AddListener(delegate(bool y){
                if(y == true){
                    StoreSelectedPredicate(predicateHolder);
                }else{
                    RemoveSelectedPredicate(predicateHolder);
                }
            });
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(myRectTransform);
    }

    public void ClearBox(){
        foreach(PredicateToggle x in myPredicateToggles){
            Destroy(x.gameObject);
        }
        myPredicateToggles.Clear();
    }


    public void StoreSelectedPredicate(PredicateToggle p){
        if(!selectedPredicates.ContainsKey(p.Label)){
            selectedPredicates.Add(p.Label, p);
            Debug.Log("Selected new predicate" + p.Label);
        }
    }
    public void RemoveSelectedPredicate(PredicateToggle p){
        if(selectedPredicates.ContainsKey(p.Label)){
            selectedPredicates.Remove(p.Label);
            Debug.Log("Deselected predicate" + p.Label);
        }
    }

    public void CreateArcs(Unitoken subject, Dictionary<string, PredicateToggle> selectedP){
        foreach(KeyValuePair<string, PredicateToggle> x in selectedP){
            string value = x.Value.myPredicate.value;
            string property = x.Value.myPredicate.property;
            ArcFactory.Instance.AddNewArc(subject, value, StructConstructor.CreateUnitokenStruct(property));
        }
    }
    public void CreateArcs(Dictionary<string, PredicateToggle> selectedP){
        Unitoken subject = TokenFactory.Instance.AddNewToken(StructConstructor.CreateUnitokenStruct(selectedSearchElement));
        foreach(KeyValuePair<string, PredicateToggle> x in selectedP){
            string value = x.Value.myCategory.URI;
            string property = x.Value.myCategory.Label;
            ArcFactory.Instance.AddNewArc(subject, value, StructConstructor.CreateUnitokenStruct(property));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.U)){
            //LayoutRebuilder.ForceRebuildLayoutImmediate(myRectTransform);
            CreateArcs(selectedPredicates);
        }
    }
}
