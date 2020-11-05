using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene4 : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponent<ScriptPerson>().GoToLastPose();
            other.GetComponent<ScriptPerson>().LoseLife();
        }
    }
}
