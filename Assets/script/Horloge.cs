using UnityEngine;
using System.Collections;

public class Horloge : MonoBehaviour {
	public int min;
	public int sec;
	int fraction;
	float timecount;
	float starttime;
	public GUIText timeCounter;

	void Start ()
	{
		starttime = Time.time;


	}
	
	void Update () {
		timecount = (float)(Time.time - starttime);
		min = (int)(timecount/60f);
		sec = (int)(timecount % 60f);

		fraction = (int)((timecount * 10) %10);
		string.Format("{0:00}:{1:00}:{2:00}",min,sec,fraction);
		timeCounter.text=""+timecount;
	}
	// Use this for initialization
	public int gettime(){

		return (int)timecount;
	}



}
