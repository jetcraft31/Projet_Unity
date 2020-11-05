using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene5 : MonoBehaviour
{

    public ScriptMonster monster;
    public GameObject AllLight;
    private bool load = false;

    // Update is called once per frame
    void Update()
    {
        if (!monster.Alive()) 
        {
            monster.player.EndMode();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<ScriptPerson>().DisplayInfo("Press E");
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!load)
                {
                    load = true;
                    monster.enabled = true;
                    monster.immortal = false;
                    monster.player = other.GetComponent<ScriptPerson>();

                    foreach (Light light in AllLight.transform.GetComponentsInChildren<Light>())
                    {
                        light.enabled = true;
                        light.intensity = 2;
                    }
                }
            }
        }
    }
}
