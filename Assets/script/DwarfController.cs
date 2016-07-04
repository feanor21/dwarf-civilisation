using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DwarfController : MonoBehaviour
{
    int HealthPoint;
    string HungerStatus;
    string sleepStatut;
    int time;
    public int inventory_max_size;
    //public int sleepTime;
    private Inventory inventory;
    public GameController gameController;
    public GUIText statuts;
    bool changeHungerstatut = true;
    bool changeSleepingStatus = true;
    Horloge horloge;
    public int timebeforesleep;
    //public int eatduration;
    public int timebeforeeat;
    ClickerMover movescript;
    public bool iseating;
    public bool isSleaping;
    GameObject TargetFood;
    private string jobstatut;
    private Vector3 jobplace;
    string currentjob;
    private Vector3 PosbeforeEat;
    eatscript eatscript;
    sleepscript sleepscript;
    // Use this for initialization

    //*****************************************************************************************//
    void Start()
    {
        inventory = new Inventory();
        inventory.setInventorySize(inventory_max_size);
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
        horloge = gameControllerObject.GetComponent<Horloge>();
        movescript = this.GetComponent<ClickerMover>();
        HungerStatus = "satieted";
        sleepStatut = "Awake";
        jobstatut = "idle";
        updateStatutText();
        eatscript = gameObject.GetComponent<eatscript>();
        sleepscript = gameObject.GetComponent<sleepscript>();
        iseating = false;
        isSleaping = false;
    }
    public void setcurrentjob(string s)
    {
        currentjob = s;
    }

    public void setPoseBeforeeat(Vector3 pos)
    {
        PosbeforeEat = pos;
    }
    public void setjobstatut(string s)
    {
        jobstatut = s;
    }
    public void setHungerStatus(string s)
    {
        HungerStatus = s;
    }
    public string getHungerStatus()
    {
        return HungerStatus;
    }
    public Vector3 getjobplace()
    {
        return jobplace;
    }
    public Vector3 getPosBeforeEat()
    {
        return PosbeforeEat;
    }
    public string GetSleepStatut()
    {
        return sleepStatut;
    }
    public void setSleepStatut(string s)
    {
        sleepStatut = s;
    }
    public string getcurrentjob()
    {
        return currentjob;
    }
    //*****************************************************************************************//
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log ("Trigger de nain et de " + other.tag);
        if (other.tag == "farm")
        {
            other.GetComponent<FarmController>().initDwarf(gameObject);
            other.GetComponent<FarmController>().dwarfOnDuty = true;
        }
        if (other.tag == "item")
        {
            Debug.Log("j'essaye de mettre la nourriture dans mon sac");
            inventory.addToInventory(other.gameObject);
            Debug.Log("j'ai mis la nourriture dans mon sac!!!");
        }
        if (other.tag == "entrepot")
        {
            Debug.Log("trouvé ou poser");
            if (inventory.getInventory() != null)
            {
                Debug.Log("je vais poser l'item");
                inventory.getItemBack(inventory.getInventory()[0], other.transform.position);
            }

        }

    }


    public bool isavailable()
    {

        if (iseating || isSleaping)
            return false;



        return true;
    }

    //*****************************************************************************************//
    public void updateJobStatut(string statut)
    {
        jobstatut = statut;
        updateStatutText();
    }
    //*****************************************************************************************//
    public void setjobplace(Vector3 place)
    {
        jobplace = place;
        Debug.Log("jobplace position =" + jobplace);
    }
    public void startwork()
    {
        if (jobstatut == "on Break")
            jobstatut = currentjob;
        StartCoroutine(work());
    }

    public void die()
    {
        Destroy(gameObject);
        Debug.Log("dwarf is dead");
    }


    //*****************************************************************************************//
    IEnumerator work()
    {
        movescript.setpos(jobplace);
        while (readyToWork() && jobstatut != "idle")
        {
            Debug.Log(jobplace);
            Debug.Log("jobstatut = " + jobstatut);
            Debug.Log("Siffler en travaillant!! tululululululut");

            yield return new WaitForSeconds(5);
        }
    }
    //*****************************************************************************************//
    public bool readyToWork()
    {
        if (sleepStatut == "Awake" && (HungerStatus == "satieted" || HungerStatus == "fine"))
            return true;

        return false;
    }


    public string getjob()
    {
        return jobstatut;
    }

    //*****************************************************************************************//
    /*
	IEnumerator sleep(){
		sleepStatut = "Sleeping";
		currentjob = jobstatut;
		jobstatut="on Break";
		updateStatutText();
	yield return new WaitForSeconds(sleepTime);
		sleepStatut = "Awake";
		updateStatutText();
		if (jobplace != null) {
			startwork();
		}
	

	}*/
    //*****************************************************************************************//

    //*****************************************************************************************//
    public bool isawake()
    {

        if (sleepStatut == "Awake" || sleepStatut == "Sleepy")
            return true;
        return false;
    }
    //*****************************************************************************************//
    void changeHunger_statut()
    {
        if (HungerStatus == "Hungry")
        {
            HungerStatus = "starved";
            updateStatutText();
            return;
        }
        if (HungerStatus == "satieted")
        {
            HungerStatus = "fine";
            updateStatutText();
            return;
        }
        if (HungerStatus == "fine")
        {
            HungerStatus = "Hungry";
            updateStatutText();
            return;
        }
        if (HungerStatus == "starved")
        {
            HungerStatus = "starving";
            updateStatutText();
            return;
        }
        if (HungerStatus == "starving") die();
    }

    private void changeSleepingStatus_()
    {
        if (sleepStatut == "Awake")
        {
            sleepStatut = "Sleepy";
            updateStatutText();
            return;
        }
        else if (sleepStatut == "Sleepy")
        {
            sleepStatut = "Wanna Sleeeep !!";
            updateStatutText();
            return;
        }
        else if (sleepStatut == "Wanna Sleeeep !!") die();
    }
    //*****************************************************************//




    //*****************************************************************//

    public void updateStatutText()
    {
        statuts.text = "" + HungerStatus + "\n" + sleepStatut + "\n" + jobstatut;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log ("" + horloge.gettime () % 10);
        if ((int)horloge.gettime() == 0)
            return;

        if ((horloge.gettime() % timebeforeeat) == 0)
        {
            //	Debug.Log("enter into hunger loop! at "+horloge.gettime ()+ "and bool = "+changeHungerstatut);
            if (changeHungerstatut && iseating == false)
            {
                changeHunger_statut();
                changeHungerstatut = false;
            }

            if (HungerStatus == "Hungry" && iseating == false)
            {
                Debug.Log("go to eat!!!");
                eatscript.eat();
            }
        }
        if (horloge.gettime() % timebeforesleep == 0)
        {
            if (changeSleepingStatus && isSleaping == false)
            {
                changeSleepingStatus_();
                changeSleepingStatus = false;
            }
            if (sleepStatut == "Sleepy" && isSleaping == false)
            {
                Debug.Log("Going to sleep !");
                sleepscript.startsleep();
            }
        }
        if (horloge.gettime() % 10 == 1 && changeHungerstatut == false) changeHungerstatut = true;
        if (horloge.gettime() % 10 == 1 && changeSleepingStatus == false) changeSleepingStatus = true;
    }

    //*****************************************************************//

}