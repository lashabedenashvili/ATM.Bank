namespace ATM.Bank.Aplication.Service.LoggTimeServ
{
    public interface ILoggTimeService
    {
        Task LoggIn(string cardNumber);
        Task LoggOut(string cardNumber);
    }
}