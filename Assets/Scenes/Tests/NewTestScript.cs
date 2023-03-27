using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Unity.PerformanceTesting;
using UnityEngine.TestTools;

public class NewTestScript
{
    [Test, Performance]
    public void Test()
    {
        Measure.Method(() =>
        {
            Vector2.MoveTowards(Vector2.one, Vector2.zero, 0.5f);
        }).Run();
    }

}
