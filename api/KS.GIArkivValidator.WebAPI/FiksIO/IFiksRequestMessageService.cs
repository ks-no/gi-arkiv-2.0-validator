using System;
using KS.GIArkivValidator.WebAPI.Models;

namespace KS.GIArkivValidator.WebAPI.FiksIO
{
    public interface IFiksRequestMessageService : IDisposable
    {
        Guid Send(FiksRequest fiksRequest, Guid receiverId);
    }
}
