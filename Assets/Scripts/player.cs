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
    // Start is called before the first frame update
    void Start()
    {
	    rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
	    moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
	    moveVelosity = moveInput.normalized * speed;
    }
    
	// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	protected void FixedUpdate()
	{
		rb.MovePosition(rb.position + moveVelosity*Time.deltaTime);
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
}
