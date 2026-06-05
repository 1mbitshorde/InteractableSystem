using UnityEngine;

namespace OneM.InteractableSystem
{
    /// <summary>
    /// Interface used on objects able to be interacted with.
    /// </summary>
    public interface IInteractable : ICollisionable
    {
        /// <summary>
        /// Whether can interact with this object.
        /// </summary>
        /// <returns>True if can interact with this object. False otherwise.</returns>
        bool CanInteract();

        /// <summary>
        /// Tries to interact with this object if possible. Shows an interaction fail if not.
        /// </summary>
        /// <param name="interactor">The object interacting.</param>
        void TryInteract(Transform interactor)
        {
            if (CanInteract()) Interact(interactor);
            else ShowInteractionFail();
        }

        /// <summary>
        /// Interacts with this object.
        /// </summary>
        /// <param name="interactor">The object interacting</param>
        void Interact(Transform interactor);

        /// <summary>
        /// Shows an interaction fail message.
        /// </summary>
        void ShowInteractionFail();

        /// <summary>
        /// Change this object availability to be interacted.
        /// </summary>
        /// <param name="isAvailable">Whether is available.</param>
        void ChangeAvailability(bool isAvailable);
    }
}