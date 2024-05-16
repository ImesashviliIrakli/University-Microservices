using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Shared.Interfaces.AuthInterfaces;

public interface ITokenProvider
{
    void SetToken(string token);
    string GetToken();
    void ClearToken();
}
