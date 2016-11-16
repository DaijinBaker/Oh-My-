//Allows access to the classes of these systems, to be used in the script
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadOnclick : MonoBehaviour
{

    public void LoadScene(string pSceneName)
    { //create a method named loadscene and handing it parameter pSceneName
        SceneManager.LoadScene(pSceneName); //using scene manager method to hand it scene it will navigate to
    }

    public void ShowCanvas(Canvas pCanvas)
    { //creating a method called ShowCanvas and handing it parameter pCanvas

        pCanvas.gameObject.SetActive(true); //setting parameter to the gameobject and activating it
        Debug.Log(gameObject.name); //make unity log game objects name
        Canvas[] canvases = gameObject.GetComponentsInChildren<Canvas>(); //get the canvases out of canvas and store it into the canvases array variable

        foreach (Canvas cnv in canvases)
        { //loop throught he canvases array variables
            if (cnv.name != pCanvas.name)
            {
                cnv.gameObject.SetActive(false);
            }
        }

    }
}

