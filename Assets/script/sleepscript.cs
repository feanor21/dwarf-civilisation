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
        dwarfcontroller.isSleaping = true;
        GameObject theBed = searchForBed();
        Debug.Log("Is Available ?" + theBed.GetComponent<litcontroller>().getAvailability());
        if(theBed != null && theBed.GetComponent<litcontroller>().getAvailability()){
            movescript.setpos(theBed.transform.position);
            theBed.GetComponent<litcontroller>().setInavailable();
            StartCoroutine(sleep(theBed));
        }else{
            StartCoroutine(sleep(null));
        }
    }

    private IEnumerator sleep(GameObject theBed){
        if (theBed != null){
            yield return new WaitUntil(() => dwarfcontroller.transform.position == theBed.transform.position);
            Debug.Log("On the bed !!" + dwarfcontroller.transform.position + ":::" + theBed.transform.position);
        }
        dwarfcontroller.setSleepStatut("Sleeping");
        dwarfcontroller.setcurrentjob(dwarfcontroller.getjob());
		dwarfcontroller.setjobstatut("on Break");
		dwarfcontroller.updateStatutText();
		Debug.Log (dwarfcontroller.getcurrentjob ());
        if (theBed != null && dwarfcontroller.transform.position != theBed.transform.position){
            yield return new WaitForSeconds(sleepTime);
        }else{
            yield return new WaitForSeconds(sleepTime*2); //Si il est pas sur le lit ou qu'il y a pas de lit, il dors deux fois plus longtemps
        }
		dwarfcontroller.setSleepStatut("Awake");
        dwarfcontroller.isSleaping = false;
		dwarfcontroller.setjobstatut (dwarfcontroller.getcurrentjob ());
		dwarfcontroller.updateStatutText();
        if (dwarfcontroller.getjobplace() != null)
        {
            Debug.Log(dwarfcontroller.getjobplace());
            dwarfcontroller.startwork();
        }
	}
	// Update is called once per frame
	void Update () {
	
	}
}
