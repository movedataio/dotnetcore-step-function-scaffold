using System.Threading.Tasks;

public interface IWorkflowStep01
{
    Task<WorkflowStepResponse> Execute(dynamic request);
}