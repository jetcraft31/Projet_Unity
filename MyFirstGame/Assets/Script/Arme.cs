using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arme : MonoBehaviour
{

    public int nbMaxBullet = -1;
    public int bulletUsed = 0;
    public float force = 500;

    public float TimeBeforeShootAgain = 1f;
    private float GlobalTime = 0;

    public Rigidbody bullet;
    public Transform barrelEnd;
    public Rigidbody body;

    private bool onSomeone = false;
    private bool explanationForShoot = false;

    private ScriptPerson person;
    void Update()
    {
        if (person != null) 
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Use();
            }

            if (Input.GetKeyDown(KeyCode.T) && !onSomeone)
            {
                Take(person);
            }else if (Input.GetKeyDown(KeyCode.T) && onSomeone)
            {
                Remove(person);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !onSomeone) 
        {
            person = other.gameObject.GetComponentInParent<ScriptPerson>();

            person.DisplayInfo("Press T");
        }
    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !onSomeone)
        {
            other.gameObject.GetComponentInParent<ScriptPerson>().DisplayInfo(" ");
            person = null;
        }
    }

    public void Take(ScriptPerson player)
    {
        onSomeone = true;
        player.weapon = this;

        transform.SetParent(player.positionWeapon.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        body.isKinematic = true;

        if (!explanationForShoot)
        {
            player.DisplayInfo("Left click to shoot", 3);
            explanationForShoot = true;
        }
    }

    public void Remove(ScriptPerson player)
    {
        onSomeone = false;
        player.weapon = null;


        transform.SetParent(null);
        body.isKinematic = false;

    }

    public void Use() 
    {
        if ( onSomeone && ((nbMaxBullet ==-1)|| (nbMaxBullet>bulletUsed) ) && (GlobalTime - Time.time <= 0))
        {
            Rigidbody rocketInstance;
            rocketInstance = Instantiate(bullet, barrelEnd.position, barrelEnd.rotation) as Rigidbody;
            rocketInstance.AddForce(barrelEnd.right * force);
            bulletUsed++;
            GlobalTime = Time.time +TimeBeforeShootAgain;
        }
    }
}
