﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNPC : MonoBehaviour
{
	public float speed;
	private Rigidbody2D rb;
	public int moveX, moveY;
	
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
	
	public void changeDirection(crossRoadConfig crc){
		
		List<int> possibleDirections = new List<int>();
		
		if (crc.Right) possibleDirections.Add(1);  
		if (crc.Left) possibleDirections.Add(-1);  
		if (crc.Up) possibleDirections.Add(2);     
		if (crc.Down) possibleDirections.Add(-2); 
		
		if (possibleDirections.Count > 0)
		{
			int direction = possibleDirections[Random.Range(0, possibleDirections.Count)];

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
	
	protected void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "crossRoad"){
			crossRoadConfig newCross = other.GetComponent<crossRoadConfig>();
			changeDirection(newCross);
		}
	}
	
}