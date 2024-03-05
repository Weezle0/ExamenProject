using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public AudioMixer sfxAudiomixer;
    public AudioMixer musicAudiomixer;
    public Dropdown resolutionDropdown;

    Resolution[] resolutions;


    void Start()
    {
        resolutions = Screen.resolutions; //Here we fill our array with the screen resolutions we get from unity 

        resolutionDropdown.ClearOptions(); //Here we clear the dropdown options 
        
        List<string> options = new List<string>(); //Here we create a list

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            } 
        } 
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void ChangeScene(int sceneNumber) //This ensures that the correct scene is loaded
    {
        SceneManager.LoadScene(sceneNumber);
    }
   
    public void QuitButton() //This causes the application to close
    {
        Application.Quit();
    }

    public void SfxVolume(float sfxVolume) //Here we change the sfx volume we set the float of the exposed parameter by using our own float
    { 
        sfxAudiomixer.SetFloat("sfxVolume", sfxVolume); 
    }
    public void MusicVolume(float musicVolume) //Here we change the music volume we set the float of the exposed parameter by using our own float
    {
        musicAudiomixer.SetFloat("musicVolume", musicVolume); 
    }
    public void SetQuality(int qualityIndex) //Here we change the quality we set the quality level to the quality index
    {
        QualitySettings.SetQualityLevel(qualityIndex); 
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width , resolution.height,true);
    }



}
