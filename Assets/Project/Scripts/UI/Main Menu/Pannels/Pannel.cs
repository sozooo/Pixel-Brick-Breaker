using UnityEngine;
using YG;

public class Pannel : MonoBehaviour
{
    protected void OnEnable()
    {
        Display();
    }

    protected virtual void Display()
    {
        YG2.StickyAdActivity(true);
    }
}