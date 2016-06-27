using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Inventory{
	private List<GameObject> inventaire;
	private GameObject owner;
	/// <summary>
	/// la classe inventaire. permet de stocker des objets avant de les recréer.
	/// </summary>


	//this method must be call every time before begin to use inventory
	public void setInventorySize(int size){
		inventaire=new List<GameObject>(size);
	}
/*************************************************/
	public void setOwner(GameObject owner){
		owner = this.owner;
	}


	public List<GameObject> getInventory(){
		return inventaire;
	}
	public void addToInventory(GameObject c){
		Debug.Log ("dans le sac?");
		inventaire.Add(c.gameObject);
		c.SetActive(false);
		Debug.Log ("et hop, dans l'inventaire!!!");
	}
	
	public void getItemBack(GameObject c){
		Debug.Log ("je remet l'item");
		inventaire.Remove (c);
		c.SetActive (true);
	}
	public void getItemBack(GameObject c,Vector3 position){
		Debug.Log ("je remet l'item");
		inventaire.Remove (c);
		c.transform.position = position;
		c.SetActive (true);
	}
}
