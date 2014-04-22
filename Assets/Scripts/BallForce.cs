using UnityEngine;
using System.Collections;

public class BallForce : MonoBehaviour {
    public bool useForce = false;
    public float forceAmount = 2;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (useForce) {
            rigidbody.AddForce(new Vector3(0, 0, -forceAmount));
            useForce = false;
        }
	}
}
