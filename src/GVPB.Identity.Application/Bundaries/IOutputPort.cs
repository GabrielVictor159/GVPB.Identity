using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVPB.Identity.Application.Bundaries;
public interface IOutputPort<T>
{
    void Standard(T output);
    void Error(string message);
    void NotFound(string message);
}
