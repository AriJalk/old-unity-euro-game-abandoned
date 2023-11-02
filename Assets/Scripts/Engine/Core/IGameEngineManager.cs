using EDBG.Engine.InputManagement;

namespace EDBG.Engine.Core
{
    public interface IGameEngineManager
    {
        InputHandler GetInputHandler();
    }
}