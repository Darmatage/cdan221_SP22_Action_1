using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC_Dragon_Main : MonoBehaviour

{
    public bool isFriendly = true;
    public NPC_Dragon_Saved NPCfriendly;
    public NPC_Dragon_Enemy NPCememy;
    public SpriteRenderer NPCcolor;
    public Color friendlyColor;
    public Color enemyColor;
    

    // Start is called before the first frame update
    void Start()
    {
        NPCcolor = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFriendly== true)
        {
            NPCfriendly.enabled = true;
            NPCenemy.enabled = false;
        }

        else
        {
            NPCenemy.enabled = true;
            NPCfriendly.enabled = false;
        }

        NPCcolorSwitch();

    }





    //Function to change color of NPC

    public void NPCcolorSwitch()
    {
        if (isFriendly == true)
        {
            NPCcolor.color = friendlyColor;
        }

        else
        {
            NPCcolor.color = friendlyColor;
        }
    }

}
