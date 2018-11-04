using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Builder
{
    public GameController gameController;
    public PlayerController playerController;

    public KeyCode[] keys;
    public GameObject[] things;
    public Func<int,GameObject> objectGetter;
    public Func<GameObject,int> objectDestroyer;

    public Builder()
    {
        gameController = GameController.instance;
        playerController = PlayerController.instance;
    }

    public bool TryBuild(ref float nextBuildTime){
        //Debug.Log("Trying to build with "+money.ToString()+" ");
        for (int i = 0; i < things.Length; i++)
        {
            // get what to build
            Thing scriptOnObject = things[i].GetComponent<Thing>();

            if (playerController.currentLock!=null && scriptOnObject.identifier == playerController.currentLock.identifier)
            {
                // see if user clicked on the screen, if user clicked, try build will >0
                // decrease the try build regardless of successful build or not for the current phase
                if (playerController.currentLock.tryBuild > 0)
                {
                    playerController.currentLock.tryBuild = 0;
                    GameObject newObject = objectGetter(i);
                    Thing script = newObject.GetComponent<Thing>();
                    Vector3 mousePosition = Input.mousePosition;
                    Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
                    mousePositionInWorld.z = 0f;
                    newObject.transform.position = mousePositionInWorld;
                    // have enough money to build, build the object on the field and return true
                    // also check if the current object is in the range of a power station
                    if (gameController.IsPowered(newObject) && playerController.money >= script.cost)
                    {
                        // update status
                        playerController.money -= script.cost;
                        nextBuildTime = gameController.timeElapsed+ scriptOnObject.constructionTime;

                        // construct object
                        // Debug.Log(mousePositionInWorld);
                        return true;
                    }
                    // doesn't have enough money, build the object and return false
                    else
                    {
                        Debug.Log("Not powered or not enough money");
                        objectDestroyer(newObject);
                        return false;
                    }

                }
            }
            //}
        }
        return false;
    }
}

