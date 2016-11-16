using System;
using SQLite4Unity3d;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.Experimental.Networking;

public delegate List<T> setResult <T>( List<T> pResult);
public delegate void aSetTableConnection(string tbName, string tConnection);

[Serializable]
public class JSNDropMessage{
	public string Message;
	public string Type;
}

public class JSNDrop : MonoBehaviour
{
	private string connectionID = "";
	private string serverPathURL = "http://jsnDrop.com/Q";
	private string _result = "";
	private Dictionary <string,string> _tables;
	private JSNDropMessage _Message;
	private string qStr;


	public string Result { get { return _result;}}

	private IEnumerator SendPut <T> (setResult<T> setter){
		Debug.Log("SENT "+qStr);
		UnityWebRequest webReq = UnityWebRequest.Get(qStr);
		yield return webReq.Send();
		if(webReq.isError) {
			Debug.Log(webReq.error);
		}
		else {
			// Show results as text
			Debug.Log("Got TEXT "+webReq.downloadHandler.text);

			_Message =  JsonUtility.FromJson<JSNDropMessage>( webReq.downloadHandler.text);
			_result = webReq.downloadHandler.text; // a bit messy

			var  aResult = JsonUtility.FromJson<T>( webReq.downloadHandler.text);
			Debug.Log(aResult.ToString());
			List<T> theList = new List<T>();
			setter(theList);




		}
	}

	private List<string> getJSNStrings(string pServer)
	{
		List<string> result = new List<string>();

		var parts = Regex.Split( pServer,"},{");
		foreach(string part in parts){
			string newString = part.Replace("{","");
			newString = newString.Replace("}","");
			newString = "{"+newString+"}";
			result.Add(newString);
			Debug.Log(newString);
		}


		return result ;
	}

	private IEnumerator SendGet <T> (setResult<T> setter){
		Debug.Log("SENT "+qStr);
		UnityWebRequest webReq = UnityWebRequest.Get(qStr);
		yield return webReq.Send();
		if(webReq.isError) {
			Debug.Log(webReq.error);
		}
		else {
			// Show results as text
			Debug.Log("Got TEXT "+webReq.downloadHandler.text);

			//var  aResult = JsonUtility.FromJson<List <T>>( webReq.downloadHandler.text);
			//Debug.Log(aResult.ToString());
			Debug.Log("");
			var jsnStrs= getJSNStrings(webReq.downloadHandler.text);

			List<T> theList = new List<T>();
			foreach(string theItem in jsnStrs){
				T aDTO = JsonUtility.FromJson<T>( theItem);
				theList.Add(aDTO);
			}


			setter(theList);

		}
	}

	// Send Reg is slightly different
	// void aSetTableConnection(string tbName, string tConnection);
	private IEnumerator SendReg(string pTblName){
		UnityWebRequest webReq = UnityWebRequest.Get(qStr);
		yield return webReq.Send();
		if(webReq.isError) {
			Debug.Log(webReq.error);
		}
		else {
			// Show results as text
			Debug.Log(webReq.downloadHandler.text);

			_Message =  JsonUtility.FromJson<JSNDropMessage>( webReq.downloadHandler.text);
			if(_Message.Message == "NEW" || _Message.Message == "EXISTS"){
				_result = _Message.Type;
				connectionID = _Message.Type;
				if(_tables == null ){
					_tables = new Dictionary<string,string>();
				}
				if( !_tables.ContainsKey (pTblName))
					_tables.Add(pTblName,connectionID);
			}
			else {
				_result = webReq.downloadHandler.text; // a bit messy
			}

		}
	}


	public void jsnReg(string pGameName, string pTableName,string pPassword){


		 qStr = serverPathURL+"?cmd=jsnReg&value="+pGameName+","+pTableName+","+pPassword;

		 StartCoroutine(SendReg(pTableName));


	}

	public JSNDrop (){
		GameModel.JSNNet = this;
	}

	public void MakeTables(string pGameName,List <string> pTableNames, string pPassword)
	{
		
		foreach(string aName in pTableNames){
			 jsnReg(pGameName,aName,pPassword);

		}

	}

	public  void jsnGet<T>(string pTableName,string pStrPattern, setResult<T> aSetter ){

		string tblConnection = _tables[pTableName];
		if(tblConnection != ""){

			if(pStrPattern == "")
				qStr = serverPathURL+"?cmd=jsnGet&value="+connectionID;
			else
				qStr = serverPathURL+"?cmd=jsnGet&value="+connectionID+","+pStrPattern;
			StartCoroutine(SendGet<T> (aSetter));
		}			

	}

	private List<T> aSetter <T>( List<T> pResult){
		Debug.Log  (pResult.ToString() );
		return null;
	}
	public  void jsnPut<T>(string pTableName,string pStrKey, T aDTO, setResult<T> aSetter ){
		
		string tblConnection = _tables[pTableName];
		if(tblConnection != ""){
			 
			string jsnStr = JsonUtility.ToJson(aDTO);
			qStr = serverPathURL+"?cmd=jsnPut&value="+connectionID+","+pStrKey+","+jsnStr;
			StartCoroutine(SendPut<T>(aSetter)); 
		}			

	}
}


