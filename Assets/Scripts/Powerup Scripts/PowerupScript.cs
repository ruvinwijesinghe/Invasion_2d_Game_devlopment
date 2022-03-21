using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    public float moveSpeed = 3.0f;

    [SerializeField]
    private int _powerupID; // 0 = triple shot 1 = speed boost 2 = shields

    [SerializeField]
    private AudioClip _powerupClip;

    private void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        if(transform.position.y < -7.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        // Debug.Log("Collided with " + target.name);

        if(target.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_powerupClip, Camera.main.transform.position, 1f);

            // access the player (handle to the player)
            PlayerController playerScript = target.GetComponent<PlayerController>();

            if(playerScript != null)
            {
                if(_powerupID == 0)
                {
                    // enable triple shot bool
                    playerScript.TripleShotPowerOn();
                }
                else if(_powerupID == 1)
                {
                    // enable speed boost
                    playerScript.SpeedBoostPowerupOn();
                }
                else if(_powerupID == 2)
                {
                    // enable shields
                    playerScript.EnableShields();
                }
                
            }

            // destroy ourself
            Destroy(this.gameObject);

        }
    }// OnTriggerEnter2D

}// class
