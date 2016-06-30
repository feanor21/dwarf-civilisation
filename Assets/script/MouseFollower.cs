using UnityEngine;
using System.Collections;

public class MouseFollower : MonoBehaviour {
	Vector3 pos;
	string name;
	private GameController gameController;
	bool canclick;

	// Use this for initialization
	void Start () {
		//Updatemousepos ();
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

	public IEnumerator justclick(){
		canclick = false;
		yield return new WaitForSeconds (0.2f);
		canclick = true;
	}

	void OnTriggerStay(Collider other){
		if (other.tag == "farm") {
			if (Input.GetMouseButtonDown (1)) {
				Debug.Log ("right click on farm");
				Debug.Log ("is dward selected? " + gameController.isdwarfselected ());
			}
		}
		if (other.tag == "farm" && gameController.isdwarfselected()==true) {
			if(Input.GetMouseButtonDown(1)){
				GameObject[] dwarfs=gameController.getSelectedDwarf();
				int i;
				for(i=0;i<dwarfs.Length;i++){
					Debug.Log("updtaing dwrf statut");
					dwarfs[i].GetComponent<DwarfController>().updateJobStatut("farming");
					dwarfs[i].GetComponent<DwarfController>().setjobplace(other.transform.position);
					dwarfs[i].GetComponent<DwarfController>().startwork();
				}
			}
		}

		//Debug.Log(""+other.tag);
		if (other.tag == "dwarf") {
			if (Input.GetMouseButtonDown (0)) {
				//Debug.Log("MousePressed");
				name=other.gameObject.name;
				gameController.select_dwarf(name);
				if (gameController.isdwarfselected () == true)
				Debug.Log ("dwarf is selected");
			}
		}

		if (other.tag == "ground") {
			if (Input.GetMouseButtonDown (1)) {
				Debug.Log("clique dehors");
				GameObject[] dwarfs=gameController.getSelectedDwarf();
				if(dwarfs!=null){
					int i;
					for(i=0;i<dwarfs.Length;i++){
						dwarfs[i].GetComponent<DwarfController>().updateJobStatut("idle");
					}
					justclick();
				}
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
    void Update()
    {
        RaycastHit[] hits;
        if (Input.GetMouseButtonDown(0))
        {
            hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                OnTriggerStay(hit.collider);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                OnTriggerStay(hit.collider);
            }
        }
    }
}
