using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
	private GameObject Player;
	[SerializeField]
	private Color[] colors;
	private Camera _cam;
	

	void Start()
	{
		_cam = GetComponent<Camera>();
		int index = Random.Range(0,colors.Length);
		_cam.backgroundColor = colors[index];
		Player = GameObject.FindWithTag("Player");
	}

	void FixedUpdate () {
		Vector3 needHeight = new Vector3(transform.position.x, Player.transform.position.y + 6f, transform.position.z);
		transform.position = Vector3.Lerp(transform.position, needHeight, 0.1f);
	}
}
