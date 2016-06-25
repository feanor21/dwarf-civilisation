using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	GameObject dwarfobject;
	ClickerMover dwarfmover;
	//bool select;
	ClickerMover[] currentlyselected;
	GameObject[] foodlist;
	GameObject[] dwarfselectedstock;
	public float speed;
	private int foodstock;
	// Use this for initialization
	void Start () {
		//select = false;
		currentlyselected = new ClickerMover[50];
		update_food_record ();
		Debug.Log ("foodstock :" + foodstock);
	}
	//*****************************************************************************************//
	public void select_dwarf(string name){
		dwarfobject = GameObject.Find (name);
		if (dwarfobject.tag == "dwarf") {
			remove_selected();
			dwarfmover = dwarfobject.GetComponent <ClickerMover>();
			dwarfmover.select();
			addselected(dwarfmover);

		}
	}



	//*****************************************************************************************//

	public void update_food_record(){
		foodstock=GameObject.FindGameObjectsWithTag("food").Length;
		foodlist = new GameObject[foodstock];
		foodlist = GameObject.FindGameObjectsWithTag ("food");
		Debug.Log ("foodstock :" + foodstock);

	}


	//*****************************************************************************************//
	public bool isdwarfselected(){
		if (currentlyselected [0] != null) {
			//Debug.Log ("dawrf is selected");
			return true;
		}

		return false;
	}



	//*****************************************************************************************//

	public int getfoodStock(){
		
		return foodstock;
	}
	//*****************************************************************************************//

	public GameObject[] getfoodList(){
		return foodlist;
	}

	//*****************************************************************************************//

	public void addselected(ClickerMover cm){
		int i = 0;
		while (currentlyselected[i]!=null) {
			i++;
			if(i==50)
				return;
		}
		currentlyselected [i] = cm;
		addselestecdwarf ();
	}


	public void addselestecdwarf(){
		GameObject[] tempdwarfselected;
		int i;
		int count = 0;
		tempdwarfselected = new GameObject[GameObject.FindGameObjectsWithTag("dwarf").Length];


		tempdwarfselected = GameObject.FindGameObjectsWithTag ("dwarf");
		for (i=0; i<tempdwarfselected.Length; i++) {
			if(tempdwarfselected[i].GetComponent<ClickerMover>().isselected()){
				count++;
			}

		}

		dwarfselectedstock = new GameObject[count];
		int k = 0;
		for (i=0; i<tempdwarfselected.Length; i++) {
			if(tempdwarfselected[i].GetComponent<ClickerMover>().isselected()){
				dwarfselectedstock[k]=tempdwarfselected[i];
				k++;
			}

		}
	}

	public GameObject[] getSelectedDwarf(){

		return dwarfselectedstock;
	}


	//*****************************************************************************************//
	void remove_selected(){
		int i = 0;
		for (i=0; i<50; i++) {
			if(currentlyselected[i]!=null){
				currentlyselected[i].unselect();
				currentlyselected[i]=null;
			}
			else
			break;

		}
	}


	//*****************************************************************************************//
	
	// Update is called once per frame
	void Update () {

	

	}
	void FixedUpdate(){

	
	}


}
