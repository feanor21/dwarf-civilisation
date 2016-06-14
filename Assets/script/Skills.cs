using UnityEngine;
using System.Collections;

//First skill = farming;
//Second = ce que tu veux;

public class Skills : MonoBehaviour {
	//float FarmingSkill;


	//int FarmingLevel;


	public int skill_length;
	private int[] skillsLevel;
	private float[] skillsExp;

	// Use this for initialization
	void Start () {
		//Init all skills

		initSkillsLevel ();
		skillsExp=initSkillsXp ();

		//FarmingSkill = 0;
		//FarmingLevel = 1;

	}

	//Up a skill
	public void addXp(float k, int idSkill){
		if (skillsExp == null)
			skillsExp=initSkillsXp ();

		//Debug.Log ("adding skill level "+idSkill);

		if (skillsExp == null)
			Debug.Log ("still nulll -_-");

		skillsExp[idSkill] += k;


		if (skillsExp[idSkill] > skillsLevel[idSkill]* 100) {
			skillsExp[idSkill] = skillsExp[idSkill]-(skillsLevel[idSkill]*100);
			skillsLevel[idSkill]++;

		}
		//Debug.Log ("xp :" + skillsExp [idSkill]);
	}

	float[] initSkillsXp(){
		int i;
		float[] skillsExp = new float[skill_length];
		for (i=0; i<skillsExp.Length; i++) {
			skillsExp[i]=0.00f;
			Debug.Log("skills xp de "+i+" = "+skillsExp[i]);
			if(skillsExp==null)Debug.Log("skillsxp null afetr initialising");
		}
		return skillsExp;
	}

	void initSkillsLevel(){
		int i;
		skillsLevel = new int[skill_length];
		for (i=0; i<skillsLevel.Length; i++) {
			skillsLevel[i]=1;
		}
		//Debug.Log ("initSkillsLevel");
	}

	//Skill getter
	public int getSkillLvl(int idSkill){
		//Debug.Log ("asking for skill level "+idSkill);
		if (skillsLevel == null) {
			initSkillsLevel ();
			Debug.Log ("skills level == null");
		}
			//Debug.Log("returning skills level "+skillsLevel [idSkill]);
			return skillsLevel [idSkill];
	}

	/*public void upFarmingskill(float k){
		FarmingSkill += k;
		if (FarmingSkill > FarmingLevel* 100) {
			FarmingLevel++;
			FarmingSkill=0;
		}
	}

	public int getFarminglevel(){
		return FarmingLevel;
	}*/
	// Update is called once per frame

	void Update () {
	
	}
}
