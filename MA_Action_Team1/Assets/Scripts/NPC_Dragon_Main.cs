using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dragon_Main : MonoBehaviour{
    public static bool isFriendly = true;
    public NPC_Dragon_Saved NPCfriendly;
    public NPC_Dragon_Enemy NPCenemy;
    public SpriteRenderer NPCcolor;
    public Color friendlyColor;
    public Color enemyColor;
    
    void Start(){
        NPCcolor = GetComponentInChildren<SpriteRenderer>();
		//friendlyColor =  new Color(255, 255, 255, 1f);
		//enemyColor =  new Color(44, 24, 67, 1f);
    }

    void Update(){
        if(isFriendly== true){
            NPCfriendly.enabled = true;
            NPCenemy.enabled = false;
        }else{
            NPCenemy.enabled = true;
            NPCfriendly.enabled = false;
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

//include functionality to change layer from friendly to enemy as needed for plyer attacks


}
