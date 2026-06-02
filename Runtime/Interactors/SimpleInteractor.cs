using System;
using UnityEngine;

namespace OneM.InteractableSystem
{
    /// <summary>
    /// Simple interactor using a BoxCollider to detect collisions with just one Collider at a time.
    /// </summary>
    /// <remarks>
    /// Use <see cref="OnCollisionEntered"/> and <see cref="OnCollisionExited"/> to handle collision events.
    /// </remarks>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider))]
    public sealed class SimpleInteractor : MonoBehaviour
    {
        [SerializeField] private BoxCollider boxCollider;
        [Tooltip("Specifies whether should hit Triggers.")]
        public QueryTriggerInteraction TriggerInteraction = QueryTriggerInteraction.Collide;

        [Space]
        [Tooltip("The layers used to cast collisions.")]
        public LayerMask Collisions;

        public Bounds Bounds => boxCollider.bounds;

        /// <summary>
        /// Event fired when entering a collision with a GameObject.
        /// </summary>
        public event Action<GameObject> OnCollisionEntered;

        /// <summary>
        /// Event fired when exiting a collision with a GameObject.
        /// </summary>
        public event Action<GameObject> OnCollisionExited;

        /// <summary>
        /// Event fired when toggling a collision with a GameObject, providing the collision state.
        /// </summary>
        public event Action<GameObject, bool> OnCollisionToggled;

        private bool wasCollision;

        private void Reset() => SetupCollider();
        private void OnEnable() => wasCollision = false;
        private void Update() => TryUpdateCollisions();

        private void SetupCollider()
        {
            boxCollider = GetComponent<BoxCollider>();
            boxCollider.isTrigger = true;
        }

        private void TryUpdateCollisions()
        {
            // Don't check collisions is game is paused
            if (BoxInteractor.IsGameRunning()) UpdateCollisions();
        }

        private void UpdateCollisions()
        {
            var bounds = Bounds;
            var isCollision = Physics.CheckBox(
                bounds.center,
                bounds.extents,
                transform.rotation,
                Collisions,
                TriggerInteraction
            );
            var hasEnterCollision = isCollision && !wasCollision;
            if (hasEnterCollision)
            {
                OnCollisionEntered?.Invoke(gameObject);
                OnCollisionToggled?.Invoke(gameObject, true);
            }

            var hasExitCollision = !isCollision && wasCollision;
            if (hasExitCollision)
            {
                OnCollisionExited?.Invoke(gameObject);
                OnCollisionToggled?.Invoke(gameObject, false);
            }

            wasCollision = isCollision;
        }
    }
}