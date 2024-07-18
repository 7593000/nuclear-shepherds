using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent( typeof( LineRenderer ) )]
public class Resident : UnitComponent, IPointerEnterHandler, IPointerExitHandler
{


    private LineRenderer _lineRenderer;
    [SerializeField] private AttackTrigger _trigger;
    private int _segments = 46;

    private void OnEnable()
    {
        AddComponentsUnit();
    }

    private void Start()
    {
        


        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _segments + 1;
        _lineRenderer.useWorldSpace = false;
        CreateCircle();
        _trigger.Initialized( this );
        SetState( IdleState );
        SoundEngine.Instance.PlaySound( GetConfig.GetSoundIdle , SoundType.SFXPlayOne , false , this.transform );

    }

    private void CreateCircle()
    {
        float angle = 0f;
        for ( int i = 0; i < _segments + 1; i++ )
        {
            float x = Mathf.Sin( Mathf.Deg2Rad * angle ) * _config.GetWeaponsConfig.GetDistance;
            float y = Mathf.Cos( Mathf.Deg2Rad * angle ) * _config.GetWeaponsConfig.GetDistance;

            _lineRenderer.SetPosition( i , new Vector3( x , y , 0 ) );
            angle += 360f / _segments;

        }
        _lineRenderer.enabled = false;
    }

    public void OnPointerEnter( PointerEventData eventData )
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

        if ( clickedObject != null )
        {
            UnitComponent hit = clickedObject.GetComponentInParent<UnitComponent>();

            if ( hit != null )
            {
                _lineRenderer.enabled = true;

            }
        }

    }
    public void OnPointerExit( PointerEventData eventData )
    {

        _lineRenderer.enabled = false;
    }
    public override void DeactiveUnit()
    {
        base.DeactiveUnit();
        _gameHub.GetGameSettings.RemoveUnit( this );
    }

}
