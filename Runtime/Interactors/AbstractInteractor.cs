using System;
using UnityEngine;

namespace OneM.InteractableSystem
{
    public abstract class AbstractInteractor : MonoBehaviour
    {
        [Tooltip("Specifies whether should hit Triggers.")]
        public QueryTriggerInteraction TriggerInteraction = QueryTriggerInteraction.Collide;
        [Tooltip("The layers used to cast collisions.")]
        public LayerMask Collisions;

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

        /// <summary>
        /// The Colliding instance. Can be null.
        /// </summary>
        public GameObject CollidingInstance { get; private set; }

        private bool wasCollision;
        private const uint maxCollisions = 1;
        protected readonly Collider[] buffer = new Collider[maxCollisions];

        private void Reset()
        {
            FindCollider();
            SetupCollider();
        }

        private void Update() => TryUpdateCollisions();

        public T GetCollidingComponent<T>() => CollidingInstance ? CollidingInstance.GetComponent<T>() : default;

        public bool TryGetCollidingComponent<T>(out T component)
        {
            component = GetCollidingComponent<T>();
            return component != null;
        }

        protected abstract int GetHitCount();
        protected abstract void FindCollider();
        protected abstract Collider GetCollider();

        public static bool IsGameRunning() => Time.timeScale > 0f;

        private void SetupCollider() => GetCollider().isTrigger = true;

        private void TryUpdateCollisions()
        {
            // Don't check collisions if game is paused
            if (IsGameRunning()) UpdateCollisions();
        }

        private void UpdateCollisions()
        {
            var hitCount = GetHitCount();
            var hasCollision = hitCount > 0;
            var hasEnterCollision = hasCollision && !wasCollision;
            var hasExitCollision = !hasCollision && wasCollision;

            if (hasEnterCollision) EnterCollision();
            if (hasExitCollision) ExitCollision();

            wasCollision = hasCollision;
        }

        private void EnterCollision()
        {
            CollidingInstance = buffer[0].gameObject;
            OnCollisionEntered?.Invoke(CollidingInstance);
            OnCollisionToggled?.Invoke(CollidingInstance, true);
        }

        private void ExitCollision()
        {
            OnCollisionExited?.Invoke(CollidingInstance);
            OnCollisionToggled?.Invoke(CollidingInstance, false);
            CollidingInstance = null;
        }
    }
}