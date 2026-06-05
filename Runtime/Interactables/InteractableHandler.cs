using UnityEngine;
using UnityEngine.Events;

namespace OneM.InteractableSystem
{
    /// <summary>
    /// Low coupling implementation of <see cref="IInteractable"/> using serialized Unity Events.
    /// </summary>
    /// <remarks>
    /// Use this component to quickly create an Interactable instance reacting when 
    /// <see cref="AbstractInteractor{T}"/> implementations detect collisions.
    /// </remarks>
    [DisallowMultipleComponent]
    public sealed class InteractableHandler : MonoBehaviour, IInteractable
    {
        [field: SerializeField]
        public Collider Collider { get; private set; }

        [Header("EVENTS")]
        [Tooltip("Event fired when this object availability is changed. Use it to show the Interaction Input.")]
        public UnityEvent<bool> OnAvailabilityChanged;
        [Tooltip("Event fired when interacted with this object and it success.")]
        public UnityEvent<Transform> OnInteracted;
        [Tooltip("Event fired when interacted with this object and it fails.")]
        public UnityEvent OnInteractionFailed;

        public bool CanInteract() => enabled;
        public bool CanCollide() => CanInteract();
        public void Interact(Transform interactor) => OnInteracted?.Invoke(interactor);
        public void ShowInteractionFail() => OnInteractionFailed?.Invoke();
        public void ChangeAvailability(bool isAvailable) => OnAvailabilityChanged?.Invoke(isAvailable);
        public void EnterCollision(Transform _) => ChangeAvailability(true);
        public void ExitCollision(Transform _) => ChangeAvailability(false);
    }
}