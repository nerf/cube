using UnityEngine;
using System.Collections;

public class ShootingPositionController : MonoBehaviour {
	public GameObject cube;
	public Material[] cubeMaterials = new Material[4];
	public float cubesSpacing = 0.2f;
	public int projectileSpeed = 1;
	private GameObject currentCube;
	private bool isMoving = false;
	static System.Random random = new System.Random ();
	private GameObject playField;

	void Start () {
		playField = GameObject.FindGameObjectsWithTag("Field")[0];

		Reload ();
	}
	
	void Update () {
		if (isMoving)
			currentCube.transform.Translate (Vector3.up * (projectileSpeed * Time.deltaTime));
	}

	void FixedUpdate () {
		if (isMoving) {
			RaycastHit hit;
			if (Physics.Raycast (currentCube.transform.position, Vector3.up, out hit, 0.5f)) {
				if (hit.collider.CompareTag ("Cube")) {
					Vector3 target = hit.collider.gameObject.transform.position;
					currentCube.transform.position = new Vector3 (target.x, target.y - (1 + cubesSpacing), target.z);
					currentCube.transform.parent = playField.transform;
	
					Invoke ("Reload", 0.5f);
					isMoving = false;
				}
			}
		}
	}

	void OnMouseOver () {
		if (Input.GetMouseButtonUp (0))
			isMoving = true;
	}

	private void Reload () {
		Material color = cubeMaterials[random.Next (0, cubeMaterials.Length)];
		currentCube = (GameObject)Instantiate (cube, transform.position, Quaternion.identity);
		Renderer renderer = currentCube.GetComponent<Renderer> ();
		renderer.material = color;
	}

	private void Shoot () {

	}

}
