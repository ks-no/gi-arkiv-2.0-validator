using System;
using KS.FiksProtokollValidator.WebAPI.Models;

namespace KS.FiksProtokollValidator.WebAPI.FiksIO
{
    public interface IFiksRequestMessageService : IDisposable
    {
        Guid Send(FiksRequest fiksRequest, Guid receiverId);
    }
}
