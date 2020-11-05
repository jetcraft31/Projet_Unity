using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementSol : MonoBehaviour
{

    public Transform posStart;
    public Transform posEnd;
    public float speed = 2f;

    private Vector3 nextPosition;

    // Update is called once per frame
    void Update()
    {

        if (transform.position == posEnd.position)
        {
            nextPosition = posStart.position;
         }
        else if (transform.position == posStart.position)
        {
            nextPosition = posEnd.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

    }
}
