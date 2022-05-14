using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dragon_Saved : MonoBehaviour{

	public Animator anim;

	//Follow Player
    public GameObject player;
    private Vector2 playerPos;
    private float distToPlayer;
    public float startFollowDistance;   //Follow Player when further than this distance
	public float followDistance;		//Stop moving towards player when at this distance
	public float walkSpeed = 1f;
    //public Vector2 offsetFollow;

	public bool isBuddy1 = true;
	public bool isBuddy2 = false;
	public bool isBuddy3 = false;

	public float Buddy1Distance = 3.5f;
	public float Buddy2Distance = 5f;
	public float Buddy3Distance = 6.5f;
    
	//Follow Player vs attack enemies
	public bool followPlayer = true;
    public bool attackEnemy = false; // target any enemy within range of player when player atatcks
	public bool isAttacking = false; // attack a targeted enemy
	public float peacefullTime = 4f;

	//Attack variables
	public LayerMask enemyLayers;
	public GameObject enemyTarget;
    private Vector2 enemyPos;
    private float distToEnemy;
	private float timeBtwShots;
	public float startTimeBtwShots = 2;
	public GameObject projectile;
	public float attackRange = 10f;

    void Start(){
        player = GameObject.FindWithTag("Player");
		anim = GetComponent<NPC_Dragon_Main>().anim;

		if (isBuddy1){followDistance = Buddy1Distance;}
		else if (isBuddy2){followDistance = Buddy2Distance;}
		else if (isBuddy3){followDistance = Buddy3Distance;}
		startFollowDistance = followDistance + 1f;
    }

    void Update(){
        anim = GetComponent<NPC_Dragon_Main>().anim;
		//listen for player attacking an enemy, enter combat until player stops attacking for some time
		if ((Input.GetAxis("AttackFire") > 0)||(Input.GetAxis("AttackMelee") > 0)){
			followPlayer = false;
			attackEnemy = true;
			StartCoroutine(StopAttackingEnemies());
			FindTheEnemy();
		}

    }

    private void FixedUpdate(){
		
		//FOLLOW PLAYER
		if ((followPlayer) && (player != null)){
			playerPos = player.transform.position;
			distToPlayer = Vector2.Distance(transform.position, playerPos);
			
			//retreat from Player	
			if (distToPlayer <= followDistance){
				transform.position = Vector2.MoveTowards (transform.position, playerPos, -walkSpeed * Time.deltaTime);
				anim.SetBool("Walk", true);
			}
		
			//stop following Player
			if ((distToPlayer > followDistance) && (distToPlayer < startFollowDistance)){
				transform.position = this.transform.position;
				anim.SetBool("Walk", false);
			}
		
			//follow Player
			else if (distToPlayer >= startFollowDistance){
				transform.position = Vector2.MoveTowards (transform.position, playerPos, walkSpeed * Time.deltaTime);
				anim.SetBool("Walk", true);
			}
		}

		//FOLLOW ENEMY
		if ((attackEnemy) && (enemyTarget != null)){
			enemyPos = enemyTarget.transform.position;
			distToEnemy = Vector2.Distance(transform.position, enemyPos);
			
			//retreat from enemy	
			if (distToEnemy <= followDistance){
				transform.position = Vector2.MoveTowards (transform.position, enemyPos, -walkSpeed * Time.deltaTime);
				anim.SetBool("Walk", true);
			}
		
			//stop following enemy
			if ((distToEnemy > followDistance) && (distToEnemy < startFollowDistance)){
				transform.position = this.transform.position;
				anim.SetBool("Walk", false);
			}
		
			//follow enemy
			else if ((distToEnemy >= startFollowDistance)){
				transform.position = Vector2.MoveTowards (transform.position, enemyPos, walkSpeed * Time.deltaTime);
				anim.SetBool("Walk", true);
			}
		}
		

		//Timer for shooting projectiles
		if ((attackEnemy==true)&&(enemyTarget !=null)&& (distToEnemy > followDistance) && (distToEnemy < startFollowDistance)){
			if (timeBtwShots <= 0) {
				isAttacking = true;
				anim.SetBool("Attack", true);
				//rotate towards enemy target?
				
				GameObject myProjectile = Instantiate (projectile, transform.position, Quaternion.identity);
				myProjectile.GetComponent<NPC_Projectile>().attackPlayer = false;
				myProjectile.GetComponent<NPC_Projectile>().enemyTrans = enemyTarget.transform;
				
				timeBtwShots = startTimeBtwShots;
			} else {
				timeBtwShots -= Time.deltaTime;
				isAttacking = false;
				anim.SetBool("Attack", true);
			}
		}
	
	}


      void FindTheEnemy(){
            //animator.SetTrigger ("Melee");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(playerPos, attackRange, enemyLayers);
           
            foreach(Collider2D enemy in hitEnemies){
                  Debug.Log("Buddy targeting " + enemy.name);
				  enemyTarget = enemy.gameObject;
                  //enemy.GetComponent<EnemyMeleeDamage>().TakeDamage(attackDamage);
            }
      }


	IEnumerator StopAttackingEnemies(){
		yield return new WaitForSeconds(peacefullTime);
		followPlayer = true;
		attackEnemy = false;
	}


    //DISPLAY the range of enemy's attack when selected in the Editor
    void OnDrawGizmos(){
              Gizmos.DrawWireSphere(transform.position, followDistance);
       }

}
