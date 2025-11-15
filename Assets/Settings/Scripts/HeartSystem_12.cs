using UnityEngine;

public class HeartSystem : MonoBehaviour
{
    

    public void HeartGone (GameObject target)
    {
        target.SetActive(false);
    }
}
