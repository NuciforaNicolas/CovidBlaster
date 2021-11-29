using FMOD.Studio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class MainMenuButtonController : MonoBehaviour
    {
        public void StartGame()
        {
            AudioController audioController = GameObject.FindWithTag("AudioController").GetComponent<AudioController>();
            audioController.SoundtrackEventInstance.stop(STOP_MODE.IMMEDIATE);
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }

        public void QuitGame()
        {
            Application.Quit(0);
        }
    }
}