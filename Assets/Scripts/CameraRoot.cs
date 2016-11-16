//Allows access to the classes of these systems, to be used in the script
using UnityEngine;
using System.Collections;

public class CameraRoot : MonoBehaviour {
	void Awake(){
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		GameModel.makeScenes();//calls the gamemodel to run the makeScenesfunction
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
