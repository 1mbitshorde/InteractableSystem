using UnityEngine;

namespace OneM.InteractableSystem
{
    /// <summary>
    /// Sphere interactor with <see cref="ICollisionable"/> interfaces.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider))]
    public sealed class BoxInteractor : AbstractInteractor<BoxCollider>
    {
        protected override int GetOverlappedHits(Bounds bounds) => Physics.OverlapBoxNonAlloc(
            bounds.center,
            bounds.extents,
            buffer,
            transform.rotation,
            Collisions,
            TriggerInteraction
        );
    }
}