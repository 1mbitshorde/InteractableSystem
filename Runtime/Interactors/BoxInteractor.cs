using UnityEngine;

namespace OneM.InteractableSystem
{
    /// <summary>
    /// Box interactor with <see cref="ICollisionable"/> interfaces.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider))]
    public sealed class BoxInteractor : AbstractInteractor<BoxCollider>
    {
        protected override int OverlapCollider(Bounds bounds) => Physics.OverlapBoxNonAlloc(
            bounds.center,
            bounds.extents,
            buffer,
            transform.rotation,
            Collisions,
            TriggerInteraction
        );
    }
}