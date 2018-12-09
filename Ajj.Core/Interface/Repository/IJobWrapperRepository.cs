namespace Ajj.Core.Interface.Repository
{
    public interface IJobWrapperRepository
    {
        IJobRepository Job { get; }
        IPostalCodeRepository PostalCode { get; }
        IBusinessStreamRepository BusinessStream { get; }
    }
}