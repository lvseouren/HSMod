using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
	public List<GameObject> Maps = new List<GameObject>();
	public GameObject CurrentMap;
    
	private int _mapNumber = 0;

	private void Update()
    {
        // Checking if the user has pressed the Left Arrow
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_mapNumber == 0)
            {
                // Returning to the end of the list
                _mapNumber = 6;
            }
            else
            {
                // Substracting 1 to the map number count
                _mapNumber--;
            }

            SwitchMap();
        }

        // Checking if the user has pressed the Right Arrow
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_mapNumber == 6)
            {
                // Returning to the end of the list
                _mapNumber = 0;
            }
            else
            {
                // Adding 1 to the map number count
                _mapNumber++;
            }

            SwitchMap();
        }
    }

    private void SwitchMap()
    {
        // Destroying the current map
        Destroy(CurrentMap);

        // Instantiating a new map
        CurrentMap = Instantiate(Maps[_mapNumber]);
    }
}