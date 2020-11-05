using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTimeMax = 5;
    public int dommageBullet = 20;
    private float lifeTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (lifeTimeMax >= lifeTime)
            lifeTime += Time.deltaTime;
        else
            Destroy(this.gameObject);
    }
}
