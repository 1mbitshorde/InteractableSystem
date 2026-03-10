using UnityEngine;
using UnityEngine.Events;

namespace OneM.InteractableSystem
{
    /// <summary>
    /// Low coupling implementation of <see cref="IInteractable"/> using <see cref="UnityEvent"/>.
    /// </summary>
    /// <remarks>
    /// Use this component to quickly create an Interactable instance reacting when 
    /// <see cref="AbstractInteractor{T}"/> implementations detect collisions.
    /// </remarks>
    [DisallowMultipleComponent]
    public sealed class InteractableUnityEvent : MonoBehaviour, IInteractable
    {
        [field: SerializeField]
        public Collider Collider { get; private set; }

        /// <summary>
        /// Event fired when interacted with this object.
        /// </summary>
        [Header("EVENTS")]
        public UnityEvent OnInteracted;

        /// <summary>
        /// Event fired when this object availability is changed.
        /// </summary>
        [Space]
        public UnityEvent<bool> OnAvailabilityChanged;

        /// <summary>
        /// Event fired when interacted with this object fails.
        /// </summary>
        [Space]
        public UnityEvent OnInteractionFailChanged;

        private void Reset() => Collider = GetComponent<Collider>();

        public bool CanInteract() => enabled;
        public void Interact() => OnInteracted?.Invoke();
        public void ShowInteractionFail() => OnInteractionFailChanged?.Invoke();
        public void ChangeAvailability(bool isAvailable) => OnAvailabilityChanged?.Invoke(isAvailable);
        public void EnterCollision(Transform interactor) => ChangeAvailability(true);
        public void ExitCollision(Transform interactor) => ChangeAvailability(false);
    }
}