using UnityEngine;

namespace OneM.InteractableSystem
{
    /// <summary>
    /// Box interactor with <see cref="ICollisionable"/> interfaces.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider))]
    public sealed class BoxInteractor : AbstractInteractor
    {
        [SerializeField] private BoxCollider boxCollider;

        protected override void FindCollider() => boxCollider = GetComponent<BoxCollider>();
        protected override Collider GetCollider() => boxCollider;

        protected override int GetHitCount()
        {
            var bounds = GetCollider().bounds;
            return Physics.OverlapBoxNonAlloc(
                bounds.center,
                bounds.extents,
                buffer,
                transform.rotation,
                Collisions,
                TriggerInteraction
            );
        }
    }
}