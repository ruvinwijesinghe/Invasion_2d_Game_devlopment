using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    [SerializeField]
    private float _speed = 5.0f;

    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _explosionClip;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

    }

    private void Update()
    {
        MovePlayer();
        EnemyBounds();

    }//Update

    private void MovePlayer()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }// MovePlayer

    private void EnemyBounds()
    {
        if (transform.position.y <= -7.0f)
        {
            transform.position = new Vector3(Random.Range(-7f, 7f), 7.0f, 0f);
        }
    }// EnemyBounds

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Laser")
        {
            if (target.transform.parent != null)
            {
                Destroy(target.transform.parent.gameObject);
            }

            Destroy(target.gameObject);

            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);

            _uiManager.UpdateScore();

            AudioSource.PlayClipAtPoint(_explosionClip, Camera.main.transform.position, 1f);

            Destroy(this.gameObject);
            
        }
        else if(target.tag == "Player")
        {
            
            PlayerController playerScript = target.GetComponent<PlayerController>();

            if(playerScript != null)
            {
                // deal damage to player
                playerScript.Damage();

                Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);

                AudioSource.PlayClipAtPoint(_explosionClip, Camera.main.transform.position, 1f);

                Destroy(this.gameObject);
            }
        }
    }// OnTriggerEnter2D

}//class
