using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOptionPopup : MonoBehaviour
{
    SoundMgr SoundMgr;

    public Text MasterVolumText;
    public Text BGMVolumText;
    public Text SfxVolumText;

    public Slider MasterVolumSlider;
    public Slider BGMVolumSlider;
    public Slider SfxVolumSlider;
    private void Start()
    {
        SoundMgr = Shared.SoundMgr;
    }

    public void OnSetMasetVolume()
    {
        float value = MasterVolumSlider.value;
        SoundMgr.SetMasterVolume(value);
        UpdatePercentText(MasterVolumText, value);
    }
    public void OnSetBGMVolume()
    {
        float value = BGMVolumSlider.value;
        SoundMgr.SetBGMVolume(value);
        UpdatePercentText(BGMVolumText, value);
    }
    public void OnSetSfxVolume()
    {
        float value = SfxVolumSlider.value;
        SoundMgr.SetSfxVolume(value);
        UpdatePercentText(SfxVolumText, value);
    }

    private void UpdatePercentText(Text _text, float _value)
    {
        int percent = Mathf.RoundToInt(_value * 100);
        _text.text = $"{percent}%";
    }

    public void OnGameClosePopup()
    {
        Shared.UiMgr.GameClosePopup.SetActive(true);
    }
}
