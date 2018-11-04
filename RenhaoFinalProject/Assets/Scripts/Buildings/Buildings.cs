using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : Thing {
    public Builder builder;
    public KeyCode[] keys;
    public GameObject[] things;

    public bool canBuild;
    public float nextBuildTime = 0;
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

    }
	
	// Update is called once per frame
	new protected void Update () {
        base.Update();
        if (team == Team.up && canBuild && nextBuildTime < gameController.timeElapsed) 
        {
            // if build success
            builder.TryBuild(ref nextBuildTime);
        }

    }
}
