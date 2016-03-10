using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour {

	public List<GameObject> maps = new List<GameObject>();
	public GameObject currentMap;
	private int _mapNumber = 0;
	private bool _changeToZero = false;

	void Update () {
		if(_mapNumber == 7) {
			_changeToZero = true;
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)) {
			Destroy (currentMap);
			currentMap = Instantiate (maps [_mapNumber++]);
		}
		if(_changeToZero == true) {
			_mapNumber = 0;
			_changeToZero = false;
		}
	}
}
