using UnityEngine;
using System.Collections;

public class sleepscript : MonoBehaviour {
	GameController gameController;
	DwarfController dwarfcontroller; 
	public int sleepTime;
    ClickerMover movescript;
    litcontroller bedController;
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
        dwarfcontroller = gameObject.GetComponent<DwarfController>();
        movescript = gameObject.GetComponent<ClickerMover>();
        bedController = gameObject.GetComponent<litcontroller>();
    }

    private GameObject searchForBed(){
        GameObject[] bedList = gameController.getBedList();
        if(bedList.Length != 0){
            Debug.Log("Bed list :" + bedList);
            return bedList[0];
        }
        return null;
    }

    private GameObject getclosestbedposition(){
        GameObject[] bedlist = gameController.getBedList();
        Vector3 currentposition = transform.position;
        int i, save = 0;
        Vector3 bedsave = bedlist[0].transform.position, bedpos;
        for (i = 0; i < bedlist.Length; i++){
            bedpos = bedlist[i].transform.position;
            if (Vector3.Distance(currentposition, bedpos) < Vector3.Distance(currentposition, bedsave)){
                bedsave = bedpos;
                save = i;
            }
        }
        return bedlist[save];
    }

    public void startsleep(){
        dwarfcontroller.isSleaping = true;
        GameObject theBed = getclosestbedposition();
        StartCoroutine(sleep(theBed));
    }

    private IEnumerator sleep(GameObject theBed){
        if (theBed != null){
            movescript.setpos(theBed.transform.position);
            yield return new WaitUntil(() => theBed.GetComponent<Collider>().bounds.Contains(dwarfcontroller.transform.position));
        }
        dwarfcontroller.setSleepStatut("Sleeping");
        dwarfcontroller.setcurrentjob(dwarfcontroller.getjob());
		dwarfcontroller.setjobstatut("on Break");
		dwarfcontroller.updateStatutText();
		Debug.Log (dwarfcontroller.getcurrentjob ());
        if (theBed != null && dwarfcontroller.transform.position == theBed.transform.position){
            yield return new WaitForSeconds(sleepTime);
        }else{ //Si il est pas sur le lit ou qu'il y a pas de lit, il dors deux fois plus longtemps
            //Et si t'es pas content c'est la même, t'avais qu'à faire un lit
            yield return new WaitForSeconds(sleepTime*2);
        }
		dwarfcontroller.setSleepStatut("Awake");
        dwarfcontroller.isSleaping = false;
		dwarfcontroller.setjobstatut (dwarfcontroller.getcurrentjob ());
		dwarfcontroller.updateStatutText();
        if (dwarfcontroller.getjobplace() != null){
            Debug.Log(dwarfcontroller.getjobplace());
            dwarfcontroller.startwork();
        }
	}
	// Update is called once per frame
	void Update () {
	
	}
}
