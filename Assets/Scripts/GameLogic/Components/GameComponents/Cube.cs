using EDBG.GameLogic.Rules;

namespace EDBG.GameLogic.Components
{
	public class Cube
	{
		private PlayerColors _color;

		public PlayerColors Color
		{
			get { return _color; }
			set { _color = value; }
		}

		public Cube()
		{

		}
	}
}