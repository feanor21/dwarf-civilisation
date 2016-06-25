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
	private bool[,] isfoodplacefill;
	// Use this for initialization
	void Start () {
		fill = 0.0f; //On start avec de la meeeerde !!!!
		isActive = true; //Actif de base...
		dwarfOnDuty = false;
		dwarf = null;
		currentfoodstock = 0;
	
		isfoodplacefill = new bool[(int)transform.localScale.x*2,(int)transform.localScale.z*2];
		Debug.Log ("place pour la nouriturre en x:" + (int)transform.localScale.x*2);
		Debug.Log ("place pour la nouriturre en z:" + (int)transform.localScale.z*2);
		calc_max_food_stock ();
		Debug.Log ("max food stock: " + maxfoodstock);
	}

	public void initDwarf(GameObject df){
		Debug.Log ("initialising dwarf");
		skills = df.GetComponent<Skills> ();
		dwarf = df;
		if(skills != null)
		Debug.Log ("dwarf initialized");
		Debug.Log ("dwarf job status :" + dwarf.GetComponent<DwarfController> ().getjob ());

		calc_max_food_stock ();
		if(SkillUpRate<1)SkillUpRate=1;
	}

	void calc_max_food_stock() {

		maxfoodstock = ((int)transform.localScale.x*2 * (int)transform.localScale.z*2);

	}

	public int add_food(){
		int i, j;
		for (i = 0; i < (int)transform.localScale.x*2; i++) {
			for (j = 0; j < (int)transform.localScale.z*2; j++) {
				if (isfoodplacefill [i, j] == true) {
					Debug.Log ("place de nourriture déja prise");
				}
				if (isfoodplacefill [i, j] == false){
					Vector3 instantiatefoodplace = new Vector3 ();
					instantiatefoodplace.y = transform.position.y;
					//repére l'emplacement pour faire pop la bouffe
					instantiatefoodplace.x=transform.position.x-transform.localScale.x*2+i*4;
					instantiatefoodplace.z=transform.position.z-transform.localScale.z*2+j*4;

					Instantiate(food,instantiatefoodplace, Quaternion.identity);
					Debug.Log ("spawning food");
					isfoodplacefill [i, j] = true;
					return 0;
				}
			}
		}
		return 1;
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

			//if(skills==null)Debug.Log("skills is nulll!!!");
		
			fill += skills.getSkillLvl(0); //0 is Farming skill, /60 'cause we call 60 times per second Update();
			//Debug.Log("skill level = "+skills.getSkillLvl(0));
			skills.addXp(1.0f/SkillUpRate, 0);

		}
		if (fill >= 100 && currentfoodstock<=maxfoodstock) {
			Debug.Log("current food stock =" +currentfoodstock);
			Debug.Log ("max food on this farm :" + maxfoodstock);
			int tryfood=add_food ();
			if (tryfood == 0)
				Debug.Log ("food correctly spawn");
			if (tryfood == 1)
				Debug.Log ("no place for more food");
			Debug.Log("position pour la bouffe :" + transform.position + " " + Quaternion.identity);
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
				Debug.Log ("max food on this farm :" + maxfoodstock);
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