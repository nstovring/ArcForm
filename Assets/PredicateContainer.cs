using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StructureContainer;
using UnityEngine.UI;
using Xml2CSharp;

public class PredicateContainer : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform myRectTransform;
    public SearchResultElement selectedSearchElement;
    public List<PredicateHolder> myPredicateHolders;
    public Dictionary<string, PredicateHolder> selectedPredicates;
    void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
        myPredicateHolders = new List<PredicateHolder>();
        selectedPredicates = new Dictionary<string, PredicateHolder>();
    }

    //Use xml data to create predicate
    public void CreatePredicates(SearchResultElement element){
        myPredicateHolders = new List<PredicateHolder>();
        selectedPredicates = new Dictionary<string, PredicateHolder>();

        foreach(Category x in element.categories.Category){
            PredicateHolder predicateHolder = Instantiate(SearchBox.Instance.togglePrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<PredicateHolder>();
            predicateHolder.name = x.Label;
            predicateHolder.SetPredicate(x);
            myPredicateHolders.Add(predicateHolder);
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
    public void CreatePredicates(List<predicate> myPredicates){
        myPredicateHolders = new List<PredicateHolder>();
        selectedPredicates = new Dictionary<string, PredicateHolder>();

        foreach(predicate x in myPredicates){
            PredicateHolder predicateHolder = Instantiate(SearchBox.Instance.togglePrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<PredicateHolder>();
            predicateHolder.name = x.property;
            predicateHolder.SetPredicate(x);
            myPredicateHolders.Add(predicateHolder);
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
    public void StoreSelectedPredicate(PredicateHolder p){
        if(!selectedPredicates.ContainsKey(p.Label)){
            selectedPredicates.Add(p.Label, p);
            Debug.Log("Selected new predicate" + p.Label);
        }
    }
    public void RemoveSelectedPredicate(PredicateHolder p){
        if(selectedPredicates.ContainsKey(p.Label)){
            selectedPredicates.Remove(p.Label);
            Debug.Log("Deselected predicate" + p.Label);
        }
    }

    public void CreateArcs(Unitoken subject, Dictionary<string, PredicateHolder> selectedP){
        foreach(KeyValuePair<string, PredicateHolder> x in selectedP){
            string value = x.Value.myPredicate.value;
            string property = x.Value.myPredicate.property;
            ArcFactory.Instance.AddNewArc(subject, value, StructConstructor.CreateUnitokenStruct(property));
        }
    }
    public void CreateArcs(Dictionary<string, PredicateHolder> selectedP){
        Unitoken subject = TokenFactory.Instance.AddNewToken(StructConstructor.CreateUnitokenStruct(selectedSearchElement));
        foreach(KeyValuePair<string, PredicateHolder> x in selectedP){
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
