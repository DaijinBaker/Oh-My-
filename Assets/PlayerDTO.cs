using System;
using SQLite4Unity3d;
using UnityEngine;

[Serializable]
public class PlayerDTO
{
		public int PlayerID;
		public int LocationID;
		public string PlayerName;
		public string Password;
		public float Experience;

		public PlayerDTO ()
		{
		}
}


