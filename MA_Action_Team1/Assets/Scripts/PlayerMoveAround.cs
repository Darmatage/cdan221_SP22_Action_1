using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMoveAround : MonoBehaviour {

      public Animator anim;
      public Rigidbody2D rb2D;
      private bool FaceRight = false; // determine which way player is facing.
	  private bool FaceFront = true;
      public static float runSpeed = 10f;
      public float startSpeed = 10f;
      public bool isAlive = true;
 
	//Get Last Direction
	private Vector3 lastPosition;
	public string currentDirection = "front";

      void Start(){
           anim = gameObject.GetComponentInChildren<Animator>();
           rb2D = transform.GetComponent<Rigidbody2D>();
      }

	void Update(){	
		//NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
		//NOTE: Vertical axis: [w] / up arrow, [s] / down arrow
		Vector3 hvMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
		if (isAlive == true){

                  transform.position = transform.position + hvMove * runSpeed * Time.deltaTime;

             if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0)){
                   anim.SetBool ("walk", true);
             } else {anim.SetBool ("walk", false); anim.SetBool ("walk_back", false);}

            // NOTE: if input is moving the Player right and Player faces left, turn, and vice-versa
			if ((hvMove.x <0 && !FaceRight) || (hvMove.x >0 && FaceRight)){
                  playerTurn();
			}
			
			if (hvMove.y <0){
                  anim.SetBool ("walk_back", false);
				  anim.SetBool ("walk", true);
			}
			else if (hvMove.y >0){
				anim.SetBool ("walk_back", true);
				anim.SetBool ("walk", false);
			}
		}
		
		//Direction capture:
		Vector2 direction = transform.position - lastPosition;
		//var localDirection = transform.InverseTransformDirection(direction);
		lastPosition = transform.position;
		if ((direction.x > 0)||(direction.x < 0)){currentDirection = "front";}
		else if (direction.y > 0){currentDirection = "up";}
		else if (direction.y < 0){currentDirection = "down";}
		//Debug.Log("Direction = " + currentDirection);
	}

	//turn player left and right
	private void playerTurn(){
		// NOTE: Switch player facing label
		FaceRight = !FaceRight;

		// NOTE: Multiply player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	  
}
