using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private string vcaPath = "vca:/VCA Name";

    private void Awake()
    {
        GetComponent<Slider>().onValueChanged.AddListener(SetVolume);
    }

    private void OnEnable()
    { 
        VCA vca = RuntimeManager.GetVCA(vcaPath);
       // var vca = FMODUnity.RuntimeManager.GetVCA(vcaPath); 
        vca.getVolume(out var volume);
        GetComponent<Slider>().value = volume;
    }

    private void OnDestroy()
    {
        GetComponent<Slider>().onValueChanged.RemoveListener(SetVolume);
    }

    private void SetVolume(float volume)
    {
        VCA vca = RuntimeManager.GetVCA(vcaPath);
        vca.setVolume(volume);
    }
}
