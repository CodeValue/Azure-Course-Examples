using System.ServiceModel;

namespace Contract
{
    [ServiceContract(Namespace = "urn:ps")]
    public interface IProblemSolver
    {
        [OperationContract]
        int AddNumbers(int a, int b);
    }
}