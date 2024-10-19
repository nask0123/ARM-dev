using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficLight : MonoBehaviour
{
	public bool redForCars, redForWalkers;
	
    // Start is called before the first frame update
    void Start()
    {
	    redForCars = true;
	    redForWalkers = false;
	    StartCoroutine(ChangeTrafficLights());
    }

    
	IEnumerator ChangeTrafficLights()
	{
		while (true)
		{
			yield return new WaitForSeconds(5); 
			redForWalkers = false;
			redForCars = true;

			yield return new WaitForSeconds(5); 
			redForCars = false;
			redForWalkers = true;
		}
	}
    
    
}
