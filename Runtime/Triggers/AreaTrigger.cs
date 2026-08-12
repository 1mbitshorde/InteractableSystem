using OneM.Attributes;
using System;
using UnityEngine;

namespace OneM.InteractableSystem
{
    /// <summary>
    /// Triggers <see cref="OnEntered"/> and <see cref="OnExited"/> events 
    /// when a GameObject with the given tag enters/exits the area.
    /// </summary>
    [SelectionBase]
    [DisallowMultipleComponent]
    public class AreaTrigger : MonoBehaviour
    {
        [Tag, Tooltip("The Tag to detect enter/exit from the Trigger Area.")]
        public string Tag = "Player";

        /// <summary>
        /// The instance interacting with this area. It will be set only when interaction is happening.
        /// </summary>
        public GameObject Interactor { get; private set; }

        /// <summary>
        /// Event fired when entering into the trigger area.
        /// </summary>
        /// <remarks>
        /// Check <see cref="Interactor"/> to know who is entering the area.
        /// </remarks>
        public event Action OnEntered;

        /// <summary>
        /// Event fired when exiting from the trigger area.
        /// </summary>
        /// <remarks>
        /// Check <see cref="Interactor"/> to know who is exiting the area.
        /// </remarks>
        public event Action OnExited;

        /// <summary>
        /// Global event fired when entering into any trigger area. 
        /// </summary>
        /// <remarks>The given GameObject is the trigger instance.</remarks>
        public static event Action<GameObject> OnAnyEntered;

        /// <summary>
        /// Global event fired when exiting from any trigger area. 
        /// </summary>
        /// <remarks>The given GameObject is the trigger instance.</remarks>
        public static event Action<GameObject> OnAnyExited;

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
            OnEntered?.Invoke();
            OnAnyEntered?.Invoke(gameObject);
        }

        protected virtual void Exit()
        {
            OnExited?.Invoke();
            OnAnyExited?.Invoke(gameObject);
            Interactor = null;
        }
    }
}