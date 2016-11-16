//Allows access to the classes of these systems, to be used in the script
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {
	InputField input; //preset functions that we are defining 
    InputField.SubmitEvent se;
	InputField.OnChangeEvent ce;
	public Text output;

	public void TextUpdate(string aStr){
		output.text = aStr; //setting the text to be displayed as the variable
    }


	// Use this for initialization
	void Start () {
		// GameModel.makeScenes();
		input = this.GetComponent<InputField>(); //fill the input preset function with the contents of the inputfield
        se = new InputField.SubmitEvent(); //fill the variable se with a new instance of InputField.SubmitEvent
        se.AddListener(SubmitInput); //call the addlistener inside the se instance and hand it the parameter submitinput
                                     /*
                                     ce = new InputField.OnChangeEvent();
                                     ce.AddListener(ChangeInput);
                                     */
        input.onEndEdit = se; //fill the input.onendedit with the value of the se instance
        output.text = GameModel.currentPlayer.CurrentScene.Description; //display the description of the currentscene in the output variable
                                                                        //input.onValueChanged = ce;

    }
	
	// Update is called once per frame
	/*
	 * void Update () {
	
	}
	*/

	private void SubmitInput(string arg0)
	{
		string currentText = output.text; //create a new variable named currenttext and store the output.text in it

        CommandProcessor aCmd = new CommandProcessor(); //create a new instance of commandprocessor and named it aCmd


        aCmd.Parse(arg0,TextUpdate); //call the Parse method of the aCmd instance and hand it the parameters arg0 and textUpdate

        input.text = "";  //empty the input textbox
        input.ActivateInputField(); //reactivate the input text box



    }

	private void ChangeInput( string arg0)
	{
		Debug.Log(arg0);
	}
}
