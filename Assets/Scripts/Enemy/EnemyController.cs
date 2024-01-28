using System;
using System.Collections;
using Combat;
using General;
using Pathfinding;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float knockoutTime = 2f;
        
        private Animator animator;

        private Rigidbody body;
        private new Collider collider;

        private RagdollController ragdollController;
        private RichAI aiPather;

        [SerializeField]
        private Transform rigRoot;
        
        private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");

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
        }

        private void LateUpdate()
        {
            float moveSpeed = aiPather.velocity.magnitude;
            float normalizedSpeed = Mathf.Clamp01(moveSpeed / aiPather.maxSpeed);
            
            animator.SetFloat(MoveSpeed, normalizedSpeed);
        }

        private void InitiateAttack()
        {
            
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
    }
}