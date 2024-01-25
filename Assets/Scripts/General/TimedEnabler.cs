using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace General
{
    public class TimedEnabler : MonoBehaviour
    {
        [SerializeField] private float timeToEnable = 1f;
        
        [SerializeField] private UnityEvent onEnable;
        
        private float timer;
        
        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void EnableForTime()
        {
            onEnable?.Invoke();
            
            timer = timeToEnable;
            gameObject.SetActive(true);
            
            StartCoroutine(EnableTimer());
        }
        
        private IEnumerator EnableTimer()
        {
            yield return new WaitForSeconds(timeToEnable);
            gameObject.SetActive(false);
        }
    }
}