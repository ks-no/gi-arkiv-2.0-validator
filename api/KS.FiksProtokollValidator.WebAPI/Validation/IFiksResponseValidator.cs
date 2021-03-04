using KS.FiksProtokollValidator.WebAPI.Models;

namespace KS.FiksProtokollValidator.WebAPI.Validation
{
    public interface IFiksResponseValidator
    {
        void Validate(TestSession testSession);
    }
}
