using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {

	public enum GameState
		{
		EStart,
		EBlinking, 
		EUserTurn
		};

	// First and only button
	public GameObject m_button; 

	public Text m_uiText;
	ArrayList buttons = new ArrayList();

	ArrayList buttonsOrderGenerated = new ArrayList();

	// GameState 
	GameState m_gs;

	bool StartBlink = false;
	float timeToStartGame = 3f;
	float blinkTime = 1f;
	float coolDownTime = 0.5f;
	int blinkButton;
	int blinkTimes = 0;

	int userPressToCheck;

	// Use this for initialization
	void Start () {
		m_button.SetActive (false);
		m_uiText.text = "Wait for blinks, when last blink is done, remember your decissions";
		// Create initial buttons
		for (int i = 0; i < 3; i++) 
			{
			GameObject but = Instantiate (m_button);
			RectTransform bt = but.GetComponent<RectTransform> ();
			but.name = "button" + i;
			but.SetActive (true);

			but.transform.position = new Vector3 (m_button.transform.position.x, 
				m_button.transform.position.y + i*bt.rect.height, 
				m_button.transform.position.z); 
			but.transform.parent = m_button.transform.parent;

			buttons.Add(but);
			}
		}

	// Update is called once per frame
	void Update () {
		
		if (m_gs == GameState.EStart) 
			{
			if (timeToStartGame > 0) 
				{
				timeToStartGame -= Time.deltaTime;
				} 
			else 
				{
				m_gs = GameState.EBlinking;
				timeToStartGame = 3f;
				blinkButton = Random.Range (0, buttons.Count);
				GameObject but = (GameObject)buttons [blinkButton];
				ButtonsHandler bt = but.GetComponent<ButtonsHandler> ();
				bt.Blink ();
				Debug.Log ("BO COUNT" + buttonsOrderGenerated.Count);
				buttonsOrderGenerated.Add (bt.name);
				m_uiText.text = "Follow blinks";

				}
			} 
		else if (m_gs == GameState.EBlinking) 
			{
			if (blinkTime > 0) 
				{
				blinkTime -= Time.deltaTime;
				} 
			else if (coolDownTime > 0) 
				{
				coolDownTime -= Time.deltaTime;
				GameObject but = (GameObject)buttons [blinkButton];
				ButtonsHandler bt = but.GetComponent<ButtonsHandler> ();
				bt.Regular ();
				}
			else 
				{
				blinkTime = 1f;
				coolDownTime = 0.5f;
				blinkTimes++;

				if (blinkTimes < buttons.Count ) 
					{
					m_uiText.text = "Follow blinks" + blinkTimes;
					blinkButton = Random.Range (0, buttons.Count);
					GameObject butNew = (GameObject)buttons [blinkButton];
					ButtonsHandler btNew = butNew.GetComponent<ButtonsHandler> ();
					btNew.Blink ();

					buttonsOrderGenerated.Add (btNew.name);
					}
				else
					{
					m_uiText.text = "Now you. Take your time" + blinkTimes;

					m_gs = GameState.EUserTurn;
					blinkTimes = 0;
					}
				}
			}
			
	}

	// Take button events and function based to those when gamestate is user turn. 
	// Check if user is pressing in correct order.
	public void RegisterButtonPress( string buttonName ) 
		{
		if ( m_gs == GameState.EUserTurn )
			{
			string nameOfButton = (string)buttonsOrderGenerated [userPressToCheck];
			if ( nameOfButton == buttonName) 
				{
				Debug.Log ("Corret" );

				userPressToCheck++;

				m_uiText.text = "CORRECT" + userPressToCheck;

				if (userPressToCheck == buttonsOrderGenerated.Count) 
					{
					m_uiText.text = "WICTORY!";
					m_gs = GameState.EStart;
					buttonsOrderGenerated.Clear();
					userPressToCheck = 0;

					// Add new button to row
					GameObject but = Instantiate (m_button);
					RectTransform bt = but.GetComponent<RectTransform> ();
					but.name = "button" + buttons.Count;
					but.SetActive (true);

					but.transform.position = new Vector3 (m_button.transform.position.x, 
						m_button.transform.position.y + buttons.Count*bt.rect.height, 
						m_button.transform.position.z); 
					but.transform.parent = m_button.transform.parent;

					buttons.Add(but);
					}
				} 
			else 
				{
				Debug.Log ("Fail" + nameOfButton + " Compare to " + buttonName );
				m_uiText.text = "FAILED";
				m_gs = GameState.EStart;
				buttonsOrderGenerated.Clear();
				buttonsOrderGenerated = new ArrayList ();
				userPressToCheck = 0;
				}

			}
		}
	}
