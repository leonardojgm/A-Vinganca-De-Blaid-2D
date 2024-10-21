using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider SFXVolumeSlider;
    [SerializeField] private Slider environmentVolumeSlider;

    private void Start()
    {
        backButton.onClick.AddListener(ClosePanel);
        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeSliderChanged);
        SFXVolumeSlider.onValueChanged.AddListener(OnSFXVolumeSliderChanged);
        environmentVolumeSlider.onValueChanged.AddListener(OnEnvironmentVolumeSliderChanged);
        masterVolumeSlider.SetValueWithoutNotify(GameManager.Instance.AudioManager.GetMixerVolume(MixerGroup.Master));
        SFXVolumeSlider.SetValueWithoutNotify(GameManager.Instance.AudioManager.GetMixerVolume(MixerGroup.SFX));
        environmentVolumeSlider.SetValueWithoutNotify(GameManager.Instance.AudioManager.GetMixerVolume(MixerGroup.Environment));
    }

    private void ClosePanel()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);

        this.gameObject.SetActive(false);
    }

    private void OnMasterVolumeSliderChanged(float value)
    {
        GameManager.Instance.AudioManager.SetMixerVolume(MixerGroup.Master, value);
    }

    private void OnSFXVolumeSliderChanged(float value)
    {
        GameManager.Instance.AudioManager.SetMixerVolume(MixerGroup.SFX, value);
    }

    private void OnEnvironmentVolumeSliderChanged(float value)
    {
        GameManager.Instance.AudioManager.SetMixerVolume(MixerGroup.Environment, value);
    }
}
