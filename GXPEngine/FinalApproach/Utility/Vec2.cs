using System;

public struct Vec2
{
    static Random _random = new Random();


    public float x;
    public float y;

    public Vec2(float pX = 0, float pY = 0)
    {
        x = pX;
        y = pY;
    }


    public static bool Delta(float value, float realValue, float acceptableDelta) {
        return (Math.Abs(value - realValue) <= acceptableDelta);
    }

    public static Vec2 operator +(Vec2 left, Vec2 right) {
        return new Vec2(left.x + right.x, left.y + right.y);
    }
    public static Vec2 operator -(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x - right.x, left.y - right.y);
    }
    public static Vec2 operator *(float left, Vec2 right)
    {
        return new Vec2(left * right.x, left * right.y);
    }
    public static Vec2 operator *(Vec2 right, float left)
    {
        return new Vec2(left * right.x, left * right.y);
    }

    public float Dot( Vec2 vec2) {
        return (x * vec2.x) + (y * vec2.y);
    }

    public static float Deg2Rad(float degrees) {
        return (float)(degrees/180.0f * Math.PI);
    }
    public static float Rad2Degree(float radians)
    {
        return (radians/ (float)Math.PI *180.0f);
    }


    public Vec2 Normal() {
        Vec2 normalized = this.Normalized();
        return new Vec2(-normalized.y, normalized.x);
    }


    public void Reflect(Vec2 normalVec, float coefBounciness)
    {
        Vec2 reflectedVector = this - (1 + coefBounciness) * (this.Dot(normalVec.Normalized()) * normalVec.Normalized());
        this = reflectedVector;
    }


    public static Vec2 GetUnitVectorDeg(float degrees) {

        float radians = Deg2Rad(degrees);
        return new Vec2((float)Math.Cos(radians), (float)Math.Sin(radians));
    }
    public static Vec2 GetUnitVectorRad(float radians)
    {
        return new Vec2((float)Math.Cos(radians), (float)Math.Sin(radians));
    }

    public static Vec2 RandomUnitVector() {
        return GetUnitVectorDeg(_random.Next(0, 359));
    }
    public void SetAngleDegrees(float degrees) {
        float length = this.Length();
        Vec2 unitVector = GetUnitVectorDeg(degrees);

        this = unitVector * length;
    }
    public void SetAngleRadians(float radians)
    {
        float length = this.Length();
        Vec2 unitVector = GetUnitVectorRad(radians);
        this = unitVector * length;
    }
    public float GetAngleRadians() {
        return (float)Math.Atan2(this.y, this.x);
    }
    public float GetAngleDegrees()
    {
        return Rad2Degree(GetAngleRadians());
    }

    public void RotateDegree(float degree) {
        float radians = Deg2Rad(degree);

        Vec2 vec2 = new Vec2();
        vec2.x = (float)(Math.Cos(radians) * x - Math.Sin(radians)*y);
        vec2.y = (float)(Math.Sin(radians) * x + Math.Cos(radians)*y);

        this.x = vec2.x;
        this.y = vec2.y;
    }
    public void RotateRadians(float radians)
    {
        Vec2 vec2 = new Vec2();
        vec2.x = (float)(Math.Cos(radians) * x - Math.Sin(radians) * y);
        vec2.y = (float)(Math.Sin(radians) * x + Math.Cos(radians) * y);

        this.x = vec2.x;
        this.y = vec2.y;
    }

    public void RotateAroundDegrees(Vec2 point, float degree) {
        Vec2 posRelToPoint = this - point;

        posRelToPoint.RotateDegree(degree);
        Vec2 newPos = posRelToPoint + point;
        this.SetXY(newPos.x, newPos.y);
    }
    public void RotateAroundRadians(Vec2 point, float radians)
    {
        Vec2 posRelToPoint = this - point;

        posRelToPoint.RotateRadians(radians);

        Vec2 newPos = posRelToPoint + point;
        this.SetXY(newPos.x, newPos.y);
    }


    public float Length() {
        return (float)Math.Sqrt(x*x + y*y);
    }
    public void Normalize() {
        if (Length() != 0)
        {
            float length = Length();
            x = x / length;
            y = y / length ;
        }
        else {
            //throw new Exception("Tried normalizing a vector with 0 length");
        }
    }
    public Vec2 Normalized() {
        //return new Vec2(1, 0);
        if (Length() != 0)
        {
            var x = this.x * 1 / Length();
            var y = this.y * 1 / Length();
            return new Vec2(x, y);
        }
        else
        {
            return new Vec2(1, 0);


        }
    }
    public void SetXY(float x, float y) {
        this.x = x;
        this.y = y;
    }
    public override string ToString () 
	{
		return String.Format ("({0},{1})", x, y);
	}
}

