using UnityEngine;

public class MovePipeScript : MonoBehaviour
{
	public float moveSpeed = 1f;

	void Update()
	{
		transform.position += Vector3.left * moveSpeed * Time.deltaTime;

		if (transform.position.x < -10f)
		{
			Destroy(gameObject);
		}
	}
}
