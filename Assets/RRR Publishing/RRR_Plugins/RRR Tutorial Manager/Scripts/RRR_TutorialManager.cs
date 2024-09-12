using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace RRR
{
    public class RRR_TutorialManager : MonoBehaviour
    {
        public GameObject tutorialCanvas;
        public GameObject closeButtonHolder;
        public GameObject previousButtonHolder;
        public GameObject nextButtonHolder;
        public GameObject nextButtonCloseVariantHolder;
        public Image[] tutorialImages;
        private int currentIndex = 0;
        private bool tutorialActive = false;
        [SerializeField]
        private bool cursorLockedInsideUI = false;

        void Start()
        {
            // Disable all tutorial images and button holders at the start
            DisableTutorial();
        }

        void FixedUpdate()
        {
            // Block inputs if tutorial is active
            if (tutorialActive)
            {
                BlockInputs();
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartTutorial();
            }
        }

        public void StartTutorial()
        {
            tutorialActive = true;
            tutorialCanvas.SetActive(true);
            ShowImage(currentIndex);
        }

        void DisableTutorial()
        {
            foreach (Image image in tutorialImages)
            {
                image.gameObject.SetActive(false);
            }
            if (closeButtonHolder != null)
            {
                closeButtonHolder.SetActive(false);
            }
            if (previousButtonHolder != null)
            {
                previousButtonHolder.SetActive(false);
            }
            if (nextButtonHolder != null)
            {
                nextButtonHolder.SetActive(false);
            }
            if (nextButtonCloseVariantHolder != null)
            {
                nextButtonCloseVariantHolder.SetActive(false);
            }
        }

        public void NextImage()
        {
            if (currentIndex < tutorialImages.Length - 1)
            {
                currentIndex++;
                ShowImage(currentIndex);
            }
        }

        public void PreviousImage()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                ShowImage(currentIndex);
            }
        }

        public void CloseTutorial()
        {
            // Reset active state of tutorial images and button holders
            DisableTutorial();

            // Reset currentIndex
            currentIndex = 0;

            // Disable tutorial canvas
            tutorialActive = false;
            tutorialCanvas.SetActive(false);

            // Reset cursor state
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        void ShowImage(int index)
        {
            for (int i = 0; i < tutorialImages.Length; i++)
            {
                if (i == index)
                {
                    tutorialImages[i].gameObject.SetActive(true);
                }
                else
                {
                    tutorialImages[i].gameObject.SetActive(false);
                }
            }

            // Activate button holders only when there are tutorial images visible
            if (closeButtonHolder != null)
            {
                closeButtonHolder.SetActive(tutorialImages[index].gameObject.activeSelf);
            }
            if (previousButtonHolder != null)
            {
                // Enable the Previous Button Holder only if the current index is not the first tutorial image
                previousButtonHolder.SetActive(index > 0 && tutorialImages[index].gameObject.activeSelf);
            }
            if (nextButtonHolder != null)
            {
                // Enable the Next Button Holder if the current index is not the last tutorial image
                nextButtonHolder.SetActive(index < tutorialImages.Length - 1);
            }
            if (nextButtonCloseVariantHolder != null)
            {
                // Enable the Next Button Close Variant Holder only if the Next Button Holder is disabled
                nextButtonCloseVariantHolder.SetActive(!nextButtonHolder.activeSelf);
            }
        }

        void BlockInputs()
        {
            if (cursorLockedInsideUI)
            {
                if (!IsPointerOverUIObject())
                {
                    // Disable mouse input
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;

                    // Disable keyboard input
                    foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
                    {
                        if (Input.GetKey(key))
                        {
                            Input.ResetInputAxes();
                            break;
                        }
                    }
                }
                else
                {
                    // Enable mouse and keyboard input
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }

        bool IsPointerOverUIObject()
        {
            // Check if the pointer is over any UI object
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}