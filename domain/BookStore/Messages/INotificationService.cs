namespace BookStore.Messages
{
    public interface INotificationService
    {
        void SendNotificationCode(string cellPhone, int code);
    }
}
