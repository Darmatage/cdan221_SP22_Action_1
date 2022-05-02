using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dragon_Saved : MonoBehaviour{

	public Animator anim;

    public GameObject player;
    private Vector2 playerPos;
    private float distToPlayer;
    public float startFollowDistance;   //Follow Player when further than this distance
	public float followDistance;		//Stop moving towards player when at this distance

    public float walkSpeed = 1f;
    //public Vector2 offsetFollow;
    public bool followPlayer = true;
    public bool attackEnemy = false; // target any enemy within range of player when player atatcks
	public bool isAttacking = false; // attack a targeted enemy
	
	public GameObject enemyTarget;
	private float timeBtwShots;
	public float startTimeBtwShots = 2;
	public GameObject projectile;

	public bool isBuddy1 = true;
	public bool isBuddy2 = false;
	public bool isBuddy3 = false;

	public float Buddy1Distance = 3.5f;
	public float Buddy2Distance = 5f;
	public float Buddy3Distance = 6.5f;


    void Start(){
        player = GameObject.FindWithTag("Player");
		anim = GetComponentInChildren<Animator> ();

		if (isBuddy1){followDistance = Buddy1Distance;}
		else if (isBuddy2){followDistance = Buddy2Distance;}
		else if (isBuddy3){followDistance = Buddy3Distance;}
		
		startFollowDistance = followDistance + 1f;
    }

    void Update(){
        playerPos = player.transform.position;
        distToPlayer = Vector2.Distance(transform.position, playerPos);

		//listen for player attacking an enemy, enter combat until player stops attacking for some time
		if ((Input.GetAxis("AttackFire") > 0)||(Input.GetAxis("AttackMelee") > 0)){
			followPlayer = false;
			attackEnemy = true;
			StartCoroutine(StopAttackingEnemies());
		}

    }

    private void FixedUpdate(){
		
		
		//retreat		
		if ((followPlayer) && (distToPlayer <= followDistance)){
			transform.position = Vector2.MoveTowards (transform.position, playerPos, -walkSpeed * Time.deltaTime);
			anim.SetBool("Walk", true);
		}
		
		//stop
		if ((followPlayer) && (distToPlayer > followDistance) && (distToPlayer < startFollowDistance)){
			transform.position = this.transform.position;
			anim.SetBool("Walk", false);
		}
		
		//follow
		else if ((followPlayer) && (distToPlayer >= startFollowDistance)){
            transform.position = Vector2.MoveTowards (transform.position, playerPos, walkSpeed * Time.deltaTime);
			anim.SetBool("Walk", true);
		}


		if ((attackEnemy==true)&&(enemyTarget !=null)){
			//Timer for shooting projectiles
			if (timeBtwShots <= 0) {
				isAttacking = true;
				anim.SetBool("Attack", true);
				//rotate towards enemy target?
				Instantiate (projectile, transform.position, Quaternion.identity);
				timeBtwShots = startTimeBtwShots;
			} else {
				timeBtwShots -= Time.deltaTime;
				isAttacking = false;
				anim.SetBool("Attack", true);
			}
		}
	
	
	}

	IEnumerator StopAttackingEnemies(){
		yield return new WaitForSeconds(3f);
		followPlayer = true;
		attackEnemy = false;
	}


    //DISPLAY the range of enemy's attack when selected in the Editor
    void OnDrawGizmos(){
              Gizmos.DrawWireSphere(transform.position, followDistance);
       }

}
