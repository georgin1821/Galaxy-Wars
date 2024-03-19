using UnityEngine;
using System.Collections;

public class BackgroundCtrl : MonoBehaviour {

	public static float UNIFORM_TIME_FACTOR = 1f;
	public float SPEED = 0.02f;


	public static BackgroundCtrl getInstance() {
		return s_singleton;
	}

	void Awake() {
		s_singleton = this;
	}

	// Use this for initialization
	void Start () {
		m_uvOffset.x = 0f;
		m_uvOffset.y = 0f;
	}

	void Update () {
		float offset = SPEED * Time.deltaTime * UNIFORM_TIME_FACTOR;
		m_uvOffset.y += offset;
		gameObject.GetComponent<Renderer>().material.SetTextureOffset( "_MainTex", m_uvOffset );
		if( m_uvOffset.y > 1f ) {
			m_uvOffset.y -= 1f;
		}
	}

	private static BackgroundCtrl s_singleton;

	private Vector2 m_uvOffset = new Vector2();
}