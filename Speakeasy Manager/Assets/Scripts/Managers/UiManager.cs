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
            string option = resolutions[i].width + "x" + resolutions[i].height;//here we take the width and height values out of the array and put them in the option string 
            options.Add(option);//Here we add the option in the list 

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height) //Here we check if the selected resolution is the same as the current resolution and then fill in the int so we can display in the dropdown which one is selected as default
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options); //Here we fill the resolution drop down list with the options list
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

    public void SetResolution(int resolutionIndex) //Here we change the resolution if we selected an diffrent resolution in the dropdown
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width , resolution.height,true);
    }



}
