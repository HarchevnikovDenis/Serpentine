using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	private bool canJump = true;
	private Rigidbody rb;
	private bool extraJump = false;
	private float timing = 0f;
	private Animator anim;
	private bool isOver = false;
	[SerializeField] private Text _text;
	[SerializeField] private Text _textCoins;
	[SerializeField] private Camera _camera;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		_textCoins.text = GameManager.instance.coinsCount.ToString();
		StartCoroutine("AddScore");
	}

	void Update()
	{
		
	}

	// Update is called once per frame
	void LateUpdate () {

		if(Input.GetMouseButton(0) && canJump && !extraJump)
		{
			/* Ray _ray = _camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(_ray, out hit))
			{
				Transform objectHit = hit.transform;
				Debug.Log("X: "+objectHit.position.x+" Y: "+objectHit.position.y);
			}*/
			Jump();
		}
		if(Input.GetMouseButton(0) && !canJump && extraJump)
		{
			ExtraJump();
		}	
		if(Input.GetMouseButtonUp(0))
		{
			canJump = false;
			extraJump = false;
		}
		
		
			
	}

	void Jump()
	{
		rb.AddForce(Vector3.up * 15.8f, ForceMode.Impulse);
		canJump = false;
		extraJump = true;
		anim.speed = 0f;
	}

	void ExtraJump()
	{
		timing += Time.deltaTime;

		if(timing > 0.201f)
		{
			rb.AddForce(Vector3.up * 6.1f, ForceMode.Impulse);
			extraJump = false;
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.name == "Floor")
		{
			canJump = true;
			extraJump = false;
			timing  = 0f;
			anim.speed = 1f;
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Obstacle")
		{
			isOver = true;
			//Time.timeScale = 0.23f;
			GameManager.instance.GameOver();
		}

		if(other.gameObject.tag == "Coin")
		{
			GameManager.instance.coinsCount += 1;
			_textCoins.text = GameManager.instance.coinsCount.ToString();
			Destroy(other.gameObject, 0.1f);
		}
		
	}

	IEnumerator AddScore()
	{
		while(!isOver)
		{
			yield return new WaitForSeconds(0.9f);
			if(isOver)
				break;
			GameManager.instance.currentScore += 1;
			_text.text = GameManager.instance.currentScore.ToString();
		}
		yield return null;
	}
}
