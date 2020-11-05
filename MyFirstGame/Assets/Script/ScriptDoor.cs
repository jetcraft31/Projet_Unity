using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScriptDoor : MonoBehaviour
{
    private Animator _animator;
    private bool open = false;

    // Start is called before the first frame update

    public NavMeshObstacle navPorteGauche;
    public NavMeshObstacle navPorteDroite;

    private float timeAnimation;
    private float timeBeforeActivation = 0;


    void Awake()
    {
        _animator = GetComponent<Animator>();
        timeAnimation = _animator.runtimeAnimatorController.animationClips[0].length;
    }

    void Start()
    {
        if (open) 
        {
            _animator.SetBool("open", true);
        }
    }

    public void Use() 
    {
        if (timeBeforeActivation - Time.time <= 0)
        {
            if (!open)
            {
                _animator.SetBool("open", true);
                open = true;
                if (navPorteGauche != null && navPorteDroite != null)
                {
                    navPorteGauche.enabled = false;
                    navPorteDroite.enabled = false;
                }

                foreach(Renderer i in this.transform.GetComponentsInChildren<Renderer>()) 
                {
                    i.material.color = Color.yellow;
                }
            }
            else
            {
                _animator.SetBool("open", false);
                open = false;
                if (navPorteGauche != null && navPorteDroite != null)
                {
                    navPorteGauche.enabled = true;
                    navPorteDroite.enabled = true;
                }
                foreach (Renderer i in this.transform.GetComponentsInChildren<Renderer>())
                {
                    i.material.color = Color.blue;
                }
            }
            timeBeforeActivation = Time.time + timeAnimation;
        }
    }
}
