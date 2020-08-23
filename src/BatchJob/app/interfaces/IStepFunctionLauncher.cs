using System.Threading.Tasks;

public interface IStepFunctionLauncher
{
    Task<StepFunctionLaunchResponse> Execute(StepFunctionLaunchRequest request);
}