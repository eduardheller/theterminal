using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Quaternion _targetDirection;
    public static bool StopMove;
    public float rotSpeed = 10f;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StopMove) return;
        Vector3 input = new Vector3(-Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));
        var direction = input;


        if (input != Vector3.zero)
        {
            _targetDirection = Quaternion.LookRotation(-direction);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, _targetDirection, 10f * Time.deltaTime); 
        
        
        transform.position += input * (speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {

 
    }
}
