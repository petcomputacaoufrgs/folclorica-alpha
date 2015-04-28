using UnityEngine;
using System.Collections;

public class Hovering : MonoBehaviour {

	public float floatStrenght = 2.0f;

	Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		rigidbody = transform.GetComponent<Rigidbody2D>();

		rigidbody.AddRelativeForce(Vector3.up * floatStrenght);// * (rigidbody.mass * Mathf.Abs(Physics.gravity.y)));
		//rigidbody.AddForce(Vector3.up *floatStrenght);
		//AddRelativeForce
			//rigidbody2D.AddForce(Vector3.up *FloatStrenght);
		//transform.Rotate(RandomRotationStrenght,RandomRotationStrenght,RandomRotationStrenght);

	}
}
