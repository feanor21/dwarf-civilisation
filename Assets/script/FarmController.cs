using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FarmController : MonoBehaviour {
	private float fill; //percentage
	private GameObject dwarf;
	public GameController gamecontroller;
	public bool isActive;
	public bool dwarfOnDuty;
	public Skills skills;
	public GameObject food;
	private int currentfoodstock;
	private int officialFoodStock=0;
	private int maxfoodstock;
	public float SkillUpRate;
	private bool[,] isfoodplacefill;
	public float fill_rate=1/2;
	GameObject[] internalFoodStock;
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
		//internalFoodStock = new List<GameObject> ();
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

    public void decrementFoodStock()
    {
		officialFoodStock--;
    }

	public int add_food(){
		int i, j;
		for (i = 0; i < (int)transform.localScale.x*2; i++) {
			for (j = 0; j < (int)transform.localScale.z*2; j++) {
				if (isfoodplacefill [i, j] == true){
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

	private void Update_possible_food_locations(){
		int i, j,k;
		for (i = 0; i < (int)transform.localScale.x*2; i++) {
			for (j = 0; j < (int)transform.localScale.z*2; j++) {
				isfoodplacefill [i, j] = false;
				Vector3 spawnPoint = new Vector3 ();
				spawnPoint.x=transform.position.x-transform.localScale.x*2+i*4;
				spawnPoint.z=transform.position.z-transform.localScale.z*2+j*4;
				spawnPoint.y = transform.position.y;
				//spawnPoint.y=transform.position.y
				float radius=1;
				Debug.Log ("radius de la shere :" + radius);
				var hitColliders = Physics.OverlapSphere(spawnPoint,radius);//1 is purely chosen arbitrarly
				k=0;
				while (k < hitColliders.Length) {
					if (hitColliders [k].tag == "food") {
						Debug.Log ("food à cette position lors du test de food fill farm " + i + ":" + j);
						isfoodplacefill [i, j] = true;
					}
					k++;
				}
			
			}
		}
	}

   	// Update is called once per frame
	void Update () {
		update_currentfoodStock();

		if (currentfoodstock != officialFoodStock) {
			Update_possible_food_locations ();
			officialFoodStock = currentfoodstock;
		}

		if(dwarfOnDuty){
            if (dwarf.GetComponent<DwarfController>().getjob() != "farming")
                dwarfOnDuty =false;
		}
        if (dwarfOnDuty == false && dwarf != null && this.GetComponent<Collider>().bounds.Contains(dwarf.gameObject.transform.position)) {
			if(dwarf.GetComponent<DwarfController>().getjob()=="farming")
				dwarfOnDuty=true;
		}
		if (dwarfOnDuty) {
			if(currentfoodstock<maxfoodstock || fill <50)
			fill += skills.getSkillLvl(0)*fill_rate; //0 is Farming skill, /60 'cause we call 60 times per second Update();
			skills.addXp(1.0f/SkillUpRate, 0);
		}
		if (fill == 90)
			Debug.Log ("current food on farm at 90% fill : " + currentfoodstock);

		if (fill >= 100 && currentfoodstock<maxfoodstock) {
			Debug.Log("current farm food stock =" +currentfoodstock);
			Debug.Log ("max food on this farm :" + maxfoodstock);
			int tryfood=add_food ();
			if (tryfood == 0)
				Debug.Log ("food correctly spawn");
			if (tryfood == 1)
				Debug.Log ("no place for more food");
			Debug.Log("position pour la bouffe :" + transform.position + " " + Quaternion.identity);
			fill = 0;
			officialFoodStock++;
			gamecontroller.update_food_record();
		}
	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("la ferme !!!" + other.tag);
		if (other.tag == "dwarf") {
			if(other.GetComponent<DwarfController>().getjob()=="farming"){
				dwarfOnDuty=true;
				Debug.Log("dwarf is working");
				Debug.Log ("max food on this farm :" + maxfoodstock);
			}
		}
	}

    void OnTriggerStay(Collider other){
        if (other.tag == "dwarf"){
            if (other.GetComponent<DwarfController>().getjob() == "farming" && other.GetComponent<DwarfController>().GetSleepStatut() == "Awake" && other.GetComponent<DwarfController>().getHungerStatus() != "starved" && other.GetComponent<DwarfController>().getHungerStatus() != "starving" && other.GetComponent<DwarfController>().getHungerStatus() != "en digestion"){
                dwarfOnDuty = true;
            }else if (other.GetComponent<DwarfController>().GetSleepStatut() == "Sleeping" || other.GetComponent<DwarfController>().getjob() == "on Break" || other.GetComponent<DwarfController>().getHungerStatus() != "en digestion"){
                dwarfOnDuty = false;
            }
        }

    }

	public bool isIsideFarm(GameObject o){
		float o_x = o.transform.position.x;
		float o_z = o.transform.position.z;
		float xmax = gameObject.transform.position.x + gameObject.transform.localScale.x*8;
		float xmin=gameObject.transform.position.x - gameObject.transform.localScale.x*8;
		float zmax = gameObject.transform.position.x + gameObject.transform.localScale.z*8;
		float zmin=gameObject.transform.position.x - gameObject.transform.localScale.z*8;
		if (o_x > xmin && o_x < xmax && o_z < zmax && o_z > zmin) {
			return true;
		}
		return false;
	}

	private void update_currentfoodStock(){
		internalFoodStock = GameObject.FindGameObjectsWithTag("food");
		int foodInside=0;
		foreach(GameObject f in internalFoodStock){
			if (isIsideFarm (f))
				foodInside++;
		}
		currentfoodstock= foodInside;
	}

    void OnTriggerExit(Collider other){
		Debug.Log ("exiting the farm!!!");
		if (other.tag == "dwarf") {
            Debug.Log("dwarf exit the farm!!!");
            dwarfOnDuty=false;
			Debug.Log("dwarf no longerr working");
		}
		if (other.tag == "food") {
			currentfoodstock--;
		}
	}
}