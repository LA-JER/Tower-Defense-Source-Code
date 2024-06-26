using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float fadeSpeed = 1f;
    public float duration = 1.5f;
    public AnimationClip entryClip;
    public TextMeshProUGUI textMeshPro;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //StartCoroutine(FadeOut());
        Destroy(gameObject, duration);

    }
    public void Initialize(float damageAmount)
    {
        if (textMeshPro.text != null)
        {

            textMeshPro.text = NumberShorthand.FormatNumber(damageAmount);

        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        textMeshPro.alpha = Mathf.MoveTowards(textMeshPro.alpha, 0f, fadeSpeed * Time.deltaTime);
    }

    IEnumerator FadeOut()
    {
        
        if(entryClip != null )
        {
            yield return new WaitForSeconds(entryClip.length);
            if(animator != null)
            {
                animator.SetTrigger("Exit");
            }
        }
    }
}
