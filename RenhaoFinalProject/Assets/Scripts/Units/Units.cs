using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : Thing {
    //public int life;
    //public int attack;
    //public float maxSpeed;
    //public UnitType unitType;
    //public Team side;
    //public Thing thing;
    new Rigidbody2D rigidbody;
	// Use this for initialization
	new void Start () {
        base.Start();
        rigidbody = GetComponent<Rigidbody2D>();
	}
    protected void moveToTarget(GameObject target)
    {
        if (target != null)
        {

            Vector2 directionToTarget = target.transform.position - transform.position;
            if(directionToTarget.sqrMagnitude<attackRange*attackRange){
                rigidbody.velocity = new Vector2(0, 0);
            }else{
                float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
                rigidbody.MoveRotation(angle);
                rigidbody.AddForce(transform.right * acceleration);
                rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, moveSpeed);
            }

        }

    }
    // Update is called once per frame
    new void Update () {
        base.Update();
        //if (team == Team.up){
        //    rigidbody.AddForce(Vector2.up * 20f);
        //}else{
        //    rigidbody.AddForce(Vector2.down * 20f);
        //}


        //// to do 
        //// max speed
        //rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity,moveSpeed);
        // move to nearest target
        GameObject target = getTarget();
        moveToTarget(target);
        // shoot other targets

        // lost life when being shot

    }
}
