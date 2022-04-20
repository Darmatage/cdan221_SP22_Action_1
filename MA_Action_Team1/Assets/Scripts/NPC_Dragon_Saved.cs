using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dragon_Saved : MonoBehaviour
{

    public GameObject player;
    private Vector2 playerPos;
    private float distToPlayer;
    public float followDistance = 5f;
    public float walkSpeed = 4f;
    public Vector2 offsetFollow;
    public bool followPlayer = true;
    public bool attackEnemy = false;

    void Start(){
        player = GameObject.FindWithTag("Player");
    }

    void Update(){
        playerPos = player.transform.position;
        distToPlayer = Vector2.Distance(transform.position, playerPos);

    }

    private void FixedUpdate(){
        if ((followPlayer) && (distToPlayer >= followDistance)){
            Vector2 pPosOffset = playerPos + offsetFollow;
            Vector2 pos = Vector2.Lerp((Vector2)transform.position, (Vector2)pPosOffset, walkSpeed * Time.fixedDeltaTime);
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }

    }


    //DISPLAY the range of enemy's attack when selected in the Editor
    void OnDrawGizmos(){
              Gizmos.DrawWireSphere(transform.position, followDistance);
       }

}
