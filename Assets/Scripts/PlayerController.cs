using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    
    void Move(float x_input, float z_input)
    {
        if (x_input == 0.0f && z_input == 0.0f) return;
        
        // move
        Vector3 input_vector = new Vector3(x_input, 0, z_input);
        Vector3 current_position = transform.position;
        Vector3 target_vector = current_position + input_vector.normalized;
        float step = CalcStep(input_vector);
        transform.position = Vector3.MoveTowards(current_position, target_vector, step);
        
        // rotate
        Vector3 inputDirection = input_vector;
        Quaternion nextRotation = Quaternion.LookRotation(inputDirection);
        transform.rotation = nextRotation;
    }
    
    float CalcStep(Vector3 input)
    {
        float speed_param = 5.0f;
        float speed = speed_param * input.magnitude;
        return speed * Time.deltaTime;
    }
}
