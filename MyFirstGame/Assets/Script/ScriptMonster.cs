using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScriptMonster : MonoBehaviour
{

    [SerializeField]
    private bool followPlayer = false;

    public ScriptPerson player;

    public bool immortal = false;

    [SerializeField]
    private int dommage = 20;

    [SerializeField]
    private int MaxLifeMonster = 20;

    private int lifeMonster =20;

    public GameObject prefabToInstanciateAfterDead;
    private bool alreadyInstanciate = false;

    [SerializeField]
    private Transform posEnd;

    [SerializeField]
    private Transform posStart;

    private bool goToEnd = true; // Si true, on va en direction de la fin, si false, on va en direction du début

    private NavMeshAgent nav;

    [SerializeField]
    private HealthBar healthBar;

    void Start()
    {
        nav = this.GetComponent<NavMeshAgent>();
        lifeMonster = MaxLifeMonster;
        if (MaxLifeMonster >= 100 && healthBar != null)
        {
            healthBar.enabled = true;
            healthBar.SetMaxHealth(MaxLifeMonster);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Alive()) 
        {
            if (followPlayer)
            {
                if (player != null)
                {
                    nav.SetDestination(player.transform.position);
                }
            }
            else
            {
                if (nav.remainingDistance <= nav.stoppingDistance)
                {
                    goToEnd = !goToEnd;

                    if (goToEnd)
                    {
                        nav.SetDestination(posEnd.position);
                    }
                    else
                    {
                        nav.SetDestination(posStart.position);
                    }
                }
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) 
        {
            other.GetComponent<ScriptPerson>().LoseLife(GetDommage());
        }

        if (other.CompareTag("Bullet"))
        {
            LoseLife(other.GetComponent<Bullet>().dommageBullet);
            GetComponent<Renderer>().material.color = Color.red;
            StartCoroutine(ChangeColor());
        }
    }

    public void LoseLife(int valueToLose)
    {
        if (!immortal) 
        {
            lifeMonster -= valueToLose;
            if (Alive())
            {
                if (healthBar != null)
                {
                    healthBar.SetHealth(lifeMonster);
                }
            }
        }     
    }

    public bool Alive() 
    {
        if (lifeMonster <= 0)
        {
            if (prefabToInstanciateAfterDead != null && !alreadyInstanciate)
            {
                Instantiate(prefabToInstanciateAfterDead, this.transform.position, Quaternion.Euler(Vector3.zero));
                alreadyInstanciate = true;
            }
            if(this != null)
                Destroy(this.gameObject);
            return false;
        }
        else
            return true;
    }

    public int GetDommage()
    {
        return dommage;
    }

    IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<Renderer>().material.color = Color.white;
    }
}
