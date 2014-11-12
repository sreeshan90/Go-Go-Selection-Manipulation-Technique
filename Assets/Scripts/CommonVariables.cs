using UnityEngine;
using System.Collections;

// This class provides a means of using global variables among other scripts
public class CommonVariables {

	// Positional mapping between the physical space and the virtual world
	static public Vector3 mappedPosition = new Vector3(0f, 0f, 0f);
	
	// Rotational mapping between the physical space and the virtual world
	static public Vector3 mappedRotation = new Vector3(0f, 0f, 0f);
	
	// Interpupillary distance between the eyes
	static public float dynamicIPD = 0.064f;
}
