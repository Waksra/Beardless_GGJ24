using System;
using System.Collections;
using System.Xml.Serialization;
using Combat;
using General;
using Pathfinding;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public enum AttackStopType
    {
        AttackInitiated,
        AttackHit,
        AttackEnd
    }
    
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float knockoutTime = 2f;
        [SerializeField] private float attackCooldown = 2f;
        [SerializeField] private Transform rigRoot;
        
        [SerializeField] private AttackStopType attackStopType = AttackStopType.AttackInitiated;

        [SerializeField, FoldoutGroup("Events")]
        private UnityEvent onAttack;

        private Animator animator;

        private Rigidbody body;
        private new Collider collider;

        private RagdollController ragdollController;
        private RichAI aiPather;

        private EnemyState currentState = EnemyState.Normal;

        private static readonly int MoveSpeedAnim = Animator.StringToHash("MoveSpeed");
        private static readonly int AttackAnim = Animator.StringToHash("Attack");
        private static readonly int AttackOnCooldownAnim = Animator.StringToHash("AttackOnCooldown");

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();

            body = GetComponent<Rigidbody>();
            collider = GetComponent<Collider>();

            ragdollController = GetComponentInChildren<RagdollController>();
            aiPather = GetComponent<RichAI>();
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void Update()
        {
            if (aiPather.reachedDestination)
            {
                InitiateAttack();
            }

            if (currentState == EnemyState.Attacking)
            {
                Vector3 direction = (aiPather.destination - transform.position);
                direction.y = 0;
                direction.Normalize();

                // aiPather.SimulateRotationTowards(direction, 360 / Time.deltaTime);
                
                Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
                body.MoveRotation(rotation);
            }
            
            if (transform.position.y < -10)
                Destroy(gameObject);
        }

        private void LateUpdate()
        {
            float moveSpeed = aiPather.velocity.magnitude;
            float normalizedSpeed = Mathf.Clamp01(moveSpeed / aiPather.maxSpeed);

            animator.SetFloat(MoveSpeedAnim, normalizedSpeed);
        }

        private void InitiateAttack()
        {
            if (currentState == EnemyState.Attacking)
                return;

            if (attackStopType == AttackStopType.AttackInitiated)
                aiPather.canMove = false;
            
            animator.SetBool(AttackOnCooldownAnim, true);
            currentState = EnemyState.Attacking;
            animator.SetTrigger(AttackAnim);
        }

        public void AttackResponse()
        {
            if (attackStopType == AttackStopType.AttackHit)
                aiPather.canMove = false;
            
            onAttack?.Invoke();
        }

        public void AttackEndResponse()
        {
            if (attackStopType == AttackStopType.AttackEnd)
                aiPather.canMove = false;
            
            StartCoroutine(AttackCooldown());
        }

        public void HitResponse(HitData hitData)
        {
            animator.enabled = false;

            body.isKinematic = true;
            collider.enabled = false;

            ragdollController.HitResponse(hitData);

            aiPather.enabled = false;

            StartCoroutine(KnockoutTimer());
        }

        private void AlignToRoot()
        {
            Vector3 rootPosition = rigRoot.position;
            transform.position = rootPosition;

            rigRoot.position = rootPosition;
        }

        private IEnumerator KnockoutTimer()
        {
            yield return new WaitForSeconds(knockoutTime);

            ragdollController.Recover();
            AlignToRoot();

            animator.enabled = true;

            body.isKinematic = false;
            collider.enabled = true;

            aiPather.enabled = true;
        }
        
        private IEnumerator AttackCooldown()
        {
            yield return new WaitForSeconds(attackCooldown);
            
            aiPather.canMove = true;
            currentState = EnemyState.Normal;
            animator.SetBool(AttackOnCooldownAnim, false);
        }
    }
}