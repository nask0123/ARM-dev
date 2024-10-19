using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficLight : MonoBehaviour
{
	public bool redForCars, redForWalkers;
	public SpriteRenderer color;
	
    // Start is called before the first frame update
    void Start()
    {
	    redForCars = true;
	    color.color = Color.red;
	    redForWalkers = false;
	    StartCoroutine(ChangeTrafficLights());
    }

    
	IEnumerator ChangeTrafficLights()
	{
		while (true)
		{
			yield return new WaitForSeconds(5); 
			redForWalkers = false;
			color.color = Color.red;
			redForCars = true;

			yield return new WaitForSeconds(5); 
			redForCars = false;
			color.color = Color.green;
			redForWalkers = true;
		}
	}
    
    
}
