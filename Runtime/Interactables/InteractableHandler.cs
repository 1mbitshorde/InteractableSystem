using System;
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
        [field: SerializeField, Tooltip("The interaction name to use when filtering interactions.")]
        public string InteractionName { get; set; } = "Interact";

        [Header("EVENTS")]
        [Tooltip("Event fired when this object availability is changed. Use it to show the Interaction Input.")]
        public UnityEvent<bool> OnAvailabilityChanged;
        [Tooltip("Event fired when interacted with this object and it success.")]
        public UnityEvent<Transform> OnInteracted;
        [Tooltip("Event fired when interacted with this object and it fails.")]
        public UnityEvent OnInteractionFailed;

        /// <summary>
        /// Event fired when any object availability is changed. 
        /// Use it to show the Interaction Input based on the Interaction Name or the GameObject instance.
        /// </summary>
        public static event Action<bool, string, GameObject> OnAnyAvailabilityChanged;

        public bool CanInteract() => enabled;
        public bool CanCollide() => CanInteract();
        public void Interact(Transform interactor) => OnInteracted?.Invoke(interactor);
        public void ShowInteractionFail() => OnInteractionFailed?.Invoke();
        public void EnterCollision(Transform _) => ChangeAvailability(true);
        public void ExitCollision(Transform _) => ChangeAvailability(false);

        public void ChangeAvailability(bool isAvailable)
        {
            OnAvailabilityChanged?.Invoke(isAvailable);
            OnAnyAvailabilityChanged?.Invoke(isAvailable, InteractionName, gameObject);
        }
    }
}