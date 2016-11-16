//Allows access to the systems classes to be used in this script
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

// Is this a factory?
public static class GameModel  //creating a public static class called gamemodel
{

	private static String _name;  //creating a private string called _name
    private static Player _player = new Player();  //creating an instance of the player class
    public enum DIRECTION  {North, South, East, West};  //creating a public enumerator list
    private static Scene _start_scene; //creating an instance of the class Scene
    public static Players PlayersInGame = new Players(); //creating an instance of the players class
    public  static JSNDrop JSNNet;

	public static Scene Start_scene{ //encapsulating the start_scene field
		get 
		{ 
			return _start_scene;  
		}
		set{
			_start_scene = value; 
		}

	}

	public static string Name{  //encapsulating the _name field
		get 
		{ 
			return _name;  
		}
		set{
			_name = value; 
		}

	}

	public static Player currentPlayer  //encapsulating the _player field
    {
		get
		{
			return _player;
		}
		set
		{
			_player = value;
		}

	}
	public static void go(DIRECTION pDirection) //create a public method with the parameter set as pDirection
    {
		currentPlayer.Move(pDirection);  //move player in the direction entered
    }

	public static void makeScenes() //create a public method
    {
		Scene tmp; //create a variable called tmp with the scene class assigned to it 
        DataService theService = new DataService(); //creating an instance of dataservice
        theService.MakeNetPersist();

		// uncomment the following two lines to start with an empty database
		// if(theService.DbExists("GameNameDb"))
		//	theService.deleteDatabaseFile();

		// Watch out DbExists has a side effect!
		if(theService.DbExists("GameNameDb")) //check if the database exists
        {
			theService.Connect();  //connect to the database
            theService.LoadScenes();  //call the loadScenes function
            currentPlayer.InitialisePlayerState();
			currentPlayer.CurrentScene = Scene.AllScenes[0];
		}
		else
		{
			Start_scene = new Scene();  //creating an instance of the scene class

            tmp = new Scene();  //make an empty scene
            Start_scene.ID = 1;  //the start scene will be the scene that equals id 1
            Start_scene.North = tmp;  //make the scene north of the start scene the walk scene
            Start_scene.Description = " You are lost in a forest"; //description of the scene with the id 1

            tmp.ID = 2;  //the walk scene gets the id 2 for the next scene
            tmp.Description = " You are in the Mall"; //description of the scene with the id as 2
            tmp.South = Start_scene; //make the scene south of this one take you back to start scene


            tmp.North = new Scene (); //make a new scene as tmp.north
            tmp.North.ID = 3; //make the walk.north equal the scene id 3
            tmp.North.Description = "You fell off a cliff"; //displays scene description
            tmp.North.South = tmp;// // takes you back to the walk scene

            currentPlayer.InitialisePlayerState();
			currentPlayer.CurrentScene = Start_scene;


			theService.Connect();   //call fucntion that connects to database
            theService.SaveScenes();  //call function that saves scenes

        }

	}


}

