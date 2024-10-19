using UnityEngine;

public static class AxisConversion
{
    public static int AxisToInt(Axis axis) => (int)axis;
    
    public static Vector3 AxisToVector3(Axis axis)
    {
        Vector3 AxisAsVector = Vector3.zero;
        AxisAsVector[(int)axis] = 1;
        return AxisAsVector;
    }
}