using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{

    //Component
    CarSteering CarSteering;

    //Awake is called when the script instance is being loaded
    void Awake()
    {
        CarSteering = GetComponent<CarSteering>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        CarSteering.SetInputVector(inputVector);
    }
}
