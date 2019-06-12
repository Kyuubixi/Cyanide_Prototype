using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    public float movementSpeed = 0.5f;
    public float normalMovementSpeed = 0.5f;
    [SerializeField]
    private float rotateSpeed = 2f;

    public float rotationX;

    public bool isFlyingFast = false;

    private TrailRenderer trailRenderer;

    public float trailRendererLineReduction;

    Gradient trailGradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    Gradient trailGradientNormal;
    GradientColorKey[] colorKeyNormal;
    GradientAlphaKey[] alphaKeyNormal;

    Quaternion zero = new Quaternion(0, 0, 0, 1);


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();


        trailGradient = new Gradient();
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.red;
        colorKey[0].time = 0f;
        colorKey[1].color = Color.black;
        colorKey[1].time = 1f;
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        trailGradientNormal = new Gradient();
        colorKeyNormal = new GradientColorKey[2];
        colorKeyNormal[0].color = Color.cyan;
        colorKeyNormal[0].time = 0f;
        colorKeyNormal[1].color = Color.black;
        colorKeyNormal[1].time = 1f;
        alphaKeyNormal = new GradientAlphaKey[2];
        alphaKeyNormal[0].alpha = 1.0f;
        alphaKeyNormal[0].time = 0.0f;
        alphaKeyNormal[1].alpha = 0.0f;
        alphaKeyNormal[1].time = 1.0f;

        trailGradient.SetKeys(colorKey, alphaKey);
        trailGradientNormal.SetKeys(colorKeyNormal, alphaKeyNormal);
    }
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        transform.Rotate(new Vector3(rotationX, 0f, 0f), Space.Self);
        if(rotationX > 360f || rotationX < -360f)
        {
            rotationX = 0f;
        }

        if(trailRenderer.time < 0.7f)
        {
            trailRenderer.time = 0.7f;
        }
        if(trailRenderer.time > 3f)
        {
            trailRenderer.time = 3f;
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * movementSpeed);
    
        if(Input.GetAxisRaw("Horizontal") > 0.1f) // Turn right
        {
            rotationX -= rotateSpeed;
        }
        else if(Input.GetAxisRaw("Horizontal") < -0.1f) // Turn left
        {
            rotationX += rotateSpeed;
        }

        if((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0)) && trailRenderer.time > 0.7f) // Move faster
        {
            movementSpeed = normalMovementSpeed * 2;
            trailRenderer.colorGradient = trailGradient;
            isFlyingFast = true;

            trailRenderer.time -= trailRendererLineReduction * Time.deltaTime;
        }
        else
        {
            movementSpeed = normalMovementSpeed;
            trailRenderer.colorGradient = trailGradientNormal;
            isFlyingFast = false;
        }
    }
}
