using UnityEngine;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}