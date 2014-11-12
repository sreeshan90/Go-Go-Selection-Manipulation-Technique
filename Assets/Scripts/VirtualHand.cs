using UnityEngine;
using System.Collections;

// This class provides a simple virtual hand technique
// Directions:
// 		1. Attach to the GameObject that will be used for collision detection
//		2. Ensure that GameObject is encapsulated in an empty GameObject with a clean transform
//		3. Change WiimoteName to the name of the Wiimote that will be used for input
public class VirtualHand : MonoBehaviour {

	public string WiimoteName = "RightWiimote";

	private GameObject collidedObject = null;
	private GameObject grabbedObject = null;
	private Transform rootObject = null;
	private Vector3 grabbedPosition;
	private Quaternion grabbedRotation;
	private float threshold = 0.8f;
	// Use this for initialization



	void Start () {

		// We assume the attached GameObject has a collider so ensure the collider is trigger-based
		collider.isTrigger = true;
		// Turn off the object's physics by making it kinematic
		rigidbody.isKinematic = true;

	}
	
	// Update is called once per frame
	void Update () {


		// Check if no object is being collided and if no object is grabbed
		if(collidedObject == null && grabbedObject == null) {
			// If A and B are pressed, treat the virtual hand as a fist by turning off the collider's trigger
			if(InputBroker.GetKeyDown(WiimoteName + ":A") && InputBroker.GetKeyDown(WiimoteName + ":B")) {
				collider.isTrigger = false;
			}
			// Otherwise, ensure the collider is treated as a trigger
			else {
				collider.isTrigger = true;
			}
		}

		// Check if an object is being collided (but no object is grabbed yet)
		else if(collidedObject != null && grabbedObject == null) {
			// If A and B are pressed, grab the object, turn off its physics, and add it to the virtual hand's empty parent
			if(InputBroker.GetKeyDown(WiimoteName + ":A") && InputBroker.GetKeyDown(WiimoteName + ":B")) {

				// Turn off physics by turning on kinematics
				grabbedObject = collidedObject;
				grabbedObject.rigidbody.isKinematic = true;

				// Find the root of the grabbed object (for hierarchical objects)
				rootObject = grabbedObject.transform;
				while(rootObject.transform.parent != null) {
					rootObject = rootObject.transform.parent;
				}

				// Move the root of the grabbed object under the virtual hand's parent
				rootObject.parent = transform.parent;

				// Determine the root's initial position and rotation relative to the virtual hand's parent				
				grabbedPosition = rootObject.localPosition;
				grabbedRotation = rootObject.localRotation;
			}
		}

		// Check if an object is grabbed
		else if(grabbedObject != null) {
			// Update the root's position and rotation relative to the virtual hand's parent
			rootObject.localPosition = grabbedPosition;
			rootObject.localRotation = grabbedRotation;

			// If A and B are NOT pressed, turn the object's physics back on and release it
			if(!InputBroker.GetKeyDown(WiimoteName + ":A") || !InputBroker.GetKeyDown(WiimoteName + ":B")) {
				grabbedObject.rigidbody.isKinematic = false;
				rootObject.parent = null;
				grabbedObject = null;
			}
		}

		//detect if we have passed the go-go threshold
		GameObject hmdObject = GameObject.Find("HMD");
		GameObject handObject = GameObject.Find("RightWiimote"); //phyical hand
		
		Vector3 hmdPosition = hmdObject.transform.localPosition;
		Vector3 handPosition = handObject.transform.localPosition;
		Vector3 handheadDiff = new Vector3(0.0f,0.0f,0.0f);
		
		//handheadDiff.x = handPosition.x - hmdPosition.x;
		handheadDiff.z = handPosition.z - hmdPosition.z;
		Debug.Log ("position "+this.transform.localPosition );
		float factor = 2.0f;
		if (handheadDiff.magnitude > threshold) {
			
			float magCalc = handheadDiff.magnitude;
			magCalc *= factor;
			
			
			if(InputBroker.GetKeyDown(WiimoteName + ":Plus")) {
				magCalc = magCalc*magCalc*magCalc*magCalc*magCalc;
				Debug.Log ("Plus " );
				magCalc /= factor*factor*factor*factor*factor;
				
			}
			
			else if (InputBroker.GetKeyDown(WiimoteName + ":Minus")) {
				magCalc = magCalc*magCalc;
				Debug.Log ("Minus" );
				magCalc /= factor*factor;
			}
			
			else{
				magCalc = magCalc*magCalc*magCalc*magCalc;
				Debug.Log ("Normal");
				magCalc /= factor*factor*factor*factor;
			}
			
			this.transform.localPosition=handheadDiff.normalized*magCalc;	
			grabbedObject.transform.localPosition = handheadDiff.normalized*magCalc;	

			Debug.Log ("position after threshold "+handheadDiff );
			
		}
		else{
			
			this.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
			Debug.Log ("position below threshold "+this.transform.localPosition );
		}

	}
	
	// Trigger function for collisions
	void OnTriggerEnter(Collider other) {
		// If an object is not already grabbed, check for collisions with another
		if(grabbedObject == null) {
			collidedObject = other.gameObject;
		}
	}

	// Trigger function for exiting collisions
	void OnTriggerExit(Collider other) {
		// If an object is not grabbed, forget the collided object
		if(grabbedObject == null) {
			collidedObject = null;
		}
	}
}
