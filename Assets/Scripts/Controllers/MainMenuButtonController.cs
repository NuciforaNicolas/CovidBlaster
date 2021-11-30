using System;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class MainMenuButtonController : MonoBehaviour
    {
        public void StartGame()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/MenuButtons/Click");
            AudioController audioController = GameObject.FindWithTag("AudioController").GetComponent<AudioController>();
            audioController.SoundtrackEventInstance.stop(STOP_MODE.IMMEDIATE);
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }

        public void QuitGame()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/MenuButtons/Click");
            Application.Quit(0);
        }
    }
}