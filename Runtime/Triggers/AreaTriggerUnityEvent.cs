using OneM.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace OneM.InteractableSystem
{
    /// <summary>
    /// Triggers <see cref="OnEntered"/> and <see cref="OnExited"/> Unity Events 
    /// when a GameObject with the given tag enters/exits the area.
    /// </summary>
    [SelectionBase]
    [DisallowMultipleComponent]
    public class AreaTriggerUnityEvent : MonoBehaviour
    {
        [Tag, Tooltip("The Tag to detect enter/exit from the Trigger Area.")]
        public string Tag = "Player";

        [Space]
        [Tooltip("Event fired when the entering in the trigger area.")]
        public UnityEvent<GameObject> OnEntered;
        [Tooltip("Event fired when exiting from the trigger area.")]
        public UnityEvent<GameObject> OnExited;

        /// <summary>
        /// The instance interacting with this area. It will be set only when interaction is happening.
        /// </summary>
        public GameObject Interactor { get; private set; }

        private void Reset() => CheckCollider();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tag)) Enter(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Tag)) Exit();
        }

        public void SetActive(bool isEnabled) => gameObject.SetActive(isEnabled);

        private void CheckCollider()
        {
            if (TryGetComponent(out Collider collider)) collider.isTrigger = true;
            else Debug.LogWarning("Add any Collider component.");
        }

        protected virtual void Enter(GameObject interactor)
        {
            Interactor = interactor;
            OnEntered?.Invoke(Interactor);
        }

        protected virtual void Exit()
        {
            OnExited?.Invoke(Interactor);
            Interactor = null;
        }
    }
}