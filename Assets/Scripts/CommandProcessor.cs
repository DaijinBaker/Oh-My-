using System; //Using unity namespace
using UnityEngine; //allows access to any class in the unity engine
using System.Collections; //allows access to any class in the system.collections
using System.Collections.Generic; //allows access to any class in the system.collections.generic

using System.IO; //allows access to any class in the system.IO

public delegate void aDisplayer( String value ); //public delegate references a method and hands it a parameter

public class CommandProcessor //creating a public class
{
		public CommandProcessor ()
		{
		}
		
	    public void Parse(String pCmdStr, aDisplayer display){   //Making a public method
			String strResult = "Do not understand"; //Creating a string variable and giving it the the string "Do not understand"

        char[] charSeparators = new char[] {' '}; //looks for spaces in the entered command
        pCmdStr = pCmdStr.ToLower(); //transform all characters to lower case in the entered commands
        String[] parts = pCmdStr.Split(charSeparators,StringSplitOptions.RemoveEmptyEntries); //Splitting up the users entered commands into single word values

        // process the tokens
        if ( parts.Length > 0) 
            switch ( parts[0]){ //switch statement
				case "pick": //If the first word is 'pick' then perform the code below.
                    if ( parts[1] == "up"){ //If the secound part is 'up' then store the data in the unity log
                        Debug.Log("Got Pick up"); //Set the unity log value to got pick up
                        strResult = "Got Pick up"; //Set the strResult to got pick up

                        if ( parts.Length == 3)
                        { //see if the length is 3 words long
                            String param = parts[2]; // Create a variable named param
                        }// do pick up command
						// GameModel.Pickup();
					}
					break; //end of the case statement
                case "go": //if the first word is 'go' then perform the code below
                    switch ( parts[1]) {  //Switch through the secound value
						case "north":  //If the secound part is north then store the data in the unity log
                            Debug.Log("Got go North"); //set the unity log value to got go north
                                                       // strResult = "Got Go North"; //set the strResult to got go north
                            GameModel.go(GameModel.DIRECTION.North); //go to the gamemodel and select the direction.north

                            break; //end of case statement for north
                        case "south":  //if the secound part is south then store the data in the unity log
                            Debug.Log("Got go South"); //set the unity log value to got go south
                                                       //strResult = "Got Go South"; //set the strResult to got go south
                            GameModel.go(GameModel.DIRECTION.South); //go to the game model and select the direction.south
                            break; //end of case statement for south
                        case "west": //if the secound part is west then store the data in the unity log
                            Debug.Log("Got go West"); //set the unity log to got go west
                                                      //strResult = "Got Go West"; //set the strResult to got go west
                            GameModel.go(GameModel.DIRECTION.West); //go to the game model and select the direction.west
                            break; //end of case statement for west
                        case "east": //if the secound part is east the store the data in the unity log
                            Debug.Log("Got go East"); //set the unity log to got go east
                                                      //strResult = "Got Go East"; //set the strResult to got go east
                            GameModel.go(GameModel.DIRECTION.East); //go to the game model and select the direction.east
                            break; //end of case statement for east
                        default: //if the value does not match, display next piece of code
                            Debug.Log(" do not know how to go there"); //display this message in the unity log
                            strResult = "Do not know how to go there"; //set strResult to do no know how to go there
                            break; //end of statment
                    }// end switch
						
						strResult = GameModel.currentPlayer.CurrentScene.Description;
			            display(strResult);  //display strResult results
                    break;  //end of case statement
                default:
			        Debug.Log("Do not understand");  //display this message in the unity log
                    strResult = "Do not understand";  //set the strResult to do not understand
                    break; //end of statement

            }// end switch
		    
			// return strResult;

		}// Parse
}


