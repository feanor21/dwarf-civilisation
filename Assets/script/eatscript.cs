using UnityEngine;
using System.Collections;

public class eatscript : MonoBehaviour {
	GameController gameController;
	DwarfController dwarfcontroller; 
	GameObject TargetFood;
	ClickerMover movescript;
	public int eatduration;
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
		movescript = gameObject.GetComponent<ClickerMover> ();
	}
	public void eat(){
		if (gameController.getfoodStock() > 0) {
			dwarfcontroller.iseating=true;
			dwarfcontroller.setPoseBeforeeat(dwarfcontroller.transform.position);
			dwarfcontroller.setcurrentjob(dwarfcontroller.getjob());
			Debug.Log("dwarf job : "+dwarfcontroller.getcurrentjob());
			dwarfcontroller.setjobstatut("on Break");
			TargetFood=getclosestfoodposition();
			TargetFood.tag="food_being_eat";
			gameController.update_food_record();
			Debug.Log ("move to food");
			movescript.setpos(TargetFood.transform.position);
		}
	}
	IEnumerator digere(){
		yield return new WaitForSeconds (eatduration);
		dwarfcontroller.setHungerStatus("satieted");
		Debug.Log("dwarf job : "+dwarfcontroller.getcurrentjob());
		dwarfcontroller.setjobstatut(dwarfcontroller.getcurrentjob());
		dwarfcontroller.updateStatutText();
		if (dwarfcontroller.getjobplace() != null && dwarfcontroller.getjob()!="idle") {
			dwarfcontroller.startwork();
		}
		movescript.setpos (dwarfcontroller.getPosBeforeEat());
		dwarfcontroller.iseating=false;
	}
	


	private GameObject getclosestfoodposition(){
		GameObject[] foodlist = gameController.getfoodList ();
		Vector3 currentposition = transform.position;
		int i;
		int save;
		//Vector3 distancebet=new Vector3 (9999999999,999999999,999999999);
		Vector3 foodsave = foodlist[0].transform.position;
		save = 0;
		Vector3 foodpos;
		Debug.Log ("foodlist lenght : " + foodlist.Length);
		for (i=0; i<foodlist.Length; i++) {
			foodpos=foodlist[i].transform.position;
			if(Vector3.Distance(currentposition,foodpos)<Vector3.Distance(currentposition,foodsave)){
				foodsave=foodpos;
				save=i;
			}
		}
		Debug.Log ("food position ="+foodlist[save].transform.position);
		return foodlist[save];
	}


	void OnTriggerEnter(Collider other){

		if (other.tag == "food_being_eat" && Vector3.Distance(transform.position,TargetFood.transform.position)<2 ){
			Debug.Log ("Trigger de food");
			Destroy(TargetFood);
			dwarfcontroller.setHungerStatus("en digestion");
			dwarfcontroller.updateStatutText();
			StartCoroutine(digere());
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
