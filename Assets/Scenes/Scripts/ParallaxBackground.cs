using UnityEngine;

/// <summary>
/// Efeito de Parallax — o fundo se move mais devagar que a câmera,
/// criando sensação de profundidade.
/// Coloque esse script em cada camada de background.
/// </summary>
public class ParallaxBackground : MonoBehaviour
{
    [Header("Configurações")]
    [Range(0f, 1f)]
    public float parallaxFactor = 0.5f; // 0 = fundo fixo, 1 = move igual à câmera

    public bool repeatHorizontal = true;  // Repete o fundo horizontalmente?
    public bool repeatVertical = false;   // Repete verticalmente?

    private Transform cam;
    private Vector3 lastCamPos;
    private float textureUnitSizeX;
    private float textureUnitSizeY;

    void Start()
    {
        cam = Camera.main.transform;
        lastCamPos = cam.position;

        // Calcula o tamanho do sprite para repetição
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        if (sprite != null)
        {
            Texture2D texture = sprite.texture;
            textureUnitSizeX = texture.width / sprite.pixelsPerUnit * transform.localScale.x;
            textureUnitSizeY = texture.height / sprite.pixelsPerUnit * transform.localScale.y;
        }
    }

    void LateUpdate()
    {
        // Calcula o quanto a câmera se moveu
        Vector3 deltaMovement = cam.position - lastCamPos;

        // Move o fundo proporcionalmente ao parallax
        transform.position += new Vector3(
            deltaMovement.x * parallaxFactor,
            deltaMovement.y * parallaxFactor,
            0
        );

        lastCamPos = cam.position;

        // Repete o fundo horizontalmente
        if (repeatHorizontal)
        {
            if (Mathf.Abs(cam.position.x - transform.position.x) >= textureUnitSizeX)
            {
                float offsetX = (cam.position.x - transform.position.x) % textureUnitSizeX;
                transform.position = new Vector3(
                    cam.position.x + offsetX,
                    transform.position.y,
                    transform.position.z
                );
            }
        }

        // Repete o fundo verticalmente
        if (repeatVertical)
        {
            if (Mathf.Abs(cam.position.y - transform.position.y) >= textureUnitSizeY)
            {
                float offsetY = (cam.position.y - transform.position.y) % textureUnitSizeY;
                transform.position = new Vector3(
                    transform.position.x,
                    cam.position.y + offsetY,
                    transform.position.z
                );
            }
        }
    }
}
