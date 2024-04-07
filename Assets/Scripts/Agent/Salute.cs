using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salute : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void DoSalute()
    {
        anim.Play("Armature|Salute");
    }
}
