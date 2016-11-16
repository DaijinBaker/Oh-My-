using System;
using SQLite4Unity3d;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class PlayerLocationDTO
{
	[SerializeField]
	private int _PlayerID;
	[SerializeField]
	private int _LocationID;

	public int PlayerID{ get{ return _PlayerID;} set{_PlayerID = value;}}
	public int LocationID{ get{ return _LocationID;} set{_LocationID = value;}}

	public void Put(){
	}

	public void Get(){
	}

	public  List <PlayerLocationDTO> GetAll(){
		return null;
	}
}

