using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class AimTurret : MonoBehaviour
{

    public Rigidbody2D Cursorb2D;

    [SerializeField] private float TurretrotationSpeed = 100;

    [SerializeField] private Transform Cursor;
    [SerializeField] private float VelocidadCursorMax;

    [SerializeField] private Vector2 screenBounds;
    private float objectWith;
    private float objectHeight;
    [SerializeField] private SpriteRenderer CursorSprite;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWith = CursorSprite.bounds.size.x / 2;
        objectHeight = CursorSprite.bounds.size.y / 2;

    }
    public void OnRStickMove(InputAction.CallbackContext context)
    {
        var vector2 = context.ReadValue<Vector2>();
        var vector3 = new Vector3(vector2.x, vector2.y, 0);
        Cursorb2D.velocity = vector3 * VelocidadCursorMax * Time.fixedDeltaTime;
        //Cursor no puede salirse
        /*Vector3 viewPos = Cursor.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + objectWith, screenBounds.x * -1 - objectWith);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + objectHeight, screenBounds.y * -1 - objectHeight);
        Cursorb2D.position = new Vector3(Mathf.Clamp(viewPos.x, screenBounds.x + objectWith, screenBounds.x * -1 - objectWith), Mathf.Clamp(viewPos.y, screenBounds.y + objectHeight, screenBounds.y * -1 - objectHeight),0);
    */
        }

    public void Turret()
    {
        var turretDirection = (Vector3)Cursor.position - transform.position;

        var desiredAngle = Mathf.Atan2(turretDirection.y, turretDirection.x) * Mathf.Rad2Deg;

        var rotationstep = TurretrotationSpeed * Time.deltaTime;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, desiredAngle - 90), rotationstep);
        //Debug.Log(desiredAngle + rotationstep);
    }
    public void Update()
    {
        Turret();
    }
}
