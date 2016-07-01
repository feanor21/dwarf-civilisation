using UnityEngine;
using System.Collections;

public class litcontroller : MonoBehaviour {

    private bool isAvailable;
    private int nbPlaces;

    //Getters
    public bool getAvailability() { return isAvailable; }
    public int getnbPlaces() { return nbPlaces; }

    //Setters
    public void setNbPlaces(int nbPlacesToSet) { this.nbPlaces = nbPlacesToSet; }

    // Use this for initialization
    void Start () {
        isAvailable = true;
        nbPlaces = 1;
    }

    // Update is called once per frame
    void Update(){
    }

    //Triggers
    void OnTriggerEnter(Collider other){
        if(other.tag == "dwarf"){
            Debug.Log("Dwarf enter bed");
        }
    }

    void OnTriggerExit(Collider other){
        if (other.tag == "dwarf"){
            Debug.Log("Dwarf exit bed");
            isAvailable = true;
        }
    }

    void OnTriggerStay(Collider other){
        if (other.tag == "dwarf"){
            Debug.Log("Dwarf stay in bed");
            isAvailable = false;
        }
    }
}
