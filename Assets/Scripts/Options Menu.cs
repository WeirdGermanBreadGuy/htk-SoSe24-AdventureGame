using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private PauseMenu pauseMenu;
    private void Awake()
    {
        closeButton.onClick.AddListener(Close);
        closeButton.Select();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            Close();
        }
    }

    private void Close()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            UiService.Open(pauseMenu.gameObject);
        }
        Destroy(gameObject);
    }
}
