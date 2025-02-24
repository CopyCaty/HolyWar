using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    private int playerId = -1;
    
    public int playerTeam;
    public ServantController servantController;

    override public void OnNetworkSpawn()
    {
        servantController = this.GetComponent<ServantController>();
        if (IsOwner)  // Make sure this code runs for the local player
        {
            
            AssignTeamServerRpc();  // Ask the server to assign a team
        }

    }
    [ServerRpc]
    private void AssignTeamServerRpc(ServerRpcParams rpcParams = default)
    {
        // Assign the team based on the number of connected clients (even = team 0, odd = team 1)
        int team = NetworkManager.Singleton.ConnectedClientsList.Count % 2;

        // Log the assigned team (for debugging)
        Debug.Log($"Assigned team {team} to player {rpcParams.Receive.SenderClientId}");

        // Set the team for the servant controller on the server side
        //servantController.Team = team;

        // Send the team assignment to the client via ClientRpc
        AssignTeamClientRpc(team);
    }

    // ClientRpc to inform the client of their team assignment
    [ClientRpc]
    private void AssignTeamClientRpc(int assignedTeam)
    {
        // Assign the team to the player on the client side
        playerTeam = assignedTeam;
        servantController.Team = playerTeam;  // Update the team on the servant controller
        Debug.Log($"Player assigned to team {playerTeam} on client side");
    }
    private void Update()
    {
        if (!IsLocalPlayer) return;
        MoveAndAttack();
    }


    private void MoveAndAttack()
    {
        if (Input.GetMouseButton(1))
        {
            PerformRaycastAndSendToServer();
        }
    }
    private void PerformRaycastAndSendToServer()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray from the camera to the mouse position

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            NetworkObject hitObjectNet = hit.collider.gameObject.GetComponent<NetworkObject>();
            if (hitObjectNet != null)
            {
                Debug.Log("TARGET FOUND");
                MoveAndAttackServerRpc(hit.point, (long)hitObjectNet.NetworkObjectId);
            }
            else
            {
                Debug.Log("NO TARGET");
                // If it's not a networked object, you can still send the position to move
                MoveAndAttackServerRpc(hit.point, -1); // Using -1 to indicate no specific target
            }
            // Send the hit position to the server

        }
    }

    [ServerRpc]
    private void MoveAndAttackServerRpc(Vector3 hitPoint, long targetNetworkObjectId)
    {
        if(targetNetworkObjectId == -1)
        {
            Debug.Log("Move to Client");
            servantController.MoveToDesClientRpc(hitPoint);
            return;
        }
        // Find the NetworkObject from the NetworkObjectId
        NetworkObject targetNetworkObject = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject((ulong)targetNetworkObjectId);

        if (targetNetworkObject != null)
        {
            GameObject targetObject = targetNetworkObject.gameObject;

            // Check if it's a unit and handle the logic
            if (targetObject.CompareTag("Unit"))
            {
                if (!targetObject.GetComponent<AbstractUnit>().IsDead &&
                    targetObject.GetComponent<AbstractUnit>().Team != playerTeam)
                {
                    Debug.Log("Attack to Client");
                    servantController.SetTargetClientRpc((ulong)targetNetworkObjectId);
                }
                else
                {
                    Debug.Log("Move to Client");
                    servantController.MoveToDesClientRpc(hitPoint);
                }
            }
        }
        else
        {
            // If no valid target, just move to the hit point
            servantController.MoveToDesClientRpc(hitPoint);
        }

        // Optionally, send the hit point to the client for visualization
        ShowHitPositionClientRpc(hitPoint);
    }


    [ClientRpc]
    private void ShowHitPositionClientRpc(Vector3 hitPosition)
    {
        // Instantiate a red sphere at the hit position on the client side
        GameObject hitSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        hitSphere.transform.position = hitPosition;
        hitSphere.GetComponent<Renderer>().material.color = Color.red;  // Make the sphere red
        Destroy(hitSphere, 2f);  // Destroy the sphere after 2 seconds (optional)
    }
}
