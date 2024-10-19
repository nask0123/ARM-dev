using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{ 
	public float dumping = 1.5f;
	public Vector2 offset = new Vector2(2f, 1f);
	private Transform player;
	private bool isLeft;
	private int lastX;
  

	void Start()
	{
		offset = new Vector2(Mathf.Abs(offset.x), offset.y);
		FindPlayer(isLeft);
	}
    
	void FindPlayer(bool playerIsleaft)
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		lastX = Mathf.RoundToInt(player.position.x);
    
		if(isLeft)
		{
			transform.position = new Vector3(player.position.x - offset.x, player.position.y - offset.y, transform.position.z);
		}
    
		else
		{
			transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(player)
		{
			int currentX = Mathf.RoundToInt(player.position.x);
			if(currentX > lastX) isLeft = false;
			else if (currentX < lastX) isLeft = true;
      
			Vector3 target;
			if (isLeft)
			{
				target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
			}
			else
			{
				target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
			}
      
			Vector3 currentPositon = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
			transform.position = currentPositon;
		}
	}
}
