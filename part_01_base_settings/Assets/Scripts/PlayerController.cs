using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    private Camera camera;
    private Vector3 moveInput;
    private Vector3 moveVelocity;

    // COMPONENTS
    private CharacterController characterController;
    public GunController gunController;
    private GameObject respawnPoint;
    public PlayerHealthController healthController;
    public PlayerAmmoController ammoController;
    public PlayerExperienceController expaController;
    public PlayerStats playerStats;
    public PlayerSoundController soundController;
    public MainMenu mainMenu;

    public float timeBetweenSteps = 0.3f;
    public float timer = 0f;
    
    public void savePlayer()
    {
        SaveSystem.savePlayer(this);
    }

    public void loadPlayer()
    {
        PlayerData data = SaveSystem.loadPlayer();

        Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
        transform.position = position;
        healthController.currentHealth = data.currentHealth;
        ammoController.currentAmmo = data.currentAmmo;
        expaController.currentExpa = data.currentExpa;
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn");
        healthController = gameObject.GetComponent<PlayerHealthController>();
        ammoController = gameObject.GetComponent<PlayerAmmoController>();
        expaController = gameObject.GetComponent<PlayerExperienceController>();
        playerStats = gameObject.GetComponent<PlayerStats>();
        mainMenu = GameObject.FindGameObjectWithTag("MainMenuCanvas").GetComponent<MainMenu>();
        soundController = gameObject.GetComponent<PlayerSoundController>();
        respawnPlayer();
    }


    void FixedUpdate()
    {
        if (!mainMenu.isGamePaused())
        {
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveVelocity = moveInput * speed;
            characterController.SimpleMove(moveVelocity);

            playStepSound();

            rotatePlayerToMousePosition();
            //playerAttackControls();

            if (Input.GetMouseButtonDown(0))
            {
                gunController.startShooting(true);
            }

            if (Input.GetMouseButtonUp(0))
            {
                gunController.startShooting(false);
            }

            savingSystemControls();
        }
    }

    private void playStepSound()
    {
        if (new Vector3(characterController.velocity.x, 0, characterController.velocity.z).magnitude > 0.2f)
        {
            timer += Time.deltaTime;

            if(timer > timeBetweenSteps)
            {
                timer = 0;
                soundController.playOneStep();
            }
        }
    }

    private void rotatePlayerToMousePosition()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(ray, out rayLength))
        {
            Vector3 pointToLook = ray.GetPoint(rayLength);
            //Debug.Log("pointToLook" + pointToLook);
            //Debug.DrawLine(ray.origin, pointToLook, Color.red);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    private void playerAttackControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gunController.startShooting(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            gunController.startShooting(false);
        }
    }

    private void savingSystemControls()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Player is Saved");
            savePlayer();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Player is Loaded");
            loadPlayer();
        }
    }

    public void respawnPlayer()
    {
        transform.position = respawnPoint.transform.position;
        healthController.changeHealth(healthController.maxHealth);
        playerStats.setUserMessage("");
    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Player collided something: " + other.gameObject);
    }

    private void rotatePlayerToPosition(Vector3 newTargetPosition)
    {
        Quaternion newRotation = Quaternion.LookRotation(newTargetPosition - transform.position, Vector3.forward);

        newRotation.x = 0.0f;
        newRotation.z = 0.0f;

        transform.rotation = newRotation;
    }

    private Vector3 locatePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.tag != "Player") // && hit.collider.tag != "Enemy")
            {
                return new Vector3(hit.point.x, hit.point.y, hit.point.z);
                //Debug.Log(position);
            }
        }

        return transform.position;
    }
    
    /*
     СТАРЫЙ ПОДХОД в управлении персонажем через RigidBody
     
     void FixedUpdate()
    {
        rigidbody.velocity = moveVelocity;
    }
    void Update()
    {
        if (!mainMenu.isGamePaused())
        {
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveVelocity = moveInput * speed;

            rotatePlayerToMousePosition();
            playerAttackControls();
            savingSystemControls();
        }
    }*/
    void someOldCode()
    {
        /*if (Input.GetMouseButtonUp(0)) {
            // Получаем направление луча
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 newTargetPosition;
            // Кидаем луч бесконечной длинны и проверяем пересечение слоев
            if (Physics.Raycast(ray, out hit, 1000)) {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
                if (hit.collider.tag != "Player") // && hit.collider.tag != "Enemy")
                {
                    // Проверяем то, что вернулось и перемещаем туда наш Target
                    //target.position = hit.point;
                    // Сообщаем персонажу о новом "задание"
                    //transform.GetTarget(target.position);

                    newTargetPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    Debug.Log("newTargetPosition: " + newTargetPosition);

                    rotatePlayerToPosition(newTargetPosition);
                }
            }
        }*/

        /*float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //перемещение относительно внутренних координат и угла вращения персонажа
        //Vector3 forward = transform.forward * v * speed * Time.deltaTime;
        //Vector3 right = transform.right * h * speed * Time.deltaTime;

        //перемещение относительно глобальных координат вне зависимости от угла вращения персонажа
        Vector3 forward = Vector3.forward * v * speed * Time.deltaTime;
        Vector3 right = Vector3.right * h * speed * Time.deltaTime;
        
        cc.Move(forward + right);*/
    }
}