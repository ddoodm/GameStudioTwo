using UnityEngine;
using System.Collections;

public class VehicleAudioManager : MonoBehaviour
{
    public AudioSource vehicleSource;

    public AudioClip
        impactClip,
        clangClip;

    void OnCollisionEnter(Collision collision)
    {
        float hitForce = collision.impulse.magnitude;

        if (hitForce >= 50.0f)
            vehicleSource.PlayOneShot(impactClip, Mathf.Min(2.0f, hitForce / 500.0f));

        if (hitForce >= 400.0f)
            vehicleSource.PlayOneShot(clangClip, Mathf.Min(2.0f, hitForce / 300.0f));
    }
}
