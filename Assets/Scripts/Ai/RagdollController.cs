using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Rigidbody[] _rigidbodies;

    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _anim = GetComponent<Animator>();
        DeactivateRagdoll();
    }

    public void DeactivateRagdoll()
    {
        foreach (Rigidbody rb in _rigidbodies)
        {
            rb.isKinematic = true;
        }
        _anim.enabled = true;
    }

    public void ActivateRagdoll()
    {
        foreach (Rigidbody rb in _rigidbodies)
        {
            rb.isKinematic = false;
        }

        _anim.enabled = false;
    }

    public void ApplyForce(Vector3 force)
    {
        var rigidbody = _anim.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigidbody.AddForce(force, ForceMode.VelocityChange);
    }
}
