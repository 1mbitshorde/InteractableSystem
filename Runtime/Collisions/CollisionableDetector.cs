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
            var hasCollisionable = AbstractInteractor.TryGetCollidingComponent(instance, out ICollisionable collisionable);
            var validCollisionable = hasCollisionable && collisionable.CanCollide();
            if (validCollisionable) collisionable.EnterCollision(transform);
        }

        private void HandleCollisionExited(GameObject instance)
        {
            if (instance == null) return; // The instance may be destroyed during gameplay

            var hasCollisionable = AbstractInteractor.TryGetCollidingComponent(instance, out ICollisionable collisionable);
            if (hasCollisionable) collisionable.ExitCollision(transform);
        }
    }
}