using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            const float kForce = 100.0f;
            if (touch.phase == TouchPhase.Began)
            {
                _rb.AddForce(kForce * Vector3.left);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                _rb.AddForce(kForce * Vector3.forward);
            }
        }
    }
    
    void Move(float xInput, float zInput)
    {
        if (xInput == 0.0f && zInput == 0.0f) return;
        
        // move
        Vector3 inputVector = new Vector3(xInput, 0, zInput);
        Vector3 currentPosition = transform.position;
        Vector3 targetVector = currentPosition + inputVector.normalized;
        float step = CalcStep(inputVector);
        transform.position = Vector3.MoveTowards(currentPosition, targetVector, step);
        
        // rotate
        Vector3 inputDirection = inputVector;
        Quaternion nextRotation = Quaternion.LookRotation(inputDirection);
        transform.rotation = nextRotation;
    }
    
    float CalcStep(Vector3 input)
    {
        const float kSpeedParam = 5.0f;
        float speed = kSpeedParam * input.magnitude;
        return speed * Time.deltaTime;
    }
}
