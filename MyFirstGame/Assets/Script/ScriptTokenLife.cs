using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTokenLife : MonoBehaviour
{
    public int lifeAdd = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<ScriptPerson>().AddLife(lifeAdd);
            Destroy(this.gameObject);
        }
    }
}
