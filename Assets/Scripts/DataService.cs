//Allows access to the classes of these systems, to be used in the script
using SQLite4Unity3d;
using UnityEngine;
using System.IO;

#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService  { //creating a class called DataService

	private SQLiteConnection _connection; //creating a instance of the class SQLiteConnection as _connection
    private string currentDbPath = "";
	private bool dbExists; //creating a boolean called dbExists

    public bool	DbExists(string DatabaseName){
		// Watch out! this method has a side effect
		bool result = false;

		#if UNITY_EDITOR
		var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName); //creating the pathway to te database
        result = File.Exists(dbPath);
		#else
		// check if file exists in Application.persistentDataPath
		var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

		if (!File.Exists(filepath))
		{
		result = false;
		Debug.Log("Database not in Persistent path");
		// if it doesn't ->
		// open StreamingAssets directory and load the db ->

		#if UNITY_ANDROID 
		var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
		while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
		// then save to Application.persistentDataPath
		File.WriteAllBytes(filepath, loadDb.bytes);
		#elif UNITY_IOS
		var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		#elif UNITY_WP8
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);

		#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		#else
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);

		#endif

		Debug.Log("Database written");
		}

		var dbPath = filepath;
		#endif

		currentDbPath = dbPath; //the current path value equals dbPath
        Debug.Log("Final PATH: " + dbPath); //unity log shows dbPath

        return result;
	}

	public void Connect(){ //creating a function called connect
		_connection = new SQLiteConnection(currentDbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create); //connecting to the specified database


    }
	public DataService(){
           
	}



// Set up utilities
	public void deleteDatabaseFile() { //creating a function called deleteDatabaseFile
        File.Delete(currentDbPath);  //Deletes File of the current DB path
    }

// Scene Save

	private void CreateIfNotExists<T>( ) where T:new () {
		// Since we are taking the perspective that Set puts the data into the
		// database, if the table does not exist then we create.
		_connection.CreateTable<T>();

	}
	private void IfNotExistsCreateSceneToScene( ){
	  // Since we are taking the perspective that Set puts the data into the
	  // database, if the table does not exist then we create.
	  _connection.CreateTable<SceneToSceneDTO>();

	}
	private void IfNotExistsCreateScene(){

		// Since we are taking the perspective that Set puts the data into the
		// database, if the table does not exist then we create.
		_connection.CreateTable<SceneDTO>();
	
	}
	private bool SceneToFromExists( int pFromID, int pToId)
	{
	   var y = _connection.Table<SceneToSceneDTO>().Where(
	              x => x.FromSceneID == pFromID && x.ToSceneID == pToId).FirstOrDefault();
		return y != null;

	}
	private bool SceneExists(int pSceneID){
		var y = _connection.Table<SceneDTO>().Where(
				x => x.SceneID == pSceneID).FirstOrDefault();

		return y != null;

	}

	private void SetScene(SceneDTO aSceneDTO){
		CreateIfNotExists<SceneDTO>();

		if(SceneExists(aSceneDTO.SceneID)){
			_connection.Update(aSceneDTO);
		}
		else{
			_connection.Insert(aSceneDTO);
		}
		// aSceneDTO.ToString();
	}
	private void SetSceneToFrom(Scene pScene, Scene pDirection, string pLabel){
			if(pDirection != null ){

			  // IfNotExistsCreateSceneToScene();
				CreateIfNotExists<SceneToSceneDTO>();
				SceneToSceneDTO aDTO = new SceneToSceneDTO{
					FromSceneID = pScene.ID, 
					ToSceneID = pDirection.ID,
					Label = pLabel
					};

				if(SceneToFromExists(aDTO.FromSceneID,aDTO.ToSceneID)){
					_connection.Update(aDTO);
				}
				else{
					_connection.Insert(aDTO);
				}

			}
	}// SetSceneToFrom

	public void SaveScenes( ){
		foreach( Scene aScene in Scene.AllScenes)
		{
				SceneDTO currentSceneDTO = new SceneDTO{
							SceneID = aScene.ID,
							GameID = 1, // need to add a Game Number here
							Name = "Any name",
							Story =  aScene.Description
							};

				SetSceneToFrom(aScene, aScene.North, "North");
				SetSceneToFrom(aScene, aScene.South,"South");
				SetSceneToFrom(aScene, aScene.East, "East");
				SetSceneToFrom(aScene, aScene.West, "West");
				SetScene(currentSceneDTO);
				
		}

	}

	public void MakeNetPersist(){
		List <string> tableNames = new List<string>();

		tableNames.Add("tblPlayer");
		GameModel.JSNNet.MakeTables("ZORG321",tableNames,"53211");
		Debug.Log(GameModel.JSNNet.Result);
	}

// Scene Load
	public void LoadScenes( ){

		// Clear the current Scenes
		if(Scene.AllScenes.Count > 0){
			Scene.AllScenes.Clear();
		}

		// What to do about the current Scene ? GameModel.currentPlayer.CurrentScene

		// Get the Scenes
		var SceneDTOs = _connection.Table<SceneDTO>();

		// Rebuild the local data structure
		foreach(SceneDTO aDTO in SceneDTOs){
		    // Check we have not created this already!!
			Scene firstCheckScene = Scene.AllScenes.Find(x => x.ID == aDTO.SceneID);
			Scene aScene;
			if( firstCheckScene == null)
				aScene = new Scene(){ ID = aDTO.SceneID, Description = aDTO.Story};
			else{
				aScene = firstCheckScene;
			}
			string testJSN = aDTO.ToString();
			// Get North , South, East and West
			var directions = _connection.Table<SceneToSceneDTO>().Where(
						x => x.FromSceneID == aDTO.SceneID);
		    
			foreach( SceneToSceneDTO aDirScene in directions){
				var aSceneDTO = (_connection.Table<SceneDTO>().Where(
						x => x.SceneID == aDirScene.ToSceneID)).FirstOrDefault();
		        
				Scene aCheck = Scene.AllScenes.Find(x => x.ID == aSceneDTO.SceneID);
				Scene toScene;
				if( aCheck == null){
		          	toScene = new Scene(){ID = aSceneDTO.SceneID, 
										Description = aSceneDTO.Story};
				}
				else{
					toScene = aCheck;
				}

				switch( aDirScene.Label){
					case("North"): 
									aScene.North = toScene;
					break;
					case ("South"):
									aScene.South = toScene;
					break;
					case("East"): 
									aScene.East = toScene;
					break;
					case ("West"):
									aScene.West = toScene;
					break;
				}
			}//for each Direction
		    

		Debug.Log(aScene.ToString());

		}// for each SceneDTO

	}

    public bool GetAccount(string pusername, string ppassword)
    { //create a function called GetAccount and hand it the parameters

        int count = _connection.Table<Account>().Where(x => x.Username == pusername && x.Password == ppassword).Count(); //connect to table and select from the table Username and Password and give them the parameters

        if (count > 0) //if there is more than one return the bool
        {
            return true; //if there is more than 0 return boolean as true
        }
        else
        {
            return false; //or return it false if less than 0
        }

    }
}
