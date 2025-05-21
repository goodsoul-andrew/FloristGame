using System.Collections;
using UnityEngine;

class Root : MonoBehaviour
{
    [SerializeField] private float attackDelay;
    [SerializeField] private float existenceTime;
    private CircleCollider2D selfCollider;
    private bool isAttackStarted;
    private Health hp;
    protected void Awake()
    {
        hp = GetComponent<Health>();
        selfCollider = GetComponent<CircleCollider2D>();
    }

    protected void Start()
    {
        selfCollider.enabled = false;
        StartCoroutine(StartAttack());
    }

    void Hide()
    {
        Destroy(this.gameObject);
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        Debug.Log("Root started attack");
        selfCollider.enabled = true;
    }

}