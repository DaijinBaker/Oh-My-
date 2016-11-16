using System;
using SQLite4Unity3d;
using UnityEngine;

[Serializable]
public class SceneDTO
{
	[SerializeField] 
	private int _SceneID;
	[SerializeField] 
	private int _GameID;
	[SerializeField]
	private string _Name;
	[SerializeField]
	private string _Story;

	[PrimaryKey]
	public int SceneID{ get{return _SceneID;} set{_SceneID = value;}} 
	public int GameID{get{return _GameID;} set{_GameID = value;}}
	public string Name{ get{return _Name;} set{_Name = value;}}
	public string Story{ get{return _Story;} set{ _Story = value;}}

	public override string ToString ()
	{
		return string.Format ("[Scene: SceneID={0}, GameID={1},  Name={2}, Story={3}]", SceneID, GameID, Name, Story);
	}
}
public class SceneToSceneDTO
{
	[PrimaryKey][AutoIncrement]
	public int Id{ get; set;} 
	public int FromSceneID{ get; set;} 
	public int ToSceneID{ get; set;} 
	public string Label {get; set; }
	public override string ToString ()
	{
		return string.Format ("[Scene: FromSceneID={0}, ToSceneID={1}", FromSceneID, ToSceneID);
	}
}

