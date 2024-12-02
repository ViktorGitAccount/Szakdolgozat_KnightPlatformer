using UnityEngine;

public class HiddenWall : MonoBehaviour
{
    public void DestroyWall()
    {
        Destroy(gameObject, 1f);
    }
}
