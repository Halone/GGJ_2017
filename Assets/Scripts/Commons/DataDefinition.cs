public delegate void DoAction();

public struct Point {
    public float x;
    public float z;

    public Point(float p_X, float p_Z) {
        x = p_X;
        z = p_Z;
    }

    public void Set(float p_X, float p_Z) {
        x = p_X;
        z = p_Z;
    }

    public bool IsEqualTo(Point p_Point) {
        return (x == p_Point.x && z == p_Point.z);
    }
}