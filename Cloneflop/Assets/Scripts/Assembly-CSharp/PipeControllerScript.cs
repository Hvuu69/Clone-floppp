using UnityEngine;
using System.Collections.Generic;

public class PipeControllerScript : MonoBehaviour
{
	[Header("Prefab Settings")]
	public GameObject pipeGroupPrefab;
	public float spawnRate = 2f;
	public float safeMargin = 1.5f;

	[Header("Gap Settings")]
	public float minGap = 2.5f;
	public float maxGap = 3.5f;

	[Header("Visual Assets")]
	public List<Sprite> pipeSprites; // Kéo các loại ảnh ống nước vào đây

	private float timer = 0f;
	private Camera mainCam;

	void Start()
	{
		mainCam = Camera.main;
		// Spawn ngay lập tức phát đầu tiên
		SpawnPipe();
	}

	void Update()
	{
		if (timer < spawnRate)
		{
			timer += Time.deltaTime;
		}
		else
		{
			SpawnPipe();
			timer = 0f;
		}
	}

	void SpawnPipe()
	{
		if (mainCam == null) return;

		// 1. Tính toán vị trí X (ngoài mép phải)
		float camX = mainCam.transform.position.x;
		float screenWidth = mainCam.orthographicSize * mainCam.aspect;
		float spawnX = camX + screenWidth + 1.5f;

		// 2. Tính toán vị trí Y ngẫu nhiên
		float camY = mainCam.transform.position.y;
		float screenHeight = mainCam.orthographicSize;
		float minY = camY - screenHeight + safeMargin;
		float maxY = camY + screenHeight - safeMargin;
		float spawnY = Random.Range(minY, maxY);

		// 3. Tạo Instance
		GameObject newPipeGroup = Instantiate(pipeGroupPrefab, new Vector3(spawnX, spawnY, 0), Quaternion.identity);

		// 4. Xử lý Logic hình ảnh và vật lý
		AdjustPipeLogic(newPipeGroup);
	}

	void AdjustPipeLogic(GameObject group)
	{
		Transform topPipe = group.transform.GetChild(0);
		Transform bottomPipe = group.transform.GetChild(1);
		Transform scoreZone = group.transform.GetChild(2);

		// --- BƯỚC 1: THAY SPRITE VÀ FIX COLLIDER ---
		if (pipeSprites != null && pipeSprites.Count > 0)
		{
			Sprite selectedSprite = pipeSprites[Random.Range(0, pipeSprites.Count)];

			ApplySpriteAndFixCollider(topPipe, selectedSprite);
			ApplySpriteAndFixCollider(bottomPipe, selectedSprite);
		}

		// --- BƯỚC 2: ĐIỀU CHỈNH KHE HỞ (GAP) ---
		float randomGap = Random.Range(minGap, maxGap);

		// Đẩy 2 ống ra xa nhau dựa trên gap
		topPipe.localPosition = new Vector3(0, randomGap / 2f, 0);
		bottomPipe.localPosition = new Vector3(0, -randomGap / 2f, 0);

		// --- BƯỚC 3: CĂN CHỈNH SCORE ZONE ---
		scoreZone.localPosition = Vector3.zero;
		BoxCollider2D zoneCollider = scoreZone.GetComponent<BoxCollider2D>();

		if (zoneCollider != null)
		{
			// Reset scale của zone về 1 để collider size hoạt động chính xác
			scoreZone.localScale = Vector3.one;
			// Chiều rộng giữ nguyên, chiều cao bằng đúng khoảng cách Gap
			zoneCollider.size = new Vector2(zoneCollider.size.x, randomGap);
		}
	}

	// Hàm bổ trợ: Gán Sprite và ép Collider phải to bằng đúng Sprite đó
	void ApplySpriteAndFixCollider(Transform pipeTransform, Sprite newSprite)
	{
		SpriteRenderer sr = pipeTransform.GetComponent<SpriteRenderer>();
		BoxCollider2D col = pipeTransform.GetComponent<BoxCollider2D>();

		if (sr != null)
		{
			sr.sprite = newSprite;
		}

		if (col != null && sr != null && sr.sprite != null)
		{
			// Đảm bảo Scale của Pipe là 1 để tránh sai lệch kích thước vật lý
			pipeTransform.localScale = Vector3.one;

			// Cập nhật size của BoxCollider2D theo size thực tế của Sprite (tính bằng Unit)
			col.size = sr.sprite.bounds.size;
		}
	}
}