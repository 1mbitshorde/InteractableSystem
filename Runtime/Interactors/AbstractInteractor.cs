using System;
using System.Collections.Generic;
using UnityEngine;

namespace OneM.InteractableSystem
{
    /// <summary>
    /// Abstract interactor with <see cref="ICollisionable"/> interfaces.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractInteractor<T> : MonoBehaviour where T : Collider
    {
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        [SerializeField, Tooltip("The local Collider component.")]
        private T collider;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        [Tooltip("The max number of collisions.")]
        public uint MaxCollisions = 4;
        [Tooltip("Specifies whether should hit Triggers.")]
        public QueryTriggerInteraction TriggerInteraction = QueryTriggerInteraction.Collide;

        [Space]
        [Tooltip("The layers used to cast collisions.")]
        public LayerMask Collisions;

        public event Action<GameObject> OnCollisionEntered;

        public T Collider
        {
            get => collider;
            protected set => collider = value;
        }

        public Bounds Bounds => Collider.bounds;

        protected Collider[] buffer;
        private List<ICollisionable> collisionables;

        private void Reset() => SetupCollider();
        private void OnEnable() => Initialize();
        private void Update() => TryUpdateCollisions();

        public bool TryGetCollisionable<C>(int index, out C collisionable) where C : ICollisionable
        {
            var hasCollisionable = index < collisionables.Count;
            collisionable = (C)(hasCollisionable ? collisionables[index] : null);
            return hasCollisionable;
        }

        protected virtual void SetupCollider()
        {
            Collider = GetComponent<T>();
            Collider.isTrigger = true;
        }

        protected abstract int OverlapCollider(Bounds bounds);

        private bool IsGameRunning() => Time.timeScale > 0f;

        private void Initialize()
        {
            buffer = new Collider[MaxCollisions];
            collisionables = new List<ICollisionable>((int)MaxCollisions);
        }

        private void TryUpdateCollisions()
        {
            // Don't check collisions is game is paused
            if (IsGameRunning()) UpdateCollisions();
        }

        private void UpdateCollisions()
        {
            var hitCount = OverlapCollider(Bounds);
            CheckExitCollisions(hitCount);

            for (int i = 0; i < hitCount; i++)
            {
                var collider = buffer[i];
                if (collider == null) continue;

                var hasCollisionable = collider.transform.TryGetComponent(out ICollisionable collisionable);
                var invalidCollisionable = !hasCollisionable || !collisionable.CanCollide();
                if (invalidCollisionable) continue;

                var hasCollision = collisionables.Contains(collisionable);
                if (hasCollision) continue;

                collisionable.EnterCollision(transform);
                OnCollisionEntered?.Invoke(collisionable.gameObject);

                collisionables.Add(collisionable);
            }
        }

        private void CheckExitCollisions(int hitCount)
        {
            for (int i = collisionables.Count - 1; i >= 0; i--)
            {
                var item = collisionables[i];
                var collider = item.Collider;
                if (HasBufferCollider(collider, hitCount)) continue;

                item.ExitCollision(transform);
                collisionables.RemoveAt(i);
            }
        }

        private bool HasBufferCollider(Collider collider, int hitCount)
        {
            for (int i = 0; i < hitCount; i++)
            {
                if (buffer[i] == collider) return true;
            }
            return false;
        }
    }
}