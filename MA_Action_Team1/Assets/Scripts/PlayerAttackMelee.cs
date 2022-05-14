using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttackMelee : MonoBehaviour{

	public Animator anim;
	  
	//attack points for hit range
	public Transform attackPt;
	public Transform attackPt_Front;
	public Transform attackPt_Up;
	public Transform attackPt_Down;
	  
	  
	private float attackRange;
	public float attackTailRange = 3f;
	public float attackLightningRange = 2.5f;
	
	public float attackRate = 1f; //the smaller the fraction, the slower the cooldown
	//public float lightningRate = 0.3f; //the smaller the fraction, the slower the cooldown

	public float lightningTimerMax = 3f;       //set the number of seconds here
	private float lightningTimer = 0f;
	private bool lightningCooldown = false;   

	private  float nextAttackTime = 0f;
	private float nextLightningTime = 0f;
	public int attackDamage = 20;
	public LayerMask enemyLayers;
	public LayerMask stoneLayers;
	public LayerMask posessedLayers;

	public bool isTail = true;
	public bool isLightning = false;

	public bool isFront = true;
	public bool isUp = false;
	public bool isDown = false;

	public GameObject LightningFront;
	public GameObject LightningUp;
	public GameObject LightningDown;
	public AudioSource thunderSFX;
	//public AudioSource tailthumpSFX;

	//lightning cooldown indicator
	//tag == LightningIcon
	private Image lightningIcon;

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
		
		
		//if (Time.time >= nextLightningTime){
		if (lightningCooldown == false){				
			if ((Input.GetAxis("AttackLightning") > 0)&&(GameHandler.hasLightningPower)){
				isTail = false;
				isLightning = true;
				Attack();
				//nextLightningTime = Time.time + 1f / lightningRate;
				
				lightningIcon = GameObject.FindWithTag("LightningIcon").GetComponent<Image>();
				lightningIcon.fillAmount = 0;
				lightningCooldown = true;

				//lightningIcon.fillAmount = nextLightningTime / lightningRate; 
				//Debug.Log("Time.time = " + Time.time + "nextLightningTime" + nextLightningTime);
			}
		}		
	}

	   
	void FixedUpdate(){
        if (lightningCooldown == true){
			lightningTimer += 0.01f;
			//Debug.Log("time: " + theTimer);
			//timerCircleDisplay.gameObject.SetActive(true);
			lightningIcon.fillAmount = lightningTimer / lightningTimerMax;

			if (lightningTimer >= lightningTimerMax){
				lightningTimer = 0;
				//Debug.Log("I do the thing!");       //can be replaced with the desired commands
				lightningCooldown = false;
			}
		}
	} 

	void Attack(){
		if (isTail){
			anim.SetTrigger ("tailswing"); 
			//if (!tailthumpSFX.isPlaying){tailthumpSFX.Play();}
			attackPt = attackPt_Front; 
			attackRange = attackTailRange;
		} else if (isLightning){
			anim.SetTrigger ("ShootLightning");
			if (!thunderSFX.isPlaying){thunderSFX.Play();}
			attackRange = attackLightningRange;
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
		
		Collider2D[] hitStone = Physics2D.OverlapCircleAll(attackPt.position, attackRange, stoneLayers);

		foreach(Collider2D stone in hitStone){
			Debug.Log("We hit " + stone.name);
			stone.GetComponent<Barrier_Damage>().TakeDamage(attackDamage);
		}	

		Collider2D[] hitPosessed = Physics2D.OverlapCircleAll(attackPt.position, attackRange, posessedLayers);

		foreach(Collider2D posessed in hitPosessed){
			Debug.Log("I hit a possessed NPC dragon");
			posessed.GetComponent<NPC_Dragon_Damage>().TakeDamage(attackDamage);
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
		Gizmos.DrawWireSphere(attackPt_Front.transform.position, attackTailRange);
		Gizmos.DrawWireSphere(LightningFront.transform.position, attackLightningRange);
		Gizmos.DrawWireSphere(LightningUp.transform.position, attackLightningRange);
		Gizmos.DrawWireSphere(LightningDown.transform.position, attackLightningRange);
	}
	
}
