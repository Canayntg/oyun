using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 6f; // Ýstersen Inspector'dan ayarla
    [SerializeField] private Joystick joystick;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector3 movement = new Vector3(horizontal, 0, vertical);

        if (movement.magnitude > 1) movement.Normalize();

        rb.velocity = movement * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coin"))
        {
            FindAnyObjectByType<Scorecoin>().AddScore(1);
            Destroy(other.gameObject);
        }
    }
}
