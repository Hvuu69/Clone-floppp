using UnityEngine;

public class PipeControllerScript : MonoBehaviour
{
	public GameObject pipePrefab;   // Prefab ống nước
	public float spawnRate;    // Thời gian spawn (giây)
	public float heightOffset; // Độ lệch cao thấp

	public float safeMargin; // Khoảng cách an toàn trên dưới

	private float timer = 0f;

	void Update()
	{
		if (timer < spawnRate) //Thời gian
		{
			timer += Time.deltaTime;
		}
		else
		{
			SpawnPipe();
			timer = 0f;
		}
	}

	void SpawnPipe() // Spawn ống nước
	{
		Camera cam = Camera.main;

		float screenBottom = cam.transform.position.y - cam.orthographicSize;
		float screenTop = cam.transform.position.y + cam.orthographicSize;


		float minY = screenBottom + safeMargin;
		float maxY = screenTop - safeMargin;

		float spawnY = Random.Range(minY, maxY);

		float rightEdge = cam.transform.position.x
						  + cam.orthographicSize * cam.aspect;

		float spawnX = rightEdge + 0.3f;

		Instantiate(pipePrefab,
			new Vector3(spawnX, spawnY, 0),
			Quaternion.identity);
	}
}