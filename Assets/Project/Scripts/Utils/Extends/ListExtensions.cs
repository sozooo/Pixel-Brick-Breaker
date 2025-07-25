using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Utils.Extends
{
    public static class ListExtensions
    {
        public static T GetRandom<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
    }
}