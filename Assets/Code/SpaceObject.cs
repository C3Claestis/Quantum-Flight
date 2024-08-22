using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    [SerializeField] bool meteorit;
    [SerializeField] bool fuel;
    [SerializeField] bool HP;
    [SerializeField] int Damage;
    [SerializeField] GameObject explode;
    public float rotationSpeed = 100f;  // Kecepatan rotasi dalam derajat per detik
    public float Speed = 5;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);  // Rotasi pada sumbu z
        rb.velocity = new Vector2(-Speed, 0);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && meteorit)
        {
            Debug.Log("TERKENA METEOR");
            PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();
            playerStatus.SetHP(-Damage);
            Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (other.CompareTag("Player") && fuel)
        {
            Debug.Log("TERKENA FUEL");
            PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();
            playerStatus.SetFuel(100);
            Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (other.CompareTag("Player") && HP)
        {
            Debug.Log("TERKENA FUEL");
            PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();
            playerStatus.SetHP(10);
            Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (other.CompareTag("Barier") && meteorit)
        {
            Debug.Log("TERKENA BARIER");
            Destroy(gameObject);
        }
    }
}
