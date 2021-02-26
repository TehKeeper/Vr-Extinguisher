using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Utility;
using HTC.UnityPlugin.Vive;
using System;

public class ExtinguisherScript : MonoBehaviour

    , IColliderEventDragStartHandler

{
    // Start is called before the first frame update
    public Transform safePin;
    public ParticleSystem gasStream;
    public GameObject streamColliders;
    public Transform orifice;
    public LineRenderer hoseLine;

    [Range(0,1)]
    public float pinDetachDistance = 0.1f;

    private bool pinReleased;
    private bool grabbed;
    private bool trackPin;
    private bool extTrig;

    private Vector3 pinInitPos;
    private HandRole actHandRole = HandRole.Invalid;

    BoxCollider gripColl;

    
    Vector3[] hosePoints=new Vector3[2];

    //private Collider handleCollider;


    void Start()
    {
        pinInitPos = safePin.localPosition;
        gripColl = GetComponent<BoxCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        //Контроль процесса тушения

        if (grabbed && pinReleased)     //Включает огнетушитель только если его держат, снята пломба и задан контроллер
        {
            if (ViveInput.GetPressDown(actHandRole, ControllerButton.Trigger))
            {
                _extinguish(true);
            }

            if (ViveInput.GetPressUp(actHandRole, ControllerButton.Trigger))
            {
                _extinguish(false);
            }
        }
        

        if (!pinReleased&&trackPin)
            pinTracking();

        
    }

    private void LateUpdate()
    {
        hoseControl();
    }

    /// <summary>
    /// Контроль отображения шланга
    /// </summary>
    private void hoseControl()
    {
        hosePoints[1] = orifice.localPosition-hoseLine.transform.localPosition;
        hoseLine.SetPositions(hosePoints);
    }

    /// <summary>
    /// Отслеживание положения пломбы
    /// </summary>
    private void pinTracking()
    {
        if((safePin.localPosition-pinInitPos).sqrMagnitude >= pinDetachDistance)
        {
            safePin.parent = null;
            safePin.GetComponent<Rigidbody>().useGravity = true;        //Освобождение пломбы
            Destroy(safePin.GetComponent<ConfigurableJoint>());
            pinReleased = true;
        }
    }

    /// <summary>
    /// Активация/деактивация тушения
    /// </summary>
    /// <param name="value">Значение параметра</param>
    private void _extinguish(bool value)
    {

        if (extTrig == value)                       //Прерывание выполнения функции, если объекты в нужном состоянии
            return;


        extTrig = value;
        streamColliders.SetActive(value);           //Включает триггеры тушения

        if (value)                                  //Включает/отключает эффекты разбрызгивания
            gasStream.Play();
        else
            gasStream.Stop();

    }

    /// <summary>
    /// Инициирует слежение за пломбой. Используется через инспектор
    /// </summary>
    /// <param name="value">Значение параметра</param>
    public void _trackTrigger(bool value)
    {
        trackPin = value;
    }

    /// <summary>
    /// Задает значение, удерживается ли огнетушитель и включает/отключает коллайдеры пломбы
    /// </summary>
    /// <param name="value">Значение параметра</param>
    public void _grabbed(bool value)
    {


        grabbed = value;
        gripColl.enabled = !grabbed;

        if (!pinReleased)
        {
            foreach (Collider item in safePin.GetComponents<Collider>())
            {
                item.enabled = value;
            }
        }
    }


    public void OnColliderEventDragStart(ColliderButtonEventData eventData)
    {
        if (grabbed)
        {
            if (actHandRole == HandRole.Invalid)
                actHandRole = eventData.eventCaster.gameObject.GetComponent<ViveColliderEventCaster>().viveRole.ToRole<HandRole>();
        }
        else
            actHandRole = HandRole.Invalid;


        print("eventData.eventCaster.gameObject: " + actHandRole);

    }



    
}
