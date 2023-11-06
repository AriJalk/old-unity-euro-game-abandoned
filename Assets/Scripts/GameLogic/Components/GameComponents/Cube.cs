using EDBG.GameLogic.Rules;

namespace EDBG.GameLogic.Components
{
	public class Cube
	{
		private PieceColors _color;

		public PieceColors Color
		{
			get { return _color; }
			set { _color = value; }
		}

		public Cube()
		{

		}
	}
}