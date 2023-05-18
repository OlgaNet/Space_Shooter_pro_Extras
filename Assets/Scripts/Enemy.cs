using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;
    private Animator _anim; //handle to animator component
    private AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        //null check player
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }
        _anim = GetComponent<Animator>(); //assign the component to Anim

        if (_anim == null)
        {
            Debug.LogError("The Animator is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //movedown at 4 meters per second
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //if botton of screen
        //respawn at top with a new random x position
        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath"); //triger anim
            _speed = 0;
            _audioSource.Play();

            Destroy(this.gameObject, 2.8f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(10);
                //getcomponent
            }

            _anim.SetTrigger("OnEnemyDeath"); //triger anim
            _speed = 0;
            _audioSource.Play();

            Destroy(this.gameObject, 2.8f);    
        }

    }
}
