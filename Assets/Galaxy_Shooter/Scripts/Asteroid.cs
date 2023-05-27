using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 19.0f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManadger _spawnManager;

    private void Start()
    {
        _spawnManager = FindObjectOfType<SpawnManadger>();
    }


    // Update is called once per frame
    void Update()
    {
        //rotated object on the zed axis!
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    //check for LASER collission (Trigger)
    //instantiate explosion at the position of the astroid (us)
    // destroy the explosion after 3 sec
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.25f);
        }
    }
}
