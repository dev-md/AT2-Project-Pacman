using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pacman : MonoBehaviour
{
    //Serialized variables
    [SerializeField] private int lives = 3;
    [SerializeField] private float invincibleTime = 3;
    [SerializeField] private float speed = 3;
    private float maxSpeed; // Added Vars
    [SerializeField] private Transform pacmanSpawn;
    [SerializeField] private GameObject lifePanel;
    [SerializeField] private GameObject[] lifeIcons;
    [SerializeField] private AudioClip deathClip;

    //Private variables
    private int currentLives = 0;
    private float respawnTimer = -1;
    private Vector2 input;
    private CharacterController controller;
    private AudioSource aSrc;

    //Sprint vars
    private float sprintTimer = 0f;
    [SerializeField] private float sprintSpeed = 2f;
    [SerializeField] private Image sprintFill;
    [SerializeField] private Text sprintText;

    /// <summary>
    /// Creates necessary references.
    /// </summary>
    private void Awake()
    {
        //Find controller reference
        TryGetComponent(out CharacterController charController);
        if (charController != null)
        {
            controller = charController;
        }
        else
        {
            Debug.LogError("Pacman: Character controller required.");
        }
        //Find audio source
        TryGetComponent(out AudioSource audioSource);
        if (audioSource != null)
        {
            aSrc = audioSource;
        }
        else
        {
            Debug.LogError("Pacman: Audio source required.");
        }
        //Check for trigger collider
        bool colliderFound = false;
        foreach (Collider col in GetComponents<Collider>())
        {
            if (col.isTrigger == true)
            {
                colliderFound = true;
                break;
            }
        }
        if (colliderFound == false)
        {
            Debug.LogError("Pacman: Collider with 'isTrigger' set to true is required.");
        }

        //Remebering the speed value. // Also Adding new stuff.
        maxSpeed = speed;

    }

    /// <summary>
    /// Set initial game state.
    /// </summary>
    private void Start()
    {
        //Set lives
        currentLives = lives;
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            if (lifeIcons[i] != null)
            {
                if (i < currentLives)
                {
                    lifeIcons[i].SetActive(true);
                }
                else
                {
                    lifeIcons[i].SetActive(false);
                }
            }
        }
        //Victory method assigned to event
        GameManager.Instance.Event_GameVictory += delegate { enabled = false; };
    }

    /// <summary>
    /// Frame by frame functionality.
    /// </summary>
    void Update()
    {
        //Respawn timer
        if(respawnTimer > -1)
        {
            respawnTimer += Time.deltaTime; 
            if(respawnTimer > invincibleTime)
            {
                respawnTimer = -1;
            }
        }

        //Read inputs
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        Vector3 motion = Vector3.zero;
        //Left/right movement
        if (input.x > 0)
        {
            transform.forward = Vector3.right;
            motion = transform.forward.normalized;
        }
        else if (input.x < 0)
        {
            transform.forward = -Vector3.right;
            motion = transform.forward.normalized;
        }
        //Forward/backward movement
        else if (input.y > 0)
        {
            transform.forward = Vector3.forward;
            motion = transform.forward.normalized;
        }
        else if (input.y < 0)
        {
            transform.forward = -Vector3.forward;
            motion = transform.forward.normalized;
        }
        else
        {
            motion = transform.forward.normalized;
        }

        if ((Input.GetKey(KeyCode.LeftShift)) && (sprintTimer > 0) && (speed != 0))
        {
            sprintTimer -= Time.deltaTime;
            if (speed != maxSpeed + sprintSpeed)
            {
                speed += sprintSpeed;
            }
        }
        else
        {
            if(speed != 0)
            {
                speed = maxSpeed;
            }
        }

        //Apply movement to controller
        controller.Move(motion.normalized * speed * Time.deltaTime);
        UpdateSprintUI();
    }

    /// <summary>
    /// World interactions.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        //Detect collisions
        switch (other.tag)
        {
            case "Pellet":
                GameManager.Instance.PickUpPellet(1);
                sprintTimer += 0.1f;
                other.gameObject.SetActive(false);
                break;
            case "Power Pellet":
                GameManager.Instance.PickUpPellet(1, 1);
                sprintTimer += 0.75f;
                other.gameObject.SetActive(false);
                break;
            case "Bonus Item":
                GameManager.Instance.PickUpPellet(50, 2);
                sprintTimer += 3f;
                other.gameObject.SetActive(false);
                break;
            case "Ghost":
                if (GameManager.Instance.PowerUpTimer > -1)
                {
                    other.TryGetComponent(out Ghost ghost);
                    if (ghost.CurrentState != ghost.RespawnState)
                    {
                        GameManager.Instance.EatGhost(ghost);
                        sprintTimer += 0.25f;
                        sprintTimer *= 1.05f;
                    }
                }
                break;
        }
    }

    /// <summary>
    /// Respawns the player.
    /// </summary>
    private void RespawnPlayer()
    {
        //Move player to spawn
        controller.enabled = false;
        transform.position = pacmanSpawn.position;
        transform.forward = pacmanSpawn.forward;
        controller.enabled = true;
        respawnTimer = 0;
        GameManager.Instance.RespawnPowerPellet();
    }

    /// <summary>
    /// Subtracts player lives.
    /// </summary>
    /// <returns></returns>
    public bool Die()
    {
        if (respawnTimer == -1)
        {
            //Subtract life
            aSrc.PlayOneShot(deathClip);
            if (currentLives > 1)
            {
                currentLives--;
                if(currentLives < lifeIcons.Length)
                {
                    lifeIcons[currentLives].SetActive(false);
                }
                else
                {
                    Debug.LogError("There are less life icons then the player's current lives.");
                }
                RespawnPlayer();
                sprintTimer += 1f;
                return false;
            }
            else    //Game over
            {
                enabled = false;
                lifePanel.SetActive(false);
                GameManager.Instance.Delegate_GameOver.Invoke();
                return true;
            }
        }
        return false;
    }


    //Dylan Mount
    //25/08/2022
    public bool toggleSpeed(bool value)
    {
        if(value == false)
        {
            speed = 0;
        }
        else if (value == true)
        {
            speed = maxSpeed;
        }
        else
        {
            Debug.Log("EHHH?");
        }

        return value;
    }

    private void UpdateSprintUI()
    {
        if(sprintTimer > 0)
        {
            string _fillerText;
            _fillerText = sprintTimer.ToString();
            _fillerText = _fillerText.Substring(0, 1);
            sprintText.text = _fillerText;

            int _firstDigit = int.Parse(_fillerText);
            float fillNumber = (sprintTimer - _firstDigit);
            if (fillNumber > 1)
            {
                fillNumber = 1;
            }
            sprintFill.fillAmount = fillNumber;
        }
        
    }
}
