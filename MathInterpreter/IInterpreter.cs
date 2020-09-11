using System.Threading.Tasks;

namespace MathInterpreter
{

    public interface IMathInterpreter
    {
        double Parse(string expression);
    }
}
