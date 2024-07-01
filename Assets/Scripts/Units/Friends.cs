using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(LineRenderer))]
public class Friends : UnitComponent, IAttack, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler

{
   

    private LineRenderer _lineRenderer;
    [SerializeField] private AttackTrigger _trigger;
    private int _segments = 46;
  

    

    public void Attack()
    {
        float damage = GetDamageClass.DamageTarget(_unitData.DamageRatio, _unitData.SpeedAttackRatio,_unitData.LuckRatio);

        if (damage >= 0)
        {
            StartAnimation.ToRun(StateUnit.ATTACK);
            GetTargetForAttack.TakeDamage(damage);

        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
     
        if (clickedObject != null)
        {
            UnitComponent hit = clickedObject.GetComponentInParent<UnitComponent>();

            if (hit != null)
            {
               _lineRenderer.enabled = true;
                _gameHub.GetWindowInfoUnit.WindowInfo(this);
            }
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

        if ( clickedObject != null )
        {
            UnitComponent hit = clickedObject.GetComponentInParent<UnitComponent>();

            if ( hit != null )
            {
                _lineRenderer.enabled = true;
                //todo - > установить курсор
            }
        }

    }

    public void OnPointerExit( PointerEventData eventData )
    {
        //Todo => установить стандартный курсор
        _lineRenderer.enabled = false;
    }

    protected override void Start()
    {
        base.Start();

      
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _segments + 1;
        _lineRenderer.useWorldSpace = false;
        CreateCircle();
        _trigger.Initialized(this);
        SetState(IdleState);
        
    }

    private void CreateCircle()
    {
        float angle = 0f;
        for (int i = 0; i < _segments + 1; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * _config.GetDistance;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * _config.GetDistance;

            _lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            angle += 360f / _segments;

        }
        _lineRenderer.enabled = false;
    }

     
}
