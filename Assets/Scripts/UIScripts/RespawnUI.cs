using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RespawnMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    [SerializeField] private Button respawnButton;
    [SerializeField] private Button quitButton;
    private Health playerHealth;
    private GameObject player;

    private void Start()
    {
        menuUI.SetActive(false); // Ensure menu is hidden at start

        // Find player
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }

        // Button listeners
        respawnButton.onClick.AddListener(Respawn);
        quitButton.onClick.AddListener(QuitToMenu);
    }

    public void ShowMenu()
    {
        menuUI.SetActive(true);
        Time.timeScale = 0f; // Pause game
    }

    public void HideMenu()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1f; // Resume game
    }

    private void Respawn()
    {
        if (playerHealth != null)
        {
            playerHealth.Respawn(); // Calls the respawn function in Health
        }
        HideMenu();
    }

    private void QuitToMenu()
    {
        Time.timeScale = 1f; // Ensure time is reset before loading menu
        SceneManager.LoadScene("Menu"); 
    }
}
