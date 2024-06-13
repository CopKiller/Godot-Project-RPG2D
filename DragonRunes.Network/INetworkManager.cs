using DragonRunes.Network.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonRunes.Network
{
    public interface INetworkManager
    {
        void Start();
        void Stop();
        void Update();

        void Register(IService service);

    }
}
