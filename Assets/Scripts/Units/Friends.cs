using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent( typeof( LineRenderer ) )]
public class Friends : UnitComponent, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    
    private LineRenderer _lineRenderer;
    [SerializeField] private AttackTrigger _trigger;
    private int _segments = 46;
    [SerializeField] private Image _imagelevelSprite;
   
    
    public void SetSpriteLevel(Sprite img)
    {
        if (img == null)
        {
            Debug.LogError("Sprite =  null");
            return;
        }

        if (_imagelevelSprite == null)
        {
            Debug.LogError("SpriteRenderer  =  null");
            return;
        }

        _imagelevelSprite.sprite = img;
   
   
    }
     


    public void OnPointerClick( PointerEventData eventData )
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

        if ( clickedObject != null )
        {
            UnitComponent hit = clickedObject.GetComponentInParent<UnitComponent>();

            if ( hit != null )
            {
                _lineRenderer.enabled = true;
                _gameHub.GetWindowInfoUnit.WindowInfo( this );
            }
        }

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
                //todo - > установить курсор
            }
        }

    }

    public void OnPointerExit( PointerEventData eventData )
    {
        //Todo => установить стандартный курсор
        _lineRenderer.enabled = false;
    }
    public override void DeactiveUnit()
    {
        base.DeactiveUnit();
        _gameHub.GetGameSettings.RemoveUnit( this );
    }
    protected override void Start()
    {
        base.Start();

        if (_imagelevelSprite == null)
        {
            Transform imageObject = transform.Find("Canvas/Image");
            _imagelevelSprite = imageObject.GetComponent<Image>();
        }


        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _segments + 1;
        _lineRenderer.useWorldSpace = false;
        CreateCircle();
        _trigger.Initialized( this );
        
        SetState( IdleState );
      
      
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


}
