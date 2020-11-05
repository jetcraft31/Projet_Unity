using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIn : MonoBehaviour
{

    private bool monsterIn = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Monster"))
        {
            monsterIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Monster"))
        {
            monsterIn = false;
        }
    }

    public bool GetMonsterIn() 
    {
        return monsterIn;
    }

}
