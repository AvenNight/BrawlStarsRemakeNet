using UnityEngine;

public class GrassInvisible : MonoBehaviour
{
    //private void OnTriggerEnter(Collider other)
    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent(out IInvisible invisibleObj))
            return;
        invisibleObj.SetInvisible(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out IInvisible invisibleObj))
            return;
        invisibleObj.SetInvisible(false);
    }
}