//Allows access to the classes of these systems, to be used in the script
using System.Collections;
using UnityEngine;

public class RunOnlyOnce : MonoBehaviour
{
    public static RunOnlyOnce instance; //creating a static instance of the class RunOnlyOnce
    void Awake()
    {
        if (instance != null && instance != this)
        {
            DestroyImmediate(gameObject); //destroy specified gameObject
            return;
        }
        instance = this;
        GameModel.makeScenes(); //call function from GameModel

        // DontDestroyOnLoad(gameObject);
    }
}