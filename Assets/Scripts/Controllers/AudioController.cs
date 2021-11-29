using System;
using FMOD.Studio;
using UnityEngine;

namespace Controllers
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField]
        private string sceneName;

        private EventInstance _soundtrackEventInstance;

        public EventInstance SoundtrackEventInstance => _soundtrackEventInstance;

        private void Awake()
        {
            if (sceneName == "MainMenuScene")
            {
                _soundtrackEventInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/MainMenu");
                _soundtrackEventInstance.start();
            }
            else if (sceneName == "MainScene")
            {
                _soundtrackEventInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Gameplay");
                _soundtrackEventInstance.start();
            }
            
            
        }
    }
}