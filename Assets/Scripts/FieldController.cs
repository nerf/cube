using UnityEngine;
using System.Collections;

public class FieldController : MonoBehaviour {

	public int cubesPerRow = 5;
	public int numberOfRows = 5;
	public float cubesOffset = 0.02f;
	public Material[] myMaterials = new Material[4];
	public GameObject cube;
	public int rotationStep = 10;
	private int rotationDirection;
	private Vector3 currentRotation;
	private Vector3 targetRotation;
	private bool rotationInProgress = false;

	void Start () {
		System.Random random = new System.Random();
		float rowStartPosition = transform.position.y - (numberOfRows / 2f) + (transform.localScale.y / 2f);
		float colStartPosition = transform.position.x - (cubesPerRow / 2f) + (transform.localScale.x / 2f);


		for (int row = 0; row < numberOfRows; row++) {
			float fieldY = rowStartPosition + row + (cubesOffset * row);

			for (int col = 0; col < cubesPerRow; col++) {
				float fieldX = colStartPosition + col + (cubesOffset * col);
				Material color = myMaterials[random.Next (0, myMaterials.Length)];
				GameObject newCube = (GameObject)Instantiate (cube, new Vector3 (fieldX, fieldY), Quaternion.identity);
				newCube.transform.parent = transform;
				Renderer renderer = newCube.GetComponent<Renderer> ();
				renderer.material = color;					
			}
		}
	}

	void FixedUpdate () {
		if (rotationInProgress)
			return;

		if (Input.GetKeyUp ("left")) {
			rotationDirection = 1;
			rotateObject ();
		} else if (Input.GetKeyUp ("right")) {
			rotationDirection = -1;
			rotateObject ();
		}
	}

	private void rotateObject () {
		rotationInProgress = true;
		currentRotation = transform.eulerAngles;
		targetRotation.z = (currentRotation.z + (90 * rotationDirection));
		StartCoroutine (objectRotationAnimation());
	}

	IEnumerator objectRotationAnimation()
	{
		// add rotation step to current rotation.
		currentRotation.z += (rotationStep * rotationDirection);
		gameObject.transform.eulerAngles = currentRotation;

		yield return new WaitForSeconds (0);

		if (((int)currentRotation.z >
			(int)targetRotation.z && rotationDirection < 0)  ||  // for clockwise
			((int)currentRotation.z <  (int)targetRotation.z && rotationDirection > 0)) // for anti-clockwise
		{
			StartCoroutine (objectRotationAnimation());
		}
		else
		{
			gameObject.transform.eulerAngles = targetRotation;
			rotationInProgress = false;
		}
	}

	IEnumerator rotateObjectAgain()
	{
		yield return new WaitForSeconds (0.2f);
		rotateObject();
	}
}
