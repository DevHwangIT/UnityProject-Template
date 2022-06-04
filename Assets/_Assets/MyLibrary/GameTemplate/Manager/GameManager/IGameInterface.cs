namespace MyLibrary.Manager
{
    public enum GamePlayState
    {
        Play,
        Pause,
        Resume,
        Quit,
        None
    }

    public enum GamePlayResult
    {
        Win,
        Lose,
        Draw,
        None
    }
    
    public interface IGamePlayState
    {
        void GamePlayCallBack();
        void GamePauseCallBack();
        void GameResumeCallBack();
        
        void GameQuitCallBack();
    }

    public interface IGamePlayResult
    {
        void InGameWinCallback();
        void InGameLoseCallback();
        void InGameDrawCallback();
    }
}