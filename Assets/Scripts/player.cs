using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
	[SerializeField] Joystick joystick;
	private Rigidbody2D rb;
	public float speed;
	private Vector2 moveInput, moveVelosity;
	public Animator anim;
	private bool facingRight;
    // Start is called before the first frame update
    void Start()
	{
		facingRight = false;
	    rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
	    moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
	    moveVelosity = moveInput.normalized * speed;
	    if(Mathf.Abs(joystick.Horizontal) == 0f && Mathf.Abs(joystick.Vertical) == 0f ){
	    	anim.Play("nowalking");
	    }
	    if(joystick.Horizontal > 0 && facingRight == false){
	    	Flip();
	    }
	    else if(joystick.Horizontal < 0 && facingRight == true){
	    	Flip();
	    }
	    if(joystick.Vertical > 0.3f){
	    	anim.Play("upwaking");
	    }
    }
    
	// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	protected void FixedUpdate()
	{
		move();
		
	}
	
	public void move(){
		rb.MovePosition(rb.position + moveVelosity*Time.deltaTime);
		if(joystick.Vertical <= 0.3f)anim.Play("walking");
	}
	
	// Sent when another object enters a trigger collider attached to this object (2D physics only).
	protected void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "car"){
			death();
		}
	}
	public void death(){
		
	}
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 newScale = transform.localScale;
		newScale.x *= -1; 
		transform.localScale = newScale;
	}
}
