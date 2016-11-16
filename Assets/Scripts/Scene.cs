using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Scene //creating a class called Scene
{
    private Players _players = new Players(); //creating an instance of the Players class
    private Scene[] _connected_scenes = new Scene[4]; //create an instance of the scene class,there are only to be 4 different scenes from a single scene
    private string _description = "default"; //create a variable named _description of the type string and fill it with the string
    private int _id; //create a variable called _id
    public static List<Scene> AllScenes = new List<Scene>(); //creating a static list and filling it with Scenes

    public int ID
    { //encapsulating the ID field
        get
        {
            return _id;
        }
        set
        {
            _id = value;
        }
    }

    public string Description
    { //Encapsulating the Description field
        get
        {
            return _description;
        }
        set
        {
            _description = value;
        }
    }

    public Scene North
    { //encapsulate the north field in the scene class
        get
        {
            return _connected_scenes[(int)GameModel.DIRECTION.North]; //makes the north scene connectable
        }
        set
        {
            _connected_scenes[(int)GameModel.DIRECTION.North] = value; //set the directional value to north
        }
    }

    public Scene South
    { //encapsulate the south field in the scene class
        get
        {
            return _connected_scenes[(int)GameModel.DIRECTION.South]; //makes the south scene connectable
        }
        set
        {
            _connected_scenes[(int)GameModel.DIRECTION.South] = value; //set the directional value to south
        }
    }
    public Scene West
    { //encapsulate the West field in the scene class
        get
        {
            return _connected_scenes[(int)GameModel.DIRECTION.West]; //makes the west scene connectable
        }
        set
        {
            _connected_scenes[(int)GameModel.DIRECTION.West] = value; //set the directional value to west
        }
    }
    public Scene East
    {  //encapsulate the East field in the scene class
        get
        {
            return _connected_scenes[(int)GameModel.DIRECTION.East]; //makes the east scene connectable
        }
        set
        {
            _connected_scenes[(int)GameModel.DIRECTION.East] = value; //set the directional value to east
        }
    }
    public Scene()
    {
        Scene.AllScenes.Add(this);
    }

    public override string ToString (){
			string result = JsonUtility.ToJson(this);
			Debug.Log(result);
			return result;
		}
	    
	}


