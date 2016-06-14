using UnityEngine;
using System.Collections;

public class FarmController : MonoBehaviour {
	private float fill; //percentage
	private GameObject dwarf;
	public GameController gamecontroller;
	public bool isActive;
	public bool dwarfOnDuty;
	public Skills skills;
	public GameObject food;
	private int currentfoodstock;
	private int maxfoodstock;
	public float SkillUpRate;
	// Use this for initialization
	void Start () {
		fill = 0.0f; //On start avec de la meeeerde !!!!
		isActive = true; //Actif de base...
		dwarfOnDuty = false;
		dwarf = null;
		currentfoodstock = 0;
	}

	public void initDwarf(GameObject df){
		Debug.Log ("initialising dwarf");
		skills = df.GetComponent<Skills> ();
		dwarf = df;
		if(skills != null)
		Debug.Log ("dwarf initialized");

		calc_max_food_stock ();
		if(SkillUpRate<1)SkillUpRate=1;
	}

	void calc_max_food_stock() {

		maxfoodstock = ((int)transform.localScale.x + (int)transform.localScale.y + (int)transform.localScale.z) * 2;

	}


	// Update is called once per frame
	void Update () {
		if(dwarfOnDuty){
			if(dwarf.GetComponent<DwarfController>().getjob()!="farming")
				dwarfOnDuty=false;
		}

		if (dwarfOnDuty == false && dwarf != null) {
			if(dwarf.GetComponent<DwarfController>().getjob()=="farming")
				dwarfOnDuty=true;
		}


		if (dwarfOnDuty) {

			if(skills==null)Debug.Log("skills is nulll!!!");
		
			fill += skills.getSkillLvl(0); //0 is Farming skill, /60 'cause we call 60 times per second Update();
			//Debug.Log("skill level = "+skills.getSkillLvl(0));
			skills.addXp(1.0f/SkillUpRate, 0);

		}
		if (fill >= 100 && currentfoodstock<=maxfoodstock) {
			Debug.Log("current food stock =" +currentfoodstock);
			Instantiate(food, transform.position, Quaternion.identity);
			fill = 0;
			currentfoodstock++;
			gamecontroller.update_food_record();
		}
	}
	void onTriggerEnter(Collider other){
		Debug.Log ("la ferme !!!" + other.tag);
		if (other.tag == "dwarf") {
			if(other.GetComponent<DwarfController>().getjob()=="farming"){
				dwarfOnDuty=true;
				Debug.Log("dwarf is working");
			}

		}
	
	}


	void update_currentfoodStock(){


	}


	void onTriggerExit(Collider other){
		Debug.Log ("dwarf exit the farm!!!");
		if (other.tag == "dwarf") {
			if(other.GetComponent<DwarfController>().getjob()=="farming"){
				dwarfOnDuty=false;
				Debug.Log("dwarf no longerr working");
			}
		}
		if (other.tag == "food") {
			currentfoodstock--;
		}


	}

}