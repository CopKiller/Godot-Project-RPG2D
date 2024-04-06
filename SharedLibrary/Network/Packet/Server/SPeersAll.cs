

using SharedLibrary.Models;

namespace SharedLibrary.Network.Packet.Server
{
    public class SPeersAll
    {
        public List<PlayerDataModel> PlayerDataModels { get; set; }

        public List<PlayerDataModel> GetAllPeers()
        {
            //var gameServerNetwork = NodeManager.GetNode<ServerNetworkService>("ServerNetworkService").Players.GetItems();

            //var newPeers = new List<PlayerDataModel>();

            //foreach (PlayerDataModel player in gameServerNetwork.Values)
            //{
            //    newPeers.Add(player);
            //}
            //return newPeers;

            return null;
        }
    }
}
