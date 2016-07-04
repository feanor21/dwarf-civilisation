using UnityEngine;
using System.Collections;

public class litcontroller : MonoBehaviour {

    private bool isAvailable;
    private int nbPlaces;
    private int remaningPlace;

    //Getters
    public bool getAvailability() { return isAvailable; }
    public int getnbPlaces() { return nbPlaces; }
    public int getRemaningPlaces() { return remaningPlace; }

    //Setters
    public void setNbPlaces(int nbPlacesToSet) { this.nbPlaces = nbPlacesToSet; }
    public void setInavailable() { this.isAvailable = false; }

    //Controler On Places
    public int addDwarfOnBed(int nbDwarf){
        if(remaningPlace >= nbDwarf){
            return remaningPlace -= nbDwarf;
        }
        return -1; //Not enought space
    }
    public int deleteDwarfFromBed(int nbDwarf){
        return remaningPlace += nbDwarf;
    }

    // Use this for initialization
    void Start () {
        isAvailable = true;
        nbPlaces = 1;
        remaningPlace = nbPlaces;
    }

    // Update is called once per frame
    void Update(){
        if(remaningPlace == 0) isAvailable = false;else isAvailable = true;
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
