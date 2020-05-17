using System.Threading.Tasks;

public interface ISearchAmiService
{
    Task Execute(ImageBuilderPackagerRequest request);
}