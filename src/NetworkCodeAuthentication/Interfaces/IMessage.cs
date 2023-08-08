namespace NetworkCodeAuthentication.Interfaces {
    public interface IMessage 
    {
        void LongAlert(string message);
        void ShortAlert(string message);
        void ExitApp();
    }
}
