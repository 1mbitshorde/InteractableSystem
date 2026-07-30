using UnityEngine;

namespace OneM.InteractableSystem
{
    /// <summary>
    /// Sphere interactor with <see cref="ICollisionable"/> interfaces.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SphereCollider))]
    public sealed class SphereInteractor : AbstractInteractor
    {
        [SerializeField] private SphereCollider sphereCollider;

        protected override void FindCollider() => sphereCollider = GetComponent<SphereCollider>();
        protected override Collider GetCollider() => sphereCollider;

        protected override int GetHitCount()
        {
            var bounds = GetCollider().bounds;
            return Physics.OverlapSphereNonAlloc(
                bounds.center,
                GetWorldRadius(sphereCollider),
                buffer,
                Collisions,
                TriggerInteraction
            );
        }

        private static float GetWorldRadius(SphereCollider sphere)
        {
            var scale = sphere.transform.lossyScale;
            var maxScale = Mathf.Max(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z));
            return sphere.radius * maxScale;
        }
    }
}