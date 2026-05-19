using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    Transform _cam;
    Camera _cameraComponent;
    SpriteRenderer _sprite;

    void Start()
    {
        _cam = Camera.main.transform;
        _cameraComponent = Camera.main;
        _sprite = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        // Seguir posición
        transform.position = new Vector3(
            _cam.position.x,
            _cam.position.y,
            transform.position.z
        );

        // Ajustar tamaño cada frame
        float camHeight = _cameraComponent.orthographicSize * 2f;
        float camWidth = camHeight * _cameraComponent.aspect;

        float spriteHeight = _sprite.sprite.bounds.size.y;
        float spriteWidth = _sprite.sprite.bounds.size.x;

        transform.localScale = new Vector3(
            camWidth / spriteWidth,
            camHeight / spriteHeight,
            1f
        );
    }
}