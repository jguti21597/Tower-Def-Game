using System.Security.Cryptography;
using UnityEngine;

public class WizardPlacement : MonoBehaviour
{
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private LayerMask PlacementCollideMask;
    [SerializeField] private LayerMask PlacementCheckMask;

    private GameObject CurrentPlacingWizard;
    private Transform PlacementPoint;

    void Update()
    {
        if (CurrentPlacingWizard == null)
            return;

        Ray camRay = PlayerCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, 100f, PlacementCollideMask))
        {
            // Move wizard
            Vector3 offset = hit.point - PlacementPoint.position;
            CurrentPlacingWizard.transform.position += offset;

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Destroy(CurrentPlacingWizard);
                CurrentPlacingWizard = null;
                return;
            }

            // Click to place
            if (Input.GetMouseButtonDown(0))
            {
                // Check if this area is forbidden
                if (hit.collider.CompareTag("NotAllowed"))
                {
                    Debug.Log("Cannot place here - NotAllowed");
                    return;
                }

                BoxCollider wizardCollider =
                    CurrentPlacingWizard.GetComponent<BoxCollider>();

                if (wizardCollider == null)
                {
                    Debug.LogError("Wizard has no BoxCollider!");
                    return;
                }

                Vector3 boxCenter =
                    CurrentPlacingWizard.transform.position +
                    wizardCollider.center;

                Vector3 halfExtents =
                    wizardCollider.size / 2f;

                // IMPORTANT:
                // Disable the wizard collider so it doesn't detect itself
                // Disable wizard collider temporarily
                wizardCollider.enabled = false;

                Collider[] overlappingObjects = Physics.OverlapBox(
                boxCenter,
                halfExtents,
                CurrentPlacingWizard.transform.rotation,
                PlacementCheckMask,
                QueryTriggerInteraction.Ignore
                );

                bool isBlocked = false;

                foreach (Collider collider in overlappingObjects)
                {
                    Debug.Log(
                        "BLOCKING OBJECT: " +
                        collider.gameObject.name +
                        " | TAG: " +
                        collider.gameObject.tag +
                        " | LAYER: " +
                        LayerMask.LayerToName(collider.gameObject.layer)
                        );

                    isBlocked = true;
                }

                // Enable wizard collider again
                wizardCollider.enabled = true;

                if (!isBlocked)
                {
                    GameM.WizardsInGame.Add(CurrentPlacingWizard.GetComponent<WizardActions>());
                    Debug.Log("WIZARD PLACED!");
                    CurrentPlacingWizard = null;
                }
                else
                {
                    Debug.Log("Cannot place wizard - area is blocked!");
                }
            }
        }
    }

    public void SetWizardToPlace(GameObject wizard)
{
    CurrentPlacingWizard =
        Instantiate(wizard, Vector3.zero, Quaternion.identity);

    PlacementPoint =
        CurrentPlacingWizard.transform.Find("PlacementPoint");
}
}