using UnityEngine;
using YG;

public class Pannel : MonoBehaviour
{
    protected void OnEnable()
    {
        YG2.StickyAdActivity(true);
    }
}