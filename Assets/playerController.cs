using UnityEngine;

public class playerController : MonoBehaviour
{
    public float playerSpeed;
    public float distGround;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        // Garantir que o Rigidbody seja configurado corretamente
        rb.constraints = RigidbodyConstraints.FreezeRotation; // Evita rota��o indesejada
    }

    void Update()
    {
        // Ajuste de posi��o em rela��o ao terreno
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1; // Origem do raycast ligeiramente acima do jogador
        if (Physics.Raycast(castPos, Vector3.down, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + distGround;
                transform.position = movePos;
            }
        }

        // Captura do input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Movimento do jogador
        Vector3 moveDir = new Vector3(x, 0, z).normalized; // Normaliza para manter velocidade constante em todas as dire��es
        
        // para mover o Rigidbody
        rb.linearVelocity = moveDir * playerSpeed;

        // Controle da orienta��o do SpriteRenderer
        if (x != 0)
        {
            sr.flipX = x < 0;
        }
    }
}
