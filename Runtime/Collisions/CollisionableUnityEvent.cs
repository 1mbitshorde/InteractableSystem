using UnityEngine;
using UnityEngine.Events;

namespace OneM.InteractableSystem
{
    /// <summary>
    /// Low coupling implementation of <see cref="ICollisionable"/> using <see cref="UnityEvent"/>.
    /// </summary>
    /// <remarks>
    /// Use this component to quickly create a Collisionable instances reacting when 
    /// <see cref="AbstractInteractor{T}"/> implementations detect collisions.
    /// </remarks>
    [DisallowMultipleComponent]
    public sealed class CollisionableUnityEvent : MonoBehaviour, ICollisionable
    {
        [field: SerializeField, Tooltip("The local Collider component. Can be any collider type.")]
        public Collider Collider { get; private set; }

        /// <summary>
        /// Event fired when entering the collision using the given interactor.
        /// </summary>
        public UnityEvent<Transform> OnCollisionEntered;

        /// <summary>
        /// Event fired when exited the collision using the given interactor.
        /// </summary>
        public UnityEvent<Transform> OnCollisionExited;

        private void Reset() => Collider = GetComponent<Collider>();

        public void EnterCollision(Transform interactor) => OnCollisionEntered?.Invoke(interactor);
        public void ExitCollision(Transform interactor) => OnCollisionExited?.Invoke(interactor);
    }
}