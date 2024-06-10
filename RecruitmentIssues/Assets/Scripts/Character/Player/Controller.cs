using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : CharacterBase
{
    // �ePrefab�擾
    [SerializeField] private GameObject bulletPrefab;
    // �����G�t�F�N�gPrefab�擾
    [SerializeField] private GameObject explosionPrefab;
    // �c�@�\���pUI�I�u�W�F�N�g�擾
    [SerializeField] private LifeStarSpawner lifeStarsSpawner;
    // �����[�h�e�L�X�g�擾
    [SerializeField] private GameObject reloadText;

    // �L�[���͂��󂯎���ĕۑ�����p
    private Vector2 input = Vector2.zero;

    private Collider colliderScript;
    private SpriteRenderer spriteRenderer;

    // �����t���O
    private bool isLow = false;
    // ���e�����t���O
    private bool isBombInstantiate = false;
    // �c��@��
    private int life = 3;
    // ���݂̒e��
    private int currentBullet = 5;
    // ���݂̔��e��
    private int currentBomb = 3;
    // �����[�h�^�C�}�[
    private float reloadTimer = 0.0f;

    // �e���ˈʒu�����p�萔
    const float BULLET_OFFSET_Y = 1.0f;
    // �ړ����x�����{���萔
    const float SLOWDOWN_FACTOR = 0.5f;
    // �����[�h����
    const float RELOAD_TIME = 1.5f;
    // �ő�e��
    const int MAX_BULLET = 10;
    // �ő唚�e��
    const int MAX_BOMB = 3;

    public Vector2 Input { get => input;}
    public int Life { get => life;}
    public bool IsBombInstantiate { get => isBombInstantiate; set => isBombInstantiate = value; }
    public int CurrentBomb { get => currentBomb;}

    override protected void Start()
    {
        // ���N���X��Start�Ăяo��
        base.Start();

        colliderScript = GetComponent<Collider>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        reloadTimer = RELOAD_TIME; 
        currentBullet = MAX_BULLET;
        currentBomb = MAX_BOMB;
    }

    override protected void Update()
    {
        // ���N���X��Update�Ăяo��
        base.Update();

        // ��_���[�W
        if (colliderScript.IsDamage)
        {
            life--;
            colliderScript.IsDamage = false;
            isBlink = true;
            // �c�@UI�X�V
            lifeStarsSpawner.UpdateLifeStarsUI(life);

        }

        // �����[�h
        if (currentBullet <= 0)
        {
            reload();
        }

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
        currentBullet--;

        // �ʒu����
        bulletPrefab.transform.position = new Vector3(transform.position.x + BULLET_OFFSET_Y, transform.position.y, transform.position.z);
        
        // ����
        Instantiate(bulletPrefab);
    }

    // �����֐�
    private void bomb()
    {
        currentBomb--;
        isBombInstantiate = true;
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
            // �����[�h�e�L�X�g��\��
            reloadText.SetActive(false);

            // ���g���\��
            this.gameObject.SetActive(false);

            // �����A�j���[�V�����I���t���O���󂯎�����玟�ɐi��
            explosionEffect();

        }
    }

    // �G�t�F�N�g����
    private void explosionEffect()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }

    // �_�ŏ���
    override protected void blinking()
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

    // �����[�h�֐�
    private void reload()
    {
        reloadText.SetActive(true);
        reloadTimer -= Time.deltaTime;
        if(reloadTimer <= 0)
        {
            // ���Z�b�g
            reloadText.SetActive(false);
            currentBullet = MAX_BULLET;
            reloadTimer = RELOAD_TIME;
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
        if (context.phase == InputActionPhase.Performed && currentBullet > 0 && life > 0)
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
        // ���N���b�N or pad�E�g���K�[ ����������  ���e���c���Ă���Ƃ�
        if (context.phase == InputActionPhase.Performed && currentBomb > 0 && life > 0)
        {
            bomb();
        }
    }
}