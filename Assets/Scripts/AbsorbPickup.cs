using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AbsorbPickup : MonoBehaviour
{
    private UnityEngine.Vector3 size;

    // Start is called before the first frame update
    void Start()
    {
        size = GetComponent<Rigidbody>().GetComponent<Collider>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if ("Pickup" == collision.gameObject.tag) {
            if (isSmallEnoughToGrab(collision)) {
                Destroy(collision.gameObject);
                UnityEngine.Vector3 incrementingSize = calcIncrementingSize(collision);
                gameObject.transform.localScale += incrementingSize;
                size += incrementingSize;
            }
        }
    }

    bool isSmallEnoughToGrab(Collision pickup)
    {
        UnityEngine.Vector3 pickupSize = pickup.collider.bounds.size;

        float pickupVolume = pickupSize.x * pickupSize.y * pickupSize.z;
        float cubicRootOfPickupVol = cubicRoot(pickupSize);
        float pickupSizeRatio = cubicRootOfPickupVol / size.x;

        return (pickupSizeRatio < 0.25f);
    }

    static bool isSmallEnoughRatio(float pickupAxisSize, float objectAxisSize)
    {
        return ((pickupAxisSize * 4.0) < objectAxisSize);
    }

    static UnityEngine.Vector3 calcIncrementingSize(Collision pickup) {
        float addedSize = cubicRoot(pickup.collider.bounds.size) / 3.14f;
        return new UnityEngine.Vector3(addedSize, addedSize, addedSize);
    }

    static float cubicRoot(UnityEngine.Vector3 size) {
        // TODO: This needs to be fixed; I think this is using the "global" volume of the object because the object's orientation is effecting its volume calculation.
        float volume = size.x * size.y * size.z;
        return (float) Math.Pow(volume, (1.0f/3.0f));
    }
}
