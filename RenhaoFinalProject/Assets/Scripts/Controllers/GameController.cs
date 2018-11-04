using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    HashSet<GameObject>[] AllThings = new HashSet<GameObject>[3];
    public float timeElapsed = 0;
    // Use this for initialization

    public Text statusTextField;

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < 3; i++)
        {
            AllThings[i] = new HashSet<GameObject>();
        }
    }

    void Start()
    {
        statusTextField.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if(AllThings[(int)Team.up].Count <= 1){
            statusTextField.enabled = true;
            statusTextField.text = "Defeated";
        }else if(AllThings[(int)Team.down].Count == 0){
            statusTextField.enabled = true;
            statusTextField.text = "Vectory";
        }
    }

    // get oppoent units
    public HashSet<GameObject> getOpponentUnits(Team team)
    {
        if (team == Team.up)
        {
            return getMyUnits(Team.down);
        }
        else
        {
            return getMyUnits(Team.up);
        }
    }

    // get my units
    public HashSet<GameObject> getMyUnits(Team team)
    {
        return AllThings[(int)team];
    }


    // add and remove objects
    public void addObjectToMyTeam(Team team, GameObject newThing)
    {
        AddObjectToMyTeam(newThing);
    }

    public void AddObjectToMyTeam(GameObject newThing)
    {
        Thing scriptOnGameObject = newThing.GetComponent<Thing>();
        AllThings[(int)scriptOnGameObject.team].Add(newThing);
    }

    public void removeObjectFromMyTeam(Team team, GameObject thingToRemove)
    {
        RemoveObjectFromMyTeam(thingToRemove);
    }

    public void RemoveObjectFromMyTeam(GameObject thingToRemove)
    {
        Thing scriptOnGameObject = thingToRemove.GetComponent<Thing>();
        AllThings[(int)scriptOnGameObject.team].Remove(thingToRemove);
    }

    // get units in Thing's range

    public HashSet<GameObject> GetObjectsInRange(GameObject _gameObject){
        HashSet<GameObject> result = new HashSet<GameObject>();
        Transform Objecttransform = _gameObject.transform;
        Thing scriptOnThing = _gameObject.GetComponent<Thing>();
        float range = scriptOnThing.visionRange;
        for (int i = 0; i < AllThings.Length;i++){
            foreach (GameObject aObject in AllThings[i]){
                Vector2 directionToTarget = aObject.transform.position - Objecttransform.position;
                if(directionToTarget.sqrMagnitude<range*range){
                    result.Add(aObject);
                }
            }
        }
        return result;
    }

    // get all objects that have me in their range
    public HashSet<GameObject> GetObjectsHaveMeInRange(GameObject _gameObject){
        return GetObjectsHaveMeInRange(_gameObject, (arg1, arg2) => true);
    }
    public HashSet<GameObject> GetObjectsHaveMeInRange(GameObject _gameObject, Func<GameObject,GameObject,bool> Me2OtherCriteria){
        HashSet<GameObject> result = new HashSet<GameObject>();
        Transform Objecttransform = _gameObject.transform;
        for (int i = 0; i < AllThings.Length; i++)
        {
            foreach (GameObject aObject in AllThings[i])
            {
                if (Me2OtherCriteria(_gameObject, aObject))
                {
                    Vector2 directionToTarget = aObject.transform.position - Objecttransform.position;
                    Thing scriptOnThing = aObject.GetComponent<Thing>();
                    float range = scriptOnThing.visionRange;
                    if (directionToTarget.sqrMagnitude < range * range)
                    {
                        result.Add(aObject);
                    }
                }
            }
        }
        return result;
    }

    // get power station
    public bool IsPowered(Team team, GameObject _gameObject){
        HashSet<GameObject> result = new HashSet<GameObject>();
        result = GetObjectsHaveMeInRange(_gameObject,
                                         (GameObject me, GameObject other) =>
        {
            Thing scriptOnOther = other.GetComponent<Thing>();
            if(scriptOnOther.identifier == "Power" && scriptOnOther.team == team){
                return true;
            }else{
                return false;
            }
        });
        return result.Count > 0;
    }

    public bool IsPowered(GameObject _gameObject){
        Thing scriptOnObject = _gameObject.GetComponent<Thing>();
        return IsPowered(scriptOnObject.team, _gameObject);
    }
    

}
