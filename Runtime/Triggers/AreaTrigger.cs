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
    public sealed class AreaTrigger : MonoBehaviour
    {
        [Tag, Tooltip("The Tag to detect enter/exit from the Trigger Area.")]
        public string _tag = "Player";

        /// <summary>
        /// The instance interacting with this area. It will be set only when interaction is happening.
        /// </summary>
        public Transform Interactor { get; private set; }

        /// <summary>
        /// Event fired when the entering in the trigger area. 
        /// Check <see cref="Interactor"/> to know who is entering the area.
        /// </summary>
        public event Action OnEntered;

        /// <summary>
        /// Event fired when exiting from the trigger area.
        /// Check <see cref="Interactor"/> to know who is exiting the area.
        /// </summary>
        public event Action OnExited;

        private void Reset() => CheckCollider();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_tag)) Enter(other.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_tag)) Exit();
        }

        public void SetActive(bool isEnabled) => gameObject.SetActive(isEnabled);

        private void CheckCollider()
        {
            if (TryGetComponent(out Collider collider)) collider.isTrigger = true;
            else Debug.LogWarning("Add any Collider component.");
        }

        private void Enter(Transform interactor)
        {
            Interactor = interactor;
            OnEntered?.Invoke();
        }

        private void Exit()
        {
            OnExited?.Invoke();
            Interactor = null;
        }
    }
}