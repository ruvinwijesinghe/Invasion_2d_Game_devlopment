using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float movespeed = 7f;

    private float _yLimit = 7f; 

    private void Update()
    {
        MoveLaser();

        DestroyOurSelf();

    }//Update

    private void MoveLaser()
    {
        transform.Translate(Vector3.up * movespeed * Time.deltaTime);
    }//MoveLaser

    private void DestroyOurSelf()
    {
        if (transform.position.y >= _yLimit)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }//DestroyOurSelf

}//class
