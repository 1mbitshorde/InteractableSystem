using UnityEngine;

namespace OneM.InteractableSystem
{
    /// <summary>
    /// Sphere interactor with <see cref="ICollisionable"/> interfaces.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SphereCollider))]
    public sealed class SphereInteractor : AbstractInteractor<SphereCollider>
    {
        protected override int GetOverlappedHits(Bounds bounds) => Physics.OverlapSphereNonAlloc(
            bounds.center,
            Collider.radius,
            buffer,
            Collisions,
            TriggerInteraction
        );
    }
}