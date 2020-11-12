using KS.GIArkivValidator.WebAPI.Models;

namespace KS.GIArkivValidator.WebAPI.Validation
{
    public interface IFiksResponseValidator
    {
        void Validate(TestSession testSession);
    }
}
