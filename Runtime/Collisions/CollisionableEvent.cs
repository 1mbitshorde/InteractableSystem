using System;
using UnityEngine;

namespace OneM.InteractableSystem
{
    /// <summary>
    /// Simple <see cref="ICollisionable"/> implementation using events.
    /// </summary>
    [DisallowMultipleComponent]
    public class CollisionableEvent : MonoBehaviour, ICollisionable
    {
        [field: SerializeField, Tooltip("The local Collider component. Can be any collider type.")]
        public Collider Collider { get; private set; }

        /// <summary>
        /// Event fired when entering the collision using the given interactor.
        /// </summary>
        public event Action<Transform> OnCollisionEntered;

        /// <summary>
        /// Event fired when exited the collision using the given interactor.
        /// </summary>
        public event Action<Transform> OnCollisionExited;

        protected virtual void Reset() => Collider = GetComponent<Collider>();

        public virtual bool CanCollide() => enabled;
        public virtual void EnterCollision(Transform interactor) => OnCollisionEntered?.Invoke(interactor);
        public virtual void ExitCollision(Transform interactor) => OnCollisionExited?.Invoke(interactor);
    }
}