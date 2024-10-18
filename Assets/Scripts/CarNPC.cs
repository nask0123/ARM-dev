﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNPC : MonoBehaviour
{
	public float speed;
	private Rigidbody2D rb;
	public int moveX, moveY;
	[SerializeField] short facingDirection;
	
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		move();
		flip();

	}
    
	public void move(){
		rb.velocity = new Vector2(speed * moveX, speed * moveY);
	}
	public void flip(){
		if(facingDirection == 0){
			Vector3 newRotation = transform.rotation.eulerAngles;
			newRotation.z = 0;
			transform.rotation = Quaternion.Euler(newRotation);
		}
		else if(facingDirection == 1){
			Vector3 newRotation = transform.rotation.eulerAngles;
			newRotation.z = 90;
			transform.rotation = Quaternion.Euler(newRotation);		}
		else if(facingDirection == 2){
			Vector3 newRotation = transform.rotation.eulerAngles;
			newRotation.z = 180;
			transform.rotation = Quaternion.Euler(newRotation);
		}
		else if(facingDirection == 3){
			Vector3 newRotation = transform.rotation.eulerAngles;
			newRotation.z = 270;
			transform.rotation = Quaternion.Euler(newRotation);		}
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
				facingDirection = 0;
			}
			else if (direction == -1) 
			{
				moveX = -1;
				moveY = 0;
				facingDirection = 2;
			}
			else if (direction == 2) 
			{
				moveX = 0;
				moveY = 1;
				facingDirection = 1;
			}
			else if (direction == -2) 
			{
				moveX = 0;
				moveY = -1;
				facingDirection = 3;
			}
		}
	}
	
	protected void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "crossRoad"){
			crossRoadConfig newCross = other.GetComponent<crossRoadConfig>();
			changeDirection(newCross);
		}
		if(other.tag == "trafficLight"){
			trafficLight newTL = other.GetComponent<trafficLight>();
			StartCoroutine(waitTillTrafficLight(newTL, moveX, moveY));
		}
	}
	
	IEnumerator waitTillTrafficLight(trafficLight tL, int currentX, int currentY){
		
		while(tL.redForCars == true){
			Debug.Log(tL.redForCars);
			moveX = 0;
			moveY = 0;
			yield return null;
		}
		Debug.Log(currentX);
		moveX = currentX;
		moveY = currentY;
	}
	
}