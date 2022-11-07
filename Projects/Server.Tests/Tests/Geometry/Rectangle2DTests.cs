﻿using System;
using Xunit;

namespace Server.Tests;

public class Rectangle2DTests
{
    [Fact]
    public void TestRectangle2DToString()
    {
        const int max = int.MaxValue;
        const int min = int.MinValue;
        Assert.Equal("(0, 0)+(0, 0)", new Rectangle2D(0, 0, 0, 0).ToString());
        Assert.Equal("(1, 1)+(1, 1)", new Rectangle2D(1, 1, 1, 1).ToString());
        Assert.Equal($"({max}, {max})+({max}, {max})", new Rectangle2D(max, max, max, max).ToString());
        Assert.Equal($"({min}, {min})+({min}, {min})", new Rectangle2D(min, min, min, min).ToString());
    }

    [Fact]
    public void TestRectangle2DTryFormatSucceeds()
    {
        const int max = int.MaxValue;
        const int min = int.MinValue;
        char[] array = new char[128];

        var p1 = new Rectangle2D(0, 0, 0, 0);
        Assert.True(p1.TryFormat(array, out var cp1, null, null));
        Assert.Equal(9 + 4, cp1);
        Array.Clear(array);

        var p2 = new Rectangle2D(1, 1, 1, 1);
        Assert.True(p2.TryFormat(array, out var cp2, null, null));
        Assert.Equal(9 + 4, cp2);
        Array.Clear(array);

        var p3 = new Rectangle2D(max, max, max, max);
        Assert.True(p3.TryFormat(array, out var cp3, null, null));
        Assert.Equal(9 + 4 * 10, cp3);
        Array.Clear(array);

        var p4 = new Rectangle2D(min, min, min, min);
        Assert.True(p4.TryFormat(array, out var cp4, null, null));
        Assert.Equal(9 + 4 * 11, cp4);
    }

    [Fact]
    public void TestRectangle2DTryFormatFails()
    {
        const int max = int.MaxValue;
        const int min = int.MinValue;
        char[] array = new char[1];

        var p1 = new Rectangle2D(0, 0, 0, 0);
        Assert.False(p1.TryFormat(array, out var cp1, null, null));
        Assert.Equal(0, cp1);
        Array.Clear(array);

        var p2 = new Rectangle2D(1, 1, 1, 1);
        Assert.False(p2.TryFormat(array, out var cp2, null, null));
        Assert.Equal(0, cp2);
        Array.Clear(array);

        var p3 = new Rectangle2D(max, max, max, max);
        Assert.False(p3.TryFormat(array, out var cp3, null, null));
        Assert.Equal(0, cp3);
        Array.Clear(array);

        var p4 = new Rectangle2D(min, min, min, min);
        Assert.False(p4.TryFormat(array, out var cp4, null, null));
        Assert.Equal(0, cp4);
    }
}
