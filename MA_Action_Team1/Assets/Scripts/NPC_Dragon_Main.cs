using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dragon_Main : MonoBehaviour{
    public bool isFriendly = false;
    public NPC_Dragon_Saved NPCfriendly;
    public NPC_Dragon_Enemy NPCenemy;
    
	public GameObject buddyArt; 
	public GameObject posessedArt; 
	
	public SpriteRenderer NPCcolor;
    public Color friendlyColor;
    public Color enemyColor;
    
	public Animator anim;
	
	
    void Start(){
        NPCcolor = posessedArt.GetComponent<SpriteRenderer>();
		friendlyColor =  NPCcolor.color;
		//enemyColor =  new Color(44, 24, 67, 1f);
    }

    void Update(){
        if(isFriendly== true){
			buddyArt.SetActive(true);
			posessedArt.SetActive(false);
			anim = buddyArt.GetComponent<Animator>();
            NPCfriendly.enabled = true;
            NPCenemy.enabled = false;
			gameObject.layer = LayerMask.NameToLayer("Friendlies");;
        }else{
			buddyArt.SetActive(false);
			posessedArt.SetActive(true);
			anim = posessedArt.GetComponent<Animator>();
            NPCfriendly.enabled = false;
			NPCenemy.enabled = true;
			gameObject.layer = LayerMask.NameToLayer("Possessed");;
        }
        NPCcolorSwitch();
    }

    //Function to change color of NPC
    public void NPCcolorSwitch(){
        if (isFriendly == true){
            NPCcolor.color = friendlyColor;
        }else{
            NPCcolor.color = enemyColor;
        }
    }

	//check to see if the static bool for this one NPC is friendly -- does this effect all NPCs?
	public bool amIFriendly(){
		if (isFriendly == true){
			return true;
		}
		else {
			return false;
		}
	}

	//set the static bool for this one NPC to friendly -- does this effect all NPCs?
	public void setFriendly(){
		isFriendly = true;
	}

}
