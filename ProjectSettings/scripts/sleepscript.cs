using UnityEngine;
using System.Collections;

public class sleepscript : MonoBehaviour {
	GameController gameController;
	DwarfController dwarfcontroller; 
	public int sleepTime;
	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
		dwarfcontroller = gameObject.GetComponent<DwarfController> ();
	}

	public void startsleep(){
		StartCoroutine (sleep());
	}

	public IEnumerator sleep(){
		dwarfcontroller.setSleepStatut("Sleeping");
		dwarfcontroller.setcurrentjob(dwarfcontroller.getjob());
		dwarfcontroller.setjobstatut("on Break");
		dwarfcontroller.updateStatutText();
		Debug.Log (dwarfcontroller.getcurrentjob ());
		yield return new WaitForSeconds(sleepTime);
		dwarfcontroller.setSleepStatut("Awake");
		dwarfcontroller.setjobstatut (dwarfcontroller.getcurrentjob ());
		dwarfcontroller.updateStatutText();
		if (dwarfcontroller.getjobplace() != null) {
			Debug.Log(dwarfcontroller.getjobplace());
			dwarfcontroller.startwork();
		}
		
		
	}
	// Update is called once per frame
	void Update () {
	
	}
}
