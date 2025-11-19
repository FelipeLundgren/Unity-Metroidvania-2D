using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Button backButton;

    [SerializeField] private Slider MasterVolumeSlider;
    [SerializeField] private Slider SFXVolumeSlider;
    [SerializeField] private Slider EnvironmentVolumeSlider;

    private void Start()
    {
        backButton.onClick.AddListener(ClosePanel);

        MasterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeSliderChanged);
        SFXVolumeSlider.onValueChanged.AddListener(OnSFXVolumeSliderChanged);
        EnvironmentVolumeSlider.onValueChanged.AddListener(OnEnvironmentVolumeSliderChanged);

        MasterVolumeSlider.SetValueWithoutNotify(MainMenuManager.Instance.AudioManager.GetMixerVolume(MixerGroup.Master));
        SFXVolumeSlider.SetValueWithoutNotify(MainMenuManager.Instance.AudioManager.GetMixerVolume(MixerGroup.SFX));
        EnvironmentVolumeSlider.SetValueWithoutNotify(MainMenuManager.Instance.AudioManager.GetMixerVolume(MixerGroup.Environment));
    }

    private void ClosePanel()
    {
        MainMenuManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);
        this.gameObject.SetActive(false);
    }

    private void OnMasterVolumeSliderChanged(float value)
    {
        MainMenuManager.Instance.AudioManager.SetMixerVolume(MixerGroup.Master, value);
    }

    private void OnSFXVolumeSliderChanged(float value)
    {
        MainMenuManager.Instance.AudioManager.SetMixerVolume(MixerGroup.SFX, value);
    }

    private void OnEnvironmentVolumeSliderChanged(float value)
    { 
        MainMenuManager.Instance.AudioManager.SetMixerVolume(MixerGroup.Environment, value);
    }
}