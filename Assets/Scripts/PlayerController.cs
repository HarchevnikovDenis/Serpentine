using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {
	private bool canJump = true;
	private Rigidbody rb;
	private Animator anim;
	private float screenHeight;
	private Vector2 firstPos;
	private Vector2 secondPos;
	private float swipeYlength;
	[HideInInspector]public bool isOver = false;
	[SerializeField] private Text _text;
	[SerializeField] private Text _textCoins;
	[SerializeField] private ParticleSystem _particle;

	// Use this for initialization
	void Start () {
		screenHeight = Camera.main.pixelHeight * 0.9f;
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		_textCoins.text = GameManager.instance.coinsCount.ToString();
		StartCoroutine("AddScore");
	}


	// Update is called once per frame
	void FixedUpdate () {
			//Код с MOUSEBTNDOWN
		/*if(Input.GetMouseButton(0) && canJump && !extraJump && Time.timeScale == 1f)
		{
			Ray _ray = _camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(_ray, out hit))
			{
				Transform objectHit = hit.transform;
				Debug.Log("X: "+objectHit.position.x+" Y: "+objectHit.position.y);
			}
			Jump();
			_particle.Stop();
		}
		if(Input.GetMouseButton(0) && !canJump && extraJump)
		{
			ExtraJump();
		}	
		if(Input.GetMouseButtonUp(0))
		{
			canJump = false;
			extraJump = false;
		}*/


			// Код с TOUCH
		/*if(Input.touchCount == 1 && canJump)
		{
			Touch t = Input.GetTouch(0);
			if(t.phase == TouchPhase.Began && !isTouch)
			{
				if(t.position.y > screenHeight)
					return;
				isTouch = true;
				StartCoroutine("TimeOfTouching");
				return;
			}

			if(t.phase == TouchPhase.Ended && isTouch)
			{
				isTouch = false;
			}
		}*/
		
		if(Input.touchCount > 0 && canJump)
		{
			Swipe();
		}
	}

	void Swipe()
	{
		Touch t = Input.GetTouch(0);
			if(t.position.y > screenHeight)
				return;
		
		if(t.phase == TouchPhase.Began)
		{
			firstPos = new Vector2(t.position.x, t.position.y);
		}
		if(t.phase == TouchPhase.Ended)
		{
			secondPos = new Vector2(t.position.x, t.position.y);
			swipeYlength = secondPos.y - firstPos.y;
			if(swipeYlength < 0)
				return;
			if(swipeYlength > 150)
				ExtraJump();
			else
				Jump();
			Debug.Log(swipeYlength);
		}
	}

	/*IEnumerator TimeOfTouching()
	{
		
		while(isTouch && timing < 0.3f)
		{
			yield return new WaitForSeconds(Time.deltaTime / 2);
			timing += Time.deltaTime;

			if(!isTouch || timing >= 0.3f)
				break;
		}
		Jump();
		isTouch = false;
		Debug.Log(timing);
		if(timing >= 0.3f)
			ExtraJump();
		else
			canJump = false;
		timing = 0f;
		yield return null;
	}*/

	void Jump()
	{
		rb.AddForce(Vector3.up * 15.8f, ForceMode.Impulse); //15.8
		canJump = false;
		anim.speed = 0f;
	}

	void ExtraJump()
	{
		rb.AddForce(Vector3.up * 20.8f, ForceMode.Impulse);
		canJump = false;
		anim.speed = 0f;
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.name == "Floor")
		{
			canJump = true;
			anim.speed = 1f;
			_particle.Play();
		}

	}

	void OnTriggerEnter(Collider other)
	{
		/* if(other.gameObject.tag == "Obstacle")
		{
			isOver = true;
			Time.timeScale = 0.23f;
			GameManager.instance.GameOver();
		}*/

		if(other.gameObject.tag == "Coin" && !isOver)
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
