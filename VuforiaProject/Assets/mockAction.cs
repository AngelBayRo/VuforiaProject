using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class mockAction : MonoBehaviour {

    public GameObject mummy;

    Animator anim;
    int attackHash = Animator.StringToHash("Attack");
    int deathHash = Animator.StringToHash("Death");
    private bool start, dead;
    private int direction = 2;
    private int vidas = 3;
    private float time, lastTime;
    private bool spawned, forced;
    private float force = 1;
    private GameObject myMummy;
    public RawImage vida1, vida2, vida3;
    public Text endText, timeText;
    public Button leftBtn, attackBtn, rightBtn, menuBtn;
    public GameObject parent;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        vida1.gameObject.SetActive(false);
        vida2.gameObject.SetActive(false);
        vida3.gameObject.SetActive(false);
        leftBtn.gameObject.SetActive(false);
        attackBtn.gameObject.SetActive(false);
        rightBtn.gameObject.SetActive(false);
        menuBtn.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);
        endText.gameObject.SetActive(false);
        start = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (start && !dead)
        {
            time = Time.time - lastTime;
            int minutes = (int)(time / 60);
            int seconds = (int)(time % 60);
            if (seconds % 5 == 0 && !spawned)
            {
                float random = Random.value;
                if (random <= 0.25)
                {
                    myMummy = Instantiate(mummy, new Vector3(250, 0, 255), mummy.transform.rotation, parent.transform) as GameObject;
                    myMummy.name = "Down";
                    myMummy.GetComponent<mummyAction>().force = force;
                    myMummy.GetComponent<Rigidbody>().AddForce(0.0f, 0.0f, -20f * force);
                }
                else if (random <= 0.5)
                {
                    myMummy = Instantiate(mummy, new Vector3(255, 0, 250), mummy.transform.rotation, parent.transform) as GameObject;
                    myMummy.name = "Left";
                    myMummy.transform.Rotate(0, 90f, 0);
                    myMummy.GetComponent<mummyAction>().force = force;
                    myMummy.GetComponent<Rigidbody>().AddForce(-20f * force, 0.0f, 0.0f);
                }
                else if (random <= 0.75)
                {
                    myMummy = Instantiate(mummy, new Vector3(250, 0, 245), mummy.transform.rotation, parent.transform) as GameObject;
                    myMummy.name = "Up";
                    myMummy.transform.Rotate(0, 180f, 0);
                    myMummy.GetComponent<mummyAction>().force = force;
                    myMummy.GetComponent<Rigidbody>().AddForce(0.0f, 0.0f, 20f * force);
                }
                else
                {
                    myMummy = Instantiate(mummy, new Vector3(245, 0, 250), mummy.transform.rotation, parent.transform) as GameObject;
                    myMummy.name = "Right";
                    myMummy.transform.Rotate(0, -90f, 0);
                    myMummy.GetComponent<mummyAction>().force = force;
                    myMummy.GetComponent<Rigidbody>().AddForce(20f * force, 0.0f, 0.0f);
                }
                spawned = true;
            }
            else if (seconds % 5 != 0)
                spawned = false;
            if (seconds % 30 == 0 && !forced)
            {
                force += 0.2f;
                forced = true;
            }
            else if (seconds % 30 != 0)
                forced = false;
            timeText.text = minutes + ":" + seconds;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
            anim.SetTrigger(attackHash);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            switch (direction)
            {
                case 1:
                    transform.Rotate(0, -90f, 0);
                    break;
                case 2:
                    transform.Rotate(0, -180f, 0);
                    break;
                case 3:
                    transform.Rotate(0, -270f, 0);
                    break;
            }
            direction = 0;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            switch (direction)
            {
                case 0:
                    transform.Rotate(0, 90f, 0);
                    break;
                case 2:
                    transform.Rotate(0, -90f, 0);
                    break;
                case 3:
                    transform.Rotate(0, -180f, 0);
                    break;
            }
            direction = 1;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            switch (direction)
            {
                case 0:
                    transform.Rotate(0, 180f, 0);
                    break;
                case 1:
                    transform.Rotate(0, 90f, 0);
                    break;
                case 3:
                    transform.Rotate(0, -90f, 0);
                    break;
            }
            direction = 2;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            switch (direction)
            {
                case 0:
                    transform.Rotate(0, 270f, 0);
                    break;
                case 1:
                    transform.Rotate(0, 180f, 0);
                    break;
                case 2:
                    transform.Rotate(0, 90f, 0);
                    break;
            }
            direction = 3;
        }
    }

    public void enableGame()
    {
        vida1.gameObject.SetActive(true);
        vida2.gameObject.SetActive(true);
        vida3.gameObject.SetActive(true);
        leftBtn.gameObject.SetActive(true);
        attackBtn.gameObject.SetActive(true);
        rightBtn.gameObject.SetActive(true);
        timeText.gameObject.SetActive(true);
        start = true;
        lastTime = Time.time;
    }

    public void turnRight()
    {
        transform.Rotate(0, 90f, 0);
        if (direction == 3)
            direction = 0;
        else
            direction = direction + 1;
        changeDirection(direction);
    }

    public void turnLeft()
    {
        transform.Rotate(0, -90f, 0);
        if (direction == 0)
            direction = 3;
        else
            direction = direction - 1;
        Debug.Log(direction);
        changeDirection(direction);
    }

    public void attack()
    {
        anim.SetTrigger(attackHash);
    }

    public void hitted()
    {
        switch(vidas)
        {
            case 1:
                vida1.gameObject.SetActive(false);
                break;
            case 2:
                vida2.gameObject.SetActive(false);
                break;
            case 3:
                vida3.gameObject.SetActive(false);
                break;
        }
        vidas--;
        if (vidas == 0)
        {
            anim.SetTrigger(deathHash);
            dead = true;
            deadAction();
        }
    }

    private void deadAction()
    {
        endText.gameObject.SetActive(true);
        menuBtn.gameObject.SetActive(true);
    }

    public void goToMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void changeDirection(int direction)
    {
        switch(direction)
        {
            case 0:
                name = "Down";
                break;
            case 1:
                name = "Left";
                break;
            case 2:
                name = "Up";
                break;
            case 3:
                name = "Right";
                break;
        }
    }

    public bool isStarted()
    {
        return start;
    }
}
