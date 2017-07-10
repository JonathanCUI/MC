using UnityEngine;
using System.Collections;

public class WalkingOrc : MonoBehaviour {

	private Animator animator;

	public float walkspeed = 5;
	private float horizontal;
	private float vertical;
	private float rotationDegreePerSecond = 1000;
	private bool isAttacking = false;

	public GameObject gamecam;
	public bool isRanged = false;
	public GameObject projectile;
	public Transform shootPoint;
	

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		if (animator)
		{
			//walk
			horizontal = Input.GetAxis("Horizontal");
			vertical = Input.GetAxis("Vertical");

			Vector3 stickDirection = new Vector3(horizontal, 0, vertical);
			float speedOut;

			if (stickDirection.sqrMagnitude > 1) stickDirection.Normalize();

			if (!isAttacking)
				speedOut = stickDirection.sqrMagnitude;
			else
				speedOut = 0;

			if (stickDirection != Vector3.zero)
				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(stickDirection, Vector3.up), rotationDegreePerSecond * Time.deltaTime);
			GetComponent<Rigidbody>().velocity = transform.forward * speedOut * walkspeed + new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);

			animator.SetFloat("Speed", speedOut);

			// move camera
			if (gamecam)
				gamecam.transform.position = transform.position + new Vector3(0,6f,-8f);

			// attack

			if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump") && !isAttacking)
			{
				isAttacking = true;
				StartCoroutine(stopAttack());
			}

			if (isRanged)
				animator.SetBool("isShooting", isAttacking);
			else
				animator.SetBool("isAttacking", isAttacking);

		}
	}

	public IEnumerator stopAttack()
	{
		yield return new WaitForSeconds(0.3f);

		if(isRanged)
		{
			var stone = Instantiate(projectile, shootPoint.transform.position, Random.rotation) as GameObject;
			stone.GetComponent<Rigidbody>().velocity = transform.rotation * Vector3.forward * 20 + new Vector3(0,3,0);
			stone.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 50;
		}

		yield return new WaitForSeconds(0.45f);
		isAttacking = false;
	}
}
