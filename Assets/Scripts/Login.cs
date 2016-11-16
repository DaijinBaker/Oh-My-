// Login.cs
//Allows access to the classes of these systems, to be used in the script
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {
	/*public Text Username;
	public Text Password;*/


	// Use this for initialization
	public void login() //creating method called login
	{
		DataService _connection = new DataService (); //creating an instance of the dataservice 
		if (_connection.DbExists ("Ohmy!database.db")) { //seeing if the database exists
			_connection.Connect (); //connecting to the the database specified

			GameObject aUsername = GameObject.Find("username"); //creating an instance and finding the game object
			InputField aNametext = aUsername.GetComponent<InputField> (); //creating an instance of the inputfield and calling the get component function 
			string username = aNametext.text; //creating a string that equals the inputfields instance

			GameObject aPassword = GameObject.Find("password");
			InputField aPasstext = aPassword.GetComponent<InputField> ();
			string password = aPasstext.text;//
		
			if (_connection.GetAccount (username, password)) { //calling the get account function 
				SceneManager.LoadScene (2); //load the next scene upon succeeding
				//Debug.Log ("success");
			} else {
				//Debug.Log ("failed");
			}
}
}
}