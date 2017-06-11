using System.Collections.Generic;
using AIWolf.Lib;

namespace Jinjo
{
    public class Card
    {
        public string Name {get; private set;}

        public Dictionary<System.Tuple<int,Role>,double> instance;

        public Card(string name, int agentNum, List<Role> ExistingRoleList)
        {
            Name = name;
            instance = new Dictionary<System.Tuple<int,Role>,double>();
            for(var numOfAgent = 0; numOfAgent < agentNum; numOfAgent++)
            {
                foreach(var role in ExistingRoleList)
                {
                    var agentIdRolePair = System.Tuple.Create(numOfAgent,role);
                    instance[agentIdRolePair] = 0.0;
                }
            }
        }

        static public System.Tuple<int,Role> CreateAgentIdRolePair(Agent agent, Role role)
        {
            var agentIdRolePair = System.Tuple.Create(agent.AgentIdx - 1,role);
            return agentIdRolePair;
        }

        static public System.Tuple<int,Role> CreateAgentIdRolePair(int agentIdx, Role role)
        {
            var agentIdRolePair = System.Tuple.Create(agentIdx,role);
            return agentIdRolePair;
        }
    }
}
