using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewScene : MonoBehaviour
{

    private bool load = false;
    public bool searchMonster;

    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !load) 
        {
            other.GetComponent<LoadManager>().Remove();
            load = true;

            if (searchMonster) 
                GameObject.FindWithTag("Monster").GetComponent<ScriptMonster>().player = other.GetComponent<ScriptPerson>();

            other.GetComponent<ScriptPerson>().ChangePosSave(this.transform);
        }
    }


}
