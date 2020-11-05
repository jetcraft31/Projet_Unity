using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptImmortalityToken : MonoBehaviour
{
    public float timeImmortality = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<ScriptPerson>().Immortality(timeImmortality);
            Destroy(this.gameObject);
        }
    }
}
