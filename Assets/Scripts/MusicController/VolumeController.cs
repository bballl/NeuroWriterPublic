using Assets.Scripts.SaveLoadData;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private const string MASTER = "MasterVolume";
    private const string SOUNDS = "SoundsVolume";
    private const string MUSIC = "MusicVolume";

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _soundsSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Button _button;

    private void Awake()
    {
        
        LoadVolumes();
    }

    public void LoadVolumes()
    {
        Debug.Log($"This is LOAD_VOLUMES in VolumeController in {name} GameObject");
        var loadData = new LoadData();

        float masterVolume = loadData.GetFloatData(GlobalVariables.MasterVolume);
        float soundsVolume = loadData.GetFloatData(GlobalVariables.SoundsVolume);
        float musicVolume = loadData.GetFloatData(GlobalVariables.MusicVolume);

        _masterVolumeSlider.value = masterVolume;
        _soundsSlider.value = soundsVolume;
        _musicSlider.value = musicVolume;

        ChangeMixerGroup(MASTER, masterVolume);
        ChangeMixerGroup(SOUNDS, soundsVolume);
        ChangeMixerGroup(MUSIC, musicVolume);

        Debug.Log($"Master={masterVolume}. Music={musicVolume}. Sounds={soundsVolume}");
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Save);
        _masterVolumeSlider.onValueChanged.AddListener(ChangeMasterVolume);
        _soundsSlider.onValueChanged.AddListener(ChangeSoundsVolume);
        _musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
    }

    private void ChangeMusicVolume(float value) => 
        ChangeMixerGroup(MUSIC, value);

    private void ChangeSoundsVolume(float value) => 
        ChangeMixerGroup(SOUNDS, value);

    private void ChangeMasterVolume(float value) => 
        ChangeMixerGroup(MASTER, value);

    private void ChangeMixerGroup(string groupName, float value) => 
        _audioMixer.SetFloat(groupName, value);

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Save);
        _masterVolumeSlider.onValueChanged.RemoveListener(ChangeMasterVolume);
        _soundsSlider.onValueChanged.RemoveListener(ChangeSoundsVolume);
        _musicSlider.onValueChanged.RemoveListener(ChangeMusicVolume);
    }

    private void Save()
    {
        var saveMaster = new SaveData(GlobalVariables.MasterVolume, _masterVolumeSlider.value);
        var saveMusic = new SaveData(GlobalVariables.MusicVolume, _musicSlider.value);
        var saveSounds = new SaveData(GlobalVariables.SoundsVolume, _soundsSlider.value);
    }
}
