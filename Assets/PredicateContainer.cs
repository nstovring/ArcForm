using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StructureContainer;
using UnityEngine.UI;

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
        if(!selectedPredicates.ContainsKey(p.myPredicate.property)){
            selectedPredicates.Add(p.myPredicate.property, p);
            Debug.Log("Selected new predicate" + p.myPredicate.value);
        }
    }
    public void RemoveSelectedPredicate(PredicateHolder p){
        if(selectedPredicates.ContainsKey(p.myPredicate.property)){
            selectedPredicates.Remove(p.myPredicate.property);
            Debug.Log("Deselected predicate" + p.myPredicate.value);
        }
    }

    public void CreateArcs(Dictionary<string, PredicateHolder> selectedP){
        Unitoken subject = TokenFactory.Instance.AddNewToken(StructConstructor.CreateUnitokenStruct(selectedSearchElement));
        foreach(KeyValuePair<string, PredicateHolder> x in selectedP){
            string value = x.Value.myPredicate.value;
            string property = x.Value.myPredicate.property;
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
