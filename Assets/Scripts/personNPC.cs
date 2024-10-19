using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personNPC : MonoBehaviour
{
	public float speed;
	private Rigidbody2D rb;
	public int moveX, moveY;
	public LayerMask sideWalkLayer;
	
    // Start is called before the first frame update
    void Start()
    {
	    rb = GetComponent<Rigidbody2D>();
    }
    

    // Update is called once per frame
    void Update()
    {
	    move();
    }
    
	public void move(){
		rb.velocity = new Vector2(speed * moveX, speed * moveY);
	}
	
	// Sent when another object enters a trigger collider attached to this object (2D physics only).
	protected void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "trafficLight"){
			
		}
		if(other.tag == "crossWalk"){
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f, sideWalkLayer);
			changeDirection(1);
		}
	}
	
	public void changeDirection(int direction){
		if (direction == 1) 
		{
			moveX = 1;
			moveY = 0;
		}
		else if (direction == -1) 
		{
			moveX = -1;
			moveY = 0;
		}
		else if (direction == 2) 
		{
			moveX = 0;
			moveY = 1;
		}
		else if (direction == -2) 
		{
			moveX = 0;
			moveY = -1;
		}
	}
}
