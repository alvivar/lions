using UnityEngine;

// #jam
public class MultiplayerBulletBySubscription : MonoBehaviour
{
    public MultiplayerServer server;

    private void Start()
    {
        server = GetComponent<MultiplayerServer>();

        this.tt("WaitToSubscribe")
            .Wait(() => server.connected)
            .Add(() =>
            {
                server.bite.Send("#b b");
            });
    }

    private void OnEnable()
    {
        MultiplayerBulletBySubscriptionSystem.components.Add(this);
    }

    private void OnDisable()
    {
        MultiplayerBulletBySubscriptionSystem.components.Remove(this);
    }
}