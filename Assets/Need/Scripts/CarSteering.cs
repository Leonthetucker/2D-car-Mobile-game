using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSteering : MonoBehaviour
{
	[Header("Car settings")]
	public float driftFactor = 0.95f;
	public float acceletationFactor = 30.0f;
	public float turnFactor = 3.5f;
	public float maxSpeed = 20;

	//local variables
	public float accelerationInput = 0;
	public float steeringInput = 0;

	float rotationAngle = 0;

	float velocityVsUp = 0;

	//Components
	Rigidbody2D carRidgidbody2D;

	//Awake is called when the script instance is being loaded
    void Awake()
    {
		carRidgidbody2D = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{

		ApplyEngineForce();

		KillOrthogonalVelocity();

		ApplySteering();

	}

	void ApplyEngineForce()
    {
		//Calculate how much "forward" we are going in terms of the direction of our velocity
		velocityVsUp = Vector2.Dot(transform.up, carRidgidbody2D.velocity);

		//Limit so we cannot go faster than the maz speed in the "forward" direction
		if (velocityVsUp > maxSpeed && accelerationInput > 0)
			return;

		//Limit so we cannot go faster than the 50% of max speed in the "forward" direction
		if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
			return;

		//Limit so we cannot go faster in any direction while accelerating
		if (carRidgidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
			return;

		//Apply drag if there is no accelerationInput so the car stops when the player lets go of the accelerator
		if (accelerationInput == 0)
			carRidgidbody2D.drag = Mathf.Lerp(carRidgidbody2D.drag, 1.0f, Time.fixedDeltaTime * 3);
		else carRidgidbody2D.drag = 0;

		//Create a force for the engine
		Vector2 engineForceVector = transform.up * accelerationInput * acceletationFactor;

		//Apply force and pushes the car forward
		carRidgidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

	void ApplySteering()
    {

		//Limit the cars ability to turn when moving slowly
		float minSpeedBeforeAllowTurningFactor = (carRidgidbody2D.velocity.magnitude / 8);
		minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

		//Update the rotation angle based on input
		rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

		//Apply steering by rotating the car object
		carRidgidbody2D.MoveRotation(rotationAngle);
    }

	void KillOrthogonalVelocity()
    {
		Vector2 forwardvelocity = transform.up * Vector2.Dot(carRidgidbody2D.velocity, transform.up);
		Vector2 rightVelocity = transform.right * Vector2.Dot(carRidgidbody2D.velocity, transform.right);

		carRidgidbody2D.velocity = forwardvelocity + rightVelocity * driftFactor;
	}


	public void SetInputVector(Vector2 inputvector)
    {
		steeringInput = inputvector.x;
		accelerationInput = inputvector.y;
	}
}
