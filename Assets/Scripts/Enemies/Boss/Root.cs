using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

class Root : MonoBehaviour
{
    [SerializeField] private float attackDelay;
    [SerializeField] private float existenceTime;
    [SerializeField] private GameObject spotlight;
    private Light2D spotlightLight;
    private CircleCollider2D selfCollider;
    private Animator animator;
    private DamageDealer damageDealer;
    protected void Awake()
    {
        damageDealer = GetComponent<DamageDealer>();
        damageDealer.Friends.AddRange(new string[] { "Enemy", "Spawner", "Boss" });
        damageDealer.Active = false;
        selfCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        spotlightLight = spotlight.GetComponent<Light2D>();
        spotlightLight.enabled = false;
    }

    protected void Start()
    {
        selfCollider.enabled = false;
        StartCoroutine(Attack());
    }

    void Hide()
    {
        Destroy(this.gameObject);
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDelay);
        // Debug.Log("Root started attack");
        animator.SetTrigger("StartAttack");
        spotlightLight.enabled = true;
        yield return new WaitForSeconds(0.4f);
        //yield return WaitForAnimation("StartAttack");
        selfCollider.enabled = true;
        damageDealer.Active = true;
        yield return new WaitForSeconds(existenceTime);
        animator.SetTrigger("EndAttack");
        yield return new WaitForSeconds(0.4f);
        selfCollider.enabled = false;
        //yield return WaitForAnimation("EndAttack");
        Destroy(this.gameObject);
    }

    IEnumerator WaitForAnimation(string animationName)
    {
        int animationHash = Animator.StringToHash(animationName);

        yield return new WaitUntil(() =>
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.normalizedTime >= 1 && stateInfo.shortNameHash == animationHash;
        });
    }
}