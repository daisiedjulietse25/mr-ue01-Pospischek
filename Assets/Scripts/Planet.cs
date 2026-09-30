using UnityEngine;

public class Planet : MonoBehaviour
{
    // data about the planet
    public float radius;
    public float mass;
    public float distanceToSun;
    public Transform sun;

    private float G = 100;
    private float sunMass = 333000f;
    private float gravitationalForce;

    private Rigidbody rb;

    void Start()
    {
        // set the size, this puts the radius that was entered in unity's inspector to the X, Y and Z scale of the object
        transform.localScale = Vector3.one * radius;

        // this sets the position of the planet that was entered in unity's inspector in the X-coordinate of the planet
        transform.position = new Vector3(distanceToSun, 0f, 0f);

        if (distanceToSun == 0)
            return; // because the sun has no rigid body

        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.mass = mass; // take input from inspector as mass for calc afterwards
        }

        // formula from angabe
        float velocity = Mathf.Sqrt(G * sunMass / distanceToSun);

        // velocity in z-direction
        rb.linearVelocity = new Vector3(0f, 0f, velocity);

        // formula from angabe
        gravitationalForce =
            G * mass * sunMass / (distanceToSun * distanceToSun);
    }

    // use because fixed intervals for physics calculations
    void FixedUpdate()
    {
        if (sun == null)
            return; // because sun should not move

        // current direction from the planet to the Sun
        Vector3 directionToSun =
            (sun.position - transform.position).normalized;

        // multiply with F that we calculated in start
        rb.AddForce(directionToSun * gravitationalForce);
    }
}
