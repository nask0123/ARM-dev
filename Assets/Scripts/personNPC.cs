using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class personNPC : MonoBehaviour
{
	public float speed;
	private Rigidbody2D rb;
	public int moveX, moveY;
	public LayerMask sideWalkLayer, playerLayer;
	public GameObject dialogBox;
	private string[] whatToSay;
	
    // Start is called before the first frame update
    void Start()
	{
		whatToSay = new string[3];
		whatToSay[0] = "Қарағым-ау не істеп жүрсің!";
		whatToSay[1] = "Тамақ жегін к-келеді ме?";
		whatToSay[2] = "ЭЭй кет әрі!";
		
	    rb = GetComponent<Rigidbody2D>();
		dialogBox.SetActive(false);
    }
    

    // Update is called once per frame
    void Update()
    {
	    move();
	    findPlayer();
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
			crossRoadConfig config = other.GetComponent<crossRoadConfig>();
			changeDirection(config);
		}
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
	
	public void findPlayer(){
		Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 2f, playerLayer);
		if(playerCollider != null){
			int i = Random.Range(0,3);
			talkToPlayer(whatToSay[i]);
		}
	}
	public void talkToPlayer(string textToSay){
		dialogBox.GetComponent<Text>().text = textToSay;
		dialogBox.SetActive(true);
		StartCoroutine(stopTalking());
	}
	IEnumerator stopTalking(){
		yield return new WaitForSeconds(3);
		dialogBox.SetActive(false);
	}
}
