namespace _3rdPartyScripts.ru
 {
     using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Footsteps))]

public class PlayerControl : MonoBehaviour {

	[Header("General")]
	public float speed = 1.5f; // скорость движения
	public float acceleration = 100f; // ускорение
	public float stepTimer = 0.8f; // интервал шагов во время ходьбы (секунды)

	[Header("Head / Camera")]
	public Transform head;
	public float sensitivity = 5f; // чувствительность мыши
	public float headMinY = -40f; // ограничение угла для головы
	public float headMaxY = 40f;

	[Header("Jump")]
	public KeyCode jumpButton = KeyCode.Space; // клавиша для прыжка
	public float jumpForce = 10; // сила прыжка
	public float jumpDistance = 1.2f; // расстояние от центра объекта, до поверхности

	[Header("Run")]
	public KeyCode runButton = KeyCode.LeftShift; // клавиша для бега
	public float addForce = 5; // добавить к скорости
	public float stepTimerRun = 0.35f; // интервал шагов во время бега (секунды)

	private Vector3 direction;
	private int layerMask;
	private Rigidbody body;
	private float rotY, curT, h, vvvvvvvv, curSpeed, curStepTimer;
	private string tagName;
	private bool fall;

	private Footsteps foot;
	
	void Start () 
	{
		foot = GetComponent<Footsteps>();
		body = GetComponent<Rigidbody>();
		body.freezeRotation = true;
		layerMask = 1 << gameObject.layer | 1 << 2;
		layerMask = ~layerMask;
	}
	
	void FixedUpdate()
	{
		body.AddForce(direction.normalized * curSpeed * acceleration * body.mass);
		
		// Ограничение скорости, иначе объект будет постоянно ускоряться
		if(Mathf.Abs(body.velocity.x) > curSpeed)
		{
			body.velocity = new Vector3(Mathf.Sign(body.velocity.x) * curSpeed, body.velocity.y, body.velocity.z);
		}
		if(Mathf.Abs(body.velocity.z) > curSpeed)
		{
			body.velocity = new Vector3(body.velocity.x, body.velocity.y, Mathf.Sign(body.velocity.z) * curSpeed);
		}
	}

	bool GetJump() // проверяем, есть ли коллайдер под ногами
	{
		RaycastHit hit;
		Ray ray = new Ray(transform.position, Vector3.down);
		if(Physics.Raycast(ray, out hit, jumpDistance, layerMask))
		{
			tagName = hit.transform.tag; // берем тег поверхности
			return true;
		}

		tagName = string.Empty;
		return false;
	}

	void Update () 
	{
		curSpeed = speed;
		curStepTimer = stepTimer;

		h = Input.GetAxis("Horizontal");
		vvvvvvvv = Input.GetAxis("Vertical");

		// управление головой (камерой)
		float rotX = head.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
		rotY += Input.GetAxis("Mouse Y") * sensitivity;
		rotY = Mathf.Clamp (rotY, headMinY, headMaxY);
		head.localEulerAngles = new Vector3(-rotY, rotX, 0);

		// вектор направления движения
		direction = new Vector3(h, 0, vvvvvvvv);
		direction = head.TransformDirection(direction);
		direction = new Vector3(direction.x, 0, direction.z);

		Debug.DrawRay(transform.position, Vector3.down * jumpDistance, Color.red); // подсветка, для визуальной настройки jumpDistance

		if(Input.GetKey(runButton)) // если удерживать клавишу бега
		{
			curSpeed = speed + addForce;
			curStepTimer = stepTimerRun;
		}

		if(GetJump())
		{
			if(Input.GetKeyDown(jumpButton))
			{
				body.velocity = new Vector2(0, jumpForce);
			}

			Steps();
			Falling();
		}
		else
		{
			curT = 0;
			fall = true;
		}
	}

	void Falling() // падение на что-нибудь
	{
		if(fall)
		{
			fall = false;
			curT = 0;
			GetStep();
		}
	}

	void Steps()
	{
		// округляем текущее значение скорости по оси X и Z, до сотых
		// чтобы исключить те, которые близкие к нулю, например: 0.000001805331f
		// в противном случаи, функция будет срабатывать, даже если персонаж не движется
		float velocityZ = RoundTo(Mathf.Abs(body.velocity.z), 100);
		float velocityX = RoundTo(Mathf.Abs(body.velocity.x), 100);

		if(velocityZ > 0 && Mathf.Abs(vvvvvvvv) > 0 || velocityX > 0 && Mathf.Abs(h) > 0) // если персонаж движется
		{
			curT += Time.deltaTime;

			if(curT > curStepTimer)
			{
				curT = 0;
				GetStep();
			}
		}
		else
		{
			curT = 1000;
		}
	}

	void GetStep() // фильтр по тегу
	{
		switch(tagName)
		{
		case "GameController":
			foot.PlayStep(Footsteps.StepsOn.Beton, 1);
			break;
		case "Finish":
			foot.PlayStep(Footsteps.StepsOn.Ground, 1);
			break;
		}
	}

	float RoundTo(float f, int to) // округлить до, указанного значения
	{
		return ((int)(f*to))/(float)to;
	}
}
 }