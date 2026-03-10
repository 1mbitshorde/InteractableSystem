using UnityEngine;

namespace OneM.InteractableSystem
{
    /// <summary>
    /// Interface used on objects able to have enter/exit collisions with an <see cref="AbstractInteractor{T}"/>.
    /// </summary>
    public interface ICollisionable
    {
        /// <summary>
        /// The local Collider.
        /// </summary>
        public Collider Collider { get; }

        /// <summary>
        /// Enters the collision using the given interactor.
        /// </summary>
        /// <param name="interactor">
        /// The interactor checking the collision.
        /// <para>
        /// It can be a Physics GameObject inside a Player or 
        /// other system checking its collision.
        /// </para>
        /// </param>
        void EnterCollision(Transform interactor);

        /// <summary>
        /// Exits the collision using the given interactor.
        /// </summary>
        /// <param name="interactor">
        /// <inheritdoc cref="EnterCollision(Transform)" path="/param[@name='interactor']"/>
        /// </param>
        void ExitCollision(Transform interactor);
    }
}