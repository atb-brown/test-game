using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
                // TODO: Add the average dimensional size to the main object.
                //     (x + y + z) / 3
                // UnityEngine.Vector3 addedSize = new Vector3(1.0f, 1.0f, 1.0f);
                UnityEngine.Vector3 incrementingSize = calcIncrementingSize(collision);
                gameObject.transform.localScale += incrementingSize;
                size += incrementingSize;
            }
        }
    }

    bool isSmallEnoughToGrab(Collision pickup)
    {
        UnityEngine.Vector3 pickupSize = pickup.collider.bounds.size;

        return (
            isSmallEnoughRatio(pickupSize.x, size.x)
            && isSmallEnoughRatio(pickupSize.y, size.y)
            && isSmallEnoughRatio(pickupSize.z, size.z)
        );
    }

    static bool isSmallEnoughRatio(float pickupAxisSize, float objectAxisSize)
    {
        return ((pickupAxisSize * 4.0) < objectAxisSize);
    }

    static UnityEngine.Vector3 calcIncrementingSize(Collision pickup) {
        UnityEngine.Vector3 pickupSize = pickup.collider.bounds.size;
        float avgSize2 = Queryable.Average((new List<float> { pickupSize.x, pickupSize.y, pickupSize.z }).AsQueryable()) / 3.14f;
        return new UnityEngine.Vector3(avgSize2, avgSize2, avgSize2);
    }
}
