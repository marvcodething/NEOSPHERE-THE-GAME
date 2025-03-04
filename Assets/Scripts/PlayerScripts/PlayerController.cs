using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Sprite[] healthBarSprites;
    public Image healthBarImage;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public TMPro.TextMeshProUGUI GameTimer; // Changed to TextMeshProUGUI
    public TMPro.TextMeshProUGUI CollectibleCounter;
    public float timeToComplete = 3000;
    private float currentTime = 0;

    public float speed = 0;
    public float regenSpeed = 5f;
    public int currHealth = 8;
    public int maxHealth = 8;
    private float regenTimer = 0f;
    public int CollectibleGoal = 8;
    private int currentCollectibles = 0;
    public GameObject deathScreenPrefab; // Assign in Inspector
    public GameObject redScreenPrefab; // Assign red screen prefab in Inspector

    private GameObject deathScreenInstance;
    private GameObject redScreenInstance;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateHealthText();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Poison"))
        {
            Debug.Log("Player has been poisoned!");
            currHealth = 0;
            UpdateHealthText();
        }
        if (other.gameObject.CompareTag("Collectible"))
        {
            Debug.Log("Player has collected");
            other.gameObject.SetActive(false);
            currentCollectibles += 1;
            if (currentCollectibles == CollectibleGoal){
                Time.timeScale = 0f; 
                ShowWinScreen();
            }
            UpdateCounter();
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    public void UpdateHealthText()
    {
        int spriteIndex = Mathf.Clamp((currHealth * (healthBarSprites.Length - 1)) / maxHealth, 0, healthBarSprites.Length - 1);
        healthBarImage.sprite = healthBarSprites[spriteIndex];
    }

    void ShowDeathScreen()
    {
        if (deathScreenPrefab != null && deathScreenInstance == null)
        {
            deathScreenInstance = Instantiate(deathScreenPrefab);
            healthBarImage.gameObject.SetActive(false);
            Time.timeScale = 0f; // Pause the game
        }
    }

    void ShowWinScreen()
    {

    }

    void UpdateRedScreen()
    {
        if (redScreenPrefab != null)
        {
            if (currHealth <= 2 && redScreenInstance == null)
            {
                redScreenInstance = Instantiate(redScreenPrefab);
            }
            else if (currHealth > 2 && redScreenInstance != null)
            {
                Destroy(redScreenInstance);
                redScreenInstance = null;
            }
        }
    }
    
    void UpdateTimer()
    {
        currentTime += Time.deltaTime;
        float remainingTime = timeToComplete - currentTime;
        GameTimer.text = "Corruption In " + remainingTime.ToString("F1") + " Seconds"; // Show one decimal place
    }
    void UpdateCounter()
    {
        CollectibleCounter.text = currentCollectibles.ToString() + "/" + CollectibleGoal.ToString();
    }
    void Update()
    {
        UpdateTimer();
        if (currentTime >= timeToComplete){
            ShowDeathScreen();
        }
        if (currHealth < maxHealth)
        {
            regenTimer += Time.deltaTime;
            if (regenTimer >= regenSpeed)
            {
                currHealth += 2;
                UpdateHealthText();
                regenTimer = 0f;
            }
        }

        UpdateRedScreen();

        if (currHealth <= 0)
        {
            ShowDeathScreen();
        }

        if (deathScreenInstance != null && Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1f; // Reset time
            SceneManager.LoadScene(1);
        }
    }
}
