using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhereIsBuddy : MonoBehaviour{
	
	public GameObject WhereBuddy;
	
	public Transform player;
	public Vector2 playerPos;
	
	public Transform buddy1;
	public Transform buddy2;
	public Transform buddy3;
	
	public bool Buddy1TooFar = false;
	public bool Buddy2TooFar = false;
	public bool Buddy3TooFar = false;
	
	public float BuddyThreshold = 5f;
	
    void Start(){
        WhereBuddy.SetActive(false);
		player = GameObject.FindWithTag("Player").transform;
    }

    void Update(){
		playerPos = player.position;
		
		if (buddy1 != null){
			Vector2 buddy1Pos = buddy1.position;
			float DistToBuddy = Vector2.Distance(playerPos, buddy1Pos);
			if (DistToBuddy > BuddyThreshold) {Buddy1TooFar = true;}
			else  {Buddy1TooFar = false;}
			
			if (Buddy1TooFar){
				WhereBuddy.SetActive(true);
			} else {WhereBuddy.SetActive(false);}
		}
		
		if (buddy2 != null){
			Vector2 buddy2Pos = buddy2.position;
			float DistToBuddy = Vector2.Distance(playerPos, buddy2Pos);
			if (DistToBuddy > BuddyThreshold) {Buddy2TooFar = true;}
			else  {Buddy2TooFar = false;}
			
			if (Buddy2TooFar){
				WhereBuddy.SetActive(true);
			} else {WhereBuddy.SetActive(false);}
		}
		
		if (buddy3 != null){
			Vector2 buddy3Pos = buddy3.position;
			float DistToBuddy = Vector2.Distance(playerPos, buddy3Pos);
			if (DistToBuddy > BuddyThreshold) {Buddy3TooFar = true;}
			else  {Buddy3TooFar = false;}
			
			if (Buddy3TooFar){
				WhereBuddy.SetActive(true);
			} else {WhereBuddy.SetActive(false);}
		}
    }
	
	public void AddBuddy(Transform newBuddy){
		if (buddy1 == null){buddy1 = newBuddy;}
		else if (buddy2 == null){buddy2 = newBuddy;}
		else if (buddy3 == null){buddy3 = newBuddy;}
		else {Debug.Log("Too Many Buddies");}
	}
	
	
}




// All objects in Layer finder:
//https://answers.unity.com/questions/179310/how-to-find-all-objects-in-specific-layer.html