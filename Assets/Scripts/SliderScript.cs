using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] Slider _musicSlider;
    [SerializeField] Slider _sfxSlider;

    private void OnVolumeSliderChanged(float value, Slider _slider)
    {
        if (_slider == _musicSlider)
        {
            AudioManager.Instance.MusicVolume(value);
        }
        else if (_slider == _sfxSlider)
        {
            AudioManager.Instance.SFXVolume(value);
        }
    }

    private void Awake()
    {
        if (_musicSlider != null)
        {
            _musicSlider.onValueChanged.AddListener(value => OnVolumeSliderChanged(value, _musicSlider));
            _musicSlider.value = Saves.ChangeMusicVolume();
        }
        if (_sfxSlider != null)
        {
            _sfxSlider.onValueChanged.AddListener(value => OnVolumeSliderChanged(value, _sfxSlider));
            _sfxSlider.value = Saves.ChangeSFXVolume();
        }
    }
}
