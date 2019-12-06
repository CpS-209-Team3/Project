using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Sensor : GameObject
    {
        Action<GameObject> onSense;
        GameObject host;

        public override void Loop() { position = host.Position; destroy = host.Destroy; }

        public override void OnCollision(GameObject gameObject)
        {
            if (gameObject != host) onSense(gameObject);
        }

        public Sensor(GameObject host, Action<GameObject> callback, double radius)
            : base(host.Position)
        {
            size = new Vector(radius * 2, radius * 2);
            this.host = host;
            onSense = callback;

            canSerialize = false;

            imageSources = new List<string> { };

            World.Instance.AddObject(this);
        }
    }
}
