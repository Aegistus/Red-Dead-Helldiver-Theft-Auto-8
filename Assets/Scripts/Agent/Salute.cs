using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salute : MonoBehaviour
{
    [SerializeField] bool saluteOnStart = false;

    Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        if (saluteOnStart)
        {
            StartCoroutine(Delay());
            print("TEST");
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        DoSalute();
    }

    public void DoSalute()
    {
        anim.Play("Armature|Salute");
    }
}
