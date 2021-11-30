using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Controllers
{
    public class MainMenuUiController : MonoBehaviour
    {
        private GraphicRaycaster _graphicRaycaster;
        private PointerEventData _pointerEventData;
        private EventSystem _eventSystem;

        private GameObject _currentButton;

        private void Start()
        {
            _graphicRaycaster = GetComponent<GraphicRaycaster>();
            _eventSystem = GetComponent<EventSystem>();
        }

        private void Update()
        {
            _pointerEventData = new PointerEventData(_eventSystem);
            _pointerEventData.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            
            _graphicRaycaster.Raycast(_pointerEventData, raycastResults);

            GameObject currentButton = null;
            foreach (RaycastResult raycastResult in raycastResults)
            {
                if (raycastResult.gameObject.CompareTag("Button"))
                {
                    currentButton = raycastResult.gameObject;
                }
            }

            if (_currentButton == null && currentButton != null)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/MenuButtons/MouseOver");
            }

            _currentButton = currentButton;
        }
    }
}