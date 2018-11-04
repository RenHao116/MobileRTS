using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : MonoBehaviour {
    protected PlayerController playerController;
    protected GameController gameController;

    // A unique identifier for the thing (usually a name represented by a string)
    public string identifier;

    public Team team;
    public int life;
    public int cost;
    public int constructionTime;
    public int visionRange;
    public GameObject projectilePrefab;


    float lastFireTime;
    public float fireDelay;

    //self:
    //protected Rigidbody2D rigidbody;
    public int acceleration;
    public float moveSpeed;
    public int attackRange;
    public int attack;
    public int Aromor;

    // Attack
    protected GameObject getTarget(){
        HashSet<GameObject> enemyThings = gameController.getOpponentUnits(team);
        GameObject target =null;
        float closestDistance = 999999f;
        if (enemyThings != null)
        {
            foreach (GameObject opponent in enemyThings)
            {
                Vector2 directionToTarget = opponent.transform.position - transform.position;
                if (directionToTarget.sqrMagnitude < closestDistance)
                {
                    target = opponent;
                    closestDistance = directionToTarget.sqrMagnitude;
                }
            }
        }
        return target;
    }

    void tryFireWithDelay(){
        float timeElapsed = gameController.timeElapsed;
        if(timeElapsed-fireDelay>lastFireTime){
            tryFire();
            lastFireTime = timeElapsed;
        }
    }
    void tryFire(){
        GameObject target = getTarget();
        //Debug.Log(target.ToString());
        if (target != null)
        {
            Vector2 directionToTarget = target.transform.position - transform.position;
            if (directionToTarget.sqrMagnitude <= attackRange * attackRange)
            {
                GameObject projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Projectile projectileScript = projectileObject.GetComponent<Projectile>();
                projectileScript.target = target;
                projectileScript.damage = attack;
            }
        }
    }


    //Defend:
    public void takeDamage(int damage){
        life -= Mathf.Clamp(damage - Aromor,1,9999);
        if(life<=0){
            Destroy(gameObject);
        }
    }


    // movement:


    // Use this for initialization
    protected virtual void Start () {
        //Debug.Log("On start: "+identifier + " is started");
        playerController = PlayerController.instance;
        gameController = GameController.instance;
        lastFireTime = gameController.timeElapsed;
        gameController.addObjectToMyTeam(team, gameObject);
        if(team == Team.up) gameObject.layer = LayerMask.NameToLayer("TeamUP");
        if (team == Team.down) gameObject.layer = LayerMask.NameToLayer("TeamDown");
        //rigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	protected virtual void Update () {
        if(projectilePrefab.GetComponent<Projectile>()!=null){
            tryFireWithDelay();
        }

	}
    private void OnApplicationQuit()
    {
        //Debug.Log(identifier+" is destoyed");
        gameController.removeObjectFromMyTeam(team, gameObject);
    }

    private void OnDestroy()
    {
        //Debug.Log("On destory: "+ identifier + " is destoyed");
        if(gameController!=null){
            gameController.removeObjectFromMyTeam(team, gameObject);
        }
    }
}
