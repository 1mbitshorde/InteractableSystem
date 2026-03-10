using UnityEngine;
using UnityEngine.Events;

namespace OneM.InteractableSystem
{
    /// <summary>
    /// Low coupling implementation of <see cref="IInteractable"/> using <see cref="UnityEvent"/>.
    /// </summary>
    /// <remarks>
    /// Use this component to quickly create an Interactable instance.
    /// </remarks>
    [DisallowMultipleComponent]
    public sealed class UnityEventInteractable : MonoBehaviour, IInteractable
    {
        /// <summary>
        /// Event fired when interacted with this object.
        /// </summary>
        public UnityEvent OnInteracted;

        /// <summary>
        /// Event fired when this object availability is changed.
        /// </summary>
        public UnityEvent<bool> OnAvailabilityChanged;

        /// <summary>
        /// Event fired when interacted with this object fails.
        /// </summary>
        public UnityEvent OnInteractionFailChanged;

        public bool CanInteract() => enabled;
        public void Interact() => OnInteracted?.Invoke();
        public void ShowInteractionFail() => OnInteractionFailChanged?.Invoke();
        public void ChangeAvailability(bool isAvailable) => OnAvailabilityChanged?.Invoke(isAvailable);
    }
}