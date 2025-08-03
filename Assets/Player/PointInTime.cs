using UnityEngine;

public class PointInTime
{
    public Vector3 position;
    public Quaternion rotation;
    public float ammo;
    public float health;

    public PointInTime(Vector3 _position, Quaternion _rotation, float _ammo, float _health)
    { 
        position = _position; rotation = _rotation; ammo = _ammo; health = _health;
    }
}
