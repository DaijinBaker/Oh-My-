//Allows access to the classes of these systems, to be used in the script
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

	[Serializable]
	public class Player //creating a class called Player
{
		// Class
		private static int _player_number = 0; //creating a static variable and setting it to 0

    // Instance
    private int _number = (Player._player_number++);  //taking the _player_number static variable and adding 1 
    private string _name; //creating a private variable of the type string
    private Item[] _inventory;    //creating a array this will contain all of the items in the inventory.
    private Scene _currentScene; //create a private variable using type scene

    public Scene CurrentScene //encapsulating the current scene field
    { 
			get{
				return _currentScene;
			} 
			set{
				_currentScene = value;
			}
		}
		public String Name //encapsulating the name field
    { 
			get{
				return _name;
			} 
			set{
				_name = value;
			}
		}

		 
		private void AddExperience(){ //creating a function
			Persist.control.Experience = Persist.control.Experience + 10; //creating an instance of the Persist class, getting the score and adding 10.
    }
		
		private string MakePlayerID(){
		return "\"PlayerID\":"+_number.ToString();
		}

		private List<PlayerDTO> GetPlayerDTO(List<PlayerDTO> aDTOList){
			Debug.Log("CAME BACK");
			foreach(PlayerDTO aPlayer in aDTOList){
				Debug.Log("GOT BACK" +aPlayer.PlayerID.ToString()+"<<<");
			    
			}
			
		      return aDTOList;
		}

		private List<PlayerDTO> NextCommand(List<PlayerDTO> aDTO){
			Debug.Log("CALLING NEXT");
		    GameModel.JSNNet.jsnGet<PlayerDTO>("tblPlayer","",GetPlayerDTO);
		  return aDTO;
		}

		private void MoveInDirection(Scene pToScene){
			if(  pToScene != null){
				_currentScene =   pToScene;

				PlayerDTO aDTO = new PlayerDTO{
					PlayerID = _number,
					LocationID = CurrentScene.ID,
					PlayerName = "test",
					Password = "12345",
					Experience = Persist.control.Experience
				};
			 	AddExperience();
			    
				string anID = MakePlayerID();
				GameModel.JSNNet.jsnPut<PlayerDTO>("tblPlayer",anID,aDTO,NextCommand);
				
			}
		}
		public void Move(GameModel.DIRECTION pDirection){ //creating method called move
			
			switch(pDirection)
        { //check direction value that has been set in the gamemodel
            case GameModel.DIRECTION.North: //if the direction north is inputted the code in MoveInDirection will activate
                MoveInDirection(_currentScene.North);
						break;
				case GameModel.DIRECTION.South:  //if the direction South is inputted the code in MoveInDirection will activate
                MoveInDirection(_currentScene.South);
						break;
				case GameModel.DIRECTION.East:  //if the direction East is inputted the code in MoveInDirection will activate
                MoveInDirection(_currentScene.East);
						break;
				case GameModel.DIRECTION.West: //if the direction West is inputted the code in MoveInDirection will activate
                MoveInDirection(_currentScene.West);
						break;
			}
		}

		public void InitialisePlayerState(){
			if(Persist.control != null){
				Persist.control.Experience = 0;
				Persist.control.Health = 100;
			}
		}
		public Player ()
		{
			//InitialisePlayerState();
		}
	}


