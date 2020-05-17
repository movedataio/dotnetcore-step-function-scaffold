using System.Threading.Tasks;

public interface ISearchAmiService
{
    Task<Amazon.EC2.Model.Image> Execute(ImageBuilderPackagerRequest request);
}