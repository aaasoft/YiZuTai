using System.Windows.Input;
using Newtonsoft.Json;

namespace YiZuTai.Control;

public class ControlShotcut : IControlShotcut
{
    public string Name { get; }
    [JsonIgnore]
    public ICommand ExecuteCommand { get; private set; }

    public ControlShotcut(string name, Action executeAction)
    {
        Name = name;
        ExecuteCommand = new DelegateCommand()
        {
            ExecuteCommand = obj =>
            {
                executeAction.Invoke();
            }
        };
    }
}
