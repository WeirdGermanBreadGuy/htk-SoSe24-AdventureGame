
using UnityEngine;

public class UiLink : MonoBehaviour
{
    [SerializeField] private GameObject screen;
    private GameObject _screenInstance;

    public void Open()
    {
        _screenInstance = UiService.Open(screen);
    }

    public void Close()
    {
        if (_screenInstance != null)
        {
            Destroy(_screenInstance.gameObject);
        }
    }
}

