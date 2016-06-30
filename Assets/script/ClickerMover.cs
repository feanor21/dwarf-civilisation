using UnityEngine;
using System.Collections;

public class ClickerMover : MonoBehaviour {

	private Vector3 mouse_position;
	private float mouse_positionX;
	private float mouse_positionZ;
	public float speed;
	public bool selected;
	private Ray ray;
	Vector3 pos;
	private bool moveset=false;
	DwarfController dwarfcontroller;

	// Use this for initialization
	void Start () {
        dwarfcontroller = gameObject.GetComponent <DwarfController>();
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
        if (selected ) {
			if( dwarfcontroller.isavailable()){
				if (Input.GetMouseButtonDown (1)) {
					moveset = true;
					mouse_position = Input.mousePosition;
					mouse_positionX = mouse_position.x - Screen.width / 2;
					mouse_positionZ = mouse_position.y - Screen.height / 2;
                    RaycastHit[] targetSelectedPlace = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
                    for (int i = 0; i < targetSelectedPlace.Length; i++)
                    {
                        RaycastHit hit = targetSelectedPlace[i];
                        if (dwarfcontroller.isawake() && hit.transform.tag == "ground")
                        {
                            pos = hit.point;
                        }
                    }
                }
			}
		}
        if (moveset && dwarfcontroller.isawake())
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, step);
            if(transform.position == pos)
            {
                moveset = false;
            }
        }
    }

	public void setpos(Vector3 dir){    
		pos = dir;
		moveset = true;
		//Debug.Log ("move to " + pos);
	}

	public void select(){
		selected = true;
    }

    public void unselect(){
		selected = false;
	}

	public bool isselected(){
		return selected;
	}
}
