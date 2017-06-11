using AIWolf.Lib;

namespace Jinjo.Deck 
{
    interface Base
    {
        void EvaluateTalk(Talk talk);

        void EvaluateWhisper(Whisper whisper);
        Content SelectTalk();

        Content SelectWhisper();

        Agent SelectAttack();

        Agent SelectDivine();

        Agent SelectGuard();

        Agent SelectVote();
    }
}