using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationTableStateMachine : MonoBehaviour
{
    [SerializeField] Transform elementsHolder;
    [SerializeField] PotentialProductVisualisation potentialProductVisualisation;
    [SerializeField] PortalInstantiator portalInstantiator;
    [SerializeField] AppearanceTransmutationCircle appearanceTransmutationCircle;
    [SerializeField] PotentialProductAppearance potentialProductAppearance;

    public Transform ElementsHolder { get { return elementsHolder; } }


    public int GetPotentialProductID()
    {
        return potentialProductVisualisation.CurrentProductID;
    }

    public bool CheckPortalOpened()
    {
        return portalInstantiator.PortalIsActive;
    }

    public bool CheckCircleActive()
    {
        return appearanceTransmutationCircle.CircleShown;
    }

    public int GetInstantiatedProductID()
    {
        if (potentialProductAppearance.InstantiatedProductHolder.childCount == 0)
        {
            return 0;
        } else
        {
            return potentialProductAppearance.InstantiatedProductHolder.GetChild(0).GetComponent<TransmutationProduct>().ID;
        }
        
    }

    public float[] GetCreatedObjectPosition()
    {
        float[] position = new float[3];

        if (potentialProductAppearance.CreatedObject == null)
        {
            return position;
        } else
        {
            position[0] = potentialProductAppearance.CreatedObject.position.x;
            position[1] = potentialProductAppearance.CreatedObject.position.y;
            position[2] = potentialProductAppearance.CreatedObject.position.z;
            return position;
        }
    }

    public void ApplyPackState(TransmutationTableData transmutationTableData)
    {
        int indexer = 0;

        foreach (Transform element in elementsHolder)
        {
            if (transmutationTableData.packShown[indexer])
            {
                element.GetChild(1).GetComponent<TransmutationResourceChoose>().ShowResourcePack();
            }
            else
            {
                element.GetChild(1).GetComponent<TransmutationResourceChoose>().HideResourcePack();
            }
            Debug.Log("visibility of " + indexer + " pack was: Visible " + transmutationTableData.packShown[indexer]);

            //element.GetChild(1).GetComponent<TransmutationResourceChoose>().UploadRotation(transmutationTableData.packAngle[indexer]);
            //Debug.Log("angle of " + indexer + " pack was: " + transmutationTableData.packAngle[indexer]);
            indexer++;
        }
    }

    public void ApplyElementState(TransmutationTableData transmutationTableData)
    {
        int indexer = 0;

        foreach (Transform element in elementsHolder)
        {
            
            element.GetChild(1).GetComponent<TransmutationResourceChoose>().UploadChoosenResourceID(transmutationTableData.choosenResourcesID[indexer]);
            Debug.Log("choosen resource ID of " + indexer + " element was:  " + transmutationTableData.choosenResourcesID[indexer]);
            if (transmutationTableData.elementVisible[indexer])
            {
                element.GetChild(1).GetComponent<TransmutationResourceChoose>().VisualizeChosenResource(transmutationTableData.choosenResourcesID[indexer]);
            }
            else
            {
                element.GetChild(1).GetComponent<TransmutationResourceChoose>().UploadHidingElement();
            }
            Debug.Log("visibility of " + indexer + " element was: Visible " + transmutationTableData.elementVisible[indexer]);

            indexer++;
        }
    }

    public void ApplyChoosenProductID(TransmutationTableData transmutationTableData)
    {
        potentialProductVisualisation.VisualisePotentialProduct();
    }

    public void ApplyPortalState(TransmutationTableData transmutationTableData)
    {
        
        if (transmutationTableData.portalOpened)
        {
            portalInstantiator.ImmediatePortalClosing();
            portalInstantiator.ImmediatePortalOpening();
        }
        else
        {
            portalInstantiator.ImmediatePortalClosing();
        }

        Debug.Log("portal was opened " + transmutationTableData.portalOpened);
    }

    public void ApplyCircleState(TransmutationTableData transmutationTableData)
    {
        if (transmutationTableData.circleActive)
        {
            appearanceTransmutationCircle.ImmediateCircleDisappearance();
            appearanceTransmutationCircle.CircleAppearance();
        }
        else
        {
            appearanceTransmutationCircle.ImmediateCircleDisappearance();
        }

        Debug.Log("circle was active " + transmutationTableData.circleActive);
    }

    public void ApplyInstantiatedProductState(TransmutationTableData transmutationTableData)
    {
        potentialProductAppearance.ReleaseProduct();
        if (transmutationTableData.instantiatedProductID != 0)
        {
            Vector3 uploadedPosition = new Vector3(transmutationTableData.instantiatedProductPosition[0], transmutationTableData.instantiatedProductPosition[1], transmutationTableData.instantiatedProductPosition[2]);
            potentialProductAppearance.InstantiateProduct(transmutationTableData.instantiatedProductID, uploadedPosition);
        }
        Debug.Log("instantiated product ID is " + transmutationTableData.instantiatedProductID);
        Debug.Log("instantiated product position was " + transmutationTableData.instantiatedProductPosition[0] + ", " + transmutationTableData.instantiatedProductPosition[1] + ", " + transmutationTableData.instantiatedProductPosition[2]);
    }
}
