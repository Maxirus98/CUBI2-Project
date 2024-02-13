using Unity.Netcode;
using UnityEngine;

/// <summary>
/// Script pour synchroniser PlayerNetworkData avec le serveur
/// </summary>
public class PlayerNetwork : NetworkBehaviour
{
    private NetworkVariable<PlayerNetworkData> netState = new(writePerm: NetworkVariableWritePermission.Owner);
    private Vector3 velocity;
    private float rotSpeed;
    private float interpolationTime = 0.1f;

    struct PlayerNetworkData : INetworkSerializable
    {
        private float x, y, z;
        private short yRot;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref x);
            serializer.SerializeValue(ref z);
            serializer.SerializeValue(ref yRot);
        }

        internal Vector3 Position
        {
            get => new Vector3(x, y, z);
            set
            {
                x = value.x;
                y = value.y;
                z = value.z;
            }
        }

        internal Vector3 Rotation
        {
            get => new Vector3(0, y, 0);
            set => yRot = (short) value.y;
        }
    }

    private void Update()
    {
        if (IsOwner)
        {
            WriteTransformToNetwork();
        }
        else
        {
            ReadTransformFromNetwork();
        }
    }

    private void WriteTransformToNetwork()
    {
        netState.Value = new PlayerNetworkData()
        {
            Position = transform.position,
            Rotation = transform.rotation.eulerAngles
        };
    }

    private void ReadTransformFromNetwork()
    {
        transform.position = Vector3.SmoothDamp(transform.position, netState.Value.Position, ref velocity, interpolationTime);
        var eulerY = Mathf.SmoothDampAngle(
            transform.rotation.eulerAngles.y,
            netState.Value.Rotation.y,
            ref rotSpeed,
            interpolationTime);
        transform.rotation = Quaternion.Euler(0, eulerY, 0);
    }
}
