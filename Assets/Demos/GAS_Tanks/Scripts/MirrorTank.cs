using System.Collections.Generic;
using Demo.Common;
using Mirror;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using WYGAS;
using WYGAS.SO;

namespace Demos.GASTanks.Scripts
{
    public class MirrorTank : NetworkBehaviour
    {
        private Vector2 move;

        [Header("Components")] public NavMeshAgent agent;
        public Animator animator;
        public TextMesh healthBar;
        public Transform turret;

        [Header("Movement")] public float rotationSpeed = 100;

        [Header("Firing")] public GameObject projectilePrefab;
        public Transform projectileMount;
        public GameplayEffectDefinition defaultAttributes;
        public List<GameplayAbilityDefinition> defaultAbilities;

        public AbilitySystemComponentMirror asc;
        private PlayerInput playerInput;

        void Awake()
        {
            asc = GetComponent<AbilitySystemComponentMirror>();

            playerInput = GetComponent<PlayerInput>();
            // 接管所有 Action
            if (playerInput != null)
            {
                playerInput.onActionTriggered += OnActionTriggered;
                Debug.Log("Current Action Map: " + playerInput.currentActionMap.name);
            }
        }

        void Start()
        {
            InitializeDefaultAttributes();
            InitializeDefaultAbilities();
        }

        public void OnActionTriggered(InputAction.CallbackContext ctx)
        {
            if (ctx.action.name == "Move")
            {
                move = ctx.ReadValue<Vector2>();
            }

            var abilityTag = InputActionToTagRegistry.Get(ctx.action.name);
            if (abilityTag.Equals(GameplayTag.EmptyTag)) return;

            if (ctx.started)
            {
                asc.GetAbilities().ForEach(spec =>
                {
                    if (spec.abilityTags.MatchTag(abilityTag))
                    {
                        asc.TryActivateAbility(spec);
                    }
                });
            }
            else if (ctx.canceled)
            {
                asc.CancelAbilitiesByTag(abilityTag);
            }
        }

        void Update()
        {
            // always update health bar.
            // (SyncVar hook would only update on clients, not on server)
            healthBar.text = new string('-', (int)asc.GetAttributeValue("ATTR_Health"));

            // take input from focused window only
            if (!Application.isFocused) return;

            // rotate
            float horizontal = move.x;
            transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);

            // move
            float vertical = move.y;
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            agent.velocity = forward * Mathf.Max(vertical, 0) * agent.speed;

            animator.SetBool("Moving", agent.velocity != Vector3.zero);

            RotateTurret();
        }

        void InitializeDefaultAttributes()
        {
            asc.ApplyGameplayEffect(defaultAttributes.CreateSpecInternal());
        }

        void InitializeDefaultAbilities()
        {
            defaultAbilities.ForEach(abilityDef =>
            {
                Debug.Log($"Grant Ability: {abilityDef.GetType().Name}");
                asc.GrantAbility(abilityDef.CreateSpecInternal());
            });
        }

        void RotateTurret()
        {
            var mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point);
                Vector3 lookRotation = new Vector3(hit.point.x, turret.transform.position.y, hit.point.z);
                turret.transform.LookAt(lookRotation);
            }
        }
    }
}