using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    Rigidbody2D rigidbody;

    public GameObject target;
    //public float missileSpeed;
    public int damage = 1;

    public float acceleration = 2000f;
    public float maxSpeed = 2000f;
    //public float acceleration = 1f;
    //public float maxSpeed = 2f;


    private void OnCollisionEnter2D  (Collision2D collision)
    {
        if(collision.gameObject == target){

            Destroy(gameObject);
            target.GetComponent<Thing>().takeDamage(damage);
            //GameObject explosion = Instantiate(GameController.instance.explosionPrefab, collision.transform.position, Quaternion.identity);
            //Destroy(explosion, 0.95f);
            //GameController.instance.EarnPoints(10);
        }
    }

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        gameObject.layer = LayerMask.NameToLayer("TeamUPProjectile");
    }

	
	// Update is called once per frame
	void Update () {
        if(target!=null){
            Vector2 directionToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(directionToTarget.y,directionToTarget.x)*Mathf.Rad2Deg;
            rigidbody.MoveRotation(angle);
        }
        rigidbody.AddForce(transform.right*acceleration);
        rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity,maxSpeed);
	}
}
