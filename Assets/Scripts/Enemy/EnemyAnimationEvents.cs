using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyAnimationEvents : MonoBehaviour
    {
        [SerializeField] private UnityEvent onAttack;
        [SerializeField] private UnityEvent onAttackEnd;

        public void Attack()
        {
            onAttack?.Invoke();
        }
        
        public void AttackEnd()
        { 
            onAttackEnd?.Invoke();
        }
    }
}
