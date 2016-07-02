using UnityEngine;
using System.Collections;

public class sleepscript : MonoBehaviour {
	GameController gameController;
	DwarfController dwarfcontroller; 
	public int sleepTime;
    ClickerMover movescript;
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
        movescript = gameObject.GetComponent<ClickerMover>();
    }

    private GameObject searchForBed(){
        GameObject[] bedList = gameController.getBedList();
        if(bedList.Length != 0){
            Debug.Log("Bed list :" + bedList);
            return bedList[0];
        }
        return null;
    }

	public void startsleep(){
        GameObject theBed = searchForBed();
        if(theBed != null){
            movescript.setpos(theBed.transform.position);
        }else{
            StartCoroutine(sleep());
        }
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
