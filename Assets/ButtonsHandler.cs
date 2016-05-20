using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ButtonsHandler : MonoBehaviour {

	// Create signleton to gamehandler later on
	GameHandler m_gameHandler;
	Color uglyColorRandom;

	// Use this for initialization
	void Start () {
		m_gameHandler = Camera.main.GetComponent<GameHandler> ();
		// Initial Color

		Button b = GetComponent<Button> ();
		ColorBlock cb = b.colors;

		// Randomly create "ugly" colors. TODO: Change to picked colors array later on. 
		uglyColorRandom = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f)) ;

		// Put same color to all button colors to "look like" buttons not affected by user pressing
		cb.normalColor = uglyColorRandom;
		cb.highlightedColor = uglyColorRandom;
		cb.pressedColor = uglyColorRandom;
		cb.disabledColor = uglyColorRandom;
		b.colors = cb;

		}

	public void ButtonPressed()
		{
		Debug.Log ("Button pressed: " + transform.name);
		m_gameHandler.RegisterButtonPress (gameObject.transform.name );
		}

	// Blink now
	public void Blink()
		{
		Button b = GetComponent<Button> ();
		ColorBlock cb = b.colors;

		// Put same color to all button colors to "look like" buttons not affected by user pressing
		cb.normalColor = Color.white;
		cb.highlightedColor =  Color.white;
		cb.pressedColor =  Color.white;
		cb.disabledColor =  Color.white;
		b.colors = cb;

		}

	// Back to ugly color
	public void Regular()
		{
		Button b = GetComponent<Button> ();
		ColorBlock cb = b.colors;

		// Put same color to all button colors to "look like" buttons not affected by user pressing
		cb.normalColor = uglyColorRandom;
		cb.highlightedColor =  uglyColorRandom;
		cb.pressedColor =  uglyColorRandom;
		cb.disabledColor =  uglyColorRandom;
		b.colors = cb;

		}
	}	
