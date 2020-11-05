using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptPerson : MonoBehaviour
{
    private int maxLife = 200;

    [SerializeField]
    private int life = 100;

    private bool immortality = false;
    private float timeMaxImmortality;
    private float timeImmortality = 0f;

    public Arme weapon;
    public Transform positionWeapon;

    // Créer un autre script pour cela et faire que cela fonctionne quand on clique sur les boutons
    [SerializeField]
    private Canvas canvasInGame;
    [SerializeField]
    private Canvas canvasExit;
    [SerializeField]
    private Canvas canvasDead;
    [SerializeField]
    private Canvas canvasEnd;

    [SerializeField]
    private HealthBar healthBar;
    private bool exitMode = false;

    private Transform lastPosSave;

    [SerializeField]
    private Transform posDepart;
    private int lifeStart;


    void Start()
    {
        canvasInGame.gameObject.SetActive(true);
        canvasExit.gameObject.SetActive(false);
        canvasDead.gameObject.SetActive(false);
        canvasEnd.gameObject.SetActive(false);

        canvasExit.transform.Find("Panel").Find("Continue").GetComponent<Button>().onClick.AddListener(() => ContinueMode());
        canvasExit.transform.Find("Panel").Find("Exit").GetComponent<Button>().onClick.AddListener(() => Exit());

        canvasDead.transform.Find("Panel").Find("No").GetComponent<Button>().onClick.AddListener(() => Exit());
        canvasDead.transform.Find("Panel").Find("Yes").GetComponent<Button>().onClick.AddListener(() => Respawn());

        canvasEnd.transform.Find("Panel").Find("No").GetComponent<Button>().onClick.AddListener(() => Exit());
        canvasEnd.transform.Find("Panel").Find("Yes").GetComponent<Button>().onClick.AddListener(() => StartAgain());


        healthBar.SetMaxHealth(maxLife);
        Cursor.lockState = CursorLockMode.Locked;
        lifeStart = life;

    }

    // Update is called once per frame
    void Update()
    {
        if (life >= 0) 
        {
            healthBar.SetHealth(life);
        }
        else 
        {
            DeadMode();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (exitMode)
            {
                ExitMode(); ;
            }
            else 
            {
                ContinueMode();
            }
        }

        if (immortality) 
        {
            timeImmortality += Time.deltaTime;

            if (timeImmortality >= timeMaxImmortality)
                immortality = false;
        }
    }

    void ContinueMode()
    {
        canvasInGame.gameObject.SetActive(true);
        canvasExit.gameObject.SetActive(false);
        canvasDead.gameObject.SetActive(false);
        canvasEnd.gameObject.SetActive(false);

        exitMode = true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    void ExitMode() 
    {
        canvasInGame.gameObject.SetActive(false);
        canvasExit.gameObject.SetActive(true);
        canvasDead.gameObject.SetActive(false);
        canvasEnd.gameObject.SetActive(false);


        exitMode = false;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
    }

    void DeadMode() 
    {
        canvasInGame.gameObject.SetActive(false);
        canvasExit.gameObject.SetActive(false);
        canvasDead.gameObject.SetActive(true);
        canvasEnd.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
    }

    public void EndMode() 
    {
        canvasInGame.gameObject.SetActive(false);
        canvasExit.gameObject.SetActive(false);
        canvasDead.gameObject.SetActive(false);
        canvasEnd.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
    }

    void Exit() 
    {
        Debug.Log("Le jeu est fini");
        Application.Quit();
    }

    void Respawn()
    {
        transform.position = lastPosSave.position;
        life = lifeStart;
        ContinueMode();
    }

    void StartAgain() 
    {
        transform.position = posDepart.position;
        life = lifeStart;
        ContinueMode();
        GetComponent<LoadManager>().FirstLoad();
    }

    public void DisplayInfo(String message) 
    {
        canvasInGame.transform.Find("Text").GetComponent<Text>().text = message;
    }

    public void DisplayInfo(String message, float time)
    {
        StartCoroutine(displayInfo(message,time));
    }

    public void AddLife(int valueToAdd) 
    {
        Debug.Log("Vie avant ajout :" + life);
        life += valueToAdd;
        if (life > maxLife)
            life = maxLife;
        Debug.Log("Vie après ajout :" + life);
    }

    public void LoseLife(int valueToLose = 20)
    {
        if (!immortality) 
        {
            life -= valueToLose;
            Immortality(0.5f);
        }
    }

    public void Immortality(float time) 
    {
        timeMaxImmortality = time;
        immortality = true;
        timeImmortality = 0f;
        healthBar.Immortality(timeMaxImmortality);

    }

    IEnumerator displayInfo(string message,float time) 
    {
        canvasInGame.transform.Find("Text").GetComponent<Text>().text = message;
        yield return new WaitForSeconds(time);
        canvasInGame.transform.Find("Text").GetComponent<Text>().text = "";
    }
    
    public void ChangePosSave(Transform pos) 
    {
        lastPosSave = pos;
    }

    public void GoToLastPose() 
    {
        transform.position = lastPosSave.position;
    }

}
