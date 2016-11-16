using UnityEngine;
using System.Collections;

public class JSONHttp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	private string _serverURL = "http://jsnDrop.com/jsndrop";

	public string getJSON<T>(T anObject){
		string result = JsonUtility.ToJson(anObject);
		Debug.Log(result);
		return result;
	}
}
