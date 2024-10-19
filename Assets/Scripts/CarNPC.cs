using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNPC : MonoBehaviour
{
	public float speed;
	private Rigidbody2D rb;
	public int moveX, moveY;
	[SerializeField] short facingDirection;
	[SerializeField] float distanceToKeep;
	[SerializeField] LayerMask carLayer;
	public int direction;
	
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
		keepDistance();

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
	
	public void keepDistance(){
		Vector2 direction = Vector2.zero;
		switch(facingDirection)
		{
		case 0: direction = Vector2.right; break;   
		case 1: direction = Vector2.up; break;      
		case 2: direction = Vector2.left; break;    
		case 3: direction = Vector2.down; break;    
		}
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, distanceToKeep, carLayer);
		foreach (RaycastHit2D hit in hits)
		{
			if(hit != null && hit.collider.gameObject != gameObject){
				StartCoroutine(waitTillForwardGoes(hit, moveX, moveY));
			}
		}
		
	}
	
	public void changeDirection(crossRoadConfig crc){
		
		List<int> possibleDirections = new List<int>();
		
		if (crc.Right) possibleDirections.Add(1);  
		if (crc.Left) possibleDirections.Add(-1);  
		if (crc.Up) possibleDirections.Add(2);     
		if (crc.Down) possibleDirections.Add(-2); 
		possibleDirections.Remove(-1*direction);
		
		if (possibleDirections.Count > 0)
		{
			 direction = possibleDirections[Random.Range(0, possibleDirections.Count)];

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
			moveX = 0;
			moveY = 0;
			yield return null;
		}
		moveX = currentX;
		moveY = currentY;
	}
	
	IEnumerator waitTillForwardGoes(RaycastHit2D hit, int currentX, int currentY){
		while (hit.collider.GetComponent<CarNPC>().moveX == 0 && hit.collider.GetComponent<CarNPC>().moveY == 0 )
		{
			moveX = 0;
			moveY = 0;
			yield return null; 
		}

		
		moveX = currentX;
		moveY = currentY;
	}
}
