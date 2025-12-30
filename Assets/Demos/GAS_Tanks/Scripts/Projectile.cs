using UnityEngine;
using WYGAS.SO;

namespace Demos.GASTanks.Scripts
{
    public class Projectile : MonoBehaviour
    {
        public float destroyAfter = 2f;
        public Rigidbody rigidBody;
        public float force = 1000f;
        public GameplayEffectDefinition hitEffect;

        // set velocity for server and client. this way we don't have to sync the
        // position, because both the server and the client simulate it.
        void Start()
        {
            rigidBody.AddForce(transform.forward * force);
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("Hit: " + other.name);
            if (other.transform.parent.TryGetComponent(out Tank tank))
            {
                tank.asc.ApplyGameplayEffect(hitEffect.CreateSpecInternal());
                Destroy(gameObject);
            }
        }
    }
}
