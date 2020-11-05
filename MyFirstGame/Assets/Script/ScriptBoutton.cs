using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptBoutton : MonoBehaviour
{

    public bool activate = false;
    public ScriptMonster monsterToKill;
    public MonsterIn monsterToBlock;
    public ScriptDoor doorToUse;
    public bool loadNewScene = true;

    void Start()
    {
        if (activate) 
        {
            doorToUse.Use();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && ((monsterToKill == null || !monsterToKill.Alive()) && (monsterToBlock == null || monsterToBlock.GetMonsterIn()) ) )
        {

            other.gameObject.GetComponentInParent<ScriptPerson>().DisplayInfo("Press E");

            if (Input.GetKey(KeyCode.E))
            {
                doorToUse.Use();

                if (loadNewScene) 
                {
                    other.GetComponent<LoadManager>().LoadNextScene();
                    loadNewScene = false;
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponentInParent<ScriptPerson>().DisplayInfo(" ");
        }
    }
}
