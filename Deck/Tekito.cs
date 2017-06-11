using System;
using System.Collections.Generic;
using AIWolf.Lib;

namespace Jinjo.Deck 
{
    public class Tekito : Base
    {
        private Card card;
        
        private int numOfMyAgent;

        public Tekito(GameInfo gameInfo, GameSetting gameSetting)
        {
            this.numOfMyAgent = gameInfo.Agent.AgentIdx;
            card = new Card("Tekito", gameInfo.AgentList.Count, gameInfo.ExistingRoleList);
        }

        private Tuple<int, int> SelectTekito()
        {
            int numOfMinTarget = 0;
            int numOfMaxTarget = 0;
            double minValue = Double.MaxValue;
            double maxValue = Double.MinValue;
            foreach(var i in card.instance)
            {
                if(i.Key.Item2 == Role.VILLAGER)
                {
                    if (minValue > i.Value)
                    {
                        numOfMinTarget = i.Key.Item1;
                        minValue = i.Value;
                    }
                    if (maxValue < i.Value)
                    {
                        numOfMaxTarget = i.Key.Item1;
                        maxValue = i.Value;
                    }
                }
            }
            return Tuple.Create(numOfMinTarget, numOfMaxTarget);
        }

        public void EvaluateTalk(Talk talk)
        {
            var content = new Content(talk.Text);
            if (content.Topic != Topic.Skip && content.Topic != Topic.Over)
            {
                var agentIdRolePair = Card.CreateAgentIdRolePair(talk.Agent,Role.VILLAGER);
                card.instance[agentIdRolePair] += 1.0;
            }
        }

        public void EvaluateWhisper(Whisper whisper){}

        public Content SelectTalk()
        {
            var minAndMax = SelectTekito();
            var numOfVoteTarget = minAndMax.Item1;
            var numOfMaxTarget = minAndMax.Item2;
            if (numOfMaxTarget == numOfMyAgent) 
            {
                return new Content(new SkipContentBuilder());
            }
            return new Content(new VoteContentBuilder(Agent.GetAgent(numOfVoteTarget)));
        }

        public Content SelectWhisper()
        {
            var minAndMax = SelectTekito();
            var numOfAttackTarget = minAndMax.Item2;
            return new Content(new AttackContentBuilder(Agent.GetAgent(numOfAttackTarget)));
        }


        public Agent SelectAttack()
        {
            var minAndMax = SelectTekito();
            var numOfAttackTarget = minAndMax.Item2;
            return Agent.GetAgent(numOfAttackTarget + 1);
        }

        public Agent SelectDivine()
        {
            var minAndMax = SelectTekito();
            var numOfDevineTarget = minAndMax.Item1;
            return Agent.GetAgent(numOfDevineTarget + 1);
        }
        
        public Agent SelectGuard()
        {
            var minAndMax = SelectTekito();
            var numOfGuardTarget = minAndMax.Item2;
            return Agent.GetAgent(numOfGuardTarget + 1);
        }

        public Agent SelectVote()
        {
            var minAndMax = SelectTekito();
            var numOfVoteTarget = minAndMax.Item1;
            return Agent.GetAgent(numOfVoteTarget + 1);
        }
    }
}