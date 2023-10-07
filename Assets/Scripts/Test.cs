using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    void Start()
    {

    }

    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * _speed;
    }
}
