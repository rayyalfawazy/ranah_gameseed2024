using UnityEngine;

namespace RRR
{
    public class RRR_TutorialTrigger : MonoBehaviour
    {
        public RRR_TutorialManager tutorialManager;

        private void Start()
        {
            if (tutorialManager == null)
            {
                Debug.LogError("TutorialManager script not found in the scene.");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player collided with Collider");
                tutorialManager.StartTutorial();
                Debug.Log("Starting Tutorial");
            }
        }
    }
}