using UnityEngine;
using System.Collections;

public class MouseFollower : MonoBehaviour {
	Vector3 pos;
	string name;
	private GameController gameController;
	bool canclick;

	// Use this for initialization
	void Start () {
		Updatemousepos ();
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}

		canclick = true;
	}
	void Updatemousepos(){
		pos = Input.mousePosition;
		pos.z = 50;
		pos = Camera.main.ScreenToWorldPoint(pos);
		transform.position = pos;



	}

	public IEnumerator justclick(){
		canclick = false;
		yield return new WaitForSeconds (0.2f);
		canclick = true;
	}

	void OnTriggerStay(Collider other){



		if (other.tag == "farm" && gameController.isdwarfselected()==true) {
			if(Input.GetMouseButtonDown(1)){
				GameObject[] dwarfs=gameController.getSelectedDwarf();
				Debug.Log("dwarf a la ferme");
				int i;
				for(i=0;i<dwarfs.Length;i++){
					dwarfs[i].GetComponent<DwarfController>().updateJobStatut("farming");
					dwarfs[i].GetComponent<DwarfController>().setjobplace(other.transform.position);
					dwarfs[i].GetComponent<DwarfController>().startwork();
					
				}
			}
			StartCoroutine(justclick());
			return;
		}
			if (other.tag == "Untagged" && canclick) {
				if (Input.GetMouseButtonDown (1)) {
					Debug.Log("clique dehors");
					GameObject[] dwarfs=gameController.getSelectedDwarf();
					if(dwarfs!=null){
						int i;
						for(i=0;i<dwarfs.Length;i++){
							dwarfs[i].GetComponent<DwarfController>().updateJobStatut("idle");
							//dwarfs[i].GetComponent<DwarfController>().setjobplace();
							
							
						}
					}
					
				}
			}
		


		//Debug.Log(""+other.tag);
		if (other.tag == "dwarf") {
			if (Input.GetMouseButtonDown (0)) {
				//Debug.Log("MousePressed");
				name=other.gameObject.name;
				gameController.select_dwarf(name);

			}


		}



	}
	void onTriggerEnter(Collider2D  other){
		Debug.Log(""+other.tag);


	}

	void onTrigger(Collider other){
		Debug.Log ("collide !!! "+other.tag);
	}

	// Update is called once per frame
	void Update () {
		Updatemousepos ();
	}
}
