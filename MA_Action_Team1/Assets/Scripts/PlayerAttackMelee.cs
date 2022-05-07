using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerAttackMelee : MonoBehaviour{

	public Animator anim;
	  
	//attack points for hit range
	public Transform attackPt;
	public Transform attackPt_Front;
	public Transform attackPt_Up;
	public Transform attackPt_Down;
	  
	public float attackRange = 2.5f;
	public float attackRate = 1f; //the smaller the fraction, the slower the cooldown
	public float lightningRate = 0.3f; //the smaller the fraction, the slower the cooldown
	private  float nextAttackTime = 0f;
	private float nextLightningTime = 0f;
	public int attackDamage = 20;
	public LayerMask enemyLayers;

	public bool isTail = true;
	public bool isLightning = false;

	public bool isFront = true;
	public bool isUp = false;
	public bool isDown = false;

	public GameObject LightningFront;
	public GameObject LightningUp;
	public GameObject LightningDown;

	void Start(){
		anim = gameObject.GetComponentInChildren<Animator>();
		LightningFront.SetActive(false);
		LightningUp.SetActive(false);
		LightningDown.SetActive(false);
	}

	void Update(){
		string direction = GetComponent<PlayerMoveAround>().currentDirection;
		if (direction == "front"){
			isFront = true;
			isUp = false;
			isDown = false;
		} else if (direction == "up"){
			isFront = false;
			isUp = true;
			isDown = false;
		} else if (direction == "down"){
			isFront = false;
			isUp = false;
			isDown = true;
		}
		
		if (Time.time >= nextAttackTime){
			//if (Input.GetKeyDown(KeyCode.Space))
			if (Input.GetAxis("AttackMelee") > 0){
				isTail = true;
				isLightning = false;
				Attack();
				nextAttackTime = Time.time + 1f / attackRate;
			}
		}
		
		if (Time.time >= nextLightningTime){		
			if (Input.GetAxis("AttackLightning") > 0){
				isTail = false;
				isLightning = true;
				Attack();
				nextLightningTime = Time.time + 1f / lightningRate;
				//Debug.Log("Time.time = " + Time.time + "nextLightningTime" + nextLightningTime);
			}
		}		
	}

	void Attack(){
		if (isTail){
			anim.SetTrigger ("tailswing"); attackPt = attackPt_Front;
		} else if (isLightning){
			anim.SetTrigger ("ShootLightning");
			if (isUp){attackPt = LightningUp.transform; LightningUp.SetActive(true);}
			else if (isDown){attackPt = LightningDown.transform; LightningDown.SetActive(true);}
			else {attackPt = LightningFront.transform; LightningFront.SetActive(true);}
			StopCoroutine(TurnOffLightning());
			StartCoroutine(TurnOffLightning());
		}
		
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPt.position, attackRange, enemyLayers);

		foreach(Collider2D enemy in hitEnemies){
			Debug.Log("We hit " + enemy.name);
			enemy.GetComponent<EnemyMeleeDamage>().TakeDamage(attackDamage);
		}
	}

	IEnumerator TurnOffLightning(){
		yield return new WaitForSeconds(1f);
		LightningFront.SetActive(false);
		LightningUp.SetActive(false);
		LightningDown.SetActive(false);
	}


	//NOTE: to help see the attack sphere in editor:
	void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere(LightningFront.transform.position, attackRange);
		Gizmos.DrawWireSphere(LightningUp.transform.position, attackRange);
		Gizmos.DrawWireSphere(LightningDown.transform.position, attackRange);
	}
	
}
