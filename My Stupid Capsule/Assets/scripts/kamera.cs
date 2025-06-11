using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{

    [SerializeField]

    GameObject kure;
    Vector3 aradakifark;


    // Use this for initialization
    void Start()
    {
        aradakifark = transform.position - kure.transform.position;

        //aradaki farký buluyoruz
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = kure.transform.position + aradakifark;

    }
}