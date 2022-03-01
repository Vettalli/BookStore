using System.Diagnostics;

namespace BookStore.Messages
{
    public class DebugNotificationService : INotificationService
    {
        public void SendNotificationCode(string cellPhone, int code)
        {
            Debug.WriteLine($"Cell phone : {cellPhone}, code : {code}");
        }
    }
}
