using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Buildings {

    public float earningPerSecond;

    public bool isWorking = false;
    // Use this for initialization
    new void Start () {
        base.Start();

    }
	
	// Update is called once per frame
	new void Update () {
        base.Update();
        bool isUpPower = gameController.IsPowered(Team.up, gameObject);
        bool isDownPower = gameController.IsPowered(Team.down, gameObject);
        bool provideMoney = isDownPower ^ isUpPower;
        if(!isWorking && provideMoney && isUpPower){
            isWorking = true;
            StartCoroutine("EarningTimer");
        }
        if(isWorking && !provideMoney){
            isWorking = false;
            StopCoroutine("EarningTimer");
        }
	}

    IEnumerator EarningTimer()
    {
        yield return new WaitForSeconds(1);
        playerController.Earn(earningPerSecond);
        StartCoroutine("EarningTimer");
    }
}
