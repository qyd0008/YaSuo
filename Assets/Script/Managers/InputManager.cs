using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// [System.Serializable]
public class InputManager : Singleton<InputManager>
{

    public Texture2D normal,attack;
    RaycastHit hitInfo;
    public event Action<Vector3> OnMouseClicked;
    public event Action<GameObject> OnEnemyClicked;
    public event Action<SkillType,Vector3> OnKeyboardDown;

    bool isKeyQWER;//是否正在按下技能建

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    void Update()
    {
       SetCursorTexture();
       MouseControl();
       keyboardControl();
    }

    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray,out hitInfo))
        {
            //切换鼠标贴图
            switch (hitInfo.collider.gameObject.tag)
            {
                case "Ground":
                    Cursor.SetCursor( normal, new Vector2(16,16), CursorMode.Auto);    
                    break;
                case "Enemy":
                    Cursor.SetCursor( attack, new Vector2(16,16), CursorMode.Auto);    
                    break;
            }
        }
    }

    void MouseControl()
    {
        if (Input.GetMouseButtonDown(1) && hitInfo.collider != null)
        {
            if(hitInfo.collider.gameObject.CompareTag("Ground"))
                OnMouseClicked?.Invoke(hitInfo.point);
            if(hitInfo.collider.gameObject.CompareTag("Enemy"))
                OnEnemyClicked?.Invoke(hitInfo.collider.gameObject);
        }
    }

    void keyboardControl()
    {
        if (isKeyQWER)
        {
            return;
        }
        if (Input.GetKeyUp("q"))
        {
            OnKeyboardDown?.Invoke(SkillType.Q,hitInfo.point);
        }
        else if (Input.GetKeyUp("w"))
        {
            OnKeyboardDown?.Invoke(SkillType.W,hitInfo.point);
        }
        else if (Input.GetKeyUp("e"))
        {
            OnKeyboardDown?.Invoke(SkillType.E,hitInfo.point);
        }
        else if (Input.GetKeyUp("r") && GameManager.Instance.HasFlyingZombie())
        {
            OnKeyboardDown?.Invoke(SkillType.R,hitInfo.point);
        }
    }

    public void SetisKeyQWER(bool isKeyUp)
    {
        isKeyQWER = isKeyUp;
    }
}
