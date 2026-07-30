using UnityEngine;

namespace OneM.InteractableSystem
{
    /// <summary>
    /// Detector for valid Collisionable instances.
    /// </summary>
    /// <remarks>
    /// It'll call EnterCollision/ExitCollision in any colliding 
    /// component implement the <see cref="ICollisionable"/> interface.
    /// </remarks>
    [DisallowMultipleComponent]
    public sealed class CollisionableDetector : MonoBehaviour
    {
        [SerializeField] private AbstractInteractor interactor;

        private void Reset() => interactor = GetComponent<AbstractInteractor>();
        private void OnEnable() => SubscribeEvents();
        private void OnDisable() => UnsubscribeEvents();

        private void SubscribeEvents()
        {
            interactor.OnCollisionEntered += HandleCollisionEntered;
            interactor.OnCollisionExited += HandleCollisionExited;
        }

        private void UnsubscribeEvents()
        {
            interactor.OnCollisionEntered -= HandleCollisionEntered;
            interactor.OnCollisionExited -= HandleCollisionExited;
        }

        private void HandleCollisionEntered(GameObject instance)
        {
            var hasCollisionable = TryGetCollisionable(instance, out var collisionable);
            var validCollisionable = hasCollisionable && collisionable.CanCollide();
            if (validCollisionable) collisionable.EnterCollision(transform);
        }

        private void HandleCollisionExited(GameObject instance)
        {
            // Instance may be deleted by Gameplay
            if (instance == null) return;

            var hasCollisionable = TryGetCollisionable(instance, out var collisionable);
            if (hasCollisionable) collisionable.ExitCollision(transform);
        }

        private static bool TryGetCollisionable(GameObject instance, out ICollisionable collisionable)
        {
            collisionable = instance.GetComponentInChildren<ICollisionable>();
            if (collisionable != null) return true;

            collisionable = instance.GetComponentInParent<ICollisionable>();
            return collisionable != null;
        }
    }
}