using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {
	private bool canJump = true;							//Стоит ли игрок на земле
	private Rigidbody rb;	
	private Animator anim;
	private float screenHeight;								//Длина экрана, чтобы ограничить высоту нажатия
	private Vector2 firstPos;								//1ая позиция свайпа
	private Vector2 secondPos;								//2ая позиция свайпа
	private float swipeYlength;								//длина свайпа
	[HideInInspector]public bool isOver = false;			//Окончена ли игра
	[SerializeField] private Text _text;					//Отображание SCORE на экране
	[SerializeField] private Text _textCoins;				//Отображение кол-ва монет на экране
	[SerializeField] private ParticleSystem _particle;		//эффект


	void Start () {
		screenHeight = Camera.main.pixelHeight * 0.9f;
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		_textCoins.text = GameManager.instance.coinsCount.ToString();
		StartCoroutine("AddScore");
	}



	void FixedUpdate () {
		if(GameManager.instance.isPaused)
			return;
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
			if(swipeYlength > 100)
				ExtraJump();
			else
				Jump();
		}
	}

	void Jump()
	{
		rb.AddForce(Vector3.up * 15.8f, ForceMode.Impulse);
		canJump = false;
		anim.speed = 0f;
	}

	void ExtraJump()
	{
		rb.AddForce(Vector3.up * 20f, ForceMode.Impulse);
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
		if(other.gameObject.tag == "Obstacle")
		{
			isOver = true;
			Time.timeScale = 0.23f;
			GameManager.instance.GameOver();
		}

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
