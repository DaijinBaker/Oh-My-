//Allows access to the classes of these systems, to be used in the script
using UnityEngine;
using System.Collections;

public class DataServiceUtilities : MonoBehaviour {

	public void DeleteDB(){  //creating a method called DeleteDB
		DataService _connection = new DataService();  //creating an instance of DataService
        if (_connection.DbExists("GameNameDb")){  //checking if Database Exists
			_connection.deleteDatabaseFile();  //calling the deleteDatabaseFile from dataservice

        }
	}

	public void Save(){  //creating a method called Save
		DataService _connection = new DataService();  //creating an instance of DataService
        if (_connection.DbExists("GameNameDb")){ //checking if the database exists
			_connection.Connect();  //connecting to the database
            _connection.SaveScenes();  //calling the function savescenes
        }
	}

	public void Load(){  //creating a method called load
		DataService _connection = new DataService();  //creating an instance of dataservice
        if (_connection.DbExists("GameNameDb")){  //checking if the database exists
			_connection.Connect();  //connecting to the database
            _connection.LoadScenes();  //calling the function LoadScenes
        }
	}
}
