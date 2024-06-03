using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class Controller : MonoBehaviour
{
    // �ePrefab�擾
    [SerializeField] private GameObject bulletPrefab;
    // ���ePrefab�擾
    [SerializeField] private GameObject bombPrefab;
    // �����G�t�F�N�gPrefab�擾
    [SerializeField] private GameObject explosionPrefab;
    // �c�@�\���pUI�I�u�W�F�N�g�擾
    [SerializeField] private LifeStarSpawner lifeStarsSpawner;
    // �ړ��X�s�[�h
    [SerializeField] private int moveSpeed = 0;
    // �_�Ŏ���
    [SerializeField] private float blinkInterval = 0.0f;

    // �L�[���͂��󂯎���ĕۑ�����p
    private Vector2 input = Vector2.zero;

    private Collider colliderScript;
    private SpriteRenderer spriteRenderer;
    // �����t���O
    private bool isLow = false;
    // �_�Ńt���O
    private bool isBlink = false;
    // �c��@��
    private int life = 3;

    private float timer = 0.0f;
    private float blinkTimer = 0.0f;

    // �e���ˈʒu�����p�萔
    const float BULLET_OFFSET_Y = 1.0f;
    // �ړ����x�����{���萔
    const float SLOWDOWN_FACTOR = 0.5f;
    // �_�Ŏ���
    const float BLINK_TIME = 1.0f;

    public Vector2 Input { get => input;}
    public int Life { get => life;}

    void Start()
    {
        colliderScript = GetComponent<Collider>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        blinkTimer = BLINK_TIME;
    }

    void Update()
    {
        // ��_���[�W
        if(colliderScript.IsDamage)
        {
            life--;
            colliderScript.IsDamage = false;
            isBlink = true;
            // �c�@UI�X�V
            lifeStarsSpawner.UpdateLifeStarsUI(life);

        }

        // �_�ŏ����Ǘ�
        manageBlinking();

        // �c�@�m�F
        checkLife();
    }

    // �ړ��֐�
    private void move()
    {
        if(input.x != 0 || input.y != 0)
        {
            if (isLow)
            {
                // ������
                transform.Translate(input * moveSpeed * SLOWDOWN_FACTOR * Time.deltaTime);
            }
            else
            {
                // ���͂��ꂽ�����Ɉړ�
                transform.Translate(input * moveSpeed * Time.deltaTime);
            }
        }
    }

    // �U���֐�
    private void fire()
    {
        // �ʒu����
        bulletPrefab.transform.position = new Vector3(transform.position.x + BULLET_OFFSET_Y, transform.position.y, transform.position.z);
        
        // ����
        Instantiate(bulletPrefab);
    }

    // �����֐�
    private void bomb()
    {
        // �ʒu����
        bombPrefab.transform.position = new Vector3(transform.position.x + BULLET_OFFSET_Y, transform.position.y, transform.position.z);

        // ����
        Instantiate(bombPrefab);
    }

    // �c�@�m�F
    private void checkLife()
    {
        if (life > 0)
        {
            // �ړ��\���݈̂ړ�
            if (colliderScript.IsMoveAble)
            {
                move();
            }
        }
        else
        {
            // ���g���\��
            this.gameObject.SetActive(false);

            // �����A�j���[�V�����I���t���O���󂯎�����玟�ɐi��
            explosionEffect();

        }
    }

    // �G�t�F�N�g����
    private void explosionEffect()
    {
        explosionPrefab.transform.position = transform.position;

        Instantiate(explosionPrefab);
    }

    // �_�ŏ���
    private void blinking()
    {
        // �����������o�߂�����
        timer += Time.deltaTime;

        // timer�`blinkInterval�̊ԂŌJ��Ԃ��l���擾
        float repeatValue = Mathf.Repeat((float)timer, blinkInterval);

        // �C���^�[�o���̔����ȏ�̎��͕\��
        if (repeatValue >= blinkInterval * 0.5f)
        {
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }

    // �_�ł��w�肵���b�ԑ����悤�ɂ���
    private void manageBlinking()
    {
        if (isBlink)
        {
            blinkTimer -= Time.deltaTime;
            if (blinkTimer > 0)
            {
                blinking();
            }
            else
            {
                // ���Z�b�g
                blinkTimer = BLINK_TIME;
                isBlink = false;
            }
        }
    }

    // �ȉ��L�[���͔���֐��@================================================

    // �ړ�
    public void OnMoveEvent(InputAction.CallbackContext context)
    {
        Vector2 rawInput = context.ReadValue<Vector2>();

        // Z����-90�x��]���Ă���̂ŁA�������ړ�����悤�ɒl��ϊ�
        input = new Vector2(-rawInput.y, rawInput.x);
    }

    // �U��
    public void OnFireEvent(InputAction.CallbackContext context)
    {        
        // ���N���b�N or pad�E�g���K�[ ����������
        if (context.phase == InputActionPhase.Performed)
        {
            fire();
        }
    }

    // �ᑬ
    public void OnLowEvent(InputAction.CallbackContext context)
    {
        // ���N���b�N or pad�E�g���K�[ ����������
        if (context.phase == InputActionPhase.Performed)
        {
            isLow = true;
        }
        // �L�[�𗣂�����
        else if (context.phase == InputActionPhase.Canceled)
        {
            isLow = false;
        }
    }

    // ����
    public void OnBombEvent(InputAction.CallbackContext context)
    {
        // ���N���b�N or pad�E�g���K�[ ����������
        if (context.phase == InputActionPhase.Performed)
        {
            bomb();
        }
    }
}
