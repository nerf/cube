using UnityEngine;
using System.Collections;

public class ShootingPositionController : MonoBehaviour {
	public GameObject cube;
	public Material[] cubeMaterials = new Material[4];
	private GameObject currentCube;
	static System.Random random = new System.Random ();

	void Start () {
		Reload ();
	}
	
	void Update () {
		
	}

	void OnMouseOver () {
		Debug.Log ("over");
	}

	private void Reload () {
		Material color = cubeMaterials[random.Next (0, cubeMaterials.Length)];
		GameObject newCube = (GameObject)Instantiate (cube, transform.position, Quaternion.identity);
		Renderer renderer = newCube.GetComponent<Renderer> ();
		renderer.material = color;

		currentCube = newCube;
	}

	private void Shoot () {

	}

}
