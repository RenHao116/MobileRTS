using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : Thing {
    public Builder builder;
    public KeyCode[] keys;
    public GameObject[] things;

    public bool canBuild;
    public float nextBuildTime = 0;

    public float spawnDelay;
    public float minSpwanDelay = 0.5f;
    public float maxSpwanDelay = 10f;
    // Use this for initialization
    new protected void Start () {
        base.Start();
        canBuild = true;
        builder = new Builder
        {
            keys = keys,
            things = things
        };
        builder.objectGetter = (i) =>
        {

            //canBuild = false;
            return Instantiate(builder.things[i]);
        };
        builder.objectDestroyer = (toDestroyObject) =>
        {
            Destroy(toDestroyObject);
            //canBuild = true;
            return 0;
        };
        if (team == Team.down)
        {
            StartCoroutine("SpawnUnitsWithDelay");
        }

    }
	
	// Update is called once per frame
	new protected void Update () {
        base.Update();
        if (team == Team.up && canBuild && nextBuildTime < gameController.timeElapsed) 
        {
            // if build success
            builder.TryBuild(ref nextBuildTime);
        }


        if(team == Team.down){
            float decreaseOverTime = maxSpwanDelay - ((maxSpwanDelay - minSpwanDelay) / 30f * gameController.timeElapsed);
            spawnDelay = Mathf.Clamp(decreaseOverTime, minSpwanDelay, maxSpwanDelay);
            //UpdateDisplay();
        }

    }


    // Enemy auto generation code
    void SpawnUnits(){
        GameObject newUnit = things[Random.Range(0, things.Length)];
        GameObject generatedObject =  Instantiate(newUnit, gameObject.transform.position, Quaternion.identity);
        Thing thingScript = generatedObject.GetComponent<Thing>();
        thingScript.team = Team.down;
    }
    IEnumerator SpawnUnitsWithDelay(){
        yield return new WaitForSeconds(spawnDelay);
        SpawnUnits();
        StartCoroutine("SpawnUnitsWithDelay");
    }
}
