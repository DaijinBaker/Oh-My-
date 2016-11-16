//Allows access to the classes of these systems, to be used in the script
using System;
using System.Collections;
using System.Collections.Generic;

using System.IO;

public class Players //creating a class 
{
    private List<Player> _players = new List<Player>(); //creating a list, Fill this list with all the player instances in the player class

    public Player this[int index] //encapsulate the _players field
    {
        get
        {
            return _players[index];
        }
        set
        {
            _players[index] = value;
        }
    }

    public Players()
    {

    }
}


