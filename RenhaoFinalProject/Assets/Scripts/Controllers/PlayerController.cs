using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    public GameObject[] buildLockPrefabs;
    public BuildLock[] locks;
    public BuildLock currentLock;

    public Text MoneyTextField;

    public float money = 0;

    private void Awake()
    {
        instance = this;
    }


    // Use this for initialization
    void Start () {
        locks = new BuildLock[buildLockPrefabs.Length];
        for (int i = 0; i < buildLockPrefabs.Length;i++){
            Thing thingScriptOnObject = buildLockPrefabs[i].GetComponent<Thing>();
            locks[i] = new BuildLock(thingScriptOnObject.identifier);
        }
	}
	
	// Update is called once per frame
	void Update () {

        //if(buildLock!=0){
        //    buildLock = 0;
        //}

        if (Input.GetMouseButtonDown(0)){
            if(currentLock!=null){
                currentLock.tryBuild += 1;
            }
        }
        //Debug.Log(buildLock);

        MoneyTextField.text = "Money: " + money;

	}

    public void Earn(float earnings){
        if(earnings>0)
            money += earnings;
    }

    // The following code deal with constructions:
    private void cleanAllLocks(){
        if(currentLock!=null)
            currentLock.isChosen = false;
    }
    // A button will call this function to select which thing to build
    public void SelectLock(string s ){
        // set all locks to false
        cleanAllLocks();
        // find which one I want to update, and update it
        for (int i = 0; i < locks.Length; i++)
        {
            if (locks[i].identifier == s){
                Debug.Log("Lock "+i+" set with identifier:"+s);
                currentLock = locks[i];
                currentLock.isChosen = true;
            }
        }
    }
}
