using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoScene5 : MonoBehaviour
{
    public Transform posScene5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponent<LoadManager>().LoadNextScene();
            other.gameObject.transform.position = posScene5.position;
        }
    }
}
