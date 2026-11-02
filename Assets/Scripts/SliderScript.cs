using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{

    [SerializeField] Slider _mySlider;
    private void OnVolumeSliderChanged(float value)
    {
        AudioManager.Instance.MusicSound(value);
    }

    private void Awake()
    {

        if (_mySlider != null)
        {
            _mySlider.value = Saves.ChangeSound(); 
            _mySlider.onValueChanged.AddListener(OnVolumeSliderChanged);
        }
    }
        
}
