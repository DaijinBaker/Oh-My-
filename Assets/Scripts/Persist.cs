//Allows access to the classes of these systems, to be used in the script
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Persist : MonoBehaviour {
	public static Persist control;   //creating an instance of the class Persist
    private float health = 100;  //creating a private float of health and setting its default value to 100
    private float experience = 0; //creating a private float of score and setting its value to 0
    public Text HealthText;
	public Text ExperienceText;

	public float Health{ //encapsulating the health field
		get{
			return health;
		}
		set{
			health = value;
			HealthText.text = "Health = "+ health.ToString();
		}

	}

	public float Experience{
		get{
			return experience;
		}
		set{
			experience = value;
			ExperienceText.text = "Experience = "+ experience.ToString();
		}

	}
	// Use this for initialization
	void Start () {
		// PLAYER PREFS
		// PlayerPrefs.SetInt("health",21);
		//int health = PlayerPrefs.GetInt("health");

		// DontDestroyOnLoad
		//DontDestroyOnLoad(gameObject);



	}

	// Now there can be only one of
	void Awake(){
		if( control == null){
			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else if(control!= this){
			Destroy(gameObject);
		}

		// SINGLETON ^^^^

	}

	// Serialisation

	// Unity Serialisation

	// Update is called once per frame
	//void Update () {
	
	//}
     
	public void Save(){  //creating a function called save
		BinaryFormatter bf = new BinaryFormatter(); //this line is responsible for the serialization
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat"); //creating a pathway for the data to be stored in

        String path = Application.persistentDataPath ;
		PlayerData data  = new PlayerData(); //creating an new instance of player data
        data.health = Health; //store health
        data.experience = Experience; //store experience

        bf.Serialize(file,data); //calling the serialize function to save our data

        file.Close(); //end of file
    }

	public void Load(){  //creating a function called load
		if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))  //checking if a saved file exists
        {
			BinaryFormatter bf = new BinaryFormatter();  //creating a new binary formatter
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat",FileMode.Open); //creating a pathway but this time we are opening the file
            PlayerData data = (PlayerData)bf.Deserialize(file); //call finds the file at the location we specified above and deserializes it. 
            file.Close(); //end of file

            Health = data.health; //gets health that was saved
            Experience = data.experience; //gets score 
        }
	}
}

[Serializable]
class PlayerData  //creating class playerdata
{
	public float health;
	public float experience;
}