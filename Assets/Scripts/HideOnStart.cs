//Allows accessto the classes of these systems, to be used in the script
using UnityEngine;
using System.Collections;

public class HideOnStart : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(false); //setting the gameobject activation to false
    }

}

