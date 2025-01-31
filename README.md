<h1 align="center">Dject</h1>
<h4 align="center">The dll injection library for uwp apps (for Dotnet)</h3>

# Usage
1.Download dll from releases(Default dotnet version is 4.0).
<br>
2.Add Dject dll to your project
# Example code

``` Csharp
using Dject;

namespace example {
  public class main {
    static void Main(string[] args) {
      DJect.DllInject("Process name here(e.g:Minecraft.Windows)", "Path here(e.g:C:\Internal client.dll)")
    }
  }
}
```
