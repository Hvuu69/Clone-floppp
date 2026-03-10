using UnityEngine;

public class GroundControllerScript : MonoBehaviour
{
	public float moveSpeed = 3f;   // tốc độ di chuyển
	public float resetX = 20f;     // khoảng cách reset sang phải
	private Vector3 startPos;

	void Start()
	{
		startPos = transform.position;
	}

	void Update()
	{
		// di chuyển sang trái
		transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

		// nếu đi quá vị trí cho phép thì quay lại bên phải
		if (transform.position.x < startPos.x - resetX)
		{
			transform.position = startPos;
		}
	}
}