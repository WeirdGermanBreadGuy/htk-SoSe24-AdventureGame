using UnityEngine;

public class NormalUiView : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseScreen;

    private void Update()
    {
        if (Input.GetButtonDown("Menu") && Time.timeScale > 0)
        {
            Pause();
        }
    }

    private void Pause()
    {
        UiService.Open(pauseScreen.gameObject);
    }
}